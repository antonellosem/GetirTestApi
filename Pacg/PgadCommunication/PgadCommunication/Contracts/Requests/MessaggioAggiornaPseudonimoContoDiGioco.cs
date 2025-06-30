using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioAggiornaPseudonimoContoDiGioco : MessaggioPGAD
    {
        private readonly AggiornaPseudonimoContoRequestElements _messaggioAggiornaPseudonimoContoDiGioco;
        public AggiornaPseudonimoContoRequestElements PersonaFisica
        {
            get
            {
                if (_messaggioAggiornaPseudonimoContoDiGioco.idTransazione == 0)
                {
                    _messaggioAggiornaPseudonimoContoDiGioco.idTransazione = this.GetIdTransazione();
                }

                return _messaggioAggiornaPseudonimoContoDiGioco;
            }
        }

        public MessaggioAggiornaPseudonimoContoDiGioco(int titolareSistema, long idTransazione, string CodiceConto, string Pseudonimo)
            : base(titolareSistema, idTransazione)
        {
            _messaggioAggiornaPseudonimoContoDiGioco = new AggiornaPseudonimoContoRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = CodiceConto,
                pseudonimo = Pseudonimo
            };
        }

        public void SetCodiceFiscale(string Pseudonimo)
        {
            _messaggioAggiornaPseudonimoContoDiGioco.pseudonimo = Pseudonimo;

            ValidaDatiPseudonimo();
        }

        private void ValidaDatiPseudonimo()
        {
            if (_messaggioAggiornaPseudonimoContoDiGioco.pseudonimo.Length < 1 || _messaggioAggiornaPseudonimoContoDiGioco.pseudonimo.Length > (int)MessaggioPGADConstant.PSEUDONIMO)
            {
                throw new MessaggioPGADException($"Parametro Errato: pseudonimo {_messaggioAggiornaPseudonimoContoDiGioco.pseudonimo}");
            }
        }


    }
}
