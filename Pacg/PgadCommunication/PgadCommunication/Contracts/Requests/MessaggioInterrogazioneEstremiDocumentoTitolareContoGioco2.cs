using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2 : MessaggioPGAD
    {
        private readonly InterrogazioneEstremiDocumento2RequestElements _messaggioInterrogazioneEstremiDocumento;
        public InterrogazioneEstremiDocumento2RequestElements InterrogazioneEstremiDocumento
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

        public MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2(int titolareSistema, long IdTransazione, string contoGioco)
            : base(titolareSistema, IdTransazione)
        {
            _messaggioInterrogazioneEstremiDocumento = new InterrogazioneEstremiDocumento2RequestElements
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
