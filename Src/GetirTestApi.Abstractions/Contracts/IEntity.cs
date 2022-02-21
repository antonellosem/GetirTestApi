using System;

namespace GetirTestApi.Abstractions
{
    public interface IEntity
    {
        DateTime CreatedOn { get; }

        string CreatedBy { get; }
    }
}
