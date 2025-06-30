using System;
using System.Collections.Generic;
using System.ServiceModel;
using PgadCommunication.Business;
using System.Data;
using System.ServiceModel.Web;
using PgadCommon.Enum;
using PgadCommunication.Contracts.Requests;
using PgadCommunication.Contracts.Responses;
using PgadCommunication.Pgad;

namespace PGADServiceLibrary
{
    [ServiceContract(Namespace = "http://www.intralot.it/pgad")]
    [ServiceKnownType(typeof(List<Bonus>))]
    public interface IPgad
    {
        // 4.2
        [OperationContract]
        PgadGatewayResponse ApriContoPersonaGiuridica(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string PartitaIVA, string RagioneSociale,
            string Indirizzo, string Comune, string Provincia, string Cap,
            string Email, string UserName);

        // 4.3
        [OperationContract]
        PgadGatewayResponse MovimentazioneCredito(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.3
        [OperationContract]
        PgadGatewayResponse MovimentazioneCredito_V2(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);
       
        // 4.4
        [OperationContract]
        PgadGatewayResponse MovimentazioneBonus(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.4
        [OperationContract]
        PgadGatewayResponse MovimentazioneBonus_V2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.4
        [OperationContract]
        PgadGatewayResponse StornoBonus(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.4
        [OperationContract]
        PgadGatewayResponse StornoBonus_V2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);
        
        // 4.5
        [OperationContract]
        PgadGatewayResponse CambioStato(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, CausaleCambioStatoConto CausaleCambioStato, StatoConto StatoConto);

        // 4.6
        [OperationContract]
        PgadGatewayResponse SaldoContoUtente(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.6
        [OperationContract]
        PgadGatewayResponse SaldoContoUtente_V2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.7
        [OperationContract]
        PgadGatewayResponse ModificaProvinciaResidenza(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, string ProvinciaResidenza);

        // 4.8
        [OperationContract]
        PgadGatewayStatoResponse StatoConto(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto);

        // 4.10
        [OperationContract]
        PgadGatewayResponse MigrazioneContoGioco(long Idtransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int ImportoSaldo, DateTime DataSaldo, DataSet ListBonusPresenti);

        // 4.10
        [OperationContract]
        PgadGatewayResponse MigrazioneContoGioco_V2(long Idtransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int ImportoSaldo, DateTime DataSaldo, List<Bonus> ListBonusPresenti);

        // 4.11 - ELIMINATO CON PACG 2.6
        [OperationContract]
        PgadGatewayResponse AggiornaDocumento(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento);

        // 4.12
        [OperationContract]
        PgdaGatewayRiepilogoOperazioniMovimentazioneResponse RiepilogoOperazioniMovimentazione(long Idtransazione, string PartitaIva, int TitolareSistema, DateTime Data);

        // 4.13
        [OperationContract]
        PgdaGatewayRiepilogoOperazioniServizioResponse RiepilogoOperazioniServizio(long Idtransazione, string PartitaIva, int TitolareSistema, DateTime Data);

        // 4.14
        [OperationContract]
        PgadGatewayResponse StornoMovimentazioneCredito(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, TipoStorno TipoStorno, string idRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.14
        [OperationContract]
        PgadGatewayResponse StornoMovimentazioneCredito_V2(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, TipoStorno TipoStorno, string idRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        //4.15
        [OperationContract]
        PgadGatewayResponse SubRegistrazione2(int Saldo, int SaldoBonus, long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto);

        //4.18
        [OperationContract]
        PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse ElencoContiGiocoSenzaSubregistrazione(long IdTransazione, DateTime DataRichiesta, string PartitaIva, int CodiceAgenzia, sbyte stato, short inizio, short fine);

        //4.19
        [OperationContract]
        PgadGatewayResponse ContoGiocoDormiente(int Saldo, long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, DateTime DataDormiente);

        //4.20
        [OperationContract]
        PgadGatewayElencoContiGiocoDormientiResponse ElencoContiGiocoDormienti(long IdTransazione, string PartitaIva, int CodiceAgenzia, int anno, int mese, int giorno, short inizio, short fine);

        // 4.21 - ELIMINATO CON PACG 2.6
        [OperationContract]
        PgadGatewayInterrogazioneEstremiDocumentoResponse InterrogazioneEstremiDocumento(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);

        // 4.22
        [OperationContract]
        PgadGatewayAggiornaLimiteResponse AggiornaLimite(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, sbyte GestioneLimite, sbyte TipoLimite, int Importo);

        // 4.23
        [OperationContract]
        PgadGatewayInterrogazioneLimitiResponse InterrogazioneLimiti(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);
        
        // 4.24
        [OperationContract]
        PgadGatewayGestioneAutoesclusioneTrasversaleResponse GestioneAutoesclusioneTrasversale(long IdTransazione, string PartitaIva, int CodiceAgenzia, sbyte GestioneAutoesclusione, sbyte TipoAutoesclusione, string CodiceFiscale);

        // 4.25 - ELIMINATO CON PACG 2.6
        [OperationContract]
        PgadGatewayElencoContiAutoesclusiResponse ElencoContiAutoesclusi(long IdTransazione, string PartitaIva, int CodiceAgenzia, short Inizio, short Fine);

        // 4.26 - ELIMINATO CON PACG 2.6
        [OperationContract]
        PgadGatewayAperturaContoPersonaFisica2Response ApriContoPersonaFisica2(long IdTransazione, string PartitaIva, int CodiceAgenzia,
                    string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName,
                    string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita,
                    string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza,
                    DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento,
                    sbyte NumeroLimiti, List<MessaggioLimite> ElencoLimiti
                    );

        // 4.29
        [OperationContract]
        PgadGatewayInterrogazioneSoggettoAutoesclusoResponse InterrogazioneSoggettoAutoescluso(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceFiscale);
        
        // 4.30
        [OperationContract]
        PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse AggiornaPseudonimoContoDiGioco(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, string Pseudonimo);
        
        // 4.31
        [OperationContract]
        PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse AggiornaPostaElettronicaContoDiGioco(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, string PostaElettronica);

        // 4.32 - ELIMINATO CON PACG 2.6
        [OperationContract]
        PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse InterrogazioneStoriaSoggettoAutoescluso(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceFiscale);
        
        // 4.33
        [OperationContract]
        PgadGatewayInterrogazioneProvinciaResidenzaResponse InterrogazioneProvinciaResidenza(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);
       
        // 4.34
        [OperationContract]
        PgadGatewayInterrogazionePseudonimoResponse InterrogazionePseudonimo(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);
       
        // 4.35
        [OperationContract]
        PgadGatewayInterrogazionePostaElettronicaResponse InterrogazionePostaElettronica(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);

        // 4.36
        [OperationContract]
        PgadGatewayElencoContiAutoesclusi2Response ElencoContiAutoesclusi2(long IdTransazione, string PartitaIva, int CodiceAgenzia, int Inizio, int Fine);

        // 4.37
        [OperationContract]
        PgadGatewayResponse StornoMovimentazioneBonusConto(long IdTransazione, string PartitaIva, CausaleMovimentoBonus CausaleDiMovimentoBonus, TipoStornoBonus TipoStornoBonus, string IdRicevutaBonusDaStornare, string ContoDiGioco, int TitolareSistema, int SaldoUtente, DateTime DataSaldo, List<Bonus> ListDettagliBonus, List<Bonus> ListDettagliBonusSaldo);

        // 4.38
        [OperationContract]
        PgadGatewayResponse MovimentazioneBonusConto2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        // 4.39
        [OperationContract]
        PgadGatewayAperturaContoPersonaFisica3Response ApriContoPersonaFisica3(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName,
            string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita,
            string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza,
            DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento,
            sbyte NumeroLimiti, List<MessaggioLimite> ElencoLimiti, sbyte TipoFornituraDatiPersonali
        );

        // 4.40
        [OperationContract]
        PgadGatewayResponse AggiornaDocumentoTitolareContoGioco2(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, sbyte TipoFornituraDatiPersonali);

        // 4.41
        [OperationContract]
        PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response InterrogazioneEstremiDocumentoTitolareContoGioco2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);

        //4.42
        [OperationContract]
        PgadGatewayResponse ApriContoPersonaGiuridica2(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string PartitaIVA, string RagioneSociale,
            string Indirizzo, string Comune, string Provincia, string Cap,
            string Email, string UserName, sbyte TipoContoPersonaGiuridica);

        //4.43
        [OperationContract]
        PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response ElencoContiGiocoSenzaSubregistrazione2(long IdTransazione, DateTime DataRichiesta, string PartitaIva, int CodiceAgenzia, sbyte stato, short inizio, short fine);

        //4.44
        [OperationContract]
        PgadGatewayElencoContiGiocoDormienti2Response ElencoContiGiocoDormienti2(long IdTransazione, string PartitaIva, int CodiceAgenzia, int anno, int mese, int giorno, short inizio, short fine);

    }


    [ServiceContract(Namespace = "http://www.intralot.it/pgad")]
    [ServiceKnownType(typeof(List<Bonus>))]
    public interface IPgadJson
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/ApriContoPersonaGiuridica", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse ApriContoPersonaGiuridica(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string PartitaIVA, string RagioneSociale,
            string Indirizzo, string Comune, string Provincia, string Cap,
            string Email, string UserName);
      
        [OperationContract]
        [WebInvoke(UriTemplate = "/AggiornaDocumento", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse AggiornaDocumento(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento);

        [OperationContract]
        [WebInvoke(UriTemplate = "/StatoConto", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayStatoResponse StatoConto(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CambioStato", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse CambioStato(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, CausaleCambioStatoConto CausaleCambioStato, StatoConto StatoConto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ModificaProvinciaResidenza", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse ModificaProvinciaResidenza(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, string ProvinciaResidenza);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MigrazioneContoGioco", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse MigrazioneContoGioco(long Idtransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int ImportoSaldo, DateTime DataSaldo, System.Data.DataSet ListBonusPresenti);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MigrazioneContoGioco_V2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse MigrazioneContoGioco_V2(long Idtransazione, string PartitaIva, int IdReteContoOriginario, int TitolareSistemaOriginario, string CodiceContoOriginario, int IdReteContoDestinazione, int TitolareSistemaDestinazione, string CodiceContoDestinazione, string CodiceFiscale, int ImportoSaldo, DateTime DataSaldo, List<Bonus> ListBonusPresenti);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MovimentazioneBonus", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse MovimentazioneBonus(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MovimentazioneBonus_V2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse MovimentazioneBonus_V2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/StornoBonus", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse StornoBonus(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusDaAssegnare, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/StornoBonus_V2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse StornoBonus_V2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MovimentazioneCredito", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse MovimentazioneCredito(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MovimentazioneCredito_V2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse MovimentazioneCredito_V2(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/StornoMovimentazioneCredito", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse StornoMovimentazioneCredito(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, TipoStorno TipoStorno, string idRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/StornoMovimentazioneCredito_V2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse StornoMovimentazioneCredito_V2(long Idtransazione, string PartitaIva, CausaleMovimento CausaleDiMovimento, MezzoPagamento MezzoDiPagamento, TipoStorno TipoStorno, string idRicevutaDaStornare, string ContoDiGioco, int TitolareSistema, int ImportoMovimento, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/SaldoContoUtente", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse SaldoContoUtente(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, DataSet ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/SaldoContoUtente_V2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse SaldoContoUtente_V2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/RiepilogoOperazioniMovimentazione", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgdaGatewayRiepilogoOperazioniMovimentazioneResponse RiepilogoOperazioniMovimentazione(long Idtransazione, string PartitaIva, int TitolareSistema, DateTime Data);

        [OperationContract]
        [WebInvoke(UriTemplate = "/RiepilogoOperazioniServizio", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgdaGatewayRiepilogoOperazioniServizioResponse RiepilogoOperazioniServizio(long Idtransazione, string PartitaIva, int TitolareSistema, DateTime Data);

        [OperationContract]
        [WebInvoke(UriTemplate = "/SubRegistrazione2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse SubRegistrazione2(int Saldo, int SaldoBonus, long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ElencoContiGiocoSenzaSubregistrazione", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse ElencoContiGiocoSenzaSubregistrazione(long IdTransazione, DateTime DataRichiesta, string PartitaIva, int CodiceAgenzia, sbyte stato, short inizio, short fine);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ContoGiocoDormiente", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse ContoGiocoDormiente(int Saldo, long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, DateTime DataDormiente);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ElencoContiGiocoDormienti", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayElencoContiGiocoDormientiResponse ElencoContiGiocoDormienti(long IdTransazione, string PartitaIva, int CodiceAgenzia, int anno, int mese, int giorno, short inizio, short fine);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazioneEstremiDocumento", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazioneEstremiDocumentoResponse InterrogazioneEstremiDocumento(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/AggiornaLimite", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayAggiornaLimiteResponse AggiornaLimite(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceContratto, sbyte GestioneLimite, sbyte TipoLimite, int Importo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazioneLimiti", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazioneLimitiResponse InterrogazioneLimiti(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GestioneAutoesclusioneTrasversale", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayGestioneAutoesclusioneTrasversaleResponse GestioneAutoesclusioneTrasversale(long IdTransazione, string PartitaIva, int CodiceAgenzia, sbyte GestioneAutoesclusione, sbyte TipoAutoesclusione, string CodiceFiscale);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ElencoContiAutoesclusi", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayElencoContiAutoesclusiResponse ElencoContiAutoesclusi(long IdTransazione, string PartitaIva, int CodiceAgenzia, short Inizio, short Fine);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ApriContoPersonaFisica2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayAperturaContoPersonaFisica2Response ApriContoPersonaFisica2(long IdTransazione, string PartitaIva, int CodiceAgenzia,
                    string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName,
                    string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita,
                    string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza,
                    DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento,
                    sbyte NumeroLimiti, List<MessaggioLimite> ElencoLimiti
                    );

        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazioneSoggettoAutoescluso", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazioneSoggettoAutoesclusoResponse InterrogazioneSoggettoAutoescluso(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceFiscale);

        [OperationContract]
        [WebInvoke(UriTemplate = "/AggiornaPseudonimoContoDiGioco", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse AggiornaPseudonimoContoDiGioco(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, string Pseudonimo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/AggiornaPostaElettronicaContoDiGioco", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse AggiornaPostaElettronicaContoDiGioco(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, string PostaElettronica);
      
        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazioneStoriaSoggettoAutoescluso", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse InterrogazioneStoriaSoggettoAutoescluso(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceFiscale);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazioneProvinciaResidenza", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazioneProvinciaResidenzaResponse InterrogazioneProvinciaResidenza(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);
       
        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazionePseudonimo", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazionePseudonimoResponse InterrogazionePseudonimo(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);
      
        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazionePostaElettronica", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazionePostaElettronicaResponse InterrogazionePostaElettronica(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ElencoContiAutoesclusi2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayElencoContiAutoesclusi2Response ElencoContiAutoesclusi2(long IdTransazione, string PartitaIva, int CodiceAgenzia, int Inizio, int Fine);

        [OperationContract]
        [WebInvoke(UriTemplate = "/StornoMovimentazioneBonusConto", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse StornoMovimentazioneBonusConto(long IdTransazione, string PartitaIva, CausaleMovimentoBonus CausaleDiMovimentoBonus, TipoStornoBonus TipoStornoBonus, string IdRicevutaBonusDaStornare, string ContoDiGioco, int TitolareSistema, int SaldoUtente, DateTime DataSaldo, List<Bonus> ListDettagliBonus, List<Bonus> ListDettagliBonusSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MovimentazioneBonusConto2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse MovimentazioneBonusConto2(long Idtransazione, string PartitaIva, string ContoDiGioco, int TitolareSistema, List<Bonus> ListBonusDaAssegnare, List<Bonus> ListBonusPresenti, int SaldoUtente, DateTime DataSaldo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ApriContoPersonaFisica3", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayAperturaContoPersonaFisica3Response ApriContoPersonaFisica3(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string CodiceFiscale, string Cognome, string Nome, char Sesso, string Email, string UserName,
            string Comune_Nazione_Nascita, string ProvinciaNascita, DateTime DataNascita,
            string IndirizzoResidenza, string ComuneResidenza, string ProvinciaResidenza, string CapResidenza,
            DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento,
            sbyte NumeroLimiti, List<MessaggioLimite> ElencoLimiti, sbyte TipoFornituraDatiPersonali
        );

        [OperationContract]
        [WebInvoke(UriTemplate = "/AggiornaDocumentoTitolareContoGioco2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse AggiornaDocumentoTitolareContoGioco2(long Idtransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto, DateTime DataRilascioDocumento, string AutoritaRilascioDocumento, string LocalicaRilascioDocumento, string NumeroDocumento, int IdTipoDocumento, sbyte TipoFornituraDatiPersonali);

        [OperationContract]
        [WebInvoke(UriTemplate = "/InterrogazioneEstremiDocumentoTitolareContoGioco2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response InterrogazioneEstremiDocumentoTitolareContoGioco2(long IdTransazione, string PartitaIva, int CodiceAgenzia, string CodiceConto);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "/aperturaContoPersonaGiuridica2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayResponse ApriContoPersonaGiuridica2(long IdTransazione, string PartitaIva, int CodiceAgenzia,
            string CodiceConto, string PartitaIVA, string RagioneSociale, string Indirizzo, string Comune, string Provincia, string Cap,
            string Email, string UserName, sbyte TipoContoPersonaGiuridica);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ElencoContiGiocoSenzaSubregistrazione2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response ElencoContiGiocoSenzaSubregistrazione2(long IdTransazione, DateTime DataRichiesta, string PartitaIva, int CodiceAgenzia, sbyte stato, short inizio, short fine);

        [OperationContract]
        [WebInvoke(UriTemplate = "/ElencoContiGiocoDormienti2", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PgadGatewayElencoContiGiocoDormienti2Response ElencoContiGiocoDormienti2(long IdTransazione, string PartitaIva, int CodiceAgenzia, int anno, int mese, int giorno, short inizio, short fine);

    }
}
