using GrattaEVinci.Common.Enum;

namespace GrattaEVinci.Common.Entities
{
    public record InfoPrenotazione
    {
        public int IdPrenotazioneBiglietto { get; set; }
        public int IdUtente { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public DateTime? DataAggiornamento { get; set; }
        public DateTime DataOperazione { get; set; }
        public string IdTransazione { get; set; }
        public long? IdMovimento { get; set; }
        public int IdStato { get; set; }
        public string DescrizioneStato { get; set; }
        public string Token { get; set; }
        public int CostoGiocata { get; set; }
    }
}
