using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Name = "STATO_CONTO", Namespace = "PgadCommunication.Business")]
    public enum StatoConto
    {
        [EnumMember]
        APERTO = 1,
        [EnumMember]
        SOSPESO = 2,
        [EnumMember]
        CHIUSO = 3,
        [EnumMember]
        DORMIENTE = 4,
        [EnumMember]
        BLOCCATO = 5
    }
}
