using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioRiepilogoOperazioniMovimentazione : MessaggioPGAD
    {
        private readonly RiepilogoOperazioniMovimentazioneRequestElements _riepilogoOperazioniMovimentazione;
        public RiepilogoOperazioniMovimentazioneRequestElements RiepilogoOperazioniMovimentazione
        {
            get
            {
                if (_riepilogoOperazioniMovimentazione.idTransazione == 0)
                {
                    _riepilogoOperazioniMovimentazione.idTransazione = this.GetIdTransazione();
                }
                return _riepilogoOperazioniMovimentazione;
            }
        }

        public MessaggioRiepilogoOperazioniMovimentazione(int titolareSistema,long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _riepilogoOperazioniMovimentazione = new RiepilogoOperazioniMovimentazioneRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = titolareSistema
            };
        }

        public void setData(DateTime data)
        {
            Data dt = new Data
            {
                anno = data.Year.ToString("0000"),
                giorno = data.Day.ToString("00"),
                mese = data.Month.ToString("00")
            };
            _riepilogoOperazioniMovimentazione.data = dt;
        }

       
    }
}
