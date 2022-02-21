using System;

namespace GetirTestApi.Abstractions
{
    [Flags]
    public enum QueryAreas
    {
        Root = 0,
        Account = 1,
        Transaction = 2
    }
}
