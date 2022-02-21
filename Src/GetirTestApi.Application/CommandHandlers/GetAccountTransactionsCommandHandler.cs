using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using GetirTestApi.Application.DTOs.V1;
using GetirTestApi.Infrastructure;

using MediatR;

namespace GetirTestApi.Application.CommandHandlers
{
    public class GetAccountTransactionsCommandHandler : IRequestHandler<GetAccountTransactionsCommand, GetAccountTransactionsResponseDto>
    {
        private readonly ILogger<GetAccountTransactionsCommandHandler> _logger;
        private readonly IOperationsRepository _repository;

        public GetAccountTransactionsCommandHandler(ILogger<GetAccountTransactionsCommandHandler> logger, IOperationsRepository repository)
        {
            ArgValidator.NotNull(logger, nameof(logger));
            ArgValidator.NotNull(repository, nameof(repository));

            _logger = logger;
            _repository = repository;
        }

        public async Task<GetAccountTransactionsResponseDto> Handle(GetAccountTransactionsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            var transactions = await _repository.FindTransactions(
                customerSpecification: new CustomerByAccountIdSpecification(request.Query.AccountId.Value),
                transactionSpecification: new TransactionsByDateRangeSpecification(request.Query.From.Value, request.Query.To.Value)
            );

            _logger.LogInformation($"END {nameof(Handle)} method.");

            return new GetAccountTransactionsResponseDto
            {
                Transactions = transactions.Select(x => new AccountTransactionDto
                {
                    Amount = x.Amount,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    IsWithdrawal = x.IsWithdrawal
                }).ToArray()
            };
        }
    }
}
