namespace GrattaEVinci.Common.Contract.Requests
{
    public class ReserveAddebitoRequestModel
    {
        public string Token { get; set; }
        public string CodiceContratto { get; set; }
        public string IdRivenditore { get; set; }
        public string IdConcorso { get; set; }
        public string IdTransazione { get; set; }
        public string CostoGiocata { get; set; }
        public string TimeStamp { get; set; }
        public string DatiFirmati { get; set; }
        public string Firma { get; set; }
        public string IdGioco { get; set; }
    }
}
