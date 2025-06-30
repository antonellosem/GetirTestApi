using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioLimite
    {
        //private Limite _dettaglioLimite;


        //public Limite Limite => _dettaglioLimite;



        [DataMember]
        public Limite limite;

        public MessaggioLimite()
        {
            //_dettaglioLimite = new Limite();
            limite = new Limite();
        }

        //public class Limite
        //{
        //    [DataMember]
        //    public int Importo { get; set; }
        //    [DataMember]
        //    public sbyte TipoLimite { get; set; }
        //}


        //[DataMember]
        //public int Importo
        //{
        //    get => _dettaglioLimite.importo;
        //    set
        //    {
        //        if (_dettaglioLimite == null)
        //            _dettaglioLimite = new Limite();

        //        _dettaglioLimite.importo = value;
        //    }
        //}

        //[DataMember]
        //public sbyte TipoLimite
        //{
        //    get => _dettaglioLimite.tipoLimite;
        //    set => _dettaglioLimite.tipoLimite = Convert.ToSByte(value);
        //}
    }
}
