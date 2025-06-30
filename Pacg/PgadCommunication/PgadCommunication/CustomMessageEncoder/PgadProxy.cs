using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;
using System.ServiceModel.Security;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Reflection;
using System.Runtime.Serialization;
using PgadCommunication.Pgad;
using IntralotWebLib;
using PgadCommunication.Exceptions;

namespace PgadCommunication.CustomMessageEncoder
{
    [DataContract(Namespace = "PgadCommunication.CustomMessageEncoder")]
    public class PgadProxy
    {
        private readonly ContiDiGiocoWSClient proxy;
        private readonly PgadClientCredentials credentials;
        public PgadProxy(string UserName, int TitolareSistema)
        {
            try
            {
                proxy = new ContiDiGiocoWSClient();
                //Aggiungo lo Username token nel binding
                credentials = new PgadClientCredentials(UserName, TitolareSistema);
                //proxy.InnerChannel.OperationTimeout.
                proxy.ChannelFactory.Endpoint.Behaviors.Remove(typeof(ClientCredentials));
                proxy.ChannelFactory.Endpoint.Behaviors.Add(credentials);
                proxy.Endpoint.Binding = AddUserNameSupportingTokenToBinding(proxy.Endpoint.Binding);
                proxy.ClientCredentials.UserName.UserName = UserName;
                //Aggiungere i certificati sul server per modificare il tipo di validazione
                proxy.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

                //TODO: SPERILLI
                //proxy.Endpoint.Behaviors.Add(new PgadProxyInspector());

            }
            catch (ApplicationException ex)
            {
                throw new PgadProxyNotCreated("PgadProxy: Proxy not exists.", ex);
            }
        }
        public string Username
        {
            get
            {
                if (proxy == null)
                    throw new PgadProxyNotCreated();
                return proxy.ClientCredentials.UserName.UserName;
            }
            set
            {
                if (proxy == null)
                    throw new PgadProxyNotCreated();
                proxy.ClientCredentials.UserName.UserName = value;
            }
        }

        public Binding AddUserNameSupportingTokenToBinding(Binding binding)
        {
            BindingElementCollection elements = binding.CreateBindingElements();

            SecurityBindingElement security = elements.Find<SecurityBindingElement>();
            if (security != null)
            {
                security.EnableUnsecuredResponse = true;
                UserNameSecurityTokenParameters tokenParameters = new UserNameSecurityTokenParameters();
                tokenParameters.InclusionMode = SecurityTokenInclusionMode.Never;
                tokenParameters.RequireDerivedKeys = false;
                security.EndpointSupportingTokenParameters.Signed.Add(tokenParameters);
                security.IncludeTimestamp = false;
                CustomBinding newBinding = new CustomBinding(elements.ToArray());
                newBinding.SendTimeout = binding.SendTimeout;
                newBinding.ReceiveTimeout = binding.ReceiveTimeout;
                return newBinding;
            }
            throw new ArgumentException("Binding contains no SecurityBindingElement");
        }

