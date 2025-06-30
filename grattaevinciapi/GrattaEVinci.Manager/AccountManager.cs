using System.Data.SqlClient;
using System.Globalization;
using System.ServiceModel;
using System.Text.RegularExpressions;
using Backend.Common.Contracts.Account.Requests;
using Backend.Common.Models.Account;
using Backend.Proxy;
using Gamenet.Common.Enum;
using Gamenet.Common.Exceptions;
using Gamenet.Logger;
using Gamenet.Proxy;
using Gamenet.Proxy.Contract.Seamless;
using Gamenet.RetryIntegration.Manager;
using GestioneAnagraficaReference;
using GrattaEVinciReference;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Contract.Responses;
using GrattaEVinci.Common.Enum;
using GrattaEVinci.Common.Model;
using GrattaEVinci.Common.ModelConfiguration;
using GrattaEVinci.Common.Signature;
using GrattaEVinci.Common.Utility;
using GrattaEVinci.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using CheckTokenRequest = Gamenet.Proxy.Contract.Seamless.CheckTokenRequest;
using LogoutRequest = GrattaEVinci.Common.Contract.Requests.LogoutRequest;

namespace GrattaEVinci.Manager
{
    public class AccountManager
    {
        private readonly SeamlessProxy _seamless;
        private readonly ILog _logger;
        private readonly GrattaEVinciDao _grattaEVinciDao;
        private readonly BackendAccountProxy _backendAccountProxy;
        private readonly AuthBackendProxy _authBackendProxy;

        private readonly IRetryIntegrationManager _retryIntegrationManager;
        private readonly Lazy<CashierManager> _cashierManager;
        private readonly string _class;
        private readonly IOptions<Config> _appSettings;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountManager(SeamlessProxy seamless, ILog logger,
            GrattaEVinciDao grattaEVinciDao,
            BackendAccountProxy backendAccountProxy, IRetryIntegrationManager retryIntegrationManager,
            AuthBackendProxy authBackendProxy, IOptions<Config> appSettings,
            IHttpContextAccessor contextAccessor, IServiceProvider services)
        {
            _seamless = seamless;
            _logger = logger;
            _grattaEVinciDao = grattaEVinciDao;
            _backendAccountProxy = backendAccountProxy;
            _class = GetType().Name;
            _retryIntegrationManager = retryIntegrationManager;
            _authBackendProxy = authBackendProxy;
            _appSettings = appSettings;
            _contextAccessor = contextAccessor;
            _cashierManager = new Lazy<CashierManager>(services.GetRequiredService<CashierManager>);
        }

