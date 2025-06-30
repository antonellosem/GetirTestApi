using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioElencoContiAutoesclusi : MessaggioPGAD
    {
        private readonly ElencoContiAutoesclusiRequestElements _elencoConti;
        public ElencoContiAutoesclusiRequestElements elencoConti
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

        public MessaggioElencoContiAutoesclusi(int titolareSistema, long idTransazione, short inizio, short fine)
            : base(titolareSistema, idTransazione)
        {
            _elencoConti = new ElencoContiAutoesclusiRequestElements
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
