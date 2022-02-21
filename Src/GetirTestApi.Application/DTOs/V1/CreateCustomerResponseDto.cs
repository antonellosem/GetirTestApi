using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class CreateCustomerResponseDto
    {
        [DataMember]
        public Guid CustomerId { get; set; }
    }
}
