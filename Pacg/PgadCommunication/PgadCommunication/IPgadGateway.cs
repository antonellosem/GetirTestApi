using PgadCommunication.Contracts.Requests;
using PgadCommunication.Contracts.Responses;
using PgadCommunication.Pgad;

namespace PgadCommunication
{
    public interface IPgadGateway
    {
        PgadGatewayResponse ApriContoPersonaGiuridica(MessaggioPersonaGiuridica personaGiuridica);

        PgadGatewayResponse MigrazioneContoGioco(MessaggioMigrazioneConto migrazioneConto);

        PgadGatewayResponse AggiornaDocumento(MessaggioAggiornamentoDocumento aggiornaDocumento);

        PgadGatewayResponse Movimento(MessaggioMovimentoConto movimento);

        PgadGatewayResponse MovimentazioneBonus(MessaggioMovimentoBonus movimentoBonus);

        PgadGatewayResponse CambioStato(MessaggioCambioStatoConto cambioStato);

        PgadGatewayResponse Saldo(MessaggioSaldoConto saldo);

        PgadGatewayResponse ModificaResidenza(MessaggioModificaProvinciaResidenza modificaResidenzaProvincia);

        PgadGatewayStatoResponse Stato(MessaggioStatoConto statoConto);

        PgdaGatewayRiepilogoOperazioniMovimentazioneResponse RiepilogoOperazioniMovimentazione(MessaggioRiepilogoOperazioniMovimentazione riepilogoOperazioniMovimentazione);

        PgdaGatewayRiepilogoOperazioniServizioResponse RiepilogoOperazioniServizio(MessaggioRiepilogoOperazioniServizio riepilogoOperazioniServizio);

        PgadGatewayResponse StornoMovimento(MessaggioStornoMovimentoConto movimento);

        //4.15
        PgadGatewayResponse SubRegistrazione2(MessaggioSubRegistrazione2 subregistrazione2);
       
        //4.18
        PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse ElencoContiGiocoSenzaSubregistrazione(MessaggioElencoContiGiocoSenzaSubregistrazione elencoCdGSenzaSubregistrazione);
        
        //4.19
        PgadGatewayResponse ContoGiocoDormiente(MessaggioContoDormiente contoGiocoDormiente);
        
        //4.20
        PgadGatewayElencoContiGiocoDormientiResponse ElencoContiGiocoDormienti(MessaggioElencoContiGiocoDormienti elencoContiGiocoDormienti);

        // 4.21
        PgadGatewayInterrogazioneEstremiDocumentoResponse InterrogazioneEstremiDocumento(MessaggioInterrogazioneEstremiDocumento interrogazioneEstremiDocumento);

        // 4.22
        PgadGatewayAggiornaLimiteResponse AggiornaLimite(MessaggioAggiornaLimite aggiornaLimite);

        // 4.23 
        PgadGatewayInterrogazioneLimitiResponse InterrogazioneLimiti(MessaggioInterrogazioneLimiti interrogazioneLimiti);

        // 4.24
        PgadGatewayGestioneAutoesclusioneTrasversaleResponse GestioneAutoesclusioneTrasversale(MessaggioGestioneAutoesclusioneTrasversale gestioneAutoesclusioneTrasversale);

        // 4.25 
        PgadGatewayElencoContiAutoesclusiResponse ElencoContiAutoesclusi(MessaggioElencoContiAutoesclusi elencoContiAutoesclusi);

        // 4.26 
        PgadGatewayAperturaContoPersonaFisica2Response AperturaContoPersonaFisica2(MessaggioPersonaFisica2 personaFisica);

        // 4.29 
        PgadGatewayInterrogazioneSoggettoAutoesclusoResponse InterrogazioneSoggettoAutoescluso(MessaggioInterrogazioneSoggettoAutoescluso persona);
        
        // 4.30 
        PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse AggiornaPseudonimoContoDiGioco(MessaggioAggiornaPseudonimoContoDiGioco persona);
        
        // 4.31 
        PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse AggiornaPostaElettronicaContoDiGioco(MessaggioAggiornaPostaElettronicaContoDiGioco persona);
       
        // 4.32
        PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse InterrogazioneStoriaSoggettoAutoescluso(MessaggioInterrogazioneStoriaSoggettoAutoescluso persona);
       
        // 4.33
        PgadGatewayInterrogazioneProvinciaResidenzaResponse InterrogazioneProvinciaResidenza(MessaggioInterrogazioneProvinciaResidenza interrogazioneProvinciaResidenza);
       
        // 4.34 
        PgadGatewayInterrogazionePseudonimoResponse InterrogazionePseudonimo(MessaggioInterrogazionePseudonimo interrogazionePseudonimo);
      
        // 4.35
        PgadGatewayInterrogazionePostaElettronicaResponse InterrogazionePostaElettronica(MessaggioInterrogazionePostaElettronica interrogazionePostaElettronica);

        // 4.36
        PgadGatewayElencoContiAutoesclusi2Response ElencoContiAutoesclusi2(MessaggioElencoContiAutoesclusi2 elencoContiAutoesclusi);

        // 4.37
        PgadGatewayResponse StornoMovimentazioneBonusConto(MessaggioStornoMovimentoBonusConto movimentoBonus);

        // 4.38
        PgadGatewayResponse MovimentazioneBonusConto2(MessaggioMovimentoBonusConto2 movimentoBonus);

        // 4.39
        PgadGatewayAperturaContoPersonaFisica3Response AperturaContoGiocoPersonaFisica3(MessaggioPersonaFisica3 personaFisica);

        // 4.40
        PgadGatewayResponse AggiornaDocumentoTitolareContoGioco2(MessaggioAggiornamentoDocumentoTitolareContoGioco2 aggiornaDocumento);

        // 4.41
        PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response InterrogazioneEstremiDocumentoTitolareContoGioco2(MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2 interrogazioneEstremiDocumento);

        //4.42
        PgadGatewayResponse ApriContoPersonaGiuridica2(MessaggioPersonaGiuridica2 personaGiuridica);

        //4.43
        PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response ElencoContiGiocoSenzaSubregistrazione2(MessaggioElencoContiGiocoSenzaSubregistrazione2 elencoCdGSenzaSubregistrazione);


        //4.44
        PgadGatewayElencoContiGiocoDormienti2Response ElencoContiGiocoDormienti2(MessaggioElencoContiGiocoDormienti2 elencoContiGiocoDormienti);
    }
}
