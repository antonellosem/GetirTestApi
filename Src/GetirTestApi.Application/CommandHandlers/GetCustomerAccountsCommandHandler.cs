using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using GetirTestApi.Abstractions;
using GetirTestApi.Application.DTOs.V1;
using GetirTestApi.Infrastructure;

using MediatR;

namespace GetirTestApi.Application.CommandHandlers
{
    public class GetCustomerAccountsCommandHandler : IRequestHandler<GetCustomerAccountsCommand, GetCustomerAccountsResponseDto>
    {
        private readonly ILogger<GetCustomerAccountsCommandHandler> _logger;
        private readonly IOperationsRepository _repository;

        public GetCustomerAccountsCommandHandler(ILogger<GetCustomerAccountsCommandHandler> logger, IOperationsRepository repository)
        {
            ArgValidator.NotNull(logger, nameof(logger));
            ArgValidator.NotNull(repository, nameof(repository));

            _logger = logger;
            _repository = repository;
        }

        public async Task<GetCustomerAccountsResponseDto> Handle(GetCustomerAccountsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            var customer = await _repository.Find(new CustomerByIdSpecification(request.Query.CustomerId.Value), QueryAreas.Account);
            var accounts = customer.Accounts;            

            _logger.LogInformation($"END {nameof(Handle)} method.");

            return new GetCustomerAccountsResponseDto
            {
                Accounts = accounts.Select(x => new CustomerAccountDto
                {
                    Balance = x.Balance,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    IBAN = x.IBAN
                }).ToArray()
            };
        }
    }
}
