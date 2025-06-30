using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioGestioneAutoesclusioneTrasversale : MessaggioPGAD
    {

        private readonly GestioneAutoesclusioneTrasversaleRequestElements _messaggioGestioneAutoesclusioneTrasversale;
        public GestioneAutoesclusioneTrasversaleRequestElements GestioneAutoesclusioneTrasversale
        {
            get
            {
                if (_messaggioGestioneAutoesclusioneTrasversale.idTransazione == 0)
                {
                    _messaggioGestioneAutoesclusioneTrasversale.idTransazione = this.GetIdTransazione();
                }

                return _messaggioGestioneAutoesclusioneTrasversale;
            }
        }

        public MessaggioGestioneAutoesclusioneTrasversale(int titolareSistema, long IdTransazione, sbyte GestioneAutoesclusione, sbyte TipoAutoesclusione, string CodiceFiscale)
            : base(titolareSistema, IdTransazione)
        {
            _messaggioGestioneAutoesclusioneTrasversale = new GestioneAutoesclusioneTrasversaleRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                gestioneAutoesclusione = GestioneAutoesclusione,
                tipoAutoesclusione = TipoAutoesclusione,
                codiceFiscale = CodiceFiscale
            };

        }

    }
}
