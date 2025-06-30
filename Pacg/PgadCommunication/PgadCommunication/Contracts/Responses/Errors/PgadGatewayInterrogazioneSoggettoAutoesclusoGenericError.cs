using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IntralotWebLib;
using PgadCommon.Enum;

namespace PgadCommunication.Contracts.Responses.Errors
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class PgadGatewayInterrogazioneSoggettoAutoesclusoGenericError : PgadGatewayInterrogazioneSoggettoAutoesclusoResponse
    {
        public PgadGatewayInterrogazioneSoggettoAutoesclusoGenericError(long idTransazione, string descrizioneErrore) :
            base((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now)
        {
            TraceLog.TraceError("PgadGatewayInterrogazioneSoggettoAutoesclusoGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
        }
    }
}
