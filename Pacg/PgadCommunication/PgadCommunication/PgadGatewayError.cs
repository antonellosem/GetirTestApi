using System;
using System.Configuration;
using PgadCommunication.Contracts.Requests;
using PgadCommunication.Contracts.Responses;

namespace PgadCommunication
{

    /// <summary>
    /// Classe di error torna sempre error
    /// </summary>
    public class PgadGatewayError : AbstractPgadGateway, IPgadGateway
    {

        public PgadGatewayError(string username, int TitolareSistema) : base(username, TitolareSistema) { }

        #region IPgadGateway Members

        public PgadGatewayResponse apriContoPersonaGiuridica(MessaggioPersonaGiuridica personaGiuridica)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), personaGiuridica.PersonaGiuridica.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse movimento(MessaggioMovimentoConto movimento)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), movimento.MovimentazioneConto.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse movimentazioneBonus(MessaggioMovimentoBonus movimentoBonus)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), movimentoBonus.MovimentoBonus.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse cambioStato(MessaggioCambioStatoConto cambioStato)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), cambioStato.CambioStato.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse saldo(MessaggioSaldoConto saldo)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), saldo.SaldoConto.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse modificaResidenza(MessaggioModificaProvinciaResidenza modificaResidenzaProvincia)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), modificaResidenzaProvincia.ModificaProvinciaResidenzaConto.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayStatoResponse stato(MessaggioStatoConto statoConto)
        {
            return new PgadGatewayStatoResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), statoConto.StatoConto.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse migrazioneContoGioco(MessaggioMigrazioneConto migrazioneConto)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), migrazioneConto.MigrazioneConto.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse AggiornaDocumento(MessaggioAggiornamentoDocumento aggiornaDocumento)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), aggiornaDocumento.AggiornaDocumento.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgadGatewayResponse AggiornaDocumentoTitolareContoGioco2(MessaggioAggiornamentoDocumentoTitolareContoGioco2 aggiornaDocumento)
        {
            return new PgadGatewayResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), aggiornaDocumento.AggiornaDocumento.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }

        public PgdaGatewayRiepilogoOperazioniMovimentazioneResponse riepilogoOperazioniMovimentazione(MessaggioRiepilogoOperazioniMovimentazione riepilogoOperazioniMovimentazione)
        {
            return new PgdaGatewayRiepilogoOperazioniMovimentazioneResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), riepilogoOperazioniMovimentazione.RiepilogoOperazioniMovimentazione.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }


        public PgdaGatewayRiepilogoOperazioniServizioResponse riepilogoOperazioniServizio(MessaggioRiepilogoOperazioniServizio riepilogoOperazioniServizio)
        {
            return new PgdaGatewayRiepilogoOperazioniServizioResponse(Convert.ToInt16(ConfigurationManager.AppSettings["Errore"].ToString()), riepilogoOperazioniServizio.RiepilogoOperazioniServizio.idTransazione, "Esito KO", true, DateTime.Now.ToFileTime().ToString(), DateTime.Now);
        }


        #endregion

        PgadGatewayResponse IPgadGateway.ApriContoPersonaGiuridica(MessaggioPersonaGiuridica personaGiuridica)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.MigrazioneContoGioco(MessaggioMigrazioneConto migrazioneConto)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.AggiornaDocumento(MessaggioAggiornamentoDocumento aggiornaDocumento)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.Movimento(MessaggioMovimentoConto movimento)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.MovimentazioneBonus(MessaggioMovimentoBonus movimentoBonus)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.CambioStato(MessaggioCambioStatoConto cambioStato)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.Saldo(MessaggioSaldoConto saldo)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.ModificaResidenza(MessaggioModificaProvinciaResidenza modificaResidenzaProvincia)
        {
            throw new NotImplementedException();
        }

        PgadGatewayStatoResponse IPgadGateway.Stato(MessaggioStatoConto statoConto)
        {
            throw new NotImplementedException();
        }

        PgdaGatewayRiepilogoOperazioniMovimentazioneResponse IPgadGateway.RiepilogoOperazioniMovimentazione(MessaggioRiepilogoOperazioniMovimentazione riepilogoOperazioniMovimentazione)
        {
            throw new NotImplementedException();
        }

        PgdaGatewayRiepilogoOperazioniServizioResponse IPgadGateway.RiepilogoOperazioniServizio(MessaggioRiepilogoOperazioniServizio riepilogoOperazioniServizio)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.StornoMovimento(MessaggioStornoMovimentoConto movimento)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.SubRegistrazione2(MessaggioSubRegistrazione2 subregistrazione2)
        {
            throw new NotImplementedException();
        }

        PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse IPgadGateway.ElencoContiGiocoSenzaSubregistrazione(MessaggioElencoContiGiocoSenzaSubregistrazione elencocontiSenzaSubregistrazione)
        {
            throw new NotImplementedException();
        }

        PgadGatewayResponse IPgadGateway.ContoGiocoDormiente(MessaggioContoDormiente contodormiente)
        {
            throw new NotImplementedException();
        }

        PgadGatewayElencoContiGiocoDormientiResponse IPgadGateway.ElencoContiGiocoDormienti(MessaggioElencoContiGiocoDormienti elencocontiSenzadormienti)
        {
            throw new NotImplementedException();
        }

        // 4.21 - PGAD 1.8
        PgadGatewayInterrogazioneEstremiDocumentoResponse IPgadGateway.InterrogazioneEstremiDocumento(MessaggioInterrogazioneEstremiDocumento interrogazioneEstremiDocumento)
        {
            throw new NotImplementedException();
        }

        // 4.22 - PACG 2.0
        PgadGatewayAggiornaLimiteResponse IPgadGateway.AggiornaLimite(MessaggioAggiornaLimite aggiornaLimite)
        {
            throw new NotImplementedException();
        }

        // 4.23 - PACG 2.0
        PgadGatewayInterrogazioneLimitiResponse IPgadGateway.InterrogazioneLimiti(MessaggioInterrogazioneLimiti interrogazioneLimiti)
        {
            throw new NotImplementedException();
        }

        // 4.24 - PACG 2.0
        PgadGatewayGestioneAutoesclusioneTrasversaleResponse IPgadGateway.GestioneAutoesclusioneTrasversale(MessaggioGestioneAutoesclusioneTrasversale gestioneAutoesclusioneTrasversale)
        {
            throw new NotImplementedException();
        }

        // 4.25 - PACG 2.0
        PgadGatewayElencoContiAutoesclusiResponse IPgadGateway.ElencoContiAutoesclusi(MessaggioElencoContiAutoesclusi elencoContiAutoesclusi)
        {
            throw new NotImplementedException();
        }

        // 4.26 - PACG 2.0
        PgadGatewayAperturaContoPersonaFisica2Response IPgadGateway.AperturaContoPersonaFisica2(MessaggioPersonaFisica2 personaFisica)
        {
            throw new NotImplementedException();
        }

        // 4.29 - PACG 2.1
        PgadGatewayInterrogazioneSoggettoAutoesclusoResponse IPgadGateway.InterrogazioneSoggettoAutoescluso(MessaggioInterrogazioneSoggettoAutoescluso persona)
        {
            throw new NotImplementedException();
        }

        // 4.30 - PACG 2.1
        PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse IPgadGateway.AggiornaPseudonimoContoDiGioco(MessaggioAggiornaPseudonimoContoDiGioco persona)
        {
            throw new NotImplementedException();
        }

        // 4.31 - PACG 2.1
        PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse IPgadGateway.AggiornaPostaElettronicaContoDiGioco(MessaggioAggiornaPostaElettronicaContoDiGioco persona)
        {
            throw new NotImplementedException();
        }

        // 4.32 - PACG 2.1
        PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse IPgadGateway.InterrogazioneStoriaSoggettoAutoescluso(MessaggioInterrogazioneStoriaSoggettoAutoescluso messaggioInterrogazioneStoriaSoggettoAutoescluso)
        {
            throw new NotImplementedException();
        }

        // 4.33 - PACG 2.1
        PgadGatewayInterrogazioneProvinciaResidenzaResponse IPgadGateway.InterrogazioneProvinciaResidenza(MessaggioInterrogazioneProvinciaResidenza messaggioInterrogazioneProvinciaResidenza)
        {
            throw new NotImplementedException();
        }
        // 4.34 - PACG 2.1
        PgadGatewayInterrogazionePseudonimoResponse IPgadGateway.InterrogazionePseudonimo(MessaggioInterrogazionePseudonimo messaggioInterrogazionePseudonimo)
        {
            throw new NotImplementedException();
        }
        // 4.35 - PACG 2.1
        PgadGatewayInterrogazionePostaElettronicaResponse IPgadGateway.InterrogazionePostaElettronica(MessaggioInterrogazionePostaElettronica messaggioInterrogazionePostaElettronica)
        {
            throw new NotImplementedException();
        }

        // 4.36 - PACG 2.4
        PgadGatewayElencoContiAutoesclusi2Response IPgadGateway.ElencoContiAutoesclusi2(MessaggioElencoContiAutoesclusi2 elencoContiAutoesclusi)
        {
            throw new NotImplementedException();
        }

        // 4.37 - PACG 2.4
        PgadGatewayResponse IPgadGateway.StornoMovimentazioneBonusConto(MessaggioStornoMovimentoBonusConto movimentoBonus)
        {
            throw new NotImplementedException();
        }

        // 4.38 - PACG 2.4
        PgadGatewayResponse IPgadGateway.MovimentazioneBonusConto2(MessaggioMovimentoBonusConto2 movimentoBonus)
        {
            throw new NotImplementedException();
        }

        // 4.39 - PACG 2.5
        PgadGatewayAperturaContoPersonaFisica3Response IPgadGateway.AperturaContoGiocoPersonaFisica3(MessaggioPersonaFisica3 personaFisica)
        {
            throw new NotImplementedException();
        }

        // 4.41 - PGAD 2.5
        PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response IPgadGateway.
            InterrogazioneEstremiDocumentoTitolareContoGioco2(
                MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2 interrogazioneEstremiDocumento)
        {
            throw new NotImplementedException();

        }

        // 4.42 - PGAD 2.6
        public PgadGatewayResponse ApriContoPersonaGiuridica2(MessaggioPersonaGiuridica2 personaGiuridica)
        {
            throw new NotImplementedException();
        }

        // 4.43 - PGAD 2.6
        public PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response ElencoContiGiocoSenzaSubregistrazione2(
            MessaggioElencoContiGiocoSenzaSubregistrazione2 elencoCdGSenzaSubregistrazione)
        {
            throw new NotImplementedException();
        }
        // 4.44 - PGAD 2.6
        public PgadGatewayElencoContiGiocoDormienti2Response ElencoContiGiocoDormienti2(
            MessaggioElencoContiGiocoDormienti2 elencoContiGiocoDormienti)
        {
            throw new NotImplementedException();
        }
    }
}
