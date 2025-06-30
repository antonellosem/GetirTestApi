using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioInterrogazionePostaElettronica : MessaggioPGAD
    {
        private readonly InterrogazionePostaElettronicaContoRequestElements _messaggioInterrogazionePostaElettronica;

        public InterrogazionePostaElettronicaContoRequestElements InterrogazionePostaElettronica
        {
            get
            {
                if (_messaggioInterrogazionePostaElettronica.idTransazione == 0)
                {
                    _messaggioInterrogazionePostaElettronica.idTransazione = this.GetIdTransazione();
                }

                return _messaggioInterrogazionePostaElettronica;
            }
        }

        public MessaggioInterrogazionePostaElettronica(int titolareSistema, long IdTransazione, string contoGioco)
            : base(titolareSistema, IdTransazione)
        {
            _messaggioInterrogazionePostaElettronica = new InterrogazionePostaElettronicaContoRequestElements()
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
