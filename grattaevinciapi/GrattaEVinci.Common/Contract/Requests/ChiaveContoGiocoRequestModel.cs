namespace GrattaEVinci.Common.Contract.Requests
{
    public class ChiaveContoGiocoRequestModel
    {
        public string CodiceContratto { get; set; }
        public string IdRivenditore { get; set; }
        public string DatiFirmati { get; set; }
        public string Firma { get; set; }
    }
}
