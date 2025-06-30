using System;
using System.Runtime.Serialization;
using PgadCommunication.Contracts.Responses.Errors;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Responses
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    [KnownType(typeof(PgadGatewayStatoGenericError))]
    public class PgadGatewayStatoResponse : PgadGatewayResponse
    {
        public PgadGatewayStatoResponse(short esito, long idTransazione, string descrizione, bool success, string idRicevuta, DateTime timestamp) :
            base(esito, idTransazione, descrizione, success, idRicevuta, timestamp)
        {

        }

        public PgadGatewayStatoResponse(short esito, long idTransazione, string idRicevuta, DataOra dataOra)
            : base(esito, idTransazione, idRicevuta, dataOra)
        {

        }
        [DataMember]
        public long IdCnConto { get; set; }
        [DataMember]
        public int IdReteConto { get; set; }
        [DataMember]
        public string CodiceConto { get; set; }
        [DataMember]
        public int Stato { get; set; }
        [DataMember]
        public int Causale { get; set; }

        public override string ToString()
        {
            string str = base.ToString();
            str += " " + $"{this.IdCnConto} {this.IdReteConto} {this.CodiceConto} {this.Stato} {this.Causale}";
            return str;
        }
    }
}
