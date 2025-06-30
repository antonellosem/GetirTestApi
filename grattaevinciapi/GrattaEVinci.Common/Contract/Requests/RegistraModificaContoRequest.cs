using System.Runtime.Serialization;

namespace GrattaEVinci.Common.Contract.Requests
{
    [DataContract]
    public class RegistraModificaContoRequest
    {
        [DataMember]
        public int IdUtente { get; set; }

    }
}
