using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioMovimentoConto : MessaggioPGAD
    {
        private readonly MovimentazioneContoRequestElements _movimentazioneConto;
        public MovimentazioneContoRequestElements MovimentazioneConto
        {
            get
            {
                if (_movimentazioneConto.idTransazione == 0)
                {
                    _movimentazioneConto.idTransazione = this.GetIdTransazione();
                }
                return _movimentazioneConto;
            }
        }

        public MessaggioMovimentoConto(int titolareSistema, string contoGioco,long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _movimentazioneConto = new MovimentazioneContoRequestElements
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

        public void SetMovimento(CausaleMovimento causale,MezzoPagamento mezzoPagamento,int importo)
        {
            _movimentazioneConto.causaleMovimento = Convert.ToSByte(causale);
            _movimentazioneConto.mezzoDiPagamento = Convert.ToSByte(mezzoPagamento);
            _movimentazioneConto.importoMovimento = importo;
        }

        public void SetSaldo(int importo)
        {
            _movimentazioneConto.importoSaldo = importo;
            _movimentazioneConto.dataOraSaldo.data.anno = DateTime.Now.Year.ToString("0000");
            _movimentazioneConto.dataOraSaldo.data.mese = DateTime.Now.Month.ToString("00");
            _movimentazioneConto.dataOraSaldo.data.giorno = DateTime.Now.Day.ToString("00");
            _movimentazioneConto.dataOraSaldo.ora.ore = DateTime.Now.Hour.ToString("00");
            _movimentazioneConto.dataOraSaldo.ora.minuti = DateTime.Now.Minute.ToString("00");
            _movimentazioneConto.dataOraSaldo.ora.secondi = DateTime.Now.Second.ToString("00");
        }

        public void SetSaldo(int importo, DateTime data)
        {
            _movimentazioneConto.importoSaldo = importo;
            _movimentazioneConto.dataOraSaldo.data.anno = data.Year.ToString("0000");
            _movimentazioneConto.dataOraSaldo.data.mese = data.Month.ToString("00");
            _movimentazioneConto.dataOraSaldo.data.giorno = data.Day.ToString("00");
            _movimentazioneConto.dataOraSaldo.ora.ore = data.Hour.ToString("00");
            _movimentazioneConto.dataOraSaldo.ora.minuti = data.Minute.ToString("00");
            _movimentazioneConto.dataOraSaldo.ora.secondi = data.Second.ToString("00");
        }
        
        public void SetBonus(List<MessaggioDettaglioBonus> bonus)
        {

            _movimentazioneConto.importoBonusSaldo = 0;
            if (bonus != null)
            {
                _movimentazioneConto.numDettagliBonusSaldo = Convert.ToSByte(bonus.Count);
                if (bonus.Count > 0)
                {
                    _movimentazioneConto.dettaglioBonusSaldo = new DettaglioBonus[bonus.Count];

                    int i = 0;
                    foreach (MessaggioDettaglioBonus b in bonus)
                    {
                        _movimentazioneConto.dettaglioBonusSaldo[i] = b.DettaglioBonus;
                        _movimentazioneConto.importoBonusSaldo += b.DettaglioBonus.importo;
                        i++;
                    }
                }
            }
        }


    }
}
