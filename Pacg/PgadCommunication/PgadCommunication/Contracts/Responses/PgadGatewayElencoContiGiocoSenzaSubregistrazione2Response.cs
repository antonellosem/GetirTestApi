using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response))]
    public class PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response : PgadGatewayResponse
    {
        public PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
            ElencoConti = new List<ContoGioco>();
        }

        public PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            ElencoConti = new List<ContoGioco>();
        }

        [DataMember]
        public int NumeroDettaglioConti { get; set; }

        [DataMember]
        public List<ContoGioco> ElencoConti { get; set; }
    }
}
