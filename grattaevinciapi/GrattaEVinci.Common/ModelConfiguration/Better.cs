namespace GrattaEVinci.Common.ModelConfiguration
{
    public record Better
    {
        public string IdRivenditore { get; set; }
        public string CodiceConcessione { get; set; }
        public CertificatiBetter CertificatiBetter { get; set; }
    }
}
