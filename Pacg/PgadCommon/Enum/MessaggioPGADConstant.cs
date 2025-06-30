using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public enum MessaggioPGADConstant
    {
        [EnumMember]
        CODICE_FISCALE = 16,
        [EnumMember]
        DEFAULT_STR = 100,
        [EnumMember]
        PROVINCIA = 2,
        [EnumMember]
        CAP = 5,
        [EnumMember]
        PARTITA_IVA = 11,
        [EnumMember]
        PSEUDONIMO = 100,
        [EnumMember]
        EMAIL = 100
    }
}
