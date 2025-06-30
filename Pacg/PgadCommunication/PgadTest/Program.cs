using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PgadCommunication.Business;
using PgadCommunication;
using System.Windows.Forms;


namespace PgadTest
{
    class Program
    {
        public static void PrintResponse(string method, PgadGatewayResponse resp)
        {
            Console.WriteLine("{0}: {1}", method, resp.ToString());
            Console.ReadLine();
        }

        public static void ApriContoPersonaGiuridica()
        {
            //PgadGateway pgad = new PgadGateway("09255551005");
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioPersonaGiuridica societa = new MessaggioPersonaGiuridica(4098, "4098-T100001",0);
            societa.setAnagrafica("09255551005", "Intralot Italia s.p.a.", "sistemi@intralot.it", "cncintralot");
            societa.setSede("Via Tiburtina n. 1155", "Roma", "RM", "00100");
            PgadGatewayResponse resp = pgad.apriContoPersonaGiuridica(societa);
            PrintResponse("ApriContoPersonaGiuridica", resp);
        }

        public static void ApriContoPersonaFisica()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioPersonaFisica persona = new MessaggioPersonaFisica(4098, "4098-T000006",0);
            //MessaggioPersonaFisica persona = new MessaggioPersonaFisica(4098, "4098-T000004");
            //persona.setAnagrafica("DCSMRA70A01H501B", "DI CESARE", "MARIO", 'M', "MARIODICESARE@intralot.it", "ROSSI@GIANCARLO");

            persona.setAnagrafica("MRCGST65B02H501S", "MARCOLINI", "AUGUSTO", 'M', "MARCOLINIAUGUSTO@intralot.it", "MARCOLINI@AUGUSTO");
            persona.setDatiNascita("ROMA", "RM", DateTime.Parse("1965/02/02"));
            persona.setDocumento(DateTime.Parse("2010/01/01"), "SINDACO", "ROMA", "00000000", 1);
            persona.setResidenza("Via Tiburtina N. 1155", "ROMA", "RM", "00100");

            PgadGatewayResponse resp = pgad.apriContoPersonaFisica(persona);
            PrintResponse("ApriContoPersonaFisica", resp);
        }

