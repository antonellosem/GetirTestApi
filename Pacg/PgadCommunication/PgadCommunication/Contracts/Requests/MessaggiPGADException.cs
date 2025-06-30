using System;
using System.Runtime.Serialization;
using IntralotWebLib;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    class MessaggioPGADException : ApplicationException
    {
        public MessaggioPGADException()
            : base()
        {
            TraceLog.TraceError($"MessaggioPGADException");
        }

        public MessaggioPGADException(string msg)
            : base(msg)
        {
            TraceLog.TraceError($"MessaggioPGADException {msg}");
        }

        public MessaggioPGADException(string msg, Exception ex)
            : base(msg, ex)
        {
            TraceLog.TraceError($"MessaggioPGADException {msg}", ex);
        }
    }
}
