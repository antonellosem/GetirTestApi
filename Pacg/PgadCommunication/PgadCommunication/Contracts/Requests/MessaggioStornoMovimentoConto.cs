using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioStornoMovimentoConto : MessaggioPGAD
    {
        private readonly StornoMovimentazioneContoRequestElements _stornoMovimentazioneConto;
        public StornoMovimentazioneContoRequestElements StornoMovimentazioneConto
        {
            get
            {
                if (_stornoMovimentazioneConto.idTransazione == 0)
                {
                    _stornoMovimentazioneConto.idTransazione = this.GetIdTransazione();
                }
                return _stornoMovimentazioneConto;
            }
        }
        public MessaggioStornoMovimentoConto(int titolareSistema, string contoGioco, long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _stornoMovimentazioneConto = new StornoMovimentazioneContoRequestElements
            {
                idFsc = Convert.ToInt16(this.header.FornitoreSevizi),
                idRete = Convert.ToSByte(this.header.ReteConcessionario),
                 idCn = Convert.ToInt64(this.header.Concessionario),
                idTransazione = this.idTransazione,
                idReteConto = Convert.ToSByte(this.header.ReteTitolare),
                idCnConto = Convert.ToInt64(this.header.TitolareSistema),
                codiceConto = contoGioco,
                dataOraSaldo = new DataOra() {data = new Data(), ora = new Ora()}
            };

        }

        public void SetMovimento(CausaleMovimento causale, MezzoPagamento mezzoPagamento,TipoStorno tipoStorno, int importo, string idRicevutaDaStornare)
        {
            _stornoMovimentazioneConto.tipoStorno = Convert.ToSByte(tipoStorno);
            _stornoMovimentazioneConto.causaleMovimento = Convert.ToSByte(causale);
            _stornoMovimentazioneConto.mezzoDiPagamento = Convert.ToSByte(mezzoPagamento);
            _stornoMovimentazioneConto.importoMovimento = importo;
            _stornoMovimentazioneConto.idMovDaStornare = idRicevutaDaStornare;
        }

        public void SetSaldo(int importo)
        {
            _stornoMovimentazioneConto.importoSaldo = importo;
            _stornoMovimentazioneConto.dataOraSaldo.data.anno = DateTime.Now.Year.ToString("0000");
            _stornoMovimentazioneConto.dataOraSaldo.data.mese = DateTime.Now.Month.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.data.giorno = DateTime.Now.Day.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.ora.ore = DateTime.Now.Hour.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.ora.minuti = DateTime.Now.Minute.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.ora.secondi = DateTime.Now.Second.ToString("00");
        }

        public void SetSaldo(int importo, DateTime data)
        {
            _stornoMovimentazioneConto.importoSaldo = importo;
            _stornoMovimentazioneConto.dataOraSaldo.data.anno = data.Year.ToString("0000");
            _stornoMovimentazioneConto.dataOraSaldo.data.mese = data.Month.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.data.giorno = data.Day.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.ora.ore = data.Hour.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.ora.minuti = data.Minute.ToString("00");
            _stornoMovimentazioneConto.dataOraSaldo.ora.secondi = data.Second.ToString("00");
        }
        
        public void SetBonus(List<MessaggioDettaglioBonus> bonus)
        {

            _stornoMovimentazioneConto.importoBonusSaldo = 0;
            if (bonus != null)
            {
                _stornoMovimentazioneConto.numDettagliBonusSaldo = Convert.ToSByte(bonus.Count);
                if (bonus.Count > 0)
                {
                    _stornoMovimentazioneConto.dettaglioBonusSaldo = new DettaglioBonus[bonus.Count];

                    int i = 0;
                    foreach (MessaggioDettaglioBonus b in bonus)
                    {
                        _stornoMovimentazioneConto.dettaglioBonusSaldo[i] = b.DettaglioBonus;
                        _stornoMovimentazioneConto.importoBonusSaldo += b.DettaglioBonus.importo;
                        i++;
                    }
                }
            }
        }

    }
}
