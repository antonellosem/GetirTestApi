using System;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioElencoContiGiocoSenzaSubregistrazione : MessaggioPGAD
    {
        private readonly ElencoContiSenzaSubregistrazioneRequestElements _elencoConti;
        public ElencoContiSenzaSubregistrazioneRequestElements elencoConti
        {
            get
            {
                if (_elencoConti.idTransazione == 0)
                {
                    _elencoConti.idTransazione = this.GetIdTransazione();
                }
                return _elencoConti;
            }
        }

        public MessaggioElencoContiGiocoSenzaSubregistrazione(sbyte stato, short inizio, short fine, int titolareSistema, long idTransazione, DateTime data)
            : base(titolareSistema, idTransazione)
        {
            _elencoConti = new ElencoContiSenzaSubregistrazioneRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                dataRichiesta = new Data
                {
                    anno = data.Year.ToString("0000"),
                    mese = data.Month.ToString("00"),
                    giorno = data.Day.ToString("00")
                },
                stato = stato,
                inizio = inizio,
                fine = fine
            };
        }


    }
}
