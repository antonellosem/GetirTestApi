namespace GrattaEVinci.Common.Entities
{
    public record CommitBiglietto
    {
        public int IdBiglietto { get; set; }
        public long IdMovimento { get; set; }
    }
}
