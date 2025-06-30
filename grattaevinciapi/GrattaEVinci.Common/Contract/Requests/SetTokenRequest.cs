using System.Runtime.Serialization;
using Gamenet.Common.Enum;

namespace GrattaEVinci.Common.Contract.Requests
{
    [DataContract]
    public class SetTokenRequest
    {
        [DataMember]
        public Brand Brand { get; set; }
        [DataMember]
        public string CodiceContratto { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string IpClient { get; set; }
    }
}
