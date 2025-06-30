using System;
using System.Runtime.Serialization;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayAperturaContoPersonaFisica2Error))]
    public class PgadGatewayAperturaContoPersonaFisica2Response : PgadGatewayResponse
    {
        public PgadGatewayAperturaContoPersonaFisica2Response(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {

        }

        public PgadGatewayAperturaContoPersonaFisica2Response(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {

        }

    }
}