        public static void SubRegistrazione()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioSubRegistrazione subregistrazione = new MessaggioSubRegistrazione(4098, "4098-T000003",0);
            PgadGatewayResponse resp = pgad.subRegistrazione(subregistrazione);
            PrintResponse("SubRegistrazione", resp);
        }

        public static void StatoConto()
        {
            IPgadGateway pgad = Factory.NewPgadInstance("09255551005",4098 );
            MessaggioStatoConto stato = new MessaggioStatoConto(4098, "4098-T000003",0);
            PgadGatewayResponse resp = pgad.stato(stato);
            PrintResponse("StatoConto", resp);
        }


        public static void CambioStato()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioCambioStatoConto cambioStato = new MessaggioCambioStatoConto(4098, "4098-T000003",0);
            cambioStato.setStato(CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_TITOLARE_CONTO, STATO_CONTO.APERTO);
            PgadGatewayResponse resp = pgad.cambioStato(cambioStato);
            PrintResponse("CambioStato", resp);
        }

        public static void ModificaProvinciaResidenza()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioModificaProvinciaResidenza modificaProvincia = new MessaggioModificaProvinciaResidenza(4098, "4098-T000003",0);
            modificaProvincia.setProvincia("RM");
            PgadGatewayResponse resp = pgad.modificaResidenza(modificaProvincia);
            PrintResponse("ModificaProvinciaResidenza", resp);
        }


        public static void Saldo()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioSaldoConto saldo = new MessaggioSaldoConto(4098, "4098-T000003",0);
            saldo.setSaldo(5000, DateTime.Now);

            List<MessaggioDettaglioBonus> bonus = new List<MessaggioDettaglioBonus>();
            MessaggioDettaglioBonus b = new MessaggioDettaglioBonus(100, 0, 0);
            bonus.Add(b);
            saldo.setSaldoBonus(bonus);


            PgadGatewayResponse resp = pgad.saldo(saldo);
            PrintResponse("Saldo", resp);
        }

        public static void MovimentoBonus()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioMovimentoBonus movimentoBonus = new MessaggioMovimentoBonus(4098, "4098-T000003",0);

            //Bonus movimentati
            List<MessaggioDettaglioBonus> bonusMovimentati = new List<MessaggioDettaglioBonus>();
            MessaggioDettaglioBonus bM = new MessaggioDettaglioBonus(50, 0, 0);
            bonusMovimentati.Add(bM);
            movimentoBonus.setMovimentoBonus(CAUSALE_MOVIMENTO_BONUS.BONUS, bonusMovimentati);

            //Bonus attivi
            List<MessaggioDettaglioBonus> bonusAttivi = new List<MessaggioDettaglioBonus>();
            MessaggioDettaglioBonus bA = new MessaggioDettaglioBonus(50, 0, 0);
            bonusAttivi.Add(bA);
            movimentoBonus.setSaldoBonus(bonusAttivi);
            movimentoBonus.setSaldo(50);

            PgadGatewayResponse resp = pgad.movimentazioneBonus(movimentoBonus);
            PrintResponse("MovimentoBonus", resp);
        }


        public static void stornoBonus()
        {
            IPgadGateway pgad = Factory.NewPgadInstance( "09255551005",4098 );
            MessaggioMovimentoBonus movimentoBonus = new MessaggioMovimentoBonus(4098, "4098-T000003",0);

            //Bonus movimentati
            List<MessaggioDettaglioBonus> bonusMovimentati = new List<MessaggioDettaglioBonus>();
            MessaggioDettaglioBonus bM = new MessaggioDettaglioBonus(50, 0, 0);
            bonusMovimentati.Add(bM);
            movimentoBonus.setMovimentoBonus(CAUSALE_MOVIMENTO_BONUS.STORNO_BONUS, bonusMovimentati);

            //Bonus attivi
            List<MessaggioDettaglioBonus> bonusAttivi = new List<MessaggioDettaglioBonus>();
            MessaggioDettaglioBonus bA = new MessaggioDettaglioBonus(50, 0, 0);
            bonusAttivi.Add(bA);
            movimentoBonus.setSaldoBonus(bonusAttivi);
            movimentoBonus.setSaldo(50);

            PgadGatewayResponse resp = pgad.movimentazioneBonus(movimentoBonus);
            PrintResponse("MovimentoBonus", resp);
        }


        public static void MovimentazioneConto()
        {
            IPgadGateway pgad = Factory.NewPgadInstance("09255551005",4098 );
            MessaggioMovimentoConto movimento = new MessaggioMovimentoConto(4098, "4098-T000003",0);

            movimento.setMovimento(CAUSALE_MOVIMENTO.PRELIEVO, MEZZO_PAGAMENTO.CARTA_CREDITO, 100);
            movimento.setSaldo(4900);

            //Bonus attivi
            List<MessaggioDettaglioBonus> bonus = new List<MessaggioDettaglioBonus>();
            MessaggioDettaglioBonus b = new MessaggioDettaglioBonus(50, 0, 0);
            bonus.Add(b);
            movimento.setBonus(bonus);

            PgadGatewayResponse resp = pgad.movimento(movimento);
            PrintResponse("MovimentazioneConto", resp);
        }

        public static void AddConto(string piva, int titolareSitema, string codiceFiscale, string conto, string nome, string cognome, char sesso, string username, string email,
                string comuneNascita, string provinciaNascita, DateTime dataNascita,
                string viaResidenza, string comuneResidenza, string provinciaResidenza, string capResidenza,
                DateTime dataRilascioDocumento, string autoritaRilascioDocumento, string comuneDocumento, string numeroDocumento, int tipoDocumento)
        {
            Persona p = new Persona(piva, titolareSitema, codiceFiscale, conto, nome, cognome, sesso, username, email,
                         comuneNascita, provinciaNascita, dataNascita,
                         viaResidenza, comuneResidenza, provinciaResidenza, capResidenza,
                         dataRilascioDocumento, autoritaRilascioDocumento, comuneDocumento, numeroDocumento, tipoDocumento);
            PgadGatewayResponse res = p.ApriContoPersonaFisica();
            PrintResponse("ApriContoPersonaFisica", res);
        }


        public static void Main(string[] argc)
        {
            
            PGADForm pgdaForm = new PGADForm();

            Application.Run(pgdaForm);
            


            /*
            MessaggioPersonaFisica persona = new MessaggioPersonaFisica(4098, "4098-T000006");
            //MessaggioPersonaFisica persona = new MessaggioPersonaFisica(4098, "4098-T000004");
            //persona.setAnagrafica("DCSMRA70A01H501B", "DI CESARE", "MARIO", 'M', "MARIODICESARE@intralot.it", "ROSSI@GIANCARLO");

            persona.setAnagrafica("MRCGST65B02H501S", "MARCOLINI", "AUGUSTO", 'M', "MARCOLINIAUGUSTO@intralot.it", "MARCOLINI@AUGUSTO");
            persona.setDatiNascita("ROMA", "RM", DateTime.Parse("1965/02/02"));
            persona.setDocumento(DateTime.Parse("2010/01/01"), "SINDACO", "ROMA", "00000000", 1);
            persona.setResidenza("Via Tiburtina N. 1155", "ROMA", "RM", "00100");
            */
            /*
            AddConto("09255551005", 4098, "MRCGST65B02H501S", "4098-T00009", "AUGUSTO", "MARCOLINI", 'M', "MARCOLINI@AUGUSTO","MARCOLINIAUGUSTO@intralot.it"
                , "ROMA", "RM", DateTime.Parse("1965/02/02"), "Via Tiburnina N. 1155", "ROMA", "RM", "00100", DateTime.Parse("2010/01/01"), "SINDACO", "ROMA", "00000000", 1);
            */
            
            
            //ApriContoPersonaFisica();
            /*
            SubRegistrazione();
            CambioStato();
            
            StatoConto();
            ApriContoPersonaGiuridica();
            ModificaProvinciaResidenza();
            Saldo();
            MovimentoBonus();
            stornoBonus();
            MovimentazioneConto();
            */
        }


        
    }
}
