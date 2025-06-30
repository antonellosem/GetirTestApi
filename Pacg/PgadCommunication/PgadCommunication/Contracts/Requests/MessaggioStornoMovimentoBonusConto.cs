using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PgadCommon.Enum;
using PgadCommunication.Pgad;

namespace PgadCommunication.Contracts.Requests
{
    [DataContract(Namespace = "PgadCommunication.Business")]
    public class MessaggioStornoMovimentoBonusConto : MessaggioPGAD
    {
        private readonly StornoMovimentazioneBonusContoRequestElements _stornoMovimentazioneBonusConto;
        public StornoMovimentazioneBonusContoRequestElements StornoMovimentazioneBonusConto
        {
            get
            {
                if (_stornoMovimentazioneBonusConto.idTransazione == 0)
                {
                    _stornoMovimentazioneBonusConto.idTransazione = this.GetIdTransazione();
                }
                return _stornoMovimentazioneBonusConto;
            }
        }
        public MessaggioStornoMovimentoBonusConto(int titolareSistema, string contoGioco, long idTransazione )
            : base(titolareSistema, idTransazione)
        {
            _stornoMovimentazioneBonusConto = new StornoMovimentazioneBonusContoRequestElements
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

        public void SetMovimentoBonus(CausaleMovimentoBonus causale, TipoStornoBonus tipoStornoBonus, string iDRicevutaBonus)
        {
            _stornoMovimentazioneBonusConto.causaleMovimento = Convert.ToSByte(causale);
            _stornoMovimentazioneBonusConto.tipoStornoBonus = Convert.ToSByte(tipoStornoBonus);
            _stornoMovimentazioneBonusConto.IDRicevutaBonus = iDRicevutaBonus;
        }

        public void SetSaldo(int importo)
        {
            _stornoMovimentazioneBonusConto.importoSaldo = importo;
            _stornoMovimentazioneBonusConto.dataOraSaldo.data.anno = DateTime.Now.Year.ToString("0000");
            _stornoMovimentazioneBonusConto.dataOraSaldo.data.mese = DateTime.Now.Month.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.data.giorno = DateTime.Now.Day.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.ora.ore = DateTime.Now.Hour.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.ora.minuti = DateTime.Now.Minute.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.ora.secondi = DateTime.Now.Second.ToString("00");
        }

        public void SetSaldo(int importo, DateTime data)
        {
            _stornoMovimentazioneBonusConto.importoSaldo = importo;
            _stornoMovimentazioneBonusConto.dataOraSaldo.data.anno = data.Year.ToString("0000");
            _stornoMovimentazioneBonusConto.dataOraSaldo.data.mese = data.Month.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.data.giorno = data.Day.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.ora.ore = data.Hour.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.ora.minuti = data.Minute.ToString("00");
            _stornoMovimentazioneBonusConto.dataOraSaldo.ora.secondi = data.Second.ToString("00");
        }
        
        public void SetBonus(List<MessaggioDettaglioBonus> bonus)
        {

            _stornoMovimentazioneBonusConto.importoBonus = 0;
            if (bonus != null)
            {
                _stornoMovimentazioneBonusConto.numDettagliBonus = Convert.ToSByte(bonus.Count);
                if (bonus.Count > 0)
                {
                    _stornoMovimentazioneBonusConto.dettaglioBonus = new DettaglioBonus[bonus.Count];

                    int i = 0;
                    foreach (MessaggioDettaglioBonus b in bonus)
                    {
                        _stornoMovimentazioneBonusConto.dettaglioBonus[i] = b.DettaglioBonus;
                        _stornoMovimentazioneBonusConto.importoBonus += b.DettaglioBonus.importo;
                        i++;
                    }
                }
            }
        }

        public void SetBonusSaldo(List<MessaggioDettaglioBonus> bonusSaldo)
        {

            _stornoMovimentazioneBonusConto.importoBonusSaldo = 0;
            if (bonusSaldo != null)
            {
                _stornoMovimentazioneBonusConto.numDettagliBonusSaldo = Convert.ToSByte(bonusSaldo.Count);
                if (bonusSaldo.Count > 0)
                {
                    _stornoMovimentazioneBonusConto.dettaglioBonusSaldo = new DettaglioBonus[bonusSaldo.Count];

                    int i = 0;
                    foreach (MessaggioDettaglioBonus b in bonusSaldo)
                    {
                        _stornoMovimentazioneBonusConto.dettaglioBonusSaldo[i] = b.DettaglioBonus;
                        _stornoMovimentazioneBonusConto.importoBonusSaldo += b.DettaglioBonus.importo;
                        i++;
                    }
                }
            }
        }



    }
}
