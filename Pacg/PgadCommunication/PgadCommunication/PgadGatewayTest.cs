using System;
using PgadCommunication.Contracts.Requests;
using PgadCommunication.Contracts.Responses;
using PgadCommunication.Pgad;

namespace PgadCommunication
{

    /// <summary>
    /// Classe di test torna sempre true
    /// </summary>
    public class PgadGatewayTest : AbstractPgadGateway, IPgadGateway
    {

        public PgadGatewayTest(string username, int TitolareSistema) : base(username, TitolareSistema) { }

        #region IPgadGateway Members

        public PgadGatewayResponse ApriContoPersonaGiuridica(MessaggioPersonaGiuridica personaGiuridica)
        {
            return new PgadGatewayResponse(1024, personaGiuridica.PersonaGiuridica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse Movimento(MessaggioMovimentoConto movimento)
        {
            return new PgadGatewayResponse(1024, movimento.MovimentazioneConto.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse MovimentazioneBonus(MessaggioMovimentoBonus movimentoBonus)
        {
            return new PgadGatewayResponse(1024, movimentoBonus.MovimentoBonus.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse CambioStato(MessaggioCambioStatoConto cambioStato)
        {
            return new PgadGatewayResponse(1024, cambioStato.CambioStato.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse Saldo(MessaggioSaldoConto saldo)
        {
            return new PgadGatewayResponse(1024, saldo.SaldoConto.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse ModificaResidenza(MessaggioModificaProvinciaResidenza modificaResidenzaProvincia)
        {
            return new PgadGatewayResponse(1024, modificaResidenzaProvincia.ModificaProvinciaResidenzaConto.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayStatoResponse Stato(MessaggioStatoConto statoConto)
        {
            return new PgadGatewayStatoResponse(1024, statoConto.StatoConto.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse MigrazioneContoGioco(MessaggioMigrazioneConto migrazioneConto)
        {
            return new PgadGatewayResponse(1024, migrazioneConto.MigrazioneConto.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse AggiornaDocumento(MessaggioAggiornamentoDocumento aggiornaDocumento)
        {
            return new PgadGatewayResponse(1024, aggiornaDocumento.AggiornaDocumento.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgdaGatewayRiepilogoOperazioniMovimentazioneResponse RiepilogoOperazioniMovimentazione(MessaggioRiepilogoOperazioniMovimentazione riepilogoOperazioniMovimentazione)
        {
            return new PgdaGatewayRiepilogoOperazioniMovimentazioneResponse(1024, riepilogoOperazioniMovimentazione.RiepilogoOperazioniMovimentazione.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }


        public PgdaGatewayRiepilogoOperazioniServizioResponse RiepilogoOperazioniServizio(MessaggioRiepilogoOperazioniServizio riepilogoOperazioniServizio)
        {
            return new PgdaGatewayRiepilogoOperazioniServizioResponse(1024, riepilogoOperazioniServizio.RiepilogoOperazioniServizio.idTransazione, "Esito OK", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        #endregion


        public PgadGatewayResponse StornoMovimento(MessaggioStornoMovimentoConto stornoMovimento)
        {
            return new PgadGatewayResponse(1024, stornoMovimento.StornoMovimentazioneConto.idTransazione, "Esito OK", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse SubRegistrazione2(MessaggioSubRegistrazione2 subregistrazione2)
        {
            return new PgadGatewayResponse(1024, subregistrazione2.Subregistrazione.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse ElencoContiGiocoSenzaSubregistrazione(MessaggioElencoContiGiocoSenzaSubregistrazione elencoCdGSenzaSubregistrazione)
        {
            return new PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse(1024, elencoCdGSenzaSubregistrazione.elencoConti.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse ContoGiocoDormiente(MessaggioContoDormiente contoGiocoDormiente)
        {
            return new PgadGatewayResponse(1024, contoGiocoDormiente.ContoDormiente.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayElencoContiGiocoDormientiResponse ElencoContiGiocoDormienti(MessaggioElencoContiGiocoDormienti elencoContiGiocoDormienti)
        {
            return new PgadGatewayElencoContiGiocoDormientiResponse(1024, elencoContiGiocoDormienti.elencoConti.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }


        // #24061 - A. Piscitelli

        // 4.21 - PGAD 1.8
        public PgadGatewayInterrogazioneEstremiDocumentoResponse InterrogazioneEstremiDocumento(MessaggioInterrogazioneEstremiDocumento interrogazioneEstremiDocumento)
        {
            return new PgadGatewayInterrogazioneEstremiDocumentoResponse(1024, interrogazioneEstremiDocumento.InterrogazioneEstremiDocumento.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.22 - PACG 2.0
        public PgadGatewayAggiornaLimiteResponse AggiornaLimite(MessaggioAggiornaLimite aggiornaLimite)
        {
            return new PgadGatewayAggiornaLimiteResponse(1024, aggiornaLimite.AggiornaLimite.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.23 - PACG 2.0
        PgadGatewayInterrogazioneLimitiResponse IPgadGateway.InterrogazioneLimiti(MessaggioInterrogazioneLimiti interrogazioneLimiti)
        {
            return new PgadGatewayInterrogazioneLimitiResponse(1024, interrogazioneLimiti.InterrogazioneLimiti.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.24 - PACG 2.0
        PgadGatewayGestioneAutoesclusioneTrasversaleResponse IPgadGateway.GestioneAutoesclusioneTrasversale(MessaggioGestioneAutoesclusioneTrasversale gestioneAutoesclusioneTrasversale)
        {
            return new PgadGatewayGestioneAutoesclusioneTrasversaleResponse(1024, gestioneAutoesclusioneTrasversale.GestioneAutoesclusioneTrasversale.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.25 - PACG 2.0
        PgadGatewayElencoContiAutoesclusiResponse IPgadGateway.ElencoContiAutoesclusi(MessaggioElencoContiAutoesclusi elencoContiAutoesclusi)
        {
            return new PgadGatewayElencoContiAutoesclusiResponse(1024, elencoContiAutoesclusi.elencoConti.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.26 - PACG 2.0
        public PgadGatewayAperturaContoPersonaFisica2Response AperturaContoPersonaFisica2(MessaggioPersonaFisica2 personaFisica)
        {
            return new PgadGatewayAperturaContoPersonaFisica2Response(1024, personaFisica.PersonaFisica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.29 - PACG 2.1
        public PgadGatewayInterrogazioneSoggettoAutoesclusoResponse InterrogazioneSoggettoAutoescluso(MessaggioInterrogazioneSoggettoAutoescluso persona)
        {
            return new PgadGatewayInterrogazioneSoggettoAutoesclusoResponse(1024, persona.PersonaFisica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.30 - PACG 2.1
        public PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse AggiornaPseudonimoContoDiGioco(MessaggioAggiornaPseudonimoContoDiGioco persona)
        {
            return new PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse(1024, persona.PersonaFisica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.31 - PACG 2.1
        public PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse AggiornaPostaElettronicaContoDiGioco(MessaggioAggiornaPostaElettronicaContoDiGioco persona)
        {
            return new PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse(1024, persona.PersonaFisica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.32 - PACG 2.1
        PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse IPgadGateway.InterrogazioneStoriaSoggettoAutoescluso(MessaggioInterrogazioneStoriaSoggettoAutoescluso messaggioInterrogazioneStoriaSoggettoAutoescluso)
        {
            return new PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse(1024, messaggioInterrogazioneStoriaSoggettoAutoescluso.PersonaFisica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.33 - PACG 2.1
        PgadGatewayInterrogazioneProvinciaResidenzaResponse IPgadGateway.InterrogazioneProvinciaResidenza(MessaggioInterrogazioneProvinciaResidenza messaggioInterrogazioneProvinciaResidenza)
        {
            return new PgadGatewayInterrogazioneProvinciaResidenzaResponse(1024, messaggioInterrogazioneProvinciaResidenza.InterrogazioneProvinciaResidenza.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }
        // 4.34 - PACG 2.1
        PgadGatewayInterrogazionePseudonimoResponse IPgadGateway.InterrogazionePseudonimo(MessaggioInterrogazionePseudonimo messaggioInterrogazionePseudonimo)
        {
            return new PgadGatewayInterrogazionePseudonimoResponse(1024, messaggioInterrogazionePseudonimo.InterrogazionePseudonimo.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }
        // 4.35 - PACG 2.1
        PgadGatewayInterrogazionePostaElettronicaResponse IPgadGateway.InterrogazionePostaElettronica(MessaggioInterrogazionePostaElettronica messaggioInterrogazionePostaElettronica)
        {
            return new PgadGatewayInterrogazionePostaElettronicaResponse(1024, messaggioInterrogazionePostaElettronica.InterrogazionePostaElettronica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }
        // 4.36 - PACG 2.4
        PgadGatewayElencoContiAutoesclusi2Response IPgadGateway.ElencoContiAutoesclusi2(MessaggioElencoContiAutoesclusi2 elencoContiAutoesclusi)
        {
            return new PgadGatewayElencoContiAutoesclusi2Response(1024, elencoContiAutoesclusi.elencoConti.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }
        // 4.37 - PACG 2.4
        PgadGatewayResponse IPgadGateway.StornoMovimentazioneBonusConto(MessaggioStornoMovimentoBonusConto movimentoBonus)
        {
            return new PgadGatewayResponse(1024, movimentoBonus.StornoMovimentazioneBonusConto.idTransazione, "Esito OK", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.38 - PACG 2.4
        PgadGatewayResponse IPgadGateway.MovimentazioneBonusConto2(MessaggioMovimentoBonusConto2 movimentoBonus)
        {
            return new PgadGatewayResponse(1024, movimentoBonus.MovimentoBonus.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.39 - PACG 2.5
        PgadGatewayAperturaContoPersonaFisica3Response IPgadGateway.AperturaContoGiocoPersonaFisica3(MessaggioPersonaFisica3 personaFisica)
        {
            return new PgadGatewayAperturaContoPersonaFisica3Response(1024, personaFisica.PersonaFisica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.40 - PACG 2.5
        public PgadGatewayResponse AggiornaDocumentoTitolareContoGioco2(MessaggioAggiornamentoDocumentoTitolareContoGioco2 aggiornaDocumento)
        {
            return new PgadGatewayResponse(1024, aggiornaDocumento.AggiornaDocumento.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        // 4.41 - PGAD 2.5
        public PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response InterrogazioneEstremiDocumentoTitolareContoGioco2(MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2 interrogazioneEstremiDocumento)
        {
            return new PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response(1024, interrogazioneEstremiDocumento.InterrogazioneEstremiDocumento.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }
        
        // 4.42 - PGAD 2.6
        public PgadGatewayResponse ApriContoPersonaGiuridica2(MessaggioPersonaGiuridica2 apriPersonaGiuridica2)
        {
            return new PgadGatewayResponse(1024, apriPersonaGiuridica2.PersonaGiuridica.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response ElencoContiGiocoSenzaSubregistrazione2(
            MessaggioElencoContiGiocoSenzaSubregistrazione2 elencoCdGSenzaSubregistrazione)
        {
            return new PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response(1024, elencoCdGSenzaSubregistrazione.elencoConti.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayElencoContiGiocoDormienti2Response ElencoContiGiocoDormienti2(
            MessaggioElencoContiGiocoDormienti2 elencoContiGiocoDormienti)
        {
            return new PgadGatewayElencoContiGiocoDormienti2Response(1024, elencoContiGiocoDormienti.elencoConti.idTransazione, "Esito ok", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }
    }
}
