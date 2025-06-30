using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayElencoContiGiocoDormientiResponse))]
    public class PgadGatewayElencoContiGiocoDormientiResponse : PgadGatewayResponse
    {
        public PgadGatewayElencoContiGiocoDormientiResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
            ElencoConti = new List<DettaglioConti>();
        }

        public PgadGatewayElencoContiGiocoDormientiResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            ElencoConti = new List<DettaglioConti>();
        }

        [DataMember]
        public int NumeroDettaglioConti { get; set; }

        [DataMember]
        public List<DettaglioConti> ElencoConti { get; set; }
    }
}
