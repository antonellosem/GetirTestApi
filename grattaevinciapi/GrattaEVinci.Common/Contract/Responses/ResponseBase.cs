using System.Runtime.Serialization;
using Gamenet.Common.Enum;

namespace GrattaEVinci.Common.Contract.Responses
{
    [DataContract]
    public class ResponseBase
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public ResultCode ResultCode { get; set; }
    }
}
