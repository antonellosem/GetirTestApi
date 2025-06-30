using System;
using IntralotWebLib;

namespace PgadCommunication.Configuration
{
    public class X509ManagerException : ApplicationException
    {
        public X509ManagerException()
            : base()
        {
            TraceLog.TraceError("X509ManagerException");
        }

        public X509ManagerException(string message)
            : base(message)
        {
            TraceLog.TraceError("X509ManagerException " + message);
        }

        public X509ManagerException(string message, Exception ex)
            : base(message, ex)
        {
            TraceLog.TraceError("X509ManagerException " + message, ex);
        }
    }
}
