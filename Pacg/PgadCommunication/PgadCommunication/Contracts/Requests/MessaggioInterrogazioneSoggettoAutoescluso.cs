using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioInterrogazioneSoggettoAutoescluso : MessaggioPGAD
    {
        private readonly InterrogazioneSoggettoAutoesclusoRequestElements _messaggioInterrogazioneSoggettoAutoescluso;
        public InterrogazioneSoggettoAutoesclusoRequestElements PersonaFisica
        {
            get
            {
                if (_messaggioInterrogazioneSoggettoAutoescluso.idTransazione == 0)
                {
                    _messaggioInterrogazioneSoggettoAutoescluso.idTransazione = this.GetIdTransazione();
                }

                return _messaggioInterrogazioneSoggettoAutoescluso;
            }
        }
        public MessaggioInterrogazioneSoggettoAutoescluso(int titolareSistema, long idTransazione, string CodiceFiscale)
            : base(titolareSistema, idTransazione)
        {
            _messaggioInterrogazioneSoggettoAutoescluso = new InterrogazioneSoggettoAutoesclusoRequestElements
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
            _messaggioInterrogazioneSoggettoAutoescluso.codiceFiscale = codiceFiscale;

            ValidaDatiCodiceFiscale();
        }

        private void ValidaDatiCodiceFiscale()
        {
            if (_messaggioInterrogazioneSoggettoAutoescluso.codiceFiscale.Length != (int)MessaggioPGADConstant.CODICE_FISCALE)
            {
                throw new MessaggioPGADException(
                    $"Parametro Errato: codiceFiscale {_messaggioInterrogazioneSoggettoAutoescluso.codiceFiscale}");
            }
        }
    }
}
