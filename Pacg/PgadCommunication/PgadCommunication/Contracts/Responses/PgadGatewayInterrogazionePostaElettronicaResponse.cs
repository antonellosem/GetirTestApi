using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{

    [DataContract(Namespace = "PgadCommunication.Business")]
    public class PgadGatewayInterrogazionePostaElettronicaResponse : PgadGatewayResponse
    {
        public PgadGatewayInterrogazionePostaElettronicaResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {

        }

        public PgadGatewayInterrogazionePostaElettronicaResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {

        }

        [DataMember]
        public string PostaElettronica { get; set; }

    }
}
