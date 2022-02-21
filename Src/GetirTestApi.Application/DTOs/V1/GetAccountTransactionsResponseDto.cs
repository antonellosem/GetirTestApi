using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class GetAccountTransactionsResponseDto
    {
        [DataMember]
        public AccountTransactionDto[] Transactions { get; set; }
    }

    [DataContract]
    public class AccountTransactionDto
    {
        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public bool IsWithdrawal { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }
}
