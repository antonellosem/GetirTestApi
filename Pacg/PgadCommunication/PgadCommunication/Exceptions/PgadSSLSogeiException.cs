using System;
using System.Runtime.Serialization;

namespace PgadCommunication.Exceptions
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadSSLSogeiException : PgadException
    {
        public PgadSSLSogeiException() : base() { }
        public PgadSSLSogeiException(string message, Exception innerException) : base(message, innerException) { }
    }
}
