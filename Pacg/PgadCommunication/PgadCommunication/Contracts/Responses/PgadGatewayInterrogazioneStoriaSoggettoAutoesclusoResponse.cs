using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse : PgadGatewayResponse
    {
        public PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
        }

        public PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
        }

        [DataMember]
        public int NumeroDettagliSoggettoAutoescluso { get; set; }

        [DataMember]
        public List<DettaglioSoggettoAutoescluso> ElencoDettagliSoggettoAutoescluso { get; set; }
    }
}
