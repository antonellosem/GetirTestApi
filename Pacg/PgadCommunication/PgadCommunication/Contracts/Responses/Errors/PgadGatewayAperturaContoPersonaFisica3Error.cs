using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IntralotWebLib;
using PgadCommon.Enum;

namespace PgadCommunication.Contracts.Responses.Errors
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class PgadGatewayAperturaContoPersonaFisica3Error : PgadGatewayAperturaContoPersonaFisica2Response
    {
        public PgadGatewayAperturaContoPersonaFisica3Error(long idTransazione, string descrizioneErrore) :
            base((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now)
        {
            TraceLog.TraceError("PgadGatewayAperturaContoPersonaFisica3Error ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
        }
    }
}
