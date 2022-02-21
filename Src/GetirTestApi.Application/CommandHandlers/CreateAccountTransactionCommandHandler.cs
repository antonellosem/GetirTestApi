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
    public class CreateAccountTransactionCommandHandler : IRequestHandler<CreateAccountTransactionCommand, CreateAccountTransactionResponseDto>
    {
        private readonly ILogger<CreateAccountTransactionCommandHandler> _logger;
        private readonly IOperationsRepository _repository;

        public CreateAccountTransactionCommandHandler(ILogger<CreateAccountTransactionCommandHandler> logger, IOperationsRepository repository)
        {
            ArgValidator.NotNull(logger, nameof(logger));
            ArgValidator.NotNull(repository, nameof(repository));

            _logger = logger;
            _repository = repository;
        }

        public async Task<CreateAccountTransactionResponseDto> Handle(CreateAccountTransactionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            var customer = await _repository.Find(new CustomerByAccountIdSpecification(request.Body.AccountId.Value), QueryAreas.Transaction);

            var transaction = customer.AddTransaction(
                accountId: request.Body.AccountId.Value,
                user: request.Body.User.Trim(),
                isWithdrawal: request.Body.IsWithdrawal.Value,
                amount: request.Body.Amount.Value
            );

            _logger.LogInformation($"BEFORE repository commit.");
            
            await _repository.UnitOfWork.Commit();

            _logger.LogInformation($"END {nameof(Handle)} method.");

            return new CreateAccountTransactionResponseDto
            {
                Balance = customer.Accounts.First(x => x.Id.Equals(request.Body.AccountId)).Balance
            };
        }
    }
}
