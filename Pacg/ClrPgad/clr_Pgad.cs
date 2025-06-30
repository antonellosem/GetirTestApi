using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClrPgad.PgadCommunication;
using System.Data;
using System.ServiceModel;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace ClrPgad
{
    public class clr_Pgad
    {

        public clr_Pgad()
        {
            Initialize("http://backend.intralot.it/PgadService/PGADService.svc");
        }

        public clr_Pgad(string IndirizzoServizio)
        {
            Initialize(IndirizzoServizio);
        }

        internal EndpointAddress ea { get; set; }
        internal BasicHttpBinding binding { get; set; }
        //Default timeout 5
        private static int Timeout = 5;

        public ResponsePgad InsertBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.MovimentazioneBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp.IdRicevuta = response._idRicevuta;
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            //  PrintResponse("MovimentoBonus", resp);
            return _resp;
        }

        public ResponsePgad InsertBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.MovimentazioneBonus_V2(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare.ToArray<Bonus>(), ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp.IdRicevuta = response._idRicevuta;
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            //  PrintResponse("MovimentoBonus", resp);
            return _resp;
        }

        public ResponsePgad StornoBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.StornoBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp.IdRicevuta = response._idRicevuta;
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            //  PrintResponse("MovimentoBonus", resp);
            return _resp;
        }

        public ResponsePgad StornoBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.StornoBonus_V2(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare.ToArray<Bonus>(), ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp.IdRicevuta = response._idRicevuta;
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            //  PrintResponse("MovimentoBonus", resp);
            return _resp;
        }

        public ResponsePgad Movimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.MovimentazioneCredito(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);

                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.IdRicevuta = response._idRicevuta;
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            return _resp;
        }

        public ResponsePgad Movimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.MovimentazioneCredito_V2(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);

                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.IdRicevuta = response._idRicevuta;
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            return _resp;
        }

        public ResponsePgad StornoMovimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, int TipoStorno, string IdRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.StornoMovimentazioneCredito(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, (TIPO_STORNO)TipoStorno, IdRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.IdRicevuta = response._idRicevuta;
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            return _resp;
        }

        public ResponsePgad StornoMovimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, int TipoStorno, string IdRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.StornoMovimentazioneCredito_V2(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, (TIPO_STORNO)TipoStorno, IdRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.IdRicevuta = response._idRicevuta;
                _resp.TimeStamp = response._timestamp;
            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            return _resp;
        }

        public ResponsePgad SaldoContoUtente_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.SaldoContoUtente(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.IdRicevuta = response._idRicevuta;
                _resp.TimeStamp = response._timestamp;

            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            return _resp;
        }

        public ResponsePgad SaldoContoUtente_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            PgadClient _pgad = new PgadClient(this.binding, this.ea);
            PgadGatewayResponse response = new PgadGatewayResponse();
            ResponsePgad _resp = new ResponsePgad();
            try
            {
                response = _pgad.SaldoContoUtente_V2(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp.Esito = response._esito;
                _resp.Success = response._success;
                _resp.Descrizione = response._descrizione;
                _resp.IdTransazione = response._idTransazione.ToString();
                _resp.IdRicevuta = response._idRicevuta;
                _resp.TimeStamp = response._timestamp;

            }
            catch (Exception ex)
            {

                _resp.Esito = -1;
                _resp.Success = false;
                _resp.Descrizione = ex.Message;

            }
            return _resp;
        }

        public static string InsertBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.MovimentazioneBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string InsertBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.MovimentazioneBonus_V2(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare.ToArray<Bonus>(), ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string StornoBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.StornoBonus(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string StornoBonus_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.StornoBonus_V2(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare.ToArray<Bonus>(), ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string Movimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.MovimentazioneCredito(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);

            }
            return _resp;
        }

        public static string Movimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.MovimentazioneCredito_V2(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);

            }
            return _resp;
        }

        public static string StornoMovimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, int TipoStorno, string IdRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.StornoMovimentazioneCredito(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, (TIPO_STORNO)TipoStorno, IdRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string StornoMovimentazione_v2(long IdTransazione, string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, int TipoStorno, string IdRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.StornoMovimentazioneCredito_V2(IdTransazione, PartitaIva, (CAUSALE_MOVIMENTO)CausaleDiMovimento, (MEZZO_PAGAMENTO)MezzoDiPagamento, (TIPO_STORNO)TipoStorno, IdRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string SaldoContoUtente_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {

            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.SaldoContoUtente(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti, SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);

            }
            return _resp;
        }

        public static string SaldoContoUtente_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.SaldoContoUtente_V2(IdTransazione, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti.ToArray<Bonus>(), SaldoUtente, DataSaldo);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);

            }
            return _resp;
        }

        public static string CambiaStato_v2(long IdTransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, int CausaleCambioStato, int StatoConto, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.CambioStato(IdTransazione, PartitaIva, TitolareSistema, ContoDiGioco, (CAUSALE_CAMBIO_STATO_CONTO)CausaleCambioStato, (STATO_CONTO)StatoConto);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string ApriConto_v2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName, string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita, string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.ApriContoPersonaFisica(IdTransazione, PartitaIva, CodiceAgenzia, CodiceConto, CodiceFiscale, Cognome, Nome, Sesso, Email, UserName, Comune_Nazione_Nascita, ProvinciaNascita, DataNascita, IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza, DataRilascioDocumento, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string ModificaProvinciaResidenza_v2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, string ProvinciaResidenza, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.ModificaProvinciaResidenza(IdTransazione, PartitaIva, CodiceAgenzia, CodiceContratto, ProvinciaResidenza);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string StatoConto_v2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayStatoResponse response = new PgadGatewayStatoResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.StatoConto(IdTransazione, PartitaIva, CodiceAgenzia, CodiceContratto);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string SubRegistrazione_v2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;

            try
            {
                response = _pgad.SubRegistrazione(IdTransazione, PartitaIva, CodiceAgenzia, CodiceContratto);
                _resp = XmlPgadResponse(response);

            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string AggiornaDocumento_v2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;

            try
            {
                response = _pgad.AggiornaDocumento(IdTransazione, PartitaIva, CodiceAgenzia, CodiceConto, DataRilascioDocumento, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string MigrazioneContoGioco_v2(long IdTransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int Importo, DateTime DataSaldo, DataSet ListBonusPresenti, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;

            try
            {
                response = _pgad.MigrazioneContoGioco(IdTransazione, PartitaIva, IdReteContoOriginario, TitolareSistemaOriginario, CodiceContoOriginario, IdReteContoDestinazione, TitolareSistemaDestinazione, CodiceContoDestinazione, CodiceFiscale, Importo, DataSaldo, ListBonusPresenti);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string RiepilogoOperazioniMovimentazione_v2(long IdTransazione, string PartitaIva, int TitolareSistema, DateTime Data, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.RiepilogoOperazioniMovimentazione(IdTransazione, PartitaIva, TitolareSistema, Data);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        public static string RiepilogoOperazioniServizio_v2(long IdTransazione, string PartitaIva, int TitolareSistema, DateTime Data, string IndirizzoServizio)
        {
            PgadClient _pgad = new PgadClient(GetBinding(IndirizzoServizio), new EndpointAddress(IndirizzoServizio));
            PgadGatewayResponse response = new PgadGatewayResponse();
            string _resp = string.Empty;
            try
            {
                response = _pgad.RiepilogoOperazioniServizio(IdTransazione, PartitaIva, TitolareSistema, Data);
                _resp = XmlPgadResponse(response);
            }
            catch (Exception ex)
            {
                _resp = GetGenericError(ex.Message);
            }
            return _resp;
        }

        internal static string GetGenericError(string message)
        {
            string resp = string.Empty;
            PgadGatewayResponse response = new PgadGatewayResponse();
            response._esito = 0;
            response._success = false;
            response._descrizione = message;
            response._timestamp = DateTime.Now;
            response._idRicevuta = string.Empty;
            return XmlPgadResponse(response);
        }

        internal void Initialize(string address)
        {

            EndpointAddress endpoint = new EndpointAddress(address);
            if (endpoint.Uri.Scheme == "https")
            {
                this.binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            }
            else
            {
                this.binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            }
            this.ea = endpoint;
            this.binding.SendTimeout = new TimeSpan(0, 5, 0);
            this.binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            //Setto la dimensione massima del buffer di ricezione
            this.binding.MaxReceivedMessageSize = Int32.MaxValue;
            this.binding.MaxBufferSize = Int32.MaxValue;
        }

        private static BasicHttpBinding GetBinding(string Endpoint)
        {
            BasicHttpBinding binding;
            if (String.IsNullOrEmpty(Endpoint))
            {
                throw new Exception("Indirizzo Servizio non valorizzato");
            }
            Uri uri = new Uri(Endpoint);
            if (uri.Scheme == "https")
            {
                binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            }
            else
            {
                binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            }
            //Setto la dimensione massima del buffer di ricezione
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.MaxBufferSize = Int32.MaxValue;
            binding.SendTimeout = new TimeSpan(0, 0, Timeout);
            binding.ReceiveTimeout = new TimeSpan(0, 0, Timeout);
            return binding;
        }

        private static string XmlPgadResponse(PgadGatewayResponse res)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("Response");
            xml.AppendChild(root);

            foreach (PropertyInfo p in res.GetType().GetProperties())
            {
                try
                {
                    if (p.PropertyType.IsArray)
                    {
                        XmlElement lista = xml.CreateElement(p.Name);
                        root.AppendChild(lista);

                        object _value = p.GetValue(res, null);
                        Array arr = (Array)_value;
                        foreach (object ele in arr)
                        {
                            XmlElement figlio = xml.CreateElement(ele.GetType().Name);
                            lista.AppendChild(figlio);
                            foreach (PropertyInfo pEle in ele.GetType().GetProperties())
                            {
                                if (pEle.Name == "ExtensionData" || pEle.Name == "PropertyChanged1")
                                    continue;

                                XmlElement eleFiglio = xml.CreateElement(pEle.Name);
                                string val = pEle.GetGetMethod().Invoke(ele, null).ToString();
                                eleFiglio.InnerText = val;
                                figlio.AppendChild(eleFiglio);
                            }
                        }
                    }
                    else
                    {
                        object _value = p.GetValue(res, null);
                        XmlElement valore = xml.CreateElement(p.Name);
                        if (_value != null)
                        {
                            valore.InnerText = _value.ToString();
                        }
                        root.AppendChild(valore);
                    }
                }
                catch (Exception)
                {

                }
            }
            return xml.InnerXml;
        }

        public ResponsePgad InsertBonus(string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {

            return InsertBonus_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad InsertBonus(string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return InsertBonus_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad StornoBonus(string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoBonus_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad StornoBonus(string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoBonus_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusDaAssegnare, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad Movimentazione(string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return Movimentazione_v2(0, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad Movimentazione(string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return Movimentazione_v2(0, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad StornoMovimentazione(string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, int TipoStorno, string IdRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoMovimentazione_v2(0, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, TipoStorno, IdRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad StornoMovimentazione(string PartitaIva, int CausaleDiMovimento, int MezzoDiPagamento, int TipoStorno, string IdRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return StornoMovimentazione_v2(0, PartitaIva, CausaleDiMovimento, MezzoDiPagamento, TipoStorno, IdRicevutaDaStornare, ContoDiGioco, TitolareSistema, ImportoMovimento, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad SaldoContoUtente(string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return SaldoContoUtente_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        public ResponsePgad SaldoContoUtente(string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo)
        {
            return SaldoContoUtente_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti, SaldoUtente, DataSaldo);
        }

        
        public static string SaldoContoUtente(string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {
            return SaldoContoUtente_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti, SaldoUtente, DataSaldo, IndirizzoServizio);
        }

        public static string SaldoContoUtente(string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo, string IndirizzoServizio)
        {
            return SaldoContoUtente_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, ListBonusPresenti, SaldoUtente, DataSaldo, IndirizzoServizio);
        }

        public static string CambiaStato(string PartitaIva, string ContoDiGioco, int TitolareSistema, int CausaleCambioStato, int StatoConto, string IndirizzoServizio)
        {
            return CambiaStato_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, CausaleCambioStato, StatoConto, IndirizzoServizio);
        }

        public static string ApriConto(string PartitaIva, int CodiceAgenzia, string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName, string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita, string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
        {
            return ApriConto_v2(0, PartitaIva, CodiceAgenzia, CodiceConto, CodiceFiscale, Cognome, Nome, Sesso, Email, UserName, Comune_Nazione_Nascita, ProvinciaNascita, DataNascita, IndirizzoResidenza, ComuneResidenza, ProvinciaResidenza, CapResidenza, DataRilascioDocumento, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, IndirizzoServizio);
        }

        public static string ModificaProvinciaResidenza(string PartitaIva, int CodiceAgenzia, string CodiceContratto, string ProvinciaResidenza, string IndirizzoServizio)
        {
            return ModificaProvinciaResidenza_v2(0, PartitaIva, CodiceAgenzia, CodiceContratto, ProvinciaResidenza, IndirizzoServizio);
        }

        public static string StatoConto(string PartitaIva, int CodiceAgenzia, string CodiceContratto, string IndirizzoServizio)
        {
            return StatoConto_v2(0, PartitaIva, CodiceAgenzia, CodiceContratto, IndirizzoServizio);
        }

        public static string SubRegistrazione(string PartitaIva, int CodiceAgenzia, string CodiceContratto, string IndirizzoServizio)
        {
            return SubRegistrazione_v2(0, PartitaIva, CodiceAgenzia, CodiceContratto, IndirizzoServizio);
        }

        public static string AggiornaDocumento(string PartitaIva, int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalitaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, string IndirizzoServizio)
        {
            return AggiornaDocumento_v2(0, PartitaIva, CodiceAgenzia, CodiceConto, DataRilascioDocumento, AutoritaRilascioDocumento, LocalitaRilascioDocumento, NumeroDocumento, IdTipoDocumento, IndirizzoServizio);
        }

        public static string MigrazioneContoGioco(string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int Importo, DateTime DataSaldo, DataSet ListBonusPresenti, string IndirizzoServizio)
        {
            return MigrazioneContoGioco_v2(0, PartitaIva, IdReteContoOriginario, TitolareSistemaOriginario, CodiceContoOriginario, IdReteContoDestinazione, TitolareSistemaDestinazione, CodiceContoDestinazione, CodiceFiscale, Importo, DataSaldo, ListBonusPresenti, IndirizzoServizio);
        }

        public static string RiepilogoOperazioniMovimentazione(string PartitaIva, int TitolareSistema, DateTime Data, string IndirizzoServizio)
        {
            return RiepilogoOperazioniMovimentazione_v2(0, PartitaIva, TitolareSistema, Data, IndirizzoServizio);
        }

        public static string RiepilogoOperazioniServizio(string PartitaIva, int TitolareSistema, DateTime Data, string IndirizzoServizio)
        {
            return RiepilogoOperazioniServizio_v2(0, PartitaIva, TitolareSistema, Data, IndirizzoServizio);
        }

        public static string SubRegistrazioneWithTimeout(string PartitaIva, int CodiceAgenzia, string CodiceContratto, string IndirizzoServizio, int Timeout)
        {
            clr_Pgad.Timeout = Timeout;
            return SubRegistrazione_v2(0, PartitaIva, CodiceAgenzia, CodiceContratto, IndirizzoServizio);
        }

        public static string CambiaStatoWithTimeout(string PartitaIva, string ContoDiGioco, int TitolareSistema, int CausaleCambioStato, int StatoConto, string IndirizzoServizio, int Timeout)
        {
            clr_Pgad.Timeout = Timeout;
            return CambiaStato_v2(0, PartitaIva, ContoDiGioco, TitolareSistema, CausaleCambioStato, StatoConto, IndirizzoServizio);
        }
    }
}
