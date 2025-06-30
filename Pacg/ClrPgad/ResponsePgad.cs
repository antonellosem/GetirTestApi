using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClrPgad
{
    public class ResponsePgad
    {
   
        public int Esito { get; set; }
        public bool Success { get; set; }
        public string Descrizione { get; set; }
        public string IdTransazione { get; set; }
        public string IdRicevuta { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class ResponsePgadStatoConto : ResponsePgad
    {
        public int Causale { get; set; }
        public string CodiceConto { get; set; }
        public long CodiceConcessionario { get; set; }
        public int TipoConcessione { get; set; }
        public int Stato { get; set; }
    }
}
