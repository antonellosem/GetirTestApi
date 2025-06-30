using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Name = "TIPO_STORNO_BONUS", Namespace = "PgadCommunication.Business")]
    public enum TipoStornoBonus
    {
        [EnumMember]
        ORDINARIO = 1,
        [EnumMember]
        DA_VINCITA = 2,
        [EnumMember]
        DA_CONVERSIONE = 3
    }
}
