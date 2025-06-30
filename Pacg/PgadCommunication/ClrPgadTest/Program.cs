using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClrPgad;
using System.Data;
using ClrPgad.PgadCommunication;

namespace ClrPgadTest
{
    class Program
    {

        static DataSet GetBonusDT()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Importo", typeof(int));
            dt.Columns.Add("CodiceGioco", typeof(int));
            dt.Columns.Add("CodiceFamiglia", typeof(int));
            ds.Tables.Add(dt);
            return ds;
        }

        static void AddBonus(DataSet dt,int famiglia,int gioco,int importo)
        {
            DataRow row = dt.Tables[0].NewRow();
            row["Importo"]=importo;
            row["CodiceGioco"] = gioco;
            row["CodiceFamiglia"]=famiglia;
            dt.Tables[0].Rows.Add(row);
        }

        static void Main(string[] args)
        {
            /* Casuali Movimento*/
            /*
             * 1  Ricarica
             * 2  Storno Ricarica
             * 3  Prelievo
             * 4  Storno Prelievo
             * 5  Bonus
             * 6  Storno Bonus
             * 7  Costi Servizi Aggiuntivi
             * 8  Storno Costi Servizi Aggiuntivi
             * 
             /*Mezzi Di Pagamento*/
             /*
              * 2 Carta di Credito
              * 3 Carta Prepagata
              * 4 Bonigico Bancario/Postale
              * 5 Bollettino Bancario/Postale
              * 6 Assegno di Conto Corrente
              * 7 Assegno Circolare
              * 8 Vaglia Postale
              * 9 Ricarica Scratch
              * 11 IMEL
              * 12 Conto di gioco 
              * 13 Conversione da Bonus
              */
            DataSet saldoBonus = GetBonusDT();
            DataSet movBonus = GetBonusDT();
            AddBonus(movBonus, 6, 0, 100);
            AddBonus(saldoBonus, 6, 0, 100);
            int saldo = 100;
            long idTransazione = 23;
            string endpoint = "http://localhost:55984/PGADService.svc";


            clr_Pgad pgad = new clr_Pgad(endpoint);
            List<Bonus> lsBonus = new List<Bonus>();
            lsBonus.Add(new Bonus() {Importo=100,TipoGioco=0,CodiceFamiglia=6});
            List<Bonus> lmBonus = new List<Bonus>();
            lmBonus.Add(new Bonus() { Importo = 100, TipoGioco = 0, CodiceFamiglia = 6 });

            //string respApri = clr_Pgad.ApriConto(idTransazione, "09255551005", 15115, "15115-0106391", "RVRLCU25B65H501S", "RAVARA", "LUCIA", 'F', "testone002@intralot.it", "testone002", "ROMA", "RM", DateTime.Parse("1925-02-25"), "Roma", "ROMA", "RM", "80041", DateTime.Parse("2013-01-01"), "Comune", "ROMA", "AR00000", 1, endpoint);
//            string respSub = clr_Pgad.SubRegistrazione(idTransazione, "09255551005", 15115, "15115-0106391",endpoint);
            
            //string respRMov = clr_Pgad.RiepilogoOperazioniMovimentazione(idTransazione,"09255551005", 15115, Convert.ToDateTime("2012-07-29"), endpoint);
            //string  respOp = clr_Pgad.RiepilogoOperazioniServizio(idTransazione,"09255551005", 15115, Convert.ToDateTime("2012-07-29"), endpoint);
            //string respInsertBonus = clr_Pgad.InsertBonus(idTransazione,"09255551005", "15115-0106391", 15115, movBonus, saldoBonus, saldo, Convert.ToDateTime("2013-05-15 12:39:19"), endpoint);
            //string respStornoBonus = clr_Pgad.StornoBonus(idTransazione,"09255551005", "4098-0105354", 15115, movBonus, saldoBonus, saldo, Convert.ToDateTime(DateTime.Now.ToUniversalTime()), endpoint);
            //string respMov = clr_Pgad.Movimentazione(idTransazione, "09255551005", 1, 13, "4098-0105354", 15115, 100, saldoBonus, saldo+100, Convert.ToDateTime("2013-05-15 12:39:19"), endpoint);

            //string respMov2 = clr_Pgad.Movimentazione(idTransazione, "09255551005", 1, 11, "4098-0105354", 15115, 100, saldoBonus, saldo+100, Convert.ToDateTime("2013-05-15 12:39:19"), endpoint);
            //string respStornoMov = clr_Pgad.StornoMovimentazione(idTransazione, "09255551005", 2, 13, 2, "dc07dd051d02000000000000a", "4098-0105354", 15115, 100, saldoBonus, saldo, Convert.ToDateTime("2013-05-15 13:43:43"), endpoint);
            //string respStornoB = clr_Pgad.StornoBonus("09255551005", "4098-0105354", 15115, lmBonus, lsBonus, saldo, Convert.ToDateTime("2013-05-15 12:39:19"), endpoint);
            //string respConto = clr_Pgad.SaldoContoUtente("09255551005", "4098-0105354", 15115, lsBonus, saldo, Convert.ToDateTime("2013-05-15 12:39:19"), endpoint);
            //string respStatoConto = clr_Pgad.StatoConto("09255551005", 15115, "4098-0105354", endpoint);
            //string respModRes= clr_Pgad.ModificaProvinciaResidenza(130130998266897651,"09255551005", 15115, "4098-0105354", "RM", endpoint);
            //string respModDoc =  clr_Pgad.AggiornaDocumento(130130999464497399,"09255551005", 15115, "4098-0105354", Convert.ToDateTime("2013-05-15 13:58:00"), "Comune", "Roma", "AO123456", 1, endpoint);
            //string respCambiaStato = clr_Pgad.CambiaStato("09255551005", "4098-0105354", 15115, 2, 1, endpoint);

            //ResponsePgad respObMov = pgad.Movimentazione("09255551005", 1, 13, "4098-0105354", 15115, 100, saldoBonus, saldo, Convert.ToDateTime("2013-05-15 12:39:19"));

            //ResponsePgad respObjStornoMov = pgad.StornoMovimentazione(idTransazione, "09255551005", 2, 13, 0, "dc07dd050f090000000000016", "4098-0105354", 15115, 100, saldoBonus, saldo, Convert.ToDateTime("2013-05-15 13:43:43"));
            //ResponsePgad respObjLStornoMov = pgad.StornoMovimentazione(idTransazione, "09255551005", 2, 13, 0, "dc07dd050f090000000000016", "4098-0105354", 15115, 100, lsBonus, saldo, Convert.ToDateTime("2013-05-15 13:43:43"));
            //ResponsePgad respBonus = pgad.InsertBonus(idTransazione, "09255551005", "4098-0105354", 15115, movBonus, saldoBonus, saldo, Convert.ToDateTime("2013-05-15 12:40:00"));
            //ResponsePgad respLBonus = pgad.InsertBonus(idTransazione, "09255551005", "4098-0105354", 15115, lmBonus, lsBonus, saldo, Convert.ToDateTime("2013-05-15 12:40:00"));
            //ResponsePgad respStornoBonus = pgad.StornoBonus(idTransazione, "09255551005", "4098-0105354", 15115, movBonus, saldoBonus, 900, Convert.ToDateTime("2013-05-15 12:40:00"));
            //ResponsePgad respStornoLBonus = pgad.StornoBonus(idTransazione,"09255551005", "4098-0105354", 15115, movBonus, saldoBonus, 900, Convert.ToDateTime("2013-05-15 12:40:00"));
        }
    }
}
