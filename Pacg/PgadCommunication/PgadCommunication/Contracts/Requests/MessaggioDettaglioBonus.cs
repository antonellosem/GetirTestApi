using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioDettaglioBonus
    {
        private readonly DettaglioBonus _dettagioBonus;
     
        public MessaggioDettaglioBonus()
        {
            _dettagioBonus = new DettaglioBonus();
        }

        public MessaggioDettaglioBonus(int importo, int famigliaGioco, int tipoGioco)
        {
            _dettagioBonus = new DettaglioBonus
            {
                famigliaGioco = Convert.ToSByte(famigliaGioco),
                tipoGioco = Convert.ToSByte(tipoGioco),
                importo = importo
            };
        }

        [DataMember]
        public int FamigliaGioco
        { 
            set => _dettagioBonus.famigliaGioco = Convert.ToSByte(value);
            get => _dettagioBonus.famigliaGioco;
        }
     
        [DataMember]
        public int TipoGioco
        {
            set => _dettagioBonus.tipoGioco = Convert.ToSByte(value);
            get => _dettagioBonus.tipoGioco;
        }

        [DataMember]
        public DettaglioBonus DettaglioBonus => _dettagioBonus;
    }
}
