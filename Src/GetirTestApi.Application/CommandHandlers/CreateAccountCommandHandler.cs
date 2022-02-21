using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using GetirTestApi.Abstractions;
using GetirTestApi.Application.DTOs.V1;
using GetirTestApi.Infrastructure;

using MediatR;

namespace GetirTestApi.Application.CommandHandlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponseDto>
    {
        private readonly ILogger<CreateAccountCommandHandler> _logger;
        private readonly IOperationsRepository _repository;

        public CreateAccountCommandHandler(ILogger<CreateAccountCommandHandler> logger, IOperationsRepository repository)
        {
            ArgValidator.NotNull(logger, nameof(logger));
            ArgValidator.NotNull(repository, nameof(repository));

            _logger = logger;
            _repository = repository;
        }

        public async Task<CreateAccountResponseDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            var customer = await _repository.Find(new CustomerByIdSpecification(request.Body.CustomerId.Value), QueryAreas.Account);

            var account = customer.AddAccount(
                user: request.Body.User.Trim(),
                iban: request.Body.IBAN.Trim()
            );

            _logger.LogInformation($"BEFORE repository commit.");
         
            await _repository.UnitOfWork.Commit();

            _logger.LogInformation($"END {nameof(Handle)} method.");

            return new CreateAccountResponseDto
            {
                AccountId = account.Id
            };
        }
    }
}
