using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayInterrogazioneEstremiDocumentoTitolareGenericError))]
    public class PgadGatewayInterrogazioneEstremiDocumentoResponse : PgadGatewayResponse
    {
        public PgadGatewayInterrogazioneEstremiDocumentoResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
            Documenti = new List<Documento>();
        }

        public PgadGatewayInterrogazioneEstremiDocumentoResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            Documenti = new List<Documento>();
        }

        [DataMember]
        public sbyte NumeroDocumenti { get; set; }

        [DataMember]
        public List<Documento> Documenti { get; set; }
    }
}
