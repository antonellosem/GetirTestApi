using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Model;

namespace PgadCommunication
{
    public class AbstractPgadGateway
    {
        [DataMember(Name = "_codiciRitorno")]
        protected static Dictionary<short, string> CodiciRitorno;

        public int TitolareSistema { get; set; }
        public string UserName { get; set; }

        static AbstractPgadGateway()
        {
            CodiciRitorno = PgadAdapter.getCodiciRitorno();
        }

        public AbstractPgadGateway(string UserName, int TitolareSistema)
        {
            this.UserName = UserName;
            this.TitolareSistema = TitolareSistema;
        }

        public static string DescrizioneEsitoRitorno(short esito)
        {
            if (CodiciRitorno != null && CodiciRitorno.ContainsKey(esito))
            {
                return CodiciRitorno[esito];
            }

            //Errore non gestito 0
            if (CodiciRitorno != null && CodiciRitorno.ContainsKey(0))
            {
                return CodiciRitorno[0];
            }

            return "";
        }
    }
}
