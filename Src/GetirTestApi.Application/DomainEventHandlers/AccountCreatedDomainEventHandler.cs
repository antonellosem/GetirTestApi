using GetirTestApi.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetirTestApi.Application.DomainEventHandlers
{
    public class AccountCreatedDomainEventHandler : INotificationHandler<AccountCreatedDomainEvent>
    {
        private readonly ILogger<AccountCreatedDomainEventHandler> _logger;

        public AccountCreatedDomainEventHandler(ILogger<AccountCreatedDomainEventHandler> logger)
        {
            ArgValidator.NotNull(logger, nameof(logger));

            _logger = logger;
        }

        public async Task Handle(AccountCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            _logger.LogInformation($"END {nameof(Handle)} method.");
        }
    }
}
