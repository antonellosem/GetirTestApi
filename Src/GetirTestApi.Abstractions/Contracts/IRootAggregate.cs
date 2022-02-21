using System.Collections.Generic;

using MediatR;

namespace GetirTestApi.Abstractions
{
    /// <summary>
    /// Specifies the contract of a domain aggregate roots with events
    /// </summary>
    public interface IRootAggregate : IEntity
    {
        /// <summary>
        /// Gets the domain events collection.
        /// </summary>
        IReadOnlyCollection<INotification> DomainEvents { get; }

        /// <summary>
        /// Adds a specific event notification to current domain events collection.
        /// </summary>
        void AddDomainEvent(INotification notification);

        /// <summary>
        /// Clears all event notification from current domain events collection.
        /// </summary>
        /// <param name="notification">The event notification</param>
        void RemoveDomainEvent(INotification notification);

        /// <summary>
        /// Clears all event notifications from current domain events collection.
        /// </summary>
        void ClearDomainEvents();
    }
}
