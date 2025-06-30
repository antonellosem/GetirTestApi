namespace GrattaEVinci.Common.Contract.Requests
{
    public class RegistraContoGiocoRequestModel
    {
        public string IdRivenditore { get; set; }
        public string CodiceContratto { get; set; }
        public string Anagrafica { get; set; }
        public string codResidenza { get; set; }
        public string codConcessione { get; set; }
        public string codRete { get; set; }
        public string flagCriptazione { get; set; }
        public string datiFirmati { get; set; }
        public string firma { get; set; }
    }
}
