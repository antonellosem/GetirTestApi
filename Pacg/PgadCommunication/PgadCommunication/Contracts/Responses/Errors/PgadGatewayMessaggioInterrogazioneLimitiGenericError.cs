using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IntralotWebLib;
using PgadCommon.Enum;

namespace PgadCommunication.Contracts.Responses.Errors
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class PgadGatewayMessaggioInterrogazioneLimitiGenericError : PgadGatewayInterrogazioneLimitiResponse
    {
        public PgadGatewayMessaggioInterrogazioneLimitiGenericError(long idTransazione, string descrizione) :
            base((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizione, false, "", DateTime.Now)
        {
            TraceLog.TraceError("PgadGatewayMessaggioInterrogazioneLimitiGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizione } });
        }
    }
}
