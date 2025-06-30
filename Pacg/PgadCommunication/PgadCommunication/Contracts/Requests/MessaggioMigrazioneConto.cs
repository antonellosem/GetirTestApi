using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioMigrazioneConto : MessaggioPGAD
    {
        private readonly MigrazioneContoRequestElements _migrazioneConto;

        public MigrazioneContoRequestElements MigrazioneConto
        {
            get
            {
                if (_migrazioneConto.idTransazione == 0)
                {
                    _migrazioneConto.idTransazione = this.GetIdTransazione();
                }
                return _migrazioneConto;
            }
        }

        public MessaggioMigrazioneConto(int titolareSistema,long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _migrazioneConto = new MigrazioneContoRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione
            };

        }

        public void SetContoOriginario(int IdReteTitolare, int TitolareSistema ,string Conto)
        {
            _migrazioneConto.idReteContoOriginario = Convert.ToSByte(IdReteTitolare);
            _migrazioneConto.idCnContoOriginario =  Convert.ToInt64(TitolareSistema);
            _migrazioneConto.codiceContoOriginario = Conto;
        }

        public void SetContoDestinazione( int IdReteTitolare, int TitolareSistema,string Conto)
        {
            _migrazioneConto.idReteContoDestinazione = Convert.ToSByte(IdReteTitolare);
            _migrazioneConto.idCnContoDestinazione = Convert.ToInt64(TitolareSistema);
            _migrazioneConto.codiceContoDestinazione = Conto;
        }

        public void SetCodiceFiscale(string CodiceFiscale)
        {
            _migrazioneConto.codiceFiscale = CodiceFiscale;
        }

        public void SetSaldo(int importo)
        {
            _migrazioneConto.importoSaldo = importo;
            _migrazioneConto.dataOraSaldo = new DataOra();
            _migrazioneConto.dataOraSaldo.data = new Data();
            _migrazioneConto.dataOraSaldo.ora = new Ora();

            _migrazioneConto.dataOraSaldo.data.anno = DateTime.Now.Year.ToString("0000");
            _migrazioneConto.dataOraSaldo.data.mese = DateTime.Now.Month.ToString("00");
            _migrazioneConto.dataOraSaldo.data.giorno = DateTime.Now.Day.ToString("00");
            _migrazioneConto.dataOraSaldo.ora.ore = DateTime.Now.Hour.ToString("00");
            _migrazioneConto.dataOraSaldo.ora.minuti = DateTime.Now.Minute.ToString("00");
            _migrazioneConto.dataOraSaldo.ora.secondi = DateTime.Now.Second.ToString("00");
        }

        public void SetSaldo(int importo, DateTime data)
        {
            _migrazioneConto.dataOraSaldo = new DataOra {data = new Data(), ora = new Ora()};
            _migrazioneConto.importoSaldo = importo;
            _migrazioneConto.dataOraSaldo.data.anno = data.Year.ToString("0000");
            _migrazioneConto.dataOraSaldo.data.mese = data.Month.ToString("00");
            _migrazioneConto.dataOraSaldo.data.giorno = data.Day.ToString("00");
            _migrazioneConto.dataOraSaldo.ora.ore = data.Hour.ToString("00");
            _migrazioneConto.dataOraSaldo.ora.minuti = data.Minute.ToString("00");
            _migrazioneConto.dataOraSaldo.ora.secondi = data.Second.ToString("00");
        }

        public void SetSaldoBonus(List<MessaggioDettaglioBonus> bonus)
        {
            _migrazioneConto.importoBonusSaldo = 0;
            if (bonus != null)
            {
                _migrazioneConto.numDettagliBonusSaldo = Convert.ToSByte(bonus.Count);
                if (bonus.Count > 0)
                {
                    _migrazioneConto.dettaglioBonusSaldo = new DettaglioBonus[bonus.Count];

                    int i = 0;
                    foreach (MessaggioDettaglioBonus b in bonus)
                    {
                        _migrazioneConto.dettaglioBonusSaldo[i] = b.DettaglioBonus;
                        _migrazioneConto.importoBonusSaldo += b.DettaglioBonus.importo;
                        i++;
                    }
                }
            }
        }
        
    }
}
