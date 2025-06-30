namespace GrattaEVinci.Common.Contract.Requests
{
    public class CommitAddebitoRequestModel
    {
        public string Token { get; set; }
        public string CodiceContratto { get; set; }
        public string IdRivenditore { get; set; }
        public string IdConcorso { get; set; }
        public string IdTransazione { get; set; }
        public string CodiceGiocata { get; set; }
        public string ImportoVincita { get; set; }
        public string ImportoAccredito { get; set; }
        public string FasciaVincita { get; set; }
        public string TimeStamp { get; set; }
        public string DatiFirmati { get; set; }
        public string Firma { get; set; }
        public string IdGioco { get; set; }
    }
}
