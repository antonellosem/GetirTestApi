using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PgadCommunication.Business;
using PgadCommunication;

namespace PgadTest
{
    [Serializable]
    public class Persona
    {
        public Persona()
        {
            SaldoUtente = new Saldo();
        }

        public Persona(string piva, int titolareSitema, string codiceFiscale, string conto, string nome, string cognome, char sesso, string username, string email,
                        string comuneNascita, string provinciaNascita, DateTime dataNascita,
                        string viaResidenza, string comuneResidenza, string provinciaResidenza, string capResidenza,
                        DateTime dataRilascioDocumento, string autoritaRilascioDocumento, string comuneDocumento, string numeroDocumento, int tipoDocumento)
        {
            Nome = nome;
            Cognome = cognome;
            Username = username;
            Email = email;
            Conto = conto;
            Sesso = sesso;
            TitolareSistema = titolareSitema;
            CodiceFiscale = codiceFiscale;

            ComuneNascita = comuneNascita;
            ProvinciaNascita = provinciaNascita;
            DataNascita = dataNascita;

            ViaResidenza = viaResidenza;
            ComuneResidenza = comuneResidenza;
            ProvinciaResidenza = provinciaResidenza;
            CapResidenza = capResidenza;


            DataRilascioDocumento = dataRilascioDocumento;
            AutoritaRilascioDocumento = autoritaRilascioDocumento;
            ComuneDocumento = comuneDocumento;
            NumeroDocumento = numeroDocumento;
            TipoDocumento = tipoDocumento;
            PIVAFS = piva;

            SaldoUtente = new Saldo();
        }


        public Saldo SaldoUtente { get; set; }

        //Data Anagrafici
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public char Sesso { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CodiceFiscale { get; set; }
        public string Conto { get; set; }
        public int TitolareSistema { get; set; }
        public string PIVAFS { get; set; }

        //Dati Nascita
        public string ComuneNascita;
        public string ProvinciaNascita;
        public DateTime DataNascita;

        //Dati Residenza
        public string ViaResidenza { get; set; }
        public string ComuneResidenza { get; set; }
        public string ProvinciaResidenza { get; set; }
        public string CapResidenza { get; set; }

        //Dati Documento
        public DateTime DataRilascioDocumento { get; set; }
        public string AutoritaRilascioDocumento { get; set; }
        public string ComuneDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int TipoDocumento { get; set; }

        MessaggioPersonaFisica ToMessaggioPersonaFisica
        {
            get
            {
                MessaggioPersonaFisica persona = new MessaggioPersonaFisica(this.TitolareSistema, this.Conto,0);
                persona.setAnagrafica(CodiceFiscale, Cognome, Nome, Sesso, Email, Username);
                persona.setDatiNascita(ComuneNascita, ProvinciaNascita, DataNascita);
                persona.setResidenza(ViaResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza);
                persona.setDocumento(DataRilascioDocumento, AutoritaRilascioDocumento, ComuneDocumento, NumeroDocumento, TipoDocumento);

                return persona;
            }
        }

        public PgadGatewayResponse ApriContoPersonaFisica()
        {
            IPgadGateway pgad = Factory.NewPgadInstance(PIVAFS, TitolareSistema);
            PgadGatewayResponse resp = pgad.apriContoPersonaFisica(this.ToMessaggioPersonaFisica);
            return resp;
        }

        public MessaggioSaldoConto ToMessaggioSaldoConto
        {
            get
            {
                MessaggioSaldoConto saldo = new MessaggioSaldoConto(this.TitolareSistema, this.Conto,0);
                saldo.setSaldo(this.SaldoUtente.ImportoTotale, this.SaldoUtente.DataSaldo);

                List<MessaggioDettaglioBonus> bonus = this.SaldoUtente.ToListMessaggioDettaglioBonus;
                /*
                List<MessaggioDettaglioBonus> bonus = new List<MessaggioDettaglioBonus>();
                foreach (Bonus b in this.SaldoUtene.DettaglioBonus)
                {
                    MessaggioDettaglioBonus mb = new MessaggioDettaglioBonus(b.Importo, b.Famiglia, b.Gioco);
                    bonus.Add(mb);
                }
                 */
                saldo.setSaldoBonus(bonus);
                return saldo;
            }
        }

        public PgadGatewayResponse Saldo()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( PIVAFS ,TitolareSistema);
            MessaggioSaldoConto saldo = ToMessaggioSaldoConto;
            PgadGatewayResponse resp = pgad.saldo(saldo);
            return resp;
        }


