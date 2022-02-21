using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class CreateAccountRequestDto : CommonCreationalRequestDto
    {
        [DataMember]
        public Guid? CustomerId { get; set; }

        [DataMember]
        public string IBAN { get; set; }
    }
}
