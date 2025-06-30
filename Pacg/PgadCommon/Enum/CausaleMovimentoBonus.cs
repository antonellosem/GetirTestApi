using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Name = "CAUSALE_MOVIMENTO_BONUS", Namespace = "PgadCommunication.Business")]
    public enum CausaleMovimentoBonus
    {
        [EnumMember]
        BONUS = 5,
        [EnumMember]
        STORNO_BONUS = 6
    }
}
