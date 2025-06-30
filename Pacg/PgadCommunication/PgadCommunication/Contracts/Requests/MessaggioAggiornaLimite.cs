using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioAggiornaLimite : MessaggioPGAD
    {
        private readonly AggiornaLimiteRequestElements _messaggioAggiornaLimite;

        public AggiornaLimiteRequestElements AggiornaLimite
        {
            get
            {
                if (_messaggioAggiornaLimite.idTransazione == 0)
                {
                    _messaggioAggiornaLimite.idTransazione = this.GetIdTransazione();
                }

                return _messaggioAggiornaLimite;
            }
        }

        public MessaggioAggiornaLimite(int titolareSistema, long idTransazione, string contoGioco, sbyte gestioneLimite, sbyte tipoLimite, int importo)
            : base(titolareSistema, idTransazione)
        {
            _messaggioAggiornaLimite = new AggiornaLimiteRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco,
                gestioneLimite = gestioneLimite,
                limite = new Limite { tipoLimite = tipoLimite, importo = importo }
            };
        }


    }
}
