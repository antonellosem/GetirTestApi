using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioElencoContiGiocoDormienti : MessaggioPGAD
    {
        private readonly ElencoContiDormientiRequestElements _elencoConti;
        public ElencoContiDormientiRequestElements elencoConti
        {
            get
            {
                if (_elencoConti.idTransazione == 0)
                {
                    _elencoConti.idTransazione = this.GetIdTransazione();
                }
                return _elencoConti;
            }
        }

        public MessaggioElencoContiGiocoDormienti(int anno, int mese, int giorno, short inizio, short fine, int titolareSistema, long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _elencoConti = new ElencoContiDormientiRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                dataRichiesta =
                    new Data
                    {
                        anno = anno.ToString("0000"), mese = mese.ToString("00"), giorno = giorno.ToString("00")
                    },
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                inizio = inizio,
                fine = fine
            };

        }


    }
}
