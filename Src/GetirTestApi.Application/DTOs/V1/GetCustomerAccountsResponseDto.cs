using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class GetCustomerAccountsResponseDto
    {
        [DataMember]
        public CustomerAccountDto[] Accounts { get; set; }
    }

    [DataContract]
    public class CustomerAccountDto
    {
        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public string IBAN { get; set; }

        [DataMember]
        public decimal Balance { get; set; }
    }
}
