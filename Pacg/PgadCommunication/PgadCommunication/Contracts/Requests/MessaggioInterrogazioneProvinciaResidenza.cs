using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioInterrogazioneProvinciaResidenza : MessaggioPGAD
    {
        private readonly InterrogazioneProvinciaResidenzaContoRequestElements _messaggioInterrogazioneProvinciaResidenza;

        public InterrogazioneProvinciaResidenzaContoRequestElements InterrogazioneProvinciaResidenza
        {
            get
            {
                if (_messaggioInterrogazioneProvinciaResidenza.idTransazione == 0)
                {
                    _messaggioInterrogazioneProvinciaResidenza.idTransazione = this.GetIdTransazione();
                }

                return _messaggioInterrogazioneProvinciaResidenza;
            }
        }

        public MessaggioInterrogazioneProvinciaResidenza(int titolareSistema, long IdTransazione, string contoGioco)
            : base(titolareSistema, IdTransazione)
        {
            _messaggioInterrogazioneProvinciaResidenza = new InterrogazioneProvinciaResidenzaContoRequestElements()
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
