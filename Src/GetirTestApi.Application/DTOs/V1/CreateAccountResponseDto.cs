using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class CreateAccountResponseDto
    {
        [DataMember]
        public Guid AccountId { get; set; }
    }
}
