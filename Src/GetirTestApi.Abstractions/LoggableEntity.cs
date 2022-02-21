using System;

namespace GetirTestApi.Abstractions
{
    public abstract class LoggableEntity
    {
        public LoggableEntity(DateTime createdOn, string createdBy)
        {
            ArgValidator.NotNull(createdOn, nameof(createdOn));
            ArgValidator.NotNull(createdBy, nameof(createdBy));

            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
    }
}
