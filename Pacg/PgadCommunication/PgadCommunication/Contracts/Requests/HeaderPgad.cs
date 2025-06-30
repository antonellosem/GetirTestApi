using System.Runtime.Serialization;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class HeaderPgad
    {
        public HeaderPgad()
        {
        }

        public HeaderPgad(int idFsc, int idRete, int idCnc, int idReteConto, int idCnConto)
        {
            this.idFsc = idFsc;
            this.idRete = idRete;
            this.idCnc = idCnc;
            this.idReteConto = idReteConto;
            this.idCnConto = idCnConto;
        }

        public int idFsc { get; set; }
        public int idRete { get; set; }
        public int idCnc { get; set; }
        public int idReteConto { get; set; }
        public int idCnConto { get; set; }
    }
}
