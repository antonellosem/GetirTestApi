
using System.Runtime.Serialization;

namespace GrattaEVinci.Common.Contract.Responses
{
    [DataContract]
    public class logout
    {
        [DataMember]

        public string codiceRisultato { get; set; }
        [DataMember]

        public string descrizioneRisultato { get; set; }
    }
}
