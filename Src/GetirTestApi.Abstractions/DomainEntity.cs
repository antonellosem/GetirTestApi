using System;

namespace GetirTestApi.Abstractions
{
    public abstract class DomainEntity<TId> : LoggableEntity, IEntity
    {
        protected DomainEntity(DateTime createdOn, string createdBy)
            : base(createdOn, createdBy) { }

        public virtual TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (!(obj is DomainEntity<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            DomainEntity<TId> other = (DomainEntity<TId>)obj;

            if (other.IsDefault() || IsDefault())
                return false;
            else
                return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        protected bool IsDefault()
        {
            return Id.Equals(default);
        }
    }
}
