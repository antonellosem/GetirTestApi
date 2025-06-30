using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioCambioStatoConto : MessaggioPGAD
    {
        private readonly CambioStatoContoRequestElements _cambioStato;
        public CambioStatoContoRequestElements CambioStato
        {
            get
            {
                //Imposto l'id Transazione se non è stato settato
                if (_cambioStato.idTransazione == 0)
                {
                    _cambioStato.idTransazione = this.GetIdTransazione();
                }

                return _cambioStato;
            }
        }
        public MessaggioCambioStatoConto(int titolareSistema, string contoGioco,long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _cambioStato = new CambioStatoContoRequestElements
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

        public void SetStato(CausaleCambioStatoConto causale, StatoConto stato)
        {
            _cambioStato.causale = Convert.ToSByte(causale);
            _cambioStato.stato = Convert.ToSByte(stato);
        }

    }
}