        public aperturaContoPersonaGiuridicaResponse aperturaContoPersonaGiuridica(aperturaContoPersonaGiuridicaRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                aperturaContoPersonaGiuridicaResponse response = proxy.aperturaContoPersonaGiuridica(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }


        public movimentazioneContoResponse movimentazioneConto(movimentazioneContoRequest request)
        {
            var par = new Dictionary<string, object>
            {
                {"request.idTransazione", request.requestElements.idTransazione},
                {"idCn", request.requestElements.idCn},
                {"codiceConto", request.requestElements.codiceConto}
            };
            TraceLog.TraceInformation("start", par);
            var response = proxy.movimentazioneConto(request);
            par.Add("response.idTransazione", response.responseElements.idTransazione);
            par.Add("idRicevuta", response.responseElements.idRicevuta);
            par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
            TraceLog.TraceInformation("end", par);

            return response;
        }


        public movimentazioneBonusContoResponse movimentazioneBonusConto(movimentazioneBonusContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.movimentazioneBonusConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }


        public cambioStatoContoResponse cambioStatoConto(cambioStatoContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.cambioStatoConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }


        public saldoContoResponse saldoConto(saldoContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.saldoConto(request); ;
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }


        public modificaProvinciaResidenzaContoResponse modificaProvinciaResidenzaConto(modificaProvinciaResidenzaContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.modificaProvinciaResidenzaConto(request); ;
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazioneStatoContoResponse interrogazioneStatoConto(interrogazioneStatoContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazioneStatoConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public migrazioneContoResponse migrazioneConto(migrazioneContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>();
                par.Add("request.idTransazione", request.requestElements.idTransazione);
                par.Add("idCn", request.requestElements.idCn);
                par.Add("codiceContoOriginario", request.requestElements.codiceContoOriginario);
                par.Add("codiceContoDestinazione", request.requestElements.codiceContoDestinazione);
                par.Add("codiceFiscale", request.requestElements.codiceFiscale);
                TraceLog.TraceInformation("start", par);
                var response = proxy.migrazioneConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public aggiornaDatiDocumentoTitolareContoResponse aggiornaDatiDocumentoTitolareConto(aggiornaDatiDocumentoTitolareContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto},
                    {"documento.numero", request.requestElements.documento.numero},
                    {"documento.tipo", request.requestElements.documento.tipo}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.aggiornaDatiDocumentoTitolareConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public riepilogoOperazioniMovimentazioneResponse riepilogoOperazioniMovimentazione(riepilogoOperazioniMovimentazioneRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {
                        "Data",
                        $"{request.requestElements.data.anno}-{request.requestElements.data.mese}-{request.requestElements.data.giorno}"
                    }
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.riepilogoOperazioniMovimentazione(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public riepilogoOperazioniServizioResponse riepilogoOperazioniServizio(riepilogoOperazioniServizioRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>();
                par.Add("request.idTransazione", request.requestElements.idTransazione);
                par.Add("idCn", request.requestElements.idCn);
                par.Add("data",
                    $"{request.requestElements.data.anno}-{request.requestElements.data.mese}-{request.requestElements.data.giorno}");
                TraceLog.TraceInformation("start", par);
                var response = proxy.riepilogoOperazioniServizio(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public stornoMovimentazioneContoResponse stornoMovimentazioneConto(stornoMovimentazioneContoRequest request)
        {
            var par = new Dictionary<string, object>
            {
                {"request.idTransazione", request.requestElements.idTransazione},
                {"idCn", request.requestElements.idCn},
                {"codiceConto", request.requestElements.codiceConto}
            };
            TraceLog.TraceInformation("start", par);
            var response = proxy.stornoMovimentazioneConto(request);
            par.Add("response.idTransazione", response.responseElements.idTransazione);
            par.Add("idRicevuta", response.responseElements.idRicevuta);
            par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
            TraceLog.TraceInformation("end", par);

            return response;
        }

        public subregistrazione2Response subregistrazione2(subregistrazione2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.subregistrazione2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public elencoContiSenzaSubregistrazioneResponse elencoContiGiocoSenzaSubregistrazione(elencoContiSenzaSubregistrazioneRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.elencoContiSenzaSubregistrazione(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public contoDormienteResponse contoGiocoDormiente(contoDormienteRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.contoDormiente(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public elencoContiDormientiResponse elencoContiGiocoDormienti(elencoContiDormientiRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>();
                par.Add("request.idTransazione", request.requestElements.idTransazione);
                par.Add("idCn", request.requestElements.idCn);
                TraceLog.TraceInformation("start", par);
                var response = proxy.elencoContiDormienti(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazioneEstremiDocumentoResponse interrogazioneEstremiDocumento(interrogazioneEstremiDocumentoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazioneEstremiDocumento(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public aggiornaLimiteResponse aggiornaLimite(aggiornaLimiteRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.aggiornaLimite(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazioneLimitiResponse interrogazioneLimiti(interrogazioneLimitiRequest request)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("request.idTransazione", request.requestElements.idTransazione);
                par.Add("idCn", request.requestElements.idCn);
                par.Add("codiceConto", request.requestElements.codiceConto);
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazioneLimiti(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public gestioneAutoesclusioneTrasversaleResponse gestioneAutoesclusioneTrasversale(gestioneAutoesclusioneTrasversaleRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.gestioneAutoesclusioneTrasversale(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        //Sostituito dal messaggio elencoContiAutoesclusi2 (4.36) che sostituisce i due campi INIZIO E FINE da short a int
        public elencoContiAutoesclusiResponse elencoContiAutoesclusi(elencoContiAutoesclusiRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.elencoContiAutoesclusi(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public aperturaContoPersonaFisica2Response aperturaContoPersonaFisica2(aperturaContoPersonaFisica2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.aperturaContoPersonaFisica2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }
        public aperturaContoPersonaFisica3Response aperturaContoPersonaFisica3(aperturaContoPersonaFisica3Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.aperturaContoPersonaFisica3(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazioneSoggettoAutoesclusoResponse interrogazioneSoggettoAutoescluso(interrogazioneSoggettoAutoesclusoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazioneSoggettoAutoescluso(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public aggiornaPseudonimoContoResponse aggiornaPseudonimoConto(aggiornaPseudonimoContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.aggiornaPseudonimoConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public aggiornaPostaElettronicaContoResponse aggiornaPostaElettronicaConto(aggiornaPostaElettronicaContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.aggiornaPostaElettronicaConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazioneStoriaSoggettoAutoesclusoResponse interrogazioneStoriaSoggettoAutoescluso(interrogazioneStoriaSoggettoAutoesclusoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazioneStoriaSoggettoAutoescluso(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazioneProvinciaResidenzaContoResponse interrogazioneProvinciaResidenza(interrogazioneProvinciaResidenzaContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazioneProvinciaResidenzaConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazionePseudonimoContoResponse interrogazionePseudonimo(interrogazionePseudonimoContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazionePseudonimoConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazionePostaElettronicaContoResponse interrogazionePostaElettronica(interrogazionePostaElettronicaContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazionePostaElettronicaConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        //4.36 (in sostituizione del messaggio elencoContiAutoesclusi (4.25) che sostituisce i due campi INIZIO E FINE da short a int)
        public elencoContiAutoesclusi2Response elencoContiAutoesclusi2(elencoContiAutoesclusi2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.elencoContiAutoesclusi2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        /*
         Nuovo messaggio 4.37 Storno movimentazione bonus conto di gioco. 
         Il messaggio consente al concessionario di comunicare lo storno di una precedente movimentazione bonus su un conto di gioco telematico. 
         Il concessionario ha 30 giorni di tempo per inviare la transazione. Lo storno può essere di tre tipi, 
         cosi come specificato nel campo Tipo Storno Bonus:
	        o 1: Storno bonus ordinario. Da utilizzare quando l’importo del bonus da stornare è inferiore o uguale all’importo del bonus assegnato;
	        o 2: Storno bonus da vincita. Da utilizzare quando l’importo del bonus da stornare è superiore all’importo del bonus assegnato;
	        o 3: Storno bonus da conversione. Da utilizzare quando un bonus deve essere trasformato in un importo prelevabile. 
		 In questo caso il saldo del conto rimane inalterato ma viene convertita e sottratta solo la parte bonus del saldo per l’importo indicato nel messaggio di storno. 
		 Esempio: un conto di gioco ha 300 euro di saldo di cui 100 euro di bonus, se si vuole trasformare 80 dei 100 euro di bonus in importo prelevabile, 
		 sarà necessario inviare il messaggio 4.37 con tipo storno bonus 3 e importo bonus pari a 80 euro, al termine dell’operazione il nuovo saldo sarà sempre di 300 euro di cui 20 di bonus.
         */
        public stornoMovimentazioneBonusContoResponse stornoMovimentazioneBonusConto(stornoMovimentazioneBonusContoRequest request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.stornoMovimentazioneBonusConto(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        //Nuovo messaggio 4.38 Movimentazione bonus conto di gioco 2. 
        //Il messaggio consente al concessionario di comunicare la movimentazione dei bonus su un conto di gioco telematico. 
        //Rispetto al messaggio 4.4 Movimentazione bonus conto di gioco, è consentito l’invio del solo bonus e non dello storno.
        public movimentazioneBonusConto2Response movimentazioneBonusConto2(movimentazioneBonusConto2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.movimentazioneBonusConto2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public aggiornaDatiDocumentoTitolareConto2Response aggiornaDocumentoTitolareContoGioco2(aggiornaDatiDocumentoTitolareConto2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto},
                    {"documento.numero", request.requestElements.documento.numero},
                    {"documento.tipo", request.requestElements.documento.tipo}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.aggiornaDatiDocumentoTitolareConto2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public interrogazioneEstremiDocumento2Response interrogazioneEstremiDocumentoTitolareContoGioco2(interrogazioneEstremiDocumento2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.interrogazioneEstremiDocumento2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public aperturaContoPersonaGiuridica2Response aperturaContoPersonaGiuridica2(aperturaContoPersonaGiuridica2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn},
                    {"codiceConto", request.requestElements.codiceConto}
                };
                TraceLog.TraceInformation("start", par);
                aperturaContoPersonaGiuridica2Response response = proxy.aperturaContoPersonaGiuridica2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public elencoContiSenzaSubregistrazione2Response elencoContiGiocoSenzaSubregistrazione2(elencoContiSenzaSubregistrazione2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>
                {
                    {"request.idTransazione", request.requestElements.idTransazione},
                    {"idCn", request.requestElements.idCn}
                };
                TraceLog.TraceInformation("start", par);
                var response = proxy.elencoContiSenzaSubregistrazione2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public elencoContiDormienti2Response elencoContiGiocoDormienti2(elencoContiDormienti2Request request)
        {
            try
            {
                var par = new Dictionary<string, object>();
                par.Add("request.idTransazione", request.requestElements.idTransazione);
                par.Add("idCn", request.requestElements.idCn);
                TraceLog.TraceInformation("start", par);
                var response = proxy.elencoContiDormienti2(request);
                par.Add("response.idTransazione", response.responseElements.idTransazione);
                par.Add("idRicevuta", response.responseElements.idRicevuta);
                par.Add("esitoRichiesta", response.responseElements.esitoRichiesta);
                TraceLog.TraceInformation("end", par);

                return response;
            }
            catch (SecurityNegotiationException exception)
            {
                throw new PgadSSLSogeiException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (FaultException exception)
            {
                throw new PgadFaultException(MethodBase.GetCurrentMethod().Name, exception);
            }
            catch (ApplicationException exception)
            {
                throw new PgadException(MethodBase.GetCurrentMethod().Name, exception);
            }
        }
    }

}
