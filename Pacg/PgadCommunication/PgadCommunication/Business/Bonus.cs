

using System.Runtime.Serialization;

namespace PgadCommunication.Business
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class Bonus
    {
        public Bonus()
        {

        }
        
        public Bonus(int Importo,int TipoGioco,int CodiceFamiglia)
        {
            this.Importo = Importo;
            this.TipoGioco = TipoGioco;
            this.CodiceFamiglia = CodiceFamiglia;
        }

        [DataMember]
        public int Importo { get; set; }
        [DataMember] 
        public int TipoGioco{ get;set; }
        [DataMember]
        public int CodiceFamiglia{ get;set; }

    }
}
