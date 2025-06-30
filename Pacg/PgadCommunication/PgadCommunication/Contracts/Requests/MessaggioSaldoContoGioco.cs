using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;


namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioSaldoConto : MessaggioPGAD
    {
        private readonly SaldoContoRequestElements _saldoConto;
        public SaldoContoRequestElements SaldoConto
        {
            get
            {
                if (_saldoConto.idTransazione == 0)
                {
                    _saldoConto.idTransazione = this.GetIdTransazione();
                }
                return _saldoConto;
            }
        }

        public MessaggioSaldoConto(int titolareSistema, string contoGioco, long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _saldoConto = new SaldoContoRequestElements
            {
                dataOraSaldo = new DataOra() {data = new Data(), ora = new Ora()},
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco
            };
        }

        public void SetSaldo(int importo)
        {
            _saldoConto.importoSaldo = importo;
            _saldoConto.dataOraSaldo.data.anno = DateTime.Now.Year.ToString("0000");
            _saldoConto.dataOraSaldo.data.mese = DateTime.Now.Month.ToString("00");
            _saldoConto.dataOraSaldo.data.giorno = DateTime.Now.Day.ToString("00");
            _saldoConto.dataOraSaldo.ora.ore = DateTime.Now.Hour.ToString("00");
            _saldoConto.dataOraSaldo.ora.minuti = DateTime.Now.Minute.ToString("00");
            _saldoConto.dataOraSaldo.ora.secondi = DateTime.Now.Second.ToString("00");
        }

        public void SetSaldo(int importo, DateTime data)
        {
            _saldoConto.importoSaldo = importo;
            _saldoConto.dataOraSaldo.data.anno = data.Year.ToString("0000");
            _saldoConto.dataOraSaldo.data.mese = data.Month.ToString("00");
            _saldoConto.dataOraSaldo.data.giorno = data.Day.ToString("00");
            _saldoConto.dataOraSaldo.ora.ore = data.Hour.ToString("00");
            _saldoConto.dataOraSaldo.ora.minuti = data.Minute.ToString("00");
            _saldoConto.dataOraSaldo.ora.secondi = data.Second.ToString("00");
        }

        public void SetSaldoBonus(List<MessaggioDettaglioBonus> bonus)
        {

            _saldoConto.importoBonusSaldo = 0;
            if (bonus != null)
            {
                _saldoConto.numDettagliBonusSaldo = Convert.ToSByte(bonus.Count);
                if (bonus.Count > 0)
                {
                    _saldoConto.dettaglioBonusSaldo = new DettaglioBonus[bonus.Count];

                    int i = 0;
                    foreach (MessaggioDettaglioBonus b in bonus)
                    {
                        _saldoConto.dettaglioBonusSaldo[i] = b.DettaglioBonus;
                        _saldoConto.importoBonusSaldo += b.DettaglioBonus.importo;
                        i++;
                    }
                }
            }
        }

    }
}
