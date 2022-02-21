using System;
using System.Collections.Generic;

using MediatR;

namespace GetirTestApi.Abstractions
{
    public class RootAggregate : DomainEntity<Guid>, IRootAggregate
    {
        private List<INotification> _domainEvents;

        public RootAggregate(DateTime createdOn, string createdBy) 
            : base(createdOn, createdBy) { }

        /// <summary>
        /// Gets the domain events collection.
        /// </summary>
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// Adds a specific event notification to current domain events collection.
        /// </summary>
        public void AddDomainEvent(INotification notification)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(notification);
        }

        /// <summary>
        /// Clears all event notifications from current domain events collection.
        /// </summary>
        public void RemoveDomainEvent(INotification notification)
        {
            _domainEvents?.Remove(notification);
        }

        /// <summary>
        /// Clears all event notification from current domain events collection.
        /// </summary>
        /// <param name="notification">The event notification</param>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
