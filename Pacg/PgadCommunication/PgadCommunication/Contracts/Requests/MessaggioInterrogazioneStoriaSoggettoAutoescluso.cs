using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioInterrogazioneStoriaSoggettoAutoescluso : MessaggioPGAD
    {
        private readonly InterrogazioneStoriaSoggettoAutoesclusoRequestElements _messaggioInterrogazioneStoriaSoggettoAutoescluso;
        public InterrogazioneStoriaSoggettoAutoesclusoRequestElements PersonaFisica
        {
            get
            {
                if (_messaggioInterrogazioneStoriaSoggettoAutoescluso.idTransazione == 0)
                {
                    _messaggioInterrogazioneStoriaSoggettoAutoescluso.idTransazione = this.GetIdTransazione();
                }

                return  _messaggioInterrogazioneStoriaSoggettoAutoescluso;
            }
        }
        public MessaggioInterrogazioneStoriaSoggettoAutoescluso(int titolareSistema, long idTransazione, string CodiceFiscale)
            : base(titolareSistema, idTransazione)
        {
            _messaggioInterrogazioneStoriaSoggettoAutoescluso = new InterrogazioneStoriaSoggettoAutoesclusoRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                codiceFiscale = CodiceFiscale
            };
        }

        public void SetCodiceFiscale(string codiceFiscale)
        {
            _messaggioInterrogazioneStoriaSoggettoAutoescluso.codiceFiscale = codiceFiscale;

            ValidaDatiCodiceFiscale();
        }

        private void ValidaDatiCodiceFiscale()
        {
            if (_messaggioInterrogazioneStoriaSoggettoAutoescluso.codiceFiscale.Length != (int)MessaggioPGADConstant.CODICE_FISCALE)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: codiceFiscale {_messaggioInterrogazioneStoriaSoggettoAutoescluso.codiceFiscale}");
            }
        }
    }
}
