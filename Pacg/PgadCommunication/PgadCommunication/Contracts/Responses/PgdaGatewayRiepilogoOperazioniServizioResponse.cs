using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgdaGatewayRiepilogoOperazioniServizioGenericError))]
    public class PgdaGatewayRiepilogoOperazioniServizioResponse : PgadGatewayResponse
    {

        public PgdaGatewayRiepilogoOperazioniServizioResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
            Operazioni = new List<DettaglioOperazioniServizio>();
        }

        public PgdaGatewayRiepilogoOperazioniServizioResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
        }

        public PgdaGatewayRiepilogoOperazioniServizioResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra, DettaglioOperazioniServizio[] dettaglioOperazioniServizio, Data dataRisposta)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            Operazioni = new List<DettaglioOperazioniServizio>();

            if (dettaglioOperazioniServizio != null)
            {
                Operazioni.AddRange(dettaglioOperazioniServizio);
            }

            if (dataRisposta != null)
            {
                Data = Convert.ToDateTime($"{dataRisposta.anno}-{dataRisposta.mese}-{dataRisposta.giorno}");
            }
        }

        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public List<DettaglioOperazioniServizio> Operazioni { get; set; }
    }
}
