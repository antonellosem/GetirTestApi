using System;
using System.Runtime.Serialization;
using IntralotWebLib;

namespace PgadCommunication.Exceptions
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadException : ApplicationException
    {
        public PgadException()
            : base()
        {
            TraceLog.TraceError("Pgad Exception: Generic");
        }

        public PgadException(string message, Exception innerException)
            : base(message, innerException)
        {
            TraceLog.TraceError("Pgad Exception: " + message, innerException);
        }
    }
}