        public PgadGatewayResponse MovimentoBonus(int causale, int famiglia, int gioco, int importo)
        {
            CAUSALE_MOVIMENTO_BONUS cb = CAUSALE_MOVIMENTO_BONUS.BONUS;

            switch (causale)
            { 
                case 5:
                    cb = CAUSALE_MOVIMENTO_BONUS.BONUS;
                    this.SaldoUtente.addBonus(famiglia, gioco, importo);
                    break;
                case 6:
                    cb = CAUSALE_MOVIMENTO_BONUS.STORNO_BONUS;
                    this.SaldoUtente.delBonus(famiglia, gioco, importo);
                    break;
            }


            IPgadGateway pgad = Factory.NewPgadInstance(this.PIVAFS, this.TitolareSistema);
            MessaggioMovimentoBonus movimentoBonus = new MessaggioMovimentoBonus(this.TitolareSistema, this.Conto,0);

            //Bonus movimentati
            List<MessaggioDettaglioBonus> bonusMovimentati = new List<MessaggioDettaglioBonus>();
            MessaggioDettaglioBonus bM = new MessaggioDettaglioBonus(importo,famiglia, gioco);
            bonusMovimentati.Add(bM);
            movimentoBonus.setMovimentoBonus(cb, bonusMovimentati);

            List<MessaggioDettaglioBonus> bonusAttivi =this.SaldoUtente.ToListMessaggioDettaglioBonus;
            movimentoBonus.setSaldoBonus(bonusAttivi);
            movimentoBonus.setSaldo(this.SaldoUtente.ImportoTotale);

            PgadGatewayResponse resp = pgad.movimentazioneBonus(movimentoBonus);
            return resp;
        }

        public PgadGatewayResponse MovimentoConto(int causale, int mezzo, int importo)
        {
            //((Dato)causaleBox.SelectedItem).Id
            CAUSALE_MOVIMENTO cm = CAUSALE_MOVIMENTO.RICARICA;
            switch (causale)
            {
                case 1://Ricarica
                    cm=CAUSALE_MOVIMENTO.RICARICA;
                    this.SaldoUtente.AddImporto(importo);
                    break;
                case 2://Storno Ricarica
                    this.SaldoUtente.DelImporto(importo);
                    cm=CAUSALE_MOVIMENTO.STORNO_RICARICA;
                    break;
                case 3://Prelievo
                    this.SaldoUtente.DelImporto(importo);
                    cm=CAUSALE_MOVIMENTO.PRELIEVO;
                    break;
                case 4://Storno Prelievo
                    cm=CAUSALE_MOVIMENTO.STORNO_PRELIEVO;
                    this.SaldoUtente.AddImporto(importo);
                    break;
                case 7://Costi Servizi Aggiuntivi
                    cm=CAUSALE_MOVIMENTO.COSTI_SERVIZI_AGGIUNTIVI;
                    this.SaldoUtente.DelImporto(importo);
                    break;
                case 8://Costi Servizi Aggiuntivi
                    cm = CAUSALE_MOVIMENTO.STORNO_COSTI_SERVIZI_AGGIUNTIVI;
                    this.SaldoUtente.DelImporto(importo);
                    break;
            }

            MEZZO_PAGAMENTO mp = MEZZO_PAGAMENTO.CARTA_CREDITO;
            switch(mezzo)
            {
                    /*
                case 1: //Carta di credito
                    mp = MEZZO_PAGAMENTO.CONTANTI;
                    break;
                    */
                case 2: //Carta di credito
                    mp = MEZZO_PAGAMENTO.CARTA_CREDITO;
                    break;
                case 3: //Carta Prepagata
                    mp =MEZZO_PAGAMENTO.CARTA_PREPAGATA;
                    break;
                case 4: //Bonifico Bancario Postale
                    mp =MEZZO_PAGAMENTO.BONIFICO_BANCARIO_POSTALE;
                    break;
                case 5: //Bollettino Postale
                    mp =MEZZO_PAGAMENTO.BOLLETTINO_POSTALE;
                    break;
                case 6: //Assegno conto corrente
                    mp =MEZZO_PAGAMENTO.ASSEGNO_CONTO_CORRENTE;
                    break;
                case 7: //Assegno circolare
                    mp =MEZZO_PAGAMENTO.ASSEGNO_CIRCOLARE;
                    break;
                case 8: //Vaglia Postale
                    mp =MEZZO_PAGAMENTO.VAGLIA_POSTALE;
                    break;
                case 9: //Ricarica scratch
                    mp =MEZZO_PAGAMENTO.RICARICA_SCRATCH;
                    break;
               /*
                case 10: //Altro
                    mp =MEZZO_PAGAMENTO.ALTRO;
                    break;
                */
                case 11: //IMEL
                    mp = MEZZO_PAGAMENTO.IMEL;
                    break;
                case 12: //Altro
                    mp = MEZZO_PAGAMENTO.CONTO_DI_GIOCO;
                    break;
                case 13: //Altro
                    mp = MEZZO_PAGAMENTO.CONVERSIONE_BONUS;
                    break;
                case 14://
                    mp = MEZZO_PAGAMENTO.E_WALLET;
                    break;
            }

            IPgadGateway pgad = Factory.NewPgadInstance(this.PIVAFS, this.TitolareSistema);
            MessaggioMovimentoConto movimento = new MessaggioMovimentoConto(this.TitolareSistema, this.Conto,0);
            movimento.setMovimento(cm, mp, importo);
            movimento.setSaldo(this.SaldoUtente.ImportoTotale );

            List<MessaggioDettaglioBonus> bonus = this.SaldoUtente.ToListMessaggioDettaglioBonus;
            movimento.setBonus(bonus);
            PgadGatewayResponse resp = pgad.movimento(movimento);
            return resp;
        }

    }

