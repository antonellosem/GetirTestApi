using System;
using IntralotWebLib;

namespace PgadCommunication.Model
{
    public class PgadTransactionStoreException : ApplicationException
    {
        public PgadTransactionStoreException()
            : base()
        {
            TraceLog.TraceError("PgadTransactionStoreException");
        }

        public PgadTransactionStoreException(string message)
            : base(message)
        {
            TraceLog.TraceError("PgadTransactionStoreException " + message);
        }

        public PgadTransactionStoreException(string message, Exception ex)
            : base(message, ex)
        {
            TraceLog.TraceError("PgadTransactionStoreException " + message, ex);
        }
    }
}
