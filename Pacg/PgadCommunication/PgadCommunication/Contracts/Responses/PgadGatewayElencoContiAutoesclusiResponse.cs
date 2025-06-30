using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayMessaggioElencoContiAutoesclusiError))]
    public class PgadGatewayElencoContiAutoesclusiResponse : PgadGatewayResponse
    {
        public PgadGatewayElencoContiAutoesclusiResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {

        }

        public PgadGatewayElencoContiAutoesclusiResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {

        }

        [DataMember]
        public int NumeroDettaglioConti { get; set; }

        [DataMember]
        public List<DettaglioContiAutoesclusi> ElencoConti { get; set; }
    }
}
