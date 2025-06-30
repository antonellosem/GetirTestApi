using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioInterrogazioneLimiti: MessaggioPGAD
    {
        private readonly InterrogazioneLimitiRequestElements _messaggioInterrogazioneLimiti;
        public InterrogazioneLimitiRequestElements InterrogazioneLimiti
        {
            get
            {
                if (_messaggioInterrogazioneLimiti.idTransazione == 0)
                {
                    _messaggioInterrogazioneLimiti.idTransazione = this.GetIdTransazione();
                }

                return _messaggioInterrogazioneLimiti;
            }
        }
        public MessaggioInterrogazioneLimiti(int titolareSistema, long IdTransazione, string contoGioco)
            : base(titolareSistema, IdTransazione)
        {
            _messaggioInterrogazioneLimiti = new InterrogazioneLimitiRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco
            };

        }
    }
}
