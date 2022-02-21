using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class CreateAccountTransactionResponseDto
    {
        [DataMember]
        public decimal Balance { get; set; }
    }
}
