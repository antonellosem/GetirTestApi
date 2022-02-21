using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class CreateAccountTransactionRequestDto : CommonCreationalRequestDto
    {
        [DataMember]
        public Guid? AccountId { get; set; }

        [DataMember]
        public bool? IsWithdrawal { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }
    }
}
