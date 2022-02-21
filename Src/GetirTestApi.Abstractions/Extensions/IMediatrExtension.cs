using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GetirTestApi.Abstractions;

namespace MediatR
{
    public static class IMediatrExtension
    {
        /// <summary>
        /// Dispatches all the pending domain events.
        /// </summary>
        /// <param name="mediator">The mediator service</param>
        /// <param name="context">The current DB context</param>
        /// <returns></returns>
        public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<RootAggregate>()
                .Where(x => x.Entity.DomainEvents?.Any() ?? false)
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
