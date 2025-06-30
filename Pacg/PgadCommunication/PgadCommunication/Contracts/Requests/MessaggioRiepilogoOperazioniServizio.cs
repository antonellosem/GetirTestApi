using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioRiepilogoOperazioniServizio : MessaggioPGAD
    {
        private readonly RiepilogoOperazioniServizioRequestElements _riepilogoOperazioniServizio;
        public RiepilogoOperazioniServizioRequestElements RiepilogoOperazioniServizio
        {
            get
            {
                if (_riepilogoOperazioniServizio.idTransazione == 0)
                {
                    _riepilogoOperazioniServizio.idTransazione = this.GetIdTransazione();
                }
                return _riepilogoOperazioniServizio;
            }
        }

        public MessaggioRiepilogoOperazioniServizio(int titolareSistema, long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _riepilogoOperazioniServizio = new RiepilogoOperazioniServizioRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = titolareSistema
            };
        }

        public void SetData(DateTime data)
        {
            Data dt = new Data
            {
                anno = data.Year.ToString("0000"),
                giorno = data.Day.ToString("00"),
                mese = data.Month.ToString("00")
            };
            _riepilogoOperazioniServizio.data = dt;
        }

       
    }
}
