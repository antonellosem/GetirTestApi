using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Name = "CAUSALE_MOVIMENTO", Namespace = "PgadCommunication.Business")]
    public enum CausaleMovimento
    {
        [EnumMember]
        RICARICA = 1,
        [EnumMember]
        STORNO_RICARICA = 2,
        [EnumMember]
        PRELIEVO = 3,
        [EnumMember]
        STORNO_PRELIEVO = 4,
        [EnumMember]
        COSTI_SERVIZI_AGGIUNTIVI = 7,
        [EnumMember]
        STORNO_COSTI_SERVIZI_AGGIUNTIVI = 8
    }
}
