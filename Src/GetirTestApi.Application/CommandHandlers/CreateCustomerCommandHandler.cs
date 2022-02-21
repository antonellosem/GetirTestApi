using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using GetirTestApi.Application.DTOs.V1;
using GetirTestApi.Domain;
using GetirTestApi.Infrastructure;

using MediatR;

namespace GetirTestApi.Application.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponseDto>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly IOperationsRepository _repository;

        public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IOperationsRepository repository)
        {
            ArgValidator.NotNull(logger, nameof(logger));
            ArgValidator.NotNull(repository, nameof(repository));

            _logger = logger;
            _repository = repository;
        }

        public async Task<CreateCustomerResponseDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            var newCustomer = await _repository.Create(new Customer(
                createdBy: request.Body.User.Trim(),
                nationalId: request.Body.NationalId.Trim(),
                birthdate: request.Body.Birthdate.Value.Date,
                firstName: request.Body.FirstName.Trim(),
                lastName: request.Body.LastName.Trim()
            ));

            _logger.LogInformation($"BEFORE repository commit.");
         
            await _repository.UnitOfWork.Commit();
         
            _logger.LogInformation($"END {nameof(Handle)} method.");

            return new CreateCustomerResponseDto
            {
                CustomerId = newCustomer.Id
            };
        }
    }
}
