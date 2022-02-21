using System;
using System.Runtime.Serialization;

namespace GetirTestApi.Application.DTOs.V1
{
    [DataContract]
    public class CreateCustomerRequestDto : CommonCreationalRequestDto
    {
        [DataMember]
        public string NationalId { get; set; }

        [DataMember]
        public DateTime? Birthdate { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}
