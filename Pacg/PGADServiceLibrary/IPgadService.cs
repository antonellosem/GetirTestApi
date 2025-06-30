using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using PgadCommon.Enum;
using PgadCommunication.Business;
using PgadCommunication.Contracts.Requests;
using PgadCommunication.Contracts.Responses;

namespace PGADServiceLibrary
{
    [ServiceContract(Namespace = "http://www.intralot.it/pgad")]
    [ServiceKnownType(typeof(List<Bonus>))]
    public interface IPgadService
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

        // 4.11
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

        // 4.21
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

        // 4.25
        [OperationContract]
        PgadGatewayElencoContiAutoesclusiResponse ElencoContiAutoesclusi(long IdTransazione, string PartitaIva, int CodiceAgenzia, short Inizio, short Fine);

        // 4.26
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

        // 4.32
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

    }
}
