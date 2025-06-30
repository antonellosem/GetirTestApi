using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioElencoContiAutoesclusi2 : MessaggioPGAD
    {
        private readonly ElencoContiAutoesclusi2RequestElements _elencoConti;
        public ElencoContiAutoesclusi2RequestElements elencoConti
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

        public MessaggioElencoContiAutoesclusi2(int titolareSistema, long idTransazione, int inizio, int fine)
            : base(titolareSistema, idTransazione)
        {
            _elencoConti = new ElencoContiAutoesclusi2RequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                inizio = inizio,
                fine = fine
            };

        }
    }
}
