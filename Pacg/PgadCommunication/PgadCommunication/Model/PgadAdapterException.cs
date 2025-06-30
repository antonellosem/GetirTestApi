using System;
using IntralotWebLib;

namespace PgadCommunication.Model
{
    public class PgadAdapterException : ApplicationException
    {
        public PgadAdapterException()
            : base()
        {
            TraceLog.TraceError("PgadAdapterException");
        }

        public PgadAdapterException(string message)
            : base(message)
        {
            TraceLog.TraceError("PgadAdapterException " + message);
        }

        public PgadAdapterException(string message, Exception ex)
            : base(message, ex)
        {
            TraceLog.TraceError("PgadAdapterException " + message, ex);
        }
    }


}
