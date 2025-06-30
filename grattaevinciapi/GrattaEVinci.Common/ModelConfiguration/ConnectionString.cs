namespace GrattaEVinci.Common.ModelConfiguration
{
    public record ConnectionString
    {
        public string Pacg { get; set; }
        public string Catalogo { get; set; }
        public string GrattaEVinci_GB { get; set; }
        public string Tools { get; set; }
        public string PGDA { get; set; }
        public string ConnectionStringTransactionMongoDb { get; set; }
        public string ConnectionStringTransactionMongoDbRetry { get; set; }
    }
}
