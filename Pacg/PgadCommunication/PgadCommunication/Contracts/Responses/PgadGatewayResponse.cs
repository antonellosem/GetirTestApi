using System;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(DBNull))]
    [KnownType(typeof(PgadGatewayGenericError))]
    public class PgadGatewayResponse
    {
        [DataMember]
        private bool _success;

        [DataMember]
        private short _esito;

        [DataMember]
        private string _descrizione;

        [DataMember]
        private string _idRicevuta;

        [DataMember]
        private long _idTransazione;

        [DataMember]
        private DateTime _timestamp;


        public PgadGatewayResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
        {
            this._success = false;
            this._descrizione = string.Empty;
            this._esito = esito;
            if (esito == Convert.ToInt16(EsitoRichiesta.OK))
                this._success = true;
            this._idTransazione = idTransazione;
            this._idRicevuta = idRicevuta;
            this._descrizione = AbstractPgadGateway.DescrizioneEsitoRitorno(esito);
            this._timestamp = DateTime.Parse(
                $"{dataOra.data.anno}-{dataOra.data.mese}-{dataOra.data.giorno} {dataOra.ora.ore}:{dataOra.ora.minuti}:{dataOra.ora.secondi}");
        }


        public PgadGatewayResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp)
        {
            this._esito = esito;
            this._descrizione = descrizione;
            this._success = success;
            this._idRicevuta = idRicevuta;
            this._timestamp = timestamp;
            this._idTransazione = idTransazione;
        }

        public int Esito => _esito;

        public string Descrizione => _descrizione;

        public bool Success => _success;

        public DateTime Timestamp => _timestamp;

        public long IdTransazione => _idTransazione;

        public string IdRicevuta => _idRicevuta;

        public override string ToString()
        {
            return
                $"{this.Esito} {this.Descrizione} {this.Success} {this.IdRicevuta} {this.IdTransazione} {this.Timestamp}";
        }
    }
}
