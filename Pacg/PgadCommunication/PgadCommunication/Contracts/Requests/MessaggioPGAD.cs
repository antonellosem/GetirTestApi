using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Model;
using Gamenet.Infrastructure;
using PgadCommunication.Utility;
using PgadData;
using PgadData.Entity;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public abstract class MessaggioPGAD
    {
        protected ConcessionarioInfo header;

        public MessaggioPGAD(int titolareSistema, long idTransazione)
        {
            var pacgDao = new PacgProtocolloDao(
                new DaoConnectionStringsProvider(
                    Settings.Instance.AnagraficaContiGiocoConnectionString,
                    Settings.Instance.ToolsDBConnectionString));

            header = pacgDao.GetConcessionarioInfo(titolareSistema);
            this.idTransazione = idTransazione;
        }

        protected long GetIdTransazione()
        {

            return PgadTransactionStore.GetTransactionId();
        }

        protected long idTransazione;
    }

}
