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
    public class CustomerCreatedDomainEventHandler : INotificationHandler<CustomerCreatedDomainEvent>
    {
        private readonly ILogger<CustomerCreatedDomainEventHandler> _logger;

        public CustomerCreatedDomainEventHandler(ILogger<CustomerCreatedDomainEventHandler> logger)
        {
            ArgValidator.NotNull(logger, nameof(logger));

            _logger = logger;
        }

        public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"INI {nameof(Handle)} method.");

            _logger.LogInformation($"END {nameof(Handle)} method.");
        }
    }
}