    [Serializable]
    public class PersonaFisica : Persona
    {
        public string PIVA { get; set; }

        public MessaggioPersonaGiuridica ToMessaggioPersonaGiuridica
        {
            get
            {
                MessaggioPersonaGiuridica societa = new MessaggioPersonaGiuridica(this.TitolareSistema, this.Conto,0);
                societa.setAnagrafica(this.PIVA, this.Nome, this.Email, this.Username);
                societa.setSede(this.ViaResidenza, this.ComuneResidenza, this.ProvinciaResidenza, this.CapResidenza);
                return societa;
            }
        }

        public PgadGatewayResponse ApriContoPersonaGiuridica()
        {
            IPgadGateway pgad = Factory.NewPgadInstance(PIVA, this.TitolareSistema);
            PgadGatewayResponse resp = pgad.apriContoPersonaGiuridica(this.ToMessaggioPersonaGiuridica);
            return resp;
        }
    }


    [Serializable]
    public class Saldo
    {
        public Saldo()
        {
            DettaglioBonus = new List<Bonus>();
            DataSaldo = DateTime.Now;
        }


        public DateTime DataSaldo { get; set; }
        public List<Bonus> DettaglioBonus { get; set; }
        public int Importo { get; set; }

        public int ImportoTotale
        {
            get
            {
                return Importo + ImportoSaldoBonus;
            }
        }

        public int ImportoSaldoBonus
        {
            get
            {
                int bonus = 0;
                foreach (Bonus b in DettaglioBonus)
                {
                    bonus += b.Importo;
                }
                return bonus;
            }
        }


        public void AddImporto(int importo)
        {
            this.Importo += importo;
        }

        public void DelImporto(int importo)
        {
            this.Importo -= importo;
        }

        public void addBonus(int famiglia, int gioco, int importo)
        {
            Bonus fBonus = DettaglioBonus.Find(delegate(Bonus t) { return (t.Gioco == gioco && t.Famiglia == famiglia); });
            if (fBonus != null)
                fBonus.Importo += importo;
            else
            {
                Bonus b = new Bonus(famiglia, gioco, importo);
                DettaglioBonus.Add(b);
            }
        }

        public void delBonus(int famiglia, int gioco, int importo)
        {
            Bonus fBonus = DettaglioBonus.Find(delegate(Bonus t) { return (t.Gioco == gioco && t.Famiglia == famiglia); });
            if (fBonus != null)
            {
                if (fBonus.Importo >= importo)
                    fBonus.Importo -= importo;

                if (fBonus.Importo == 0)
                {
                    DettaglioBonus.Remove(fBonus);
                }
            }
        }


        public List<MessaggioDettaglioBonus> ToListMessaggioDettaglioBonus
        {
            get
            {
                List<MessaggioDettaglioBonus> bonus = new List<MessaggioDettaglioBonus>();
                foreach (Bonus b in this.DettaglioBonus)
                {
                    MessaggioDettaglioBonus mb = new MessaggioDettaglioBonus(b.Importo, b.Famiglia, b.Gioco);
                    bonus.Add(mb);
                }
                return bonus;
            }
        }
    }



    [Serializable]
    public class Bonus
    {
        public Bonus()
        {
        }

        public Bonus(int famiglia, int gioco, int importo)
        {
            Famiglia = famiglia;
            Gioco = gioco;
            Importo = importo;
        }

        public int Famiglia { get; set; }
        public int Gioco { get; set; }
        public int Importo { get; set; }
    }

}
