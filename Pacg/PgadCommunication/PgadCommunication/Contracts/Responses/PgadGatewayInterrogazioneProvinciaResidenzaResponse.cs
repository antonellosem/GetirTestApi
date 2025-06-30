using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class PgadGatewayInterrogazioneProvinciaResidenzaResponse : PgadGatewayResponse
    {
        public PgadGatewayInterrogazioneProvinciaResidenzaResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {

        }

        public PgadGatewayInterrogazioneProvinciaResidenzaResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {

        }

        [DataMember]
        public string Provincia { get; set; }

    }
}
