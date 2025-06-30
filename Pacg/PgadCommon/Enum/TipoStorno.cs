using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Name = "TIPO_STORNO", Namespace = "PgadCommunication.Business")]
    public enum TipoStorno
    {
        [EnumMember]
        NORMALE = 1,
        [EnumMember]
        CHARGE_BACK = 2
    }
}
