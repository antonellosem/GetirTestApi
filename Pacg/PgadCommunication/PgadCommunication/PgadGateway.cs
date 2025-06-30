using System;
using System.Collections.Generic;
using PgadCommunication.Pgad;
using PgadCommunication.CustomMessageEncoder;
using IntralotWebLib;
using PgadCommon.Enum;
using PgadCommunication.Contracts.Requests;
using PgadCommunication.Contracts.Responses;

namespace PgadCommunication
{

    public class PgadGateway : AbstractPgadGateway, IPgadGateway
    {

        /// <summary>
        /// Utilizzato dal factory
        /// </summary>
        /// <param name="?"></param>
        public PgadGateway(string Username, int TitolareSistema) : base(Username, TitolareSistema) { }

        public PgadGatewayResponse ApriContoPersonaGiuridica(MessaggioPersonaGiuridica personaGiuridica)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);

                var request = new aperturaContoPersonaGiuridicaRequest
                {
                    requestElements = personaGiuridica.PersonaGiuridica
                };

                var response = proxy.aperturaContoPersonaGiuridica(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = personaGiuridica?.PersonaGiuridica?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.apriContoPersonaGiuridica"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
               
                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short) EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse AggiornaDocumento(MessaggioAggiornamentoDocumento aggiornaDocumento)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new aggiornaDatiDocumentoTitolareContoRequest
                {
                    requestElements = aggiornaDocumento.AggiornaDocumento
                };
                var response = proxy.aggiornaDatiDocumentoTitolareConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = aggiornaDocumento?.AggiornaDocumento?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.AggiornaDocumento"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                return new PgadGatewayResponse((short) EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse Movimento(MessaggioMovimentoConto movimento)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new movimentazioneContoRequest
                {
                    requestElements = movimento.MovimentazioneConto
                };
                var response = proxy.movimentazioneConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = movimento?.MovimentazioneConto?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.movimento"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
             

                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short) EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse MovimentazioneBonus(MessaggioMovimentoBonus movimentoBonus)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new movimentazioneBonusContoRequest
                {
                    requestElements = movimentoBonus.MovimentoBonus
                };
                var response = proxy.movimentazioneBonusConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = movimentoBonus?.MovimentoBonus?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.movimentazioneBonus"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse CambioStato(MessaggioCambioStatoConto cambioStato)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new cambioStatoContoRequest
                {
                    requestElements = cambioStato.CambioStato
                };
                var response = proxy.cambioStatoConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = cambioStato?.CambioStato?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.cambioStato"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
                
                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse Saldo(MessaggioSaldoConto saldo)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new saldoContoRequest
                {
                    requestElements = saldo.SaldoConto
                };
                var response = proxy.saldoConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var descrizioneErrore =
                    $"{"PgadGateway.saldo"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
                long idTransazione = 0;

                if (saldo != null && saldo.SaldoConto != null)
                    idTransazione = saldo.SaldoConto.idTransazione;
                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse ModificaResidenza(MessaggioModificaProvinciaResidenza modificaResidenzaProvincia)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new modificaProvinciaResidenzaContoRequest
                {
                    requestElements = modificaResidenzaProvincia.ModificaProvinciaResidenzaConto
                };
                var response = proxy.modificaProvinciaResidenzaConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = modificaResidenzaProvincia?.ModificaProvinciaResidenzaConto?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.modificaResidenza"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayStatoResponse Stato(MessaggioStatoConto statoConto)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazioneStatoContoRequest
                {
                    requestElements = statoConto.StatoConto
                };
                var response = proxy.interrogazioneStatoConto(request);
                var statoResponse = new PgadGatewayStatoResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);

                statoResponse.IdCnConto = response.responseElements.idCnConto;
                statoResponse.IdReteConto = Convert.ToInt32(response.responseElements.idReteConto);
                statoResponse.CodiceConto = response.responseElements.codiceConto;
                statoResponse.Stato = Convert.ToInt32(response.responseElements.stato);
                statoResponse.Causale = Convert.ToInt32(response.responseElements.causale);

                return statoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = statoConto?.StatoConto?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.stato"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";


                TraceLog.TraceError("PgadGatewayStatoGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayStatoResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);

            }
        }

        public PgadGatewayResponse MigrazioneContoGioco(MessaggioMigrazioneConto migrazioneConto)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new migrazioneContoRequest
                {
                    requestElements = migrazioneConto.MigrazioneConto
                };
                var response = proxy.migrazioneConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var descrizioneErrore =
                    $"{"PgadGateway.migrazioneContoGioco"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
                long idTransazione = 0;

                if (migrazioneConto != null && migrazioneConto.MigrazioneConto != null)
                    idTransazione = migrazioneConto.MigrazioneConto.idTransazione;
                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgdaGatewayRiepilogoOperazioniMovimentazioneResponse RiepilogoOperazioniMovimentazione(MessaggioRiepilogoOperazioniMovimentazione riepilogoOperazioniMovimentazione)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new riepilogoOperazioniMovimentazioneRequest
                {
                    requestElements = riepilogoOperazioniMovimentazione.RiepilogoOperazioniMovimentazione
                };
                var response = proxy.riepilogoOperazioniMovimentazione(request);
                var riepilogoResponse = new PgdaGatewayRiepilogoOperazioniMovimentazioneResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp, response.responseElements.dettaglioOperazioniMovimentazione, response.responseElements.data);

                return riepilogoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione =
                    riepilogoOperazioniMovimentazione?.RiepilogoOperazioniMovimentazione?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.riepilogoOperazioniMovimentazione"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgdaGatewayRiepilogoOperazioniMovimentazioneGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgdaGatewayRiepilogoOperazioniMovimentazioneResponse((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgdaGatewayRiepilogoOperazioniServizioResponse RiepilogoOperazioniServizio(MessaggioRiepilogoOperazioniServizio riepilogoOperazioniServizio)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new riepilogoOperazioniServizioRequest
                {
                    requestElements = riepilogoOperazioniServizio.RiepilogoOperazioniServizio
                };
                var response = proxy.riepilogoOperazioniServizio(request);
                var riepilogoResponse = new PgdaGatewayRiepilogoOperazioniServizioResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp,
                    response.responseElements.dettaglioOperazioniServizio, response.responseElements.data);

                return riepilogoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = riepilogoOperazioniServizio?.RiepilogoOperazioniServizio?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.riepilogoOperazioniServizio"} {DescrizioneEsitoRitorno((int)EsitoRichiesta.GENERIC_ERROR)} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgdaGatewayRiepilogoOperazioniServizioGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgdaGatewayRiepilogoOperazioniServizioResponse((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse StornoMovimento(MessaggioStornoMovimentoConto stornoMovimento)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new stornoMovimentazioneContoRequest
                {
                    requestElements = stornoMovimento.StornoMovimentazioneConto
                };
                var response = proxy.stornoMovimentazioneConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var descrizioneErrore =
                    $"{"PgadGateway.stornoMovimento"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
                long idTransazione = 0;

                if (stornoMovimento != null && stornoMovimento.StornoMovimentazioneConto != null)
                    idTransazione = stornoMovimento.StornoMovimentazioneConto.idTransazione;
                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse SubRegistrazione2(MessaggioSubRegistrazione2 subregistrazione2)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new subregistrazione2Request
                {
                    requestElements = subregistrazione2.Subregistrazione
                };
                var response = proxy.subregistrazione2(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = subregistrazione2?.Subregistrazione?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"PgadGateway.subRegistrazione2 {DescrizioneEsitoRitorno((int)EsitoRichiesta.GENERIC_ERROR)} {ex} ";

                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse ElencoContiGiocoSenzaSubregistrazione(MessaggioElencoContiGiocoSenzaSubregistrazione elencocontisenzasubregistrazione)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new elencoContiSenzaSubregistrazioneRequest
                {
                    requestElements = elencocontisenzasubregistrazione.elencoConti
                };
                var response = proxy.elencoContiGiocoSenzaSubregistrazione(request);
                var elencoResponse = new PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);

                elencoResponse.NumeroDettaglioConti = response.responseElements.numeroDettagliConti;
                for (var i = 0; i < response.responseElements.numeroDettagliConti; i++)
                {
                    elencoResponse.ElencoConti.Add(response.responseElements.dettaglioConti[i]);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = elencocontisenzasubregistrazione?.elencoConti?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.elencoContiGiocoSenzaSubregistrazione"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayElencoContiGiocoSenzaSubregistrazioneGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayElencoContiGiocoSenzaSubregistrazioneResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse ContoGiocoDormiente(MessaggioContoDormiente contogiocodormiente)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new contoDormienteRequest
                {
                    requestElements = contogiocodormiente.ContoDormiente
                };
                var response = proxy.contoGiocoDormiente(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = contogiocodormiente?.ContoDormiente?.idTransazione ??  0;
                var descrizioneErrore =
                    $"{"PgadGateway.contogiocodormiente"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
                
                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);

            }
        }

        public PgadGatewayElencoContiGiocoDormientiResponse ElencoContiGiocoDormienti(MessaggioElencoContiGiocoDormienti elencocontidormienti)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new elencoContiDormientiRequest
                {
                    requestElements = elencocontidormienti.elencoConti
                };
                var response = proxy.elencoContiGiocoDormienti(request);
                var elencoResponse = new PgadGatewayElencoContiGiocoDormientiResponse(
                    response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp)
                {
                    NumeroDettaglioConti = response.responseElements.numeroDettagliConti
                };


                for (var i = 0; i < response.responseElements.numeroDettagliConti; i++)
                {
                    elencoResponse.ElencoConti.Add(response.responseElements.dettaglioConti[i]);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = elencocontidormienti?.elencoConti?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.elencoContiGiocoDormienti"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                return new PgadGatewayElencoContiGiocoDormientiResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
                ;
            }
        }

        public PgadGatewayInterrogazioneEstremiDocumentoResponse InterrogazioneEstremiDocumento(MessaggioInterrogazioneEstremiDocumento interrogazioneEstremiDocumento)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazioneEstremiDocumentoRequest
                {
                    requestElements = interrogazioneEstremiDocumento.InterrogazioneEstremiDocumento
                };
                var response = proxy.interrogazioneEstremiDocumento(request);
                var elencoResponse = new PgadGatewayInterrogazioneEstremiDocumentoResponse(
                    response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp)
                {
                    
                    NumeroDocumenti = response.responseElements.numDocumenti
                };


                for (var i = 0; i < response.responseElements.numDocumenti; i++)
                {
                    elencoResponse.Documenti.Add(response.responseElements.documento);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = interrogazioneEstremiDocumento?.InterrogazioneEstremiDocumento?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.interrogazioneEstremiDocumento"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";
                TraceLog.TraceError("PgadGatewayInterrogazioneEstremiDocumentoTitolareGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayInterrogazioneEstremiDocumentoResponse((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);

            }
        }

        public PgadGatewayAggiornaLimiteResponse AggiornaLimite(MessaggioAggiornaLimite aggiornaLimite)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new aggiornaLimiteRequest
                {
                    requestElements = aggiornaLimite.AggiornaLimite
                };
                var response = proxy.aggiornaLimite(request);

                return new PgadGatewayAggiornaLimiteResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = aggiornaLimite?.AggiornaLimite?.idTransazione ?? 0;
                var descrizioneErrore = $"{"PgadGateway.aggiornaLimite"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";

                TraceLog.TraceError("PgadGatewayMessaggioAggiornaLimiteGenericError ",
                    new Dictionary<string, object>()
                    {
                        { "Time", DateTime.Now },
                        { "IdTransazione", idTransazione },
                        { "Errore", descrizioneErrore }
                    });

                return new PgadGatewayAggiornaLimiteResponse(
                    (short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayInterrogazioneLimitiResponse InterrogazioneLimiti(MessaggioInterrogazioneLimiti interrogazioneLimiti)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazioneLimitiRequest
                {
                    requestElements = interrogazioneLimiti.InterrogazioneLimiti
                };
                var response = proxy.interrogazioneLimiti(request);

                var elencoResponse = new PgadGatewayInterrogazioneLimitiResponse(
                    response.responseElements.esitoRichiesta,
                    response.responseElements.idTransazione,
                    response.responseElements.idRicevuta,
                    response.responseElements.timeStamp)
                {
                    NumeroLimiti = response.responseElements.numeroLimiti
                };

                for (var i = 0; i < response.responseElements.numeroLimiti; i++)
                {
                    elencoResponse.Limiti.Add(response.responseElements.limite[i]);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = interrogazioneLimiti?.InterrogazioneLimiti?.idTransazione ?? 0;
                var descrizioneErrore = $"{"PgadGateway.interrogazioneLimiti"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";


                TraceLog.TraceError("PgadGatewayMessaggioInterrogazioneLimitiGenericError ",
                    new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayInterrogazioneLimitiResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione,
                    descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayGestioneAutoesclusioneTrasversaleResponse GestioneAutoesclusioneTrasversale(MessaggioGestioneAutoesclusioneTrasversale gestioneAutoesclusioneTrasversale)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new gestioneAutoesclusioneTrasversaleRequest
                {
                    requestElements = gestioneAutoesclusioneTrasversale.GestioneAutoesclusioneTrasversale
                };
                var response = proxy.gestioneAutoesclusioneTrasversale(request);

                return new PgadGatewayGestioneAutoesclusioneTrasversaleResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = gestioneAutoesclusioneTrasversale?.GestioneAutoesclusioneTrasversale?.idTransazione ?? 0;
                var descrizioneErrore = $"{"PgadGateway.gestioneAutoesclusioneTrasversale"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";

                TraceLog.TraceError("PgadGatewayMessaggioGestioneAutoesclusioneTrasversaleError ",
                    new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayGestioneAutoesclusioneTrasversaleResponse(
                    (short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayElencoContiAutoesclusiResponse ElencoContiAutoesclusi(MessaggioElencoContiAutoesclusi elencoContiAutoesclusi)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new elencoContiAutoesclusiRequest
                {
                    requestElements = elencoContiAutoesclusi.elencoConti
                };

                var response = proxy.elencoContiAutoesclusi(request);
                var elencoResponse = new PgadGatewayElencoContiAutoesclusiResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp);

                elencoResponse.NumeroDettaglioConti = response.responseElements.numeroDettagliContiAutoesclusi;

                if (elencoResponse.NumeroDettaglioConti > 0)
                {
                    elencoResponse.ElencoConti = new List<DettaglioContiAutoesclusi>();

                    for (var i = 0; i < response.responseElements.numeroDettagliContiAutoesclusi; i++)
                    {
                        elencoResponse.ElencoConti.Add(response.responseElements.dettaglioContiAutoesclusi[i]);
                    }
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = elencoContiAutoesclusi?.elencoConti?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.gestioneAutoesclusioneTrasversale"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";

                TraceLog.TraceError("PgadGatewayMessaggioElencoContiAutoesclusiError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayElencoContiAutoesclusiResponse((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayAperturaContoPersonaFisica2Response AperturaContoPersonaFisica2(MessaggioPersonaFisica2 personaFisica)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new aperturaContoPersonaFisica2Request
                {
                    requestElements = personaFisica.PersonaFisica
                };
                var response = proxy.aperturaContoPersonaFisica2(request);

                return new PgadGatewayAperturaContoPersonaFisica2Response(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {

                var idTransazione = personaFisica?.PersonaFisica?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.apriContoPersonaFisica"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayAperturaContoPersonaFisica2Error ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayAperturaContoPersonaFisica2Response((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayInterrogazioneSoggettoAutoesclusoResponse InterrogazioneSoggettoAutoescluso(MessaggioInterrogazioneSoggettoAutoescluso persona)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazioneSoggettoAutoesclusoRequest
                {
                    requestElements = persona.PersonaFisica
                };

                var response = proxy.interrogazioneSoggettoAutoescluso(request);
                var elencoResponse = new PgadGatewayInterrogazioneSoggettoAutoesclusoResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp);

                elencoResponse.NumeroDettaglioSoggettoAutoescluso = response.responseElements.numeroDettagliSoggettoAutoescluso;

                // Può essere solo 0 o 1
                if (elencoResponse.NumeroDettaglioSoggettoAutoescluso > 0)
                {
                    elencoResponse.ElencoDettaglioSoggettoAutoescluso = new List<DettaglioSoggettoAutoescluso>();
                    elencoResponse.ElencoDettaglioSoggettoAutoescluso.Add(response.responseElements.dettaglioSoggettoAutoescluso);

                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var descrizioneErrore =
                    $"{"PgadGateway.InterrogazioneSoggettoAutoescluso"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";

                var idTransazione = persona?.PersonaFisica?.idTransazione ?? 0;

                TraceLog.TraceError("PgadGatewayInterrogazioneSoggettoAutoesclusoGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayInterrogazioneSoggettoAutoesclusoResponse(
                    (short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse AggiornaPseudonimoContoDiGioco(MessaggioAggiornaPseudonimoContoDiGioco persona)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new aggiornaPseudonimoContoRequest
                {
                    requestElements = persona.PersonaFisica
                };

                var response = proxy.aggiornaPseudonimoConto(request);

                return new PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = persona?.PersonaFisica?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.AggiornaPseudonimoContoDiGioco"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";
                TraceLog.TraceError("PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayMessaggioAggiornaPseudonimoContoDiGiocoResponse(
                    (short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);

            }
        }

        public PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse AggiornaPostaElettronicaContoDiGioco(MessaggioAggiornaPostaElettronicaContoDiGioco persona)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new aggiornaPostaElettronicaContoRequest
                {
                    requestElements = persona.PersonaFisica
                };

                var response = proxy.aggiornaPostaElettronicaConto(request);

                return new PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = persona?.PersonaFisica?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.AggiornaPseudonimoContoDiGioco"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";

                TraceLog.TraceError("PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoGenericError ",
                    new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayMessaggioAggiornaPostaElettronicaContoDiGiocoResponse(
                    (short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);


            }
        }

        public PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse InterrogazioneStoriaSoggettoAutoescluso(MessaggioInterrogazioneStoriaSoggettoAutoescluso persona)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazioneStoriaSoggettoAutoesclusoRequest
                {
                    requestElements =  persona.PersonaFisica
                };

                var response = proxy.interrogazioneStoriaSoggettoAutoescluso(request);
                var elencoResponse = new PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse(
                    response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp)
                {
                    NumeroDettagliSoggettoAutoescluso = response.responseElements.numeroDettagliSoggettoAutoescluso
                };

                for (var i = 0; i < response.responseElements.numeroDettagliSoggettoAutoescluso; i++)
                {
                    elencoResponse.ElencoDettagliSoggettoAutoescluso.Add(response.responseElements.dettaglioSoggettoAutoescluso[i]);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var descrizioneErrore =
                    $"{"PgadGateway.InterrogazioneStoriaSoggettoAutoescluso"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";

                var idTransazione = persona?.PersonaFisica?.idTransazione ?? 0;

                TraceLog.TraceError("PgadGatewayInterrogazioneSoggettoAutoesclusoGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayInterrogazioneStoriaSoggettoAutoesclusoResponse(
                    (short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayInterrogazioneProvinciaResidenzaResponse InterrogazioneProvinciaResidenza(
            MessaggioInterrogazioneProvinciaResidenza interrogazioneProvinciaResidenza)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazioneProvinciaResidenzaContoRequest()
                {
                    requestElements = interrogazioneProvinciaResidenza.InterrogazioneProvinciaResidenza
                };
                var response = proxy.interrogazioneProvinciaResidenza(request);
                var interrogazioneProvinciaResidenzaResponse = new PgadGatewayInterrogazioneProvinciaResidenzaResponse(
                    response.responseElements.esitoRichiesta,
                    response.responseElements.idTransazione,
                    response.responseElements.idRicevuta,
                    response.responseElements.timeStamp)
                {
                    Provincia = response.responseElements.provincia
                };


                return interrogazioneProvinciaResidenzaResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = interrogazioneProvinciaResidenza?.InterrogazioneProvinciaResidenza?.idTransazione ??
                                    0;
                var descrizioneErrore =
                    $"PgadGateway.InterrogazioneProvinciaResidenza {CodiciRitorno[(int) EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";


                TraceLog.TraceError("PgadGatewayStatoGenericError ",
                    new Dictionary<string, object>()
                        {{"Time", DateTime.Now}, {"IdTransazione", idTransazione}, {"Errore", descrizioneErrore}});
                return new PgadGatewayInterrogazioneProvinciaResidenzaResponse((short) EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);

            }
        }

        public PgadGatewayInterrogazionePseudonimoResponse InterrogazionePseudonimo(MessaggioInterrogazionePseudonimo interrogazionePseudonimo)
            {
                try
                {
                    var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                    var request = new interrogazionePseudonimoContoRequest()
                    {
                        requestElements = interrogazionePseudonimo.InterrogazionePseudonimo
                    };
                    var response = proxy.interrogazionePseudonimo(request);
                    var interrogazionePseudonimoResponse = new PgadGatewayInterrogazionePseudonimoResponse(
                        response.responseElements.esitoRichiesta,
                        response.responseElements.idTransazione,
                        response.responseElements.idRicevuta,
                        response.responseElements.timeStamp)
                    {
                        Pseudonimo = response.responseElements.pseudonimo
                    };


                    return interrogazionePseudonimoResponse;
                }
                catch (Exception ex)
                {
                    var idTransazione = interrogazionePseudonimo?.InterrogazionePseudonimo?.idTransazione ?? 0;
                    var descrizioneErrore =
                        $"PgadGateway.InterrogazionePseudonimo {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";


                    TraceLog.TraceError("PgadGatewayStatoGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                    return new PgadGatewayInterrogazionePseudonimoResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                        false, "", DateTime.Now);

                }
            }

        public PgadGatewayInterrogazionePostaElettronicaResponse InterrogazionePostaElettronica(MessaggioInterrogazionePostaElettronica interrogazionePostaElettronica)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazionePostaElettronicaContoRequest()
                {
                    requestElements = interrogazionePostaElettronica.InterrogazionePostaElettronica
                };
                var response = proxy.interrogazionePostaElettronica(request);
                var interrogazionePostaElettronicaResponse = new PgadGatewayInterrogazionePostaElettronicaResponse(
                    response.responseElements.esitoRichiesta,
                    response.responseElements.idTransazione,
                    response.responseElements.idRicevuta,
                    response.responseElements.timeStamp)
                {
                    PostaElettronica = response.responseElements.postaElettronica
                };


                return interrogazionePostaElettronicaResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = interrogazionePostaElettronica?.InterrogazionePostaElettronica?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"PgadGateway.InterrogazionePostaElettronica {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";


                TraceLog.TraceError("PgadGatewayStatoGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayInterrogazionePostaElettronicaResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);

            }            
        }

        public PgadGatewayElencoContiAutoesclusi2Response ElencoContiAutoesclusi2(MessaggioElencoContiAutoesclusi2 elencoContiAutoesclusi)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new elencoContiAutoesclusi2Request
                {
                    requestElements = elencoContiAutoesclusi.elencoConti
                };

                var response = proxy.elencoContiAutoesclusi2(request);
                var elencoResponse = new PgadGatewayElencoContiAutoesclusi2Response(response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp);

                elencoResponse.NumeroDettaglioConti = response.responseElements.numeroDettagliContiAutoesclusi;

                if (elencoResponse.NumeroDettaglioConti > 0)
                {
                    elencoResponse.ElencoConti = new List<DettaglioContiAutoesclusi>();

                    for (var i = 0; i < response.responseElements.numeroDettagliContiAutoesclusi; i++)
                    {
                        elencoResponse.ElencoConti.Add(response.responseElements.dettaglioContiAutoesclusi[i]);
                    }
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = elencoContiAutoesclusi?.elencoConti?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.ElencoContiAutoesclusi2"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";

                TraceLog.TraceError("PgadGatewayMessaggioElencoContiAutoesclusi2Error ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayElencoContiAutoesclusi2Response((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse StornoMovimentazioneBonusConto(MessaggioStornoMovimentoBonusConto stornoMovimentoBonus)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new stornoMovimentazioneBonusContoRequest
                {
                    requestElements = stornoMovimentoBonus.StornoMovimentazioneBonusConto
                };
                var response = proxy.stornoMovimentazioneBonusConto(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var descrizioneErrore =
                    $"{"PgadGateway.stornoMovimentazioneBonusConto"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";
                long idTransazione = 0;

                if (stornoMovimentoBonus != null && stornoMovimentoBonus.StornoMovimentazioneBonusConto != null)
                    idTransazione = stornoMovimentoBonus.StornoMovimentazioneBonusConto.idTransazione;
                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }
        
        public PgadGatewayResponse MovimentazioneBonusConto2(MessaggioMovimentoBonusConto2 movimentoBonus)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new movimentazioneBonusConto2Request
                {
                    requestElements = movimentoBonus.MovimentoBonus
                };
                var response = proxy.movimentazioneBonusConto2(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = movimentoBonus?.MovimentoBonus?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.movimentazioneBonusConto2"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayAperturaContoPersonaFisica3Response AperturaContoGiocoPersonaFisica3(MessaggioPersonaFisica3 personaFisica)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new aperturaContoPersonaFisica3Request
                {
                    requestElements = personaFisica.PersonaFisica

                };
                var response = proxy.aperturaContoPersonaFisica3(request);

                return new PgadGatewayAperturaContoPersonaFisica3Response(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {

                var idTransazione = personaFisica?.PersonaFisica?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.apriContoPersonaFisica"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayAperturaContoPersonaFisica2Error ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayAperturaContoPersonaFisica3Response((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayResponse AggiornaDocumentoTitolareContoGioco2(MessaggioAggiornamentoDocumentoTitolareContoGioco2 aggiornaDocumento)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new aggiornaDatiDocumentoTitolareConto2Request()
                {
                    requestElements = aggiornaDocumento.AggiornaDocumento
                };
                var response = proxy.aggiornaDocumentoTitolareContoGioco2(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = aggiornaDocumento?.AggiornaDocumento?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.AggiornaDocumento"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }
        
        public PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response InterrogazioneEstremiDocumentoTitolareContoGioco2(MessaggioInterrogazioneEstremiDocumentoTitolareContoGioco2 interrogazioneEstremiDocumento)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new interrogazioneEstremiDocumento2Request
                {
                    requestElements = interrogazioneEstremiDocumento.InterrogazioneEstremiDocumento
                };
                var response = proxy.interrogazioneEstremiDocumentoTitolareContoGioco2(request);
                var elencoResponse = new PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response(
                    response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp)
                {

                    NumeroDocumenti = response.responseElements.numDocumenti
                };

                elencoResponse.TipoFornituraDatiPersonali = response.responseElements.tipoFornituraDatiPersonali;

                for (var i = 0; i < response.responseElements.numDocumenti; i++)
                {
                    elencoResponse.Documenti.Add(response.responseElements.documento);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = interrogazioneEstremiDocumento?.InterrogazioneEstremiDocumento?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.interrogazioneEstremiDocumento"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.ToString()} ";
                TraceLog.TraceError("PgadGatewayInterrogazioneEstremiDocumentoTitolareGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayInterrogazioneEstremiDocumentoTitolareContoGioco2Response((short)EsitoRichiesta.GENERIC_ERROR,
                    idTransazione, descrizioneErrore, false, "", DateTime.Now);

            }
        }

        public PgadGatewayResponse ApriContoPersonaGiuridica2(MessaggioPersonaGiuridica2 personaGiuridica)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);

                var request = new aperturaContoPersonaGiuridica2Request
                {
                    requestElements = personaGiuridica.PersonaGiuridica
                };

                var response = proxy.aperturaContoPersonaGiuridica2(request);

                return new PgadGatewayResponse(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);
            }
            catch (Exception ex)
            {
                var idTransazione = personaGiuridica?.PersonaGiuridica?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.apriContoPersonaGiuridica"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });
                return new PgadGatewayResponse((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore,
                    false, "", DateTime.Now);
            }
        }

        public PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response ElencoContiGiocoSenzaSubregistrazione2(
            MessaggioElencoContiGiocoSenzaSubregistrazione2 elencoCdGSenzaSubregistrazione)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new elencoContiSenzaSubregistrazione2Request
                {
                    requestElements = elencoCdGSenzaSubregistrazione.elencoConti
                };
                var response = proxy.elencoContiGiocoSenzaSubregistrazione2(request);
                var elencoResponse = new PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response(response.responseElements.esitoRichiesta, response.responseElements.idTransazione, response.responseElements.idRicevuta, response.responseElements.timeStamp);

                elencoResponse.NumeroDettaglioConti = response.responseElements.numeroContiGioco;
                for (var i = 0; i < response.responseElements.numeroContiGioco; i++)
                {
                    elencoResponse.ElencoConti.Add(response.responseElements.contoGioco[i]);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = elencoCdGSenzaSubregistrazione?.elencoConti?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.elencoContiGiocoSenzaSubregistrazione"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                TraceLog.TraceError("PgadGatewayElencoContiGiocoSenzaSubregistrazioneGenericError ", new Dictionary<string, object>() { { "Time", DateTime.Now }, { "IdTransazione", idTransazione }, { "Errore", descrizioneErrore } });

                return new PgadGatewayElencoContiGiocoSenzaSubregistrazione2Response((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
            }
        }

        public PgadGatewayElencoContiGiocoDormienti2Response ElencoContiGiocoDormienti2(MessaggioElencoContiGiocoDormienti2 elencocontidormienti)
        {
            try
            {
                var proxy = new PgadProxy(this.UserName, this.TitolareSistema);
                var request = new elencoContiDormienti2Request
                {
                    requestElements = elencocontidormienti.elencoConti
                };
                var response = proxy.elencoContiGiocoDormienti2(request);
                var elencoResponse = new PgadGatewayElencoContiGiocoDormienti2Response(
                    response.responseElements.esitoRichiesta, response.responseElements.idTransazione,
                    response.responseElements.idRicevuta, response.responseElements.timeStamp)
                {
                    NumeroDettaglioConti = response.responseElements.numeroContiGiocoSaldo
                };


                for (var i = 0; i < response.responseElements.numeroContiGiocoSaldo; i++)
                {
                    elencoResponse.ElencoConti.Add(response.responseElements.contoGiocoSaldo[i]);
                }

                return elencoResponse;
            }
            catch (Exception ex)
            {
                var idTransazione = elencocontidormienti?.elencoConti?.idTransazione ?? 0;
                var descrizioneErrore =
                    $"{"PgadGateway.elencoContiGiocoDormienti"} {CodiciRitorno[(int)EsitoRichiesta.GENERIC_ERROR]} {ex.GetType()} {ex.Message} {((ex.InnerException != null) ? ex.InnerException.Message : "")} ";

                return new PgadGatewayElencoContiGiocoDormienti2Response((short)EsitoRichiesta.GENERIC_ERROR, idTransazione, descrizioneErrore, false, "", DateTime.Now);
                ;
            }
        }
    }
}
