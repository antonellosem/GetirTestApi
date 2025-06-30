using System;
using System.Runtime.Serialization;

namespace PgadCommunication.Exceptions
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadProxyNotCreated : PgadException
    {
        public PgadProxyNotCreated() : base() { }
        public PgadProxyNotCreated(string message, Exception innerException) : base(message, innerException) { }
    }
}
