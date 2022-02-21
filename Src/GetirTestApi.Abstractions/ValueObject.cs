using System;
using System.Collections.Generic;
using System.Linq;

namespace GetirTestApi.Abstractions
{
    /// <summary>
    /// Represents the base read only value object
    /// </summary>
    public abstract class ValueObject : LoggableEntity
    {
        protected ValueObject(DateTime createdOn, string createdBy)
            : base(createdOn, createdBy) { }

        /// <summary>
        /// Gets or sets the indentifier of the value object
        /// </summary>
        public virtual long Id { get; protected set; }

        /// <summary>
        /// Gets all the atomic object values
        /// </summary>
        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj != null || obj.GetType() != GetType())
                return false;

            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValue = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValue = other.GetAtomicValues().GetEnumerator();

            while (thisValue.MoveNext() && otherValue.MoveNext())
            {
                if (ReferenceEquals(thisValue.Current, null) ^ ReferenceEquals(otherValue.Current, null))
                    return false;

                if (thisValue.Current != null && !thisValue.Current.Equals(otherValue.Current))
                    return false;
            }

            return !thisValue.MoveNext() && !otherValue.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
