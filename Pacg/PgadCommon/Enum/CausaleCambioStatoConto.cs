using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Name = "CAUSALE_CAMBIO_STATO_CONTO", Namespace = "PgadCommunication.Business")]
    public enum CausaleCambioStatoConto
    {
        [EnumMember]
        RICHIESTO_DA_AAMS = 1,
        [EnumMember]
        RICHIESTO_DAL_CONCESSIONARIO = 2,
        [EnumMember]
        RICHIESTO_DAL_TITOLARE_CONTO = 3,
        [EnumMember]
        RICHIESTO_DA_AUTORITA_GIUDIZIARIA = 4,
        [EnumMember]
        RICHIESTO_DA_AAMS_DECESSO = 5,
        [EnumMember]
        RICHIESTO_DAL_CONCESSIONARIO_PER_MANCATO_INVIO_DOC = 6,
        [EnumMember]
        RICHIESTO_DAL_CONCESSIONARIO_PER_FRODE = 7,
        [EnumMember]
        RICHIESTO_DAL_TITOLARE_PER_AUTOESCLUSIONE = 8
    }
}
