using System;
using System.Diagnostics.Contracts;

namespace GetirTestApi
{
    public class ArgValidator
    {
        public static void NotNull<T>(T value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);

            Contract.EndContractBlock();
        }

        public static void NotNull(Guid value, string name)
        {
            if (value == default)
                throw new ArgumentNullException(name);

            Contract.EndContractBlock();
        }
    }
}