        public async Task<SaldoContoGiocoResponse> CheckToken(CheckTokenRequestModel request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            var response = new SaldoContoGiocoResponse();
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();

            int idUtente = 0;

            try
            {
                if (string.IsNullOrEmpty(request.Token)
                    || string.IsNullOrEmpty(request.CodiceContratto)
                    || (!string.IsNullOrEmpty(request.IdTransazione) &&
                        !Regex.IsMatch(request.IdTransazione, @"^[0-9]+$"))
                )
                {
                    throw new ParameterNotValid();
                }

                var utenteBrand = await _grattaEVinciDao.GetIdUtenteDaCodiceContratto(request.CodiceContratto);

                if (utenteBrand == null)
                    throw new ParameterNotValid("Utente non trovato");

                idUtente = utenteBrand.IdUtente;

                await CheckSeamlessToken(request.Token, idUtente.ToString(), null, request.CodiceContratto);

                response.codiceRisultato = ((int)Result.Successo).ToString();
                response.descrizioneRisultato = "Valido";

            }
            catch (TokenNotValidException e)
            {
                _logger.Warning($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Non_Valido).ToString();
                response.descrizioneRisultato = Result.Non_Valido.ToString().NormalizeResultCodeDescription();
            }
            catch (ParameterNotValid e)
            {
                _logger.Warning($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (SqlException e)
            {
                _logger.Error($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato =
                    Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
            }
            catch (Exception e)
            {
                _logger.Error($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato =
                    Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            finally
            {
                if (response.codiceRisultato == ((int)Result.Successo).ToString())
                {
                    var balance = await _cashierManager.Value.GetBalance(idUtente, correlationId);
                    response.saldoContoGioco = Map.ImportInEuroDecimal(balance.Saldo + balance.SaldoBonus ?? 0).ToString(CultureInfo.InvariantCulture).ReplaceSpecialCharToPoint();
                }
            }

            return response;
        }

        public async Task CheckSeamlessToken(string token, string idUtente, string? idTransazione = null, string? conto = null)
        {
            if (!string.IsNullOrEmpty(idTransazione))
            {
                var isValid = await _grattaEVinciDao.VerificaTokenTransazione(idUtente.ToInt(), token, idTransazione);

                if (!isValid)
                    throw new TokenNotValidException("Transazione non associato al token");

                return;
            }

            var checkTokenResponse = await _seamless.CheckToken(new CheckTokenRequest
            {
                IdCanale = (int)Source.GrattaEVinci,
                TipoUtente = TipoUtente.Web,
                Token = token
            });

            if (checkTokenResponse.ResultCode != (int)ResultCode.Success)
            {
                //In caso il token non sia valido controllo se è un token utilizzato per la verifica del dettaglio schedina
                #region verifica token per visualizzazione dettaglio schedina

                try
                {
                    int idxCnt = token.LastIndexOf(conto, StringComparison.Ordinal);
                    if (idxCnt == -1)
                    {
                        _logger.Information("numero conto non presente nel token");
                        throw new Exception();
                    }

                    int lenCnt = conto.Length;
                    idxCnt += lenCnt;

                    string strData = token.Substring(idxCnt, 8);

                    // var data = DateTime.ParseExact(strData, "ddMMyyyy", null);

                    DateTime.ParseExact(strData, "ddMMyyyy", null);

                    checkTokenResponse.ResultCode = ResultCode.Success;
                    checkTokenResponse.Message = "Success";
                }
                catch
                {
                    throw new TokenNotValidException(checkTokenResponse.Message);
                }

                #endregion verifica token per visualizzazione dettaglio schedina
            }
            else
            {
                if (checkTokenResponse.IdUtente != Convert.ToInt32(idUtente))
                    throw new ValidateTokenException("TOKEN NON ASSOCIATO ALL'UTENTE");
            }
        }

        public async Task<login> Login(LoginRequest request, string ipUtente)
        {
            Brand brand = Firma.GetBrandDaCodiceConcessione(request.idRivenditore, _appSettings);
            request.password = request.password.Tosha256();

            var method = ExtensionMethod.GetActualAsyncMethodName();
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();

            _logger.Information($"Nome: {_class}.{method} - Request: {request.ToLoggableRequest()}");

            login response = new login();

            try
            {
                if (string.IsNullOrEmpty(request.idRivenditore) || string.IsNullOrEmpty(request.password) || string.IsNullOrEmpty(request.username))
                    throw new ParameterNotValid();

                var getUtente = await _backendAccountProxy.GetUtenteByUsername(new RequestGetUtenteByUsername()
                {
                    Username = request.username,
                    IdCanale = (int)Source.AppGrattaEVinci,
                    CodiceTransazione = correlationId,
                    Brand = brand
                });

                var utente = getUtente.Utente;

                if (utente == null)
                    throw new UserNotFoundException("Utente non trovato");

                if (utente.UserName.ToUpper() != request.username.ToUpper() || utente.Password != request.password)
                    throw new UserNotFoundException("Username o password errati");

                var usernameUtentGbp =
                    utente.ListaUsernameProviderCategoria.FirstOrDefault(lu =>
                        lu.IdProvider == (int)Provider.Lottomatica && lu.IdCategoria == (int)GameType.GrattaEVinci);

                if (usernameUtentGbp == null)
                {
                    var resRegistraConto = await RegistraConto(new RegistraModificaContoRequest()
                    {
                        IdUtente = utente.IdUtente
                    });
                    if (resRegistraConto.ResultCode != ResultCode.Success)
                        throw new Exception($"Errore nella registrazione del contratto: {resRegistraConto.Message}");
                }

                var resLogin = await _authBackendProxy.LoginLight(new RequestLoginLight
                {
                    CodiceTransazione = correlationId,
                    IdCanale = (int)Source.AppGrattaEVinci,
                    Username = request.username,
                    Password = request.password,
                    Vertical = Verticale.Lotterie,
                    Brand = brand,
                    IpUtente = ipUtente
                });

                if (resLogin.ResultCode != ResultCode.Success)
                    throw new SeamlessException(resLogin.Message);

                var balance = await _cashierManager.Value.GetBalance(utente.IdUtente, correlationId);
                
                response.codiceRisultato = ((int)Result.Successo).ToString();
                response.descrizioneRisultato = Result.Successo.ToString().NormalizeResultCodeDescription();
                response.idContoGioco = utente.CodiceContratto;
                response.nickname = utente.UserName;
                response.token = resLogin.AuthToken;
                response.saldoContoGioco = Map.ImportInEuroDecimal(balance.Saldo + balance.SaldoBonus ?? 0).ToString(CultureInfo.InvariantCulture).ReplaceSpecialCharToPoint();

                await _grattaEVinciDao.SessionStart(utente.IdUtente, resLogin.AuthToken);
            }
            catch (ParameterNotValid e)
            {
                _logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (SeamlessException e)
            {
                _logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            catch (UserNotFoundException e)
            {
                _logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)ResultCodeAuth.Autenticazione_Fallita).ToString();
                response.descrizioneRisultato =
                    ResultCodeAuth.Autenticazione_Fallita.ToString().NormalizeResultCodeDescription();
            }
            catch (Exception e)
            {
                _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }

            return response;
        }

        public async Task<logout> Logout(LogoutRequest request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();
            var response = new logout();

            try
            {
                if (string.IsNullOrEmpty(request.idRivenditore) || string.IsNullOrEmpty(request.IdContoGioco)
                    || string.IsNullOrEmpty(request.token))
                    throw new ParameterNotValid();

                var resValidaToken = await _seamless.CheckToken(new CheckTokenRequest()
                {
                    IdCanale = (int)Source.AppGrattaEVinci,
                    Token = request.token,
                });

                var utenteBrand = await _grattaEVinciDao.GetIdUtenteDaCodiceContratto(request.IdContoGioco);
                var idUtente = utenteBrand.IdUtente;

                if (utenteBrand == null)
                    throw new ParameterNotValid("Utente non trovato");

                if (resValidaToken.ResultCode != ResultCode.Success || resValidaToken.IdUtente != idUtente)
                    throw new ParameterNotValid();

                var verificaSessioneGb = await _grattaEVinciDao.Session_Verify(idUtente, request.token);

                if (!verificaSessioneGb)
                    throw new ParameterNotValid();

                var resLogout = await _authBackendProxy.Logout(new RequestLogout()
                {
                    AuthToken = request.token,
                    CodiceTransazione = correlationId,
                    IdCanale = (int)Source.AppGrattaEVinci,
                    IdUtente = idUtente,
                    IdCanaleLogout = (int)Source.AppGrattaEVinci,
                    Brand = utenteBrand.IdBrand,
                });

                if (resLogout.ResultCode != ResultCode.Success)
                    throw new SeamlessException(resLogout.Message);

                response.codiceRisultato = ((int)Result.Successo).ToString();
                response.descrizioneRisultato = Result.Successo.ToString().NormalizeResultCodeDescription();

                await _grattaEVinciDao.SessionEnd(idUtente, request.token);
            }
            catch (ParameterNotValid e)
            {
                _logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (SeamlessException e)
            {
                _logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            catch (Exception e)
            {
                _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }

            return response;
        }

        public async Task<ResponseBase> RegistraConto(RegistraModificaContoRequest request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            int idUtente = request.IdUtente;
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();
            
            ResponseBase response = new ResponseBase();
            try
            {
                var utente = await GetUtente(idUtente);
                var usernameUtentGbp =
                    utente.ListaUsernameProviderCategoria.FirstOrDefault(lu =>
                        lu.IdProvider == (int)Provider.Lottomatica && lu.IdCategoria == (int)GameType.GrattaEVinci);

                if (usernameUtentGbp != null)
                {
                    response.SetSuccess();
                }
                else
                {
                    var anagrafica = new Anagrafica()
                    {
                        nome = utente.Nome,
                        cognome = utente.Cognome,
                        cod_fiscale = utente.CodiceFiscale
                    }.SetLimitiLol();

                    var xmlAnagrafica = $"<nome>{anagrafica.nome}</nome><cognome>{anagrafica.cognome}</cognome><cod_fiscale>{anagrafica.cod_fiscale}</cod_fiscale>"; //ToXML(anagrafica);

                    var resEncryptedAnagrafica = Firma.CriptaAnagrafica(xmlAnagrafica, null, _appSettings);

                    await _grattaEVinciDao.SalvaChiaveAnagrafrica(utente.IdUtente, resEncryptedAnagrafica.ChiaveCriptazione, resEncryptedAnagrafica.AnagraficaCriptata);

                    string datiFirmati = $"{Firma.GetIdRivenditoreDaBrand(utente.IdBrand, _appSettings)}{utente.IdUtente}{resEncryptedAnagrafica.AnagraficaCriptata}";

                    var resGoldbet = await RegistraContoGioco(utente, resEncryptedAnagrafica.AnagraficaCriptata, datiFirmati);

                    var checkEsitoGoldbet = resGoldbet.result != null && (resGoldbet.result.codiceRisultato ==
                                                                          ResultCodeGestioneAnagrafica.Successo.GetHashCode().ToString() ||
                                                                          resGoldbet.result?.codiceRisultato == ResultCodeGestioneAnagrafica
                                                                              .coppia_IdConto_IdRivenditore_esistente.GetHashCode().ToString());

                    if (checkEsitoGoldbet)
                    {
                        var resCat = await _backendAccountProxy.UsernameProviderCategoriaAdd(
                            new RequestInserisciUsernameProviderCategoria()
                            {
                                IdUtente = utente.IdUtente,
                                IdCanale = (int)Source.GrattaEVinci,
                                Username = utente.UserName,
                                IdCategoria = (int)GameType.GrattaEVinci,
                                IdProvider = (int)Provider.Lottomatica,
                                CodiceTransazione = correlationId
                            });

                        response.SetSuccess();

                        if (resCat.ResultCode != ResultCode.Success)
                        {
                            throw new Exception(resCat.Message);
                        }
                    }
                    else
                    {
                        throw new Exception(resGoldbet.result?.descrizioneRisultato);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.SetFailure();
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponseBase> ModificaConto(RegistraModificaContoRequest request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            int idUtente = request.IdUtente;

            ResponseBase response = new ResponseBase();

            try
            {
                var utente = await GetUtente(idUtente);

                var usernameUtentGbp =
                    utente.ListaUsernameProviderCategoria.FirstOrDefault(lu =>
                        lu.IdProvider == (int)Provider.Lottomatica && lu.IdCategoria == (int)GameType.GrattaEVinci);

                if (usernameUtentGbp != null)
                {
                    var anagrafica = new Anagrafica()
                    {
                        nome = utente.Nome,
                        cognome = utente.Cognome,
                        cod_fiscale = utente.CodiceFiscale
                    }.SetLimitiLol();

                    var xmlAnagrafica = $"<nome>{anagrafica.nome}</nome><cognome>{anagrafica.cognome}</cognome><cod_fiscale>{anagrafica.cod_fiscale}</cod_fiscale>";

                    var datiCriptati = await _grattaEVinciDao.AnagraficaEncryptGet(idUtente);

                    var resEncryptedAnagrafica = Firma.CriptaAnagrafica(xmlAnagrafica, datiCriptati.ChiaveCriptazione, _appSettings);

                    await _grattaEVinciDao.SalvaChiaveAnagrafrica(idUtente, resEncryptedAnagrafica.ChiaveCriptazione, resEncryptedAnagrafica.AnagraficaCriptata);

                    string datiFirmati = $"{Firma.GetIdRivenditoreDaBrand(utente.IdBrand, _appSettings)}{idUtente}{resEncryptedAnagrafica.AnagraficaCriptata}";

                    var modificaContoResponse = await ModificaContoGioco(utente, resEncryptedAnagrafica.AnagraficaCriptata, datiFirmati);

                    var checkEsito = modificaContoResponse.result != null && modificaContoResponse.result.codiceRisultato != Result.Successo.GetHashCode().ToString();

                    if (checkEsito)
                        throw new Exception(modificaContoResponse.result?.descrizioneRisultato);

                    if (modificaContoResponse.result == null)
                        throw new Exception("Errore chiamata concessionario");
                }

                if (_retryIntegrationManager.IsRetry())
                    await _retryIntegrationManager.Done();
            }
            catch (Exception e)
            {
                _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.SetFailure();
                response.Message = e.Message;

                await SetRetry(JsonConvert.SerializeObject(request));
            }
            response.SetSuccess();
            return response;
        }

        private async Task<registraContoGiocoResponse1> RegistraContoGioco(UtenteLight utente, string anagraficaCriptata, string datiFirmati)
        {
            var req = new registraContoGiocoRequest()
            {
                String_1 = Firma.GetIdRivenditoreDaBrand(utente.IdBrand, _appSettings),
                String_2 = utente.CodiceContratto,
                String_3 = anagraficaCriptata,
                String_4 = utente.ProvinciaResidenza,
                String_5 = Firma.GetCodiceConcessioneDaBrand(utente.IdBrand, _appSettings),
                String_6 = _appSettings.Value.CodiceRete,
                String_7 = 1.ToString(), //flg criptazione
                String_8 = datiFirmati,
                String_9 = datiFirmati.FirmaStringa(utente.IdBrand, _appSettings)
            };
            
            var client = new RegistraContoGiocoClient(new BasicHttpsBinding(BasicHttpsSecurityMode.Transport),
                new EndpointAddress(new Uri(_appSettings.Value.GevEndpoint)));
            registraContoGiocoResponse1 res = await client.registraContoGiocoAsync(req);

            return res;
        }

        private async Task<modificaContoGiocoResponse> ModificaContoGioco(UtenteLight utente, string anagraficaCriptata, string datiFirmati)
        {
           
                var req = new modificaContoGiocoRequest()
                {
                    String_1 = Firma.GetIdRivenditoreDaBrand(utente.IdBrand, _appSettings),
                    String_2 = utente.CodiceContratto,
                    String_3 = anagraficaCriptata,
                    String_4 = utente.ProvinciaResidenza,
                    String_5 = Firma.GetCodiceConcessioneDaBrand(utente.IdBrand, _appSettings),
                    String_6 = _appSettings.Value.CodiceRete,
                    String_7 = 1.ToString(), //flg criptazione
                    String_8 = datiFirmati,
                    String_9 = datiFirmati.FirmaStringa(utente.IdBrand, _appSettings)
                };

                var client = new RegistraContoGiocoClient(new BasicHttpsBinding(BasicHttpsSecurityMode.Transport),
                    new EndpointAddress(new Uri(_appSettings.Value.GevEndpoint)));
                modificaContoGiocoResponse res = await client.modificaContoGiocoAsync(req);

                return res;
        }

        public async Task SetRetry(string jsonRequest)
        {
            var contentType = _contextAccessor.HttpContext.Request.ContentType;
            if (contentType.Contains("xml"))
            {
                var scheme = _contextAccessor.HttpContext.Request.Scheme;
                var baseUrl = $"{scheme}://{_contextAccessor.HttpContext.Request.Host}";
                var path = $"/api{_contextAccessor.HttpContext.Request.Path.ToString().Replace(".svc", "")}";
                var actionName = _contextAccessor.HttpContext.Request.Headers["SOAPAction"].ToString().Replace("\"", "");
                actionName = actionName.Substring(actionName.LastIndexOf("/", StringComparison.Ordinal), actionName.Length - actionName.LastIndexOf("/", StringComparison.Ordinal));

                string requestUrl = $"{baseUrl}{path}{actionName}".ToLower();

                if (_retryIntegrationManager.IsRetry())
                    await _retryIntegrationManager.Retry();
                else
                    await _retryIntegrationManager.ScheuduleRetry(_appSettings.Value.RetryIntervalSeconds, requestUrl, jsonRequest, null, "application/json");

                return;
            }

            if (_retryIntegrationManager.IsRetry())
                await _retryIntegrationManager.Retry();
            else
                await _retryIntegrationManager.ScheuduleRetry(_appSettings.Value.RetryIntervalSeconds);
        }

        private async Task<UtenteLight?> GetUtente(int idUtente)
        {
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();

            var getUtenteResponse = await _backendAccountProxy.GetUtenteLight(new GetUtenteLightRequest
            {
                IdCanale = (int)Source.GrattaEVinci,
                IdUtente = idUtente,
                Vertical = Verticale.Lotterie,
                CodiceTransazione = correlationId
            });

            return getUtenteResponse.ResultCode == ResultCode.Success ? getUtenteResponse.Utente : null;
        }
    }
}
