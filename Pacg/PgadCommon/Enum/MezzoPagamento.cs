using System.Runtime.Serialization;

namespace PgadCommon.Enum
{
    [DataContract(Name = "MEZZO_PAGAMENTO", Namespace = "PgadCommunication.Business")]
    public enum MezzoPagamento
    {
        [EnumMember]
        CONTANTI = 1,
        [EnumMember]
        CARTA_CREDITO = 2,
        [EnumMember]
        CARTA_PREPAGATA = 3,
        [EnumMember]
        BONIFICO_BANCARIO_POSTALE = 4,
        [EnumMember]
        BOLLETTINO_POSTALE = 5,
        [EnumMember]
        ASSEGNO_CONTO_CORRENTE = 6,
        [EnumMember]
        ASSEGNO_CIRCOLARE = 7,
        [EnumMember]
        VAGLIA_POSTALE = 8,
        [EnumMember]
        RICARICA_SCRATCH = 9,
        [EnumMember]
        ALTRO = 10,
        [EnumMember]
        IMEL = 11,
        [EnumMember]
        CONTO_DI_GIOCO = 12,
        [EnumMember]
        CONVERSIONE_BONUS = 13,
        [EnumMember]
        E_WALLET = 14,
        [EnumMember]
        PUNTO_VENDITA = 15
    }
}
