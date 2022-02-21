using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class GetCustomerAccountsRequestDto
    {
        [DataMember]
        public Guid? CustomerId { get; set; }
    }
}
