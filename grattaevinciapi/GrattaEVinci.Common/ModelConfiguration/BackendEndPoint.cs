namespace GrattaEVinci.Common.ModelConfiguration
{
    public record BackendEndPoint
    {
        public string BackendApiBaseUrl { get; set; }
        public string AuthBackendApiBaseUrl { get; set; }
    }
}
