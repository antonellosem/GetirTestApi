using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayMessaggioInterrogazioneLimitiGenericError))]
    public class PgadGatewayInterrogazioneLimitiResponse : PgadGatewayResponse
    {
        public PgadGatewayInterrogazioneLimitiResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
            Limiti = new List<Limite>();
        }

        public PgadGatewayInterrogazioneLimitiResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            Limiti = new List<Limite>();
        }

        [DataMember]
        public sbyte NumeroLimiti { get; set; }

        [DataMember]
        public List<Limite> Limiti { get; set; }
    }
}
