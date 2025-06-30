using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayElencoContiGiocoDormienti2Response))]
    public class PgadGatewayElencoContiGiocoDormienti2Response : PgadGatewayResponse
    {
        public PgadGatewayElencoContiGiocoDormienti2Response(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
            ElencoConti = new List<ContoGiocoSaldo>();
        }

        public PgadGatewayElencoContiGiocoDormienti2Response(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            ElencoConti = new List<ContoGiocoSaldo>();
        }

        [DataMember]
        public int NumeroDettaglioConti { get; set; }

        [DataMember]
        public List<ContoGiocoSaldo> ElencoConti { get; set; }
    }
}
