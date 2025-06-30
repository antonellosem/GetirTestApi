using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGADInterface.PGAD;

namespace PGADInterface
{
    public class PgadInterface
    {
        public static PgadResponse AggiornaDocumento(string PartitaIva, int TitolareSistema, string CodiceContratto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();
            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.AggiornaDocumento(0, false, PartitaIva, TitolareSistema, true, CodiceContratto, DataRilascioDocumento, true, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, true);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse ApriConto(string PartitaIva, int TitolareSistema, string CodiceContratto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName, string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita, string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();

            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.ApriContoPersonaFisica(0, false, PartitaIva, TitolareSistema, true, CodiceContratto, CodiceFiscale, Cognome, Nome, Sesso, true, Email, UserName, Comune_Nazione_Nascita, ProvinciaNascita, DataNascita, true, IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza, DataRilascioDocumento, true, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, true);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse ApriContoSemplificato(string PartitaIva, int TitolareSistema, string CodiceContratto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita, string ProvinciaResidenza, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();

            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.ApriContoPersonaFisicaSemplificato(0, false, PartitaIva, TitolareSistema, true, CodiceContratto, CodiceFiscale, Cognome, Nome, Sesso, true, Comune_Nazione_Nascita, ProvinciaNascita, DataNascita, true, ProvinciaResidenza);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse ApriContoSemplificatoIntegrazione(string PartitaIva, int TitolareSistema, string CodiceContratto, string Email, string UserName, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();

            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.ApriContoPersonaFisicaSemplificatoIntegrazione(0, false, PartitaIva, TitolareSistema, true, CodiceContratto, Email, UserName, DataRilascioDocumento, true, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, true, IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse CambiaStato(string PartitaIva, string ContoDiGioco, int TitolareSistema, int CausaleCambioStato, int StatoConto, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();
            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;

            //APERTO = 1,
            //SOSPESO = 2,
            //CHIUSO = 3,
            //DORMIENTE=4
            //BLOCCATO = 5

            STATO_CONTO stato = STATO_CONTO.APERTO;

            switch (StatoConto)
            {
                case 1:
                    stato = STATO_CONTO.APERTO;
                    break;
                case 2:
                    stato = STATO_CONTO.SOSPESO;
                    break;
                case 3:
                    stato = STATO_CONTO.CHIUSO;
                    break;
                case 4:
                    stato = STATO_CONTO.DORMIENTE;
                    break;
                case 5:
                    stato = STATO_CONTO.BLOCCATO;
                    break;
            }

            //RICHIESTO_DA_AAMS = 1,
            //RICHIESTO_DAL_CONCESSIONARIO = 2,
            //RICHIESTO_DAL_TITOLARE_CONTO = 3,
            //RICHIESTO_DA_AUTORITA_GIUDIZIARIA = 4,
            //RICHIESTO_DA_AAMS_DECESSO = 5,
            //RICHIESTO_DAL_CONCESSIONARIO_PER_MANCATO_INVIO_DOC = 6,
            //RICHIESTO_DAL_CONCESSIONARIO_PER_FRODE = 7
            //RICHIESTO_DAL_TITOLARE_PER_AUTOESCLUSIONE = 8

            CAUSALE_CAMBIO_STATO_CONTO causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_CONCESSIONARIO;

            switch (CausaleCambioStato)
            {
                case 1:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DA_AAMS;
                    break;
                case 2:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_CONCESSIONARIO;
                    break;
                case 3:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_TITOLARE_CONTO;
                    break;
                case 4:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DA_AUTORITA_GIUDIZIARIA;
                    break;
                case 5:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DA_AAMS_DECESSO;
                    break;
                case 6:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_CONCESSIONARIO_PER_MANCATO_INVIO_DOC;
                    break;
                case 7:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_CONCESSIONARIO_PER_FRODE;
                    break;
                case 8:
                    causale = CAUSALE_CAMBIO_STATO_CONTO.RICHIESTO_DAL_TITOLARE_PER_AUTOESCLUSIONE;
                    break;
            }

            result = client.CambioStato(0, false, PartitaIva, TitolareSistema, true, ContoDiGioco, causale, true, stato, true);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse ContoGiocoDormiente(int Saldo, string PartitaIva, int TitolareSistema, string CodiceContratto, DateTime DataDormiente, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();

            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.ContoGiocoDormiente(Saldo, true, 0, false, PartitaIva, TitolareSistema, true, CodiceContratto, DataDormiente, true);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse ElencoContiGiocoDormienti(string PartitaIva, int TitolareSistema, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();

            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.ElencoContiGiocoDormienti(0, false, PartitaIva, TitolareSistema, true, 1, true, 100, true);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse ElencoContiGiocoSenzaSubRegistrazione(string PartitaIva, int TitolareSistema, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();

            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.ElencoContiGiocoSenzaSubregistrazione(0, false, DateTime.Now, true, PartitaIva, TitolareSistema, true, 0, false, 1, true, 100, true); ;

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse ModificaProvinciaResidenza(string PartitaIva, int TitolareSistema, string CodiceContratto, string ProvinciaResidenza, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();
            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.ModificaProvinciaResidenza(0, false, PartitaIva, TitolareSistema, true, CodiceContratto, ProvinciaResidenza);

            PgadResponse response = PgadInterface.ConvertResponse(result);


            return response;
        }

        public static PgadStatoContoResponse StatoConto(string PartitaIva, int TitolareSistema, string CodiceContratto, string IndirizzoServizio)
        {
            PgadGatewayStatoResponse result = new PgadGatewayStatoResponse();
            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.StatoConto(0, false, PartitaIva, TitolareSistema, true, CodiceContratto);

            PgadStatoContoResponse response = PgadInterface.ConvertStatoContoResponse(result);

            return response;
        }

        public static PgadResponse SubRegistrazione(string PartitaIva, int TitolareSistema, string CodiceContratto, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();
            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.SubRegistrazione(0, false, PartitaIva, TitolareSistema, true, CodiceContratto);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        public static PgadResponse SubRegistrazione2(int Saldo, int SaldoBonus, string PartitaIva, int TitolareSistema, string CodiceContratto, string IndirizzoServizio)
        {
            PgadGatewayResponse result = new PgadGatewayResponse();
            PGADService client = new PGADService();
            client.Url = IndirizzoServizio;
            result = client.SubRegistrazione2(Saldo, true, SaldoBonus, true, 0, false, PartitaIva, TitolareSistema, true, CodiceContratto);

            PgadResponse response = PgadInterface.ConvertResponse(result);

            return response;
        }

        private static PgadStatoContoResponse ConvertStatoContoResponse(PgadGatewayStatoResponse pgadGatewayRes)
        {
            PgadStatoContoResponse pgadres = new PgadStatoContoResponse();

            pgadres._descrizione = pgadGatewayRes._descrizione;
            pgadres._esito = pgadGatewayRes._esito;
            pgadres._idRicevuta = pgadGatewayRes._idRicevuta;
            pgadres._idTransazione = pgadGatewayRes._idTransazione;
            pgadres._success = pgadGatewayRes._success;
            pgadres._timestamp = pgadGatewayRes._timestamp;
            pgadres.Causale = pgadGatewayRes.Causale;
            pgadres.CodiceConto = pgadGatewayRes.CodiceConto;
            pgadres.IdCnConto = pgadGatewayRes.IdCnConto;
            pgadres.IdReteConto = pgadGatewayRes.IdReteConto;
            pgadres.Stato = pgadGatewayRes.Stato;

            return pgadres;
        }

        private static PgadResponse ConvertResponse(PgadGatewayResponse pgadGatewayRes)
        {
            PgadResponse pgadres = new PgadResponse();

            pgadres._descrizione = pgadGatewayRes._descrizione;
            pgadres._esito = pgadGatewayRes._esito;
            pgadres._idRicevuta = pgadGatewayRes._idRicevuta;
            pgadres._idTransazione = pgadGatewayRes._idTransazione;
            pgadres._success = pgadGatewayRes._success;
            pgadres._timestamp = pgadGatewayRes._timestamp;

            return pgadres;
        }
    }
}