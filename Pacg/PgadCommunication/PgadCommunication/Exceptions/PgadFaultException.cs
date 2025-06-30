using System;
using System.Runtime.Serialization;

namespace PgadCommunication.Exceptions
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadFaultException : PgadException
    {
        public PgadFaultException() : base() { }
        public PgadFaultException(string message, Exception innerException) : base(message, innerException) { }
    }
}
