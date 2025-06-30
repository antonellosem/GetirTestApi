
using System.Runtime.Serialization;

namespace GrattaEVinci.Common.Contract.Responses
{
    [DataContract]
    public class login
    {
        [DataMember]
        
        public string codiceRisultato { get; set; }
        [DataMember]
        
        public string descrizioneRisultato { get; set; }
        [DataMember]
        public string idContoGioco { get; set; }
        [DataMember]
        
        public string token { get; set; }
        [DataMember]
        
        public string saldoContoGioco { get; set; }
        [DataMember]
        public string nickname { get; set; }

    }
}
