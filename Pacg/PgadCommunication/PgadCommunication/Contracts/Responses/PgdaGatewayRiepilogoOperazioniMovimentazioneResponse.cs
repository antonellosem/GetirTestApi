using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgdaGatewayRiepilogoOperazioniMovimentazioneGenericError))]
    public class PgdaGatewayRiepilogoOperazioniMovimentazioneResponse : PgadGatewayResponse
    {

        public PgdaGatewayRiepilogoOperazioniMovimentazioneResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {
            Operazioni = new List<DettaglioOperazioniMovimentazione>();
        }

        public PgdaGatewayRiepilogoOperazioniMovimentazioneResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            Operazioni = new List<DettaglioOperazioniMovimentazione>();
        }

        public PgdaGatewayRiepilogoOperazioniMovimentazioneResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra, DettaglioOperazioniMovimentazione[] dettaglioOperazioniMovimentazione, Data dataRisposta)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {
            Operazioni = new List<DettaglioOperazioniMovimentazione>();

            if (dettaglioOperazioniMovimentazione != null)
            {
                Operazioni.AddRange(dettaglioOperazioniMovimentazione);
            }

            if (dataRisposta != null)
            {
                Data = Convert.ToDateTime($"{dataRisposta.anno}-{dataRisposta.mese}-{dataRisposta.giorno}");
            }
        }

        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public List<DettaglioOperazioniMovimentazione> Operazioni { get; set; }
    }
}
