using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioMovimentoBonus : MessaggioPGAD
    {
        private readonly MovimentazioneBonusContoRequestElements _movimentoBonus;
        public MovimentazioneBonusContoRequestElements MovimentoBonus
        {
            get
            {
                if (_movimentoBonus.idTransazione == 0)
                {
                    _movimentoBonus.idTransazione = GetIdTransazione();
                }
                return _movimentoBonus;
            }
        }

        public MessaggioMovimentoBonus(int titolareSistema, string contoGioco,long idTransazione)
            : base(titolareSistema, idTransazione)
        {
            _movimentoBonus = new MovimentazioneBonusContoRequestElements
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

        public void SetMovimentoBonus(CausaleMovimentoBonus causale, List<MessaggioDettaglioBonus> bonusMovimentati)
        {
            _movimentoBonus.causaleMovimento = Convert.ToSByte(causale);
            _movimentoBonus.importoBonus = 0;
            if (bonusMovimentati != null && bonusMovimentati.Count > 0)
            {
                _movimentoBonus.numDettagliBonus = Convert.ToSByte(bonusMovimentati.Count);
                _movimentoBonus.dettaglioBonus = new DettaglioBonus[bonusMovimentati.Count];
                var i = 0;
                foreach (MessaggioDettaglioBonus b in bonusMovimentati)
                {
                    _movimentoBonus.importoBonus += b.DettaglioBonus.importo;
                    _movimentoBonus.dettaglioBonus[i] = b.DettaglioBonus;

                    i++;
                }
            }
        }

        public void SetSaldo(int importo)
        {
            _movimentoBonus.importoSaldo = importo;
            _movimentoBonus.dataOraSaldo.data.anno = DateTime.Now.Year.ToString("0000");
            _movimentoBonus.dataOraSaldo.data.mese = DateTime.Now.Month.ToString("00");
            _movimentoBonus.dataOraSaldo.data.giorno = DateTime.Now.Day.ToString("00");
            _movimentoBonus.dataOraSaldo.ora.ore = DateTime.Now.Hour.ToString("00");
            _movimentoBonus.dataOraSaldo.ora.minuti = DateTime.Now.Minute.ToString("00");
            _movimentoBonus.dataOraSaldo.ora.secondi = DateTime.Now.Second.ToString("00");

            //Calcolare i bonus attivi

        }

        public void SetSaldo(int importo, DateTime data)
        {
            _movimentoBonus.importoSaldo = importo;
            _movimentoBonus.dataOraSaldo.data.anno = data.Year.ToString("0000");
            _movimentoBonus.dataOraSaldo.data.mese = data.Month.ToString("00");
            _movimentoBonus.dataOraSaldo.data.giorno = data.Day.ToString("00");
            _movimentoBonus.dataOraSaldo.ora.ore = data.Hour.ToString("00");
            _movimentoBonus.dataOraSaldo.ora.minuti = data.Minute.ToString("00");
            _movimentoBonus.dataOraSaldo.ora.secondi = data.Second.ToString("00");

        }

        public void SetSaldoBonus(List<MessaggioDettaglioBonus> bonus)
        {
            _movimentoBonus.importoBonusSaldo = 0;
            if (bonus != null && bonus.Count > 0)
            {
                _movimentoBonus.numDettagliBonusSaldo = Convert.ToSByte(bonus.Count);
                _movimentoBonus.dettaglioBonusSaldo = new DettaglioBonus[bonus.Count];
                int i = 0;
                foreach (MessaggioDettaglioBonus b in bonus)
                {
                    _movimentoBonus.importoBonusSaldo += b.DettaglioBonus.importo;
                    _movimentoBonus.dettaglioBonusSaldo[i] = b.DettaglioBonus;
                    i++;
                }
            }
        }

    }
}
