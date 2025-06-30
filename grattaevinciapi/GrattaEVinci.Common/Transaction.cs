using Gamenet.Common.Enum;
using Gamenet.Common.Interface;

namespace GrattaEVinci.Common
{
    public record Transaction : IEntity
    {
        public Guid Id { get; set; }
        public string SourceTransactionCode { get; set; }
        public string RelatedTransactionCode { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string JsonDataRequest { get; set; }
        public string JsonDataResponse { get; set; }
        public int Result { get; set; }
        public CasinoOperationType OperationType { get; set; }
        public long? IdBiglietto { get; set; }
        public long? IdMovimento { get; set; }
        public int NumeroRetry { get; set; }
        public Object Request { get; set; }
        public Object Response { get; set; }
    }
}
