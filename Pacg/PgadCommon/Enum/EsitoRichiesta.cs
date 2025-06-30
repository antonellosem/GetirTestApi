using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public enum EsitoRichiesta
    {
        [EnumMember]
        GENERIC_ERROR = 0,
        [EnumMember]
        OK = 1024
    }
}
