using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class CommonCreationalRequestDto
    {
        [DataMember]
        public string User { get; set; }
    }
}
