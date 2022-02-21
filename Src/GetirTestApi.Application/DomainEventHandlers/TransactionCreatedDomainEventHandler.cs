using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using GetirTestApi.Domain.Events;

using MediatR;

namespace GetirTestApi.Application.DomainEventHandlers
{
    public class TransactionCreatedDomainEventHandler : INotificationHandler<TransactionCreatedDomainEvent>
    {
        private readonly ILogger<TransactionCreatedDomainEventHandler> _logger;

        public TransactionCreatedDomainEventHandler(ILogger<TransactionCreatedDomainEventHandler> logger)
        {
            ArgValidator.NotNull(logger, nameof(logger));

            _logger = logger;
        }

        public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            if (notification.Transaction.IsWithdrawal)
                notification.Account.Withdraw(notification.Transaction.Amount);
            else
                notification.Account.AddMoney(notification.Transaction.Amount);

            _logger.LogInformation($"END {nameof(Handle)} method.");
        }
    }
}
