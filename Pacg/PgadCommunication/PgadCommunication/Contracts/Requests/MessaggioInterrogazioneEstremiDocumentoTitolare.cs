using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioInterrogazioneEstremiDocumento : MessaggioPGAD
    {
        private readonly InterrogazioneEstremiDocumentoRequestElements _messaggioInterrogazioneEstremiDocumento;
        public InterrogazioneEstremiDocumentoRequestElements InterrogazioneEstremiDocumento
        {
            get
            {
                if (_messaggioInterrogazioneEstremiDocumento.idTransazione == 0)
                {
                    _messaggioInterrogazioneEstremiDocumento.idTransazione = this.GetIdTransazione();
                }

                return _messaggioInterrogazioneEstremiDocumento;
            }
        }

        public MessaggioInterrogazioneEstremiDocumento(int titolareSistema, long IdTransazione, string contoGioco)
            : base(titolareSistema, IdTransazione)
        {
            _messaggioInterrogazioneEstremiDocumento = new InterrogazioneEstremiDocumentoRequestElements
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
