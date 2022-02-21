using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class GetAccountTransactionsRequestDto
    {
        [DataMember]
        public Guid? AccountId { get; set; }

        [DataMember]
        public DateTime? From { get; set; }

        [DataMember]
        public DateTime? To { get; set; }
    }
}
