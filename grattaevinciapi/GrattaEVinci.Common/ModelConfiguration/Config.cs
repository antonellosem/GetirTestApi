namespace GrattaEVinci.Common.ModelConfiguration
{
    public record Config
    {
        public string Environment { get; set; }
        public string IdRivenditore { get; set; }
        public string CodiceConcessione { get; set; }
        public string CodiceRete { get; set; }
        public int RetryIntervalSeconds { get; set; }
        public Certificati Certificati { get; set; }
        public ConnectionString ConnectionStrings { get; set; }        
        public Seamless Seamless { get; set; }
        public BackendEndPoint BackendEndPoints { get; set; }
        public string BasePath { get; set; }
        public string GevEndpoint { get; set; }

        //BETTER
        public Better Better { get; set; }
    }
}
