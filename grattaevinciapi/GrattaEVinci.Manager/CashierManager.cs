using System.Data.SqlClient;
using System.Globalization;
using System.Transactions;
using Backend.Common.Contracts.Account.Requests;
using Backend.Common.Contracts.Cashier.Requests;
using Backend.Common.Contracts.Cashier.Responses;
using Backend.Proxy;
using Gamenet.Common.Enum;
using Gamenet.Common.Exceptions;
using Gamenet.Common.Interface;
using Gamenet.Logger;
using GrattaEVinci.Common;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Entities;
using GrattaEVinci.Common.Enum;
using GrattaEVinci.Common.ModelConfiguration;
using GrattaEVinci.Common.Signature;
using GrattaEVinci.Common.Utility;
using GrattaEVinci.Data;
using GrattaEVinciReference;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using Transaction = GrattaEVinci.Common.Transaction;

namespace GrattaEVinci.Manager
{
    public class CashierManager
    {
        private readonly ITransaction<Transaction> TransactionManager;
        private readonly BackendCashierAndBonusProxy _backendCashierProxy;
        private readonly BackendAccountProxy _backendAccountProxy;
        private readonly GrattaEVinciDao _grattaEVinciDao;
        private readonly Validator _validator;
        private readonly AccountManager _accountManager;
        private readonly ILog logger;
        private readonly string? _class;
        private readonly IOptions<Config> _appSettings;
        public CashierManager(BackendCashierAndBonusProxy backendCashierAndBonusProxy, ITransaction<Transaction> transactionManager,
                                    GrattaEVinciDao grattaEVinciDao, AccountManager accountManager, ILog logger, Validator validator, BackendAccountProxy backendAccountProxy, IOptions<Config> appSettings)
        {
            _class = GetType().FullName;
            this.TransactionManager = transactionManager;
            this._backendCashierProxy = backendCashierAndBonusProxy;
            this._accountManager = accountManager;
            this.logger = logger;
            this._grattaEVinciDao = grattaEVinciDao;
            this._validator = validator;
            _backendAccountProxy = backendAccountProxy;
            _appSettings = appSettings;
        }

        public async Task<ResponseGetBalance> GetBalance(int idUtente, string? codiceTransazione)
        {
            var response = await _backendCashierProxy.GetBalance(new GetBalanceRequest()
            {
                CodiceTransazione = codiceTransazione,
                IdUtente = idUtente,
                Verticale = Verticale.Lotterie,
                Vertical = Verticale.Lotterie,
                IdProdotto = (int)Prodotto.GrattaEVinci,
                IdCanale = 1,
                IpSource = "127.0.0.1",
            });

            return response;
        }

        public async Task<AddebitoResponse> ReserveAddebito(ReserveAddebitoRequestModel request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();

            AddebitoResponse response = new AddebitoResponse();
            Transaction? transazione = null;

            bool isRetry = false;
            var jsonRequest = JsonConvert.SerializeObject(request);
            long? idBiglietto = null;
            long? idMovimento = null;

            Brand utenteIdBrand = Brand.Goldbet;
            try
            {
                if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.IdTransazione)
                                                             || string.IsNullOrWhiteSpace(request.TimeStamp)
                                                             || string.IsNullOrWhiteSpace(request.IdConcorso)
                                                             || string.IsNullOrWhiteSpace(request.CostoGiocata)
                                                             || string.IsNullOrWhiteSpace(request.DatiFirmati)
                                                             || string.IsNullOrWhiteSpace(request.Firma)
                                                             || string.IsNullOrWhiteSpace(request.CodiceContratto)
                                                             || string.IsNullOrWhiteSpace(request.IdRivenditore))
                {
                    throw new ParameterNotValid("Parametri incompleti");
                }

                int idCanale = (int)Source.GrattaEVinci;//non è possibile specificare tra mobile e app

                var utenteBrand = await _grattaEVinciDao.GetIdUtenteDaCodiceContratto(request.CodiceContratto);

                if (utenteBrand == null)
                    throw new ParameterNotValid("Utente non trovato");

                var idUtente = utenteBrand.IdUtente;
                utenteIdBrand = utenteBrand.IdBrand;

                await _accountManager.CheckSeamlessToken(request.Token, idUtente.ToString());

                var verificaFirmeIn = _validator.VerificaFirmeReserveIn(request);

                var getUtenteResponse = await _backendAccountProxy.GetUtenteLight(new GetUtenteLightRequest
                {
                    IdCanale = (int)Source.GrattaEVinci,
                    IdUtente = idUtente,
                    Vertical = Verticale.Lotterie,
                    CodiceTransazione = correlationId
                });

                var utente = getUtenteResponse.Utente;
                if (utente.IdStatoContratto != (int)StatoContratto.Aperto)
                    throw new UtenteNonAbilitatoAlGiocoException("Conto in uno stato diverso da aperto");

                if (utente.FlgAutoesclusoInterno)
                    throw new UtenteNonAbilitatoAlGiocoException("Conto Autoescluso interno");

                if (utente.ListaFunzionalita.Any(m => m.IdFunzionalita == (int)TipoFunzionalita.Gioco && m.Attivo != 1))
                    throw new UtenteNonAbilitatoAlGiocoException("Conto non abilitato al gioco");

                if (!verificaFirmeIn)
                    throw new ParameterNotValid("Firme non valide");

                var transazioneEsistenteRollback =
                    await TransactionManager.GetAsync(request.IdTransazione, request.Token, CasinoOperationType.CancelWager);

                if (transazioneEsistenteRollback != null)
                    throw new ParameterNotValid("Transazione già annullata");

                var transazioneEsistente =
                    await TransactionManager.GetAsync(request.IdTransazione, request.Token, CasinoOperationType.Wager);

                if (transazioneEsistente == null)
                {
                    transazione = new Transaction()
                    {
                        OperationType = CasinoOperationType.Wager,
                        JsonDataRequest = jsonRequest,
                        JsonDataResponse = JsonConvert.SerializeObject(response),
                        RelatedTransactionCode = request.Token,
                        Result = Convert.ToInt32(response.codiceRisultato),
                        SourceTransactionCode = request.IdTransazione,
                        Timestamp = DateTime.Now,
                        Request = request,
                        Response = response,
                        IdBiglietto = null,
                        IdMovimento = null,
                    };
                    await TransactionManager.SaveAsync(transazione);

                    var importoInCentesimi =
                        Map.ImportoInCentesimiDiEuro(request.CostoGiocata);

                    int idProdotto = await GetIdProdottoByCodiceGiocoPartner(request.IdGioco);

                    var resCredit = await _backendCashierProxy.HasSufficientFunds(new RequestIsAbleToPlay()
                    {
                        CodiceTransazione = correlationId,
                        IsPlayBonus = false,
                        IdCanale = idCanale,
                        IdUtente = idUtente,
                        Importo = importoInCentesimi,
                        IdProdotto = idProdotto,
                        IdVerticale = (int)Verticale.Lotterie,
                        Vertical = Verticale.Lotterie
                    });

                    if (resCredit.ResultCode != ResultCode.Success)
                        throw new SaldoInsufficienteException();

                    var saldoUtentePreTransaction = await _backendCashierProxy.GetBalance(
                        new GetBalanceRequest
                        {
                            CodiceTransazione = correlationId,
                            IdCanale = idCanale,
                            IdUtente = idUtente,
                            IdProdotto = idProdotto,
                            Verticale = Verticale.Lotterie
                        });

                    if (saldoUtentePreTransaction.ResultCode != ResultCode.Success)
                    {
                        throw new ImpossibileRecuperareSaldoUtenteExeception(saldoUtentePreTransaction.Message);
                    }

                    var idPrenotazione = await _grattaEVinciDao.Reserve(idUtente, Convert.ToDateTime(request.TimeStamp),
                        request.IdTransazione, request.Token, importoInCentesimi,
                        request.IdConcorso, request.IdGioco);

                    // acquisto cashier
                    var responseBuyIn = await _backendCashierProxy.BuyIn(new RequestBuyInMovement()
                    {
                        CodiceTransazione = correlationId,
                        IdComponenteBiglietto = idPrenotazione.IdPrenotazioneBiglietto,
                        IdProdotto = idProdotto,
                        IdCanale = idCanale,
                        IdUtente = idUtente,
                        Importo = importoInCentesimi,
                        Vertical = Verticale.GrattaEVinci
                    });

                    if (responseBuyIn.ResultCode != ResultCode.Success)
                    {
                        throw new InsertMovementBackendException(responseBuyIn.Message);
                    }

                    idBiglietto = idPrenotazione.IdPrenotazioneBiglietto;
                    idMovimento = responseBuyIn.IdMovimento;

                    await _grattaEVinciDao.UpdateReserve(idPrenotazione.IdPrenotazioneBiglietto, responseBuyIn.IdMovimento, responseBuyIn.ParteBonus, idCanale, idProdotto);

                    var getBalance = await GetBalance(idUtente, correlationId);
                    var saldo = getBalance.Saldo + getBalance.SaldoBonus ?? 0;
                    response.saldoContoGioco = Map.ImportInEuroDecimal(saldo).ToString(CultureInfo.InvariantCulture).ReplaceSpecialCharToPoint();
                    response.codiceRisultato = ((int)Result.Successo).ToString();
                    response.descrizioneRisultato = Result.Successo.ToString().NormalizeResultCodeDescription();
                }
                else
                {
                    isRetry = true;
                    response = (AddebitoResponse)transazioneEsistente.Response;
                    await TransactionManager.IncreaseRetryAsync(transazioneEsistente.SourceTransactionCode,
                        transazioneEsistente.RelatedTransactionCode, transazioneEsistente.OperationType);
                }
            }
            catch (TokenNotValidException e)
            {
                logger.Warning($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Cliente_Non_Autorizzato).ToString();
                response.descrizioneRisultato = Result.Cliente_Non_Autorizzato.ToString().NormalizeResultCodeDescription();
            }
            catch (UtenteNonAbilitatoAlGiocoException e)
            {
                logger.Warning($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Cliente_Non_Autorizzato).ToString();
                response.descrizioneRisultato = Result.Cliente_Non_Autorizzato.ToString().NormalizeResultCodeDescription();
            }
            catch (SaldoInsufficienteException e)
            {
                logger.Warning($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Credito_Insufficiente).ToString();
                response.descrizioneRisultato = Result.Credito_Insufficiente.ToString().NormalizeResultCodeDescription();
            }
            catch (ParameterNotValid e)
            {
                logger.Warning($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (InsertMovementBackendException e)
            {
                logger.Error($"{_class}.{method} - {e.Message}");

                await _grattaEVinciDao.CancelReserve(request.IdTransazione);
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            catch (ImpossibileRecuperareSaldoUtenteExeception e)
            {
                logger.Error($"{_class}.{method} - {e.Message}");

                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            catch (SqlException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato = Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
            }
            catch (Exception e)
            {
                logger.Error($"{_class}.{method} - {e.Message}");

                await _grattaEVinciDao.CancelReserve(request.IdTransazione);
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            finally
            {
                response.datiFirmati = $"{request.CodiceContratto}{request.IdTransazione}{request.IdConcorso}{request.CostoGiocata}{response.codiceRisultato}";
                response.firma = response.datiFirmati.FirmaStringa(utenteIdBrand, _appSettings);
            }

            var jsonResponse = JsonConvert.SerializeObject(response);
            if (!isRetry)
            {
                if (transazione != null)
                {
                    transazione.JsonDataResponse = jsonResponse;
                    transazione.Result = Convert.ToInt32(response.codiceRisultato);
                    transazione.Response = response;
                    transazione.LastUpdate = DateTime.Now;
                    transazione.IdBiglietto = idBiglietto;
                    transazione.IdMovimento = idMovimento;
                    await TransactionManager.UpdateAsync(transazione);
                }
            }
            return response;
        }

        public async Task<AddebitoResponse> CommitAddebito(CommitAddebitoRequestModel request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();

            AddebitoResponse response = new AddebitoResponse() { codiceRisultato = Result.Operazione_In_Corso.GetHashCode().ToString() };
            
            bool isRetry = false;
            var jsonRequest = JsonConvert.SerializeObject(request);
            long? idBiglietto = null;
            long? idMovimento = null;
            Brand utenteIdBrand = Brand.Goldbet;

            try
            {
                if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.IdTransazione)
                                                             || string.IsNullOrWhiteSpace(request.TimeStamp)
                                                             || string.IsNullOrWhiteSpace(request.IdConcorso)
                                                             || string.IsNullOrWhiteSpace(request.CodiceGiocata)
                                                             || string.IsNullOrWhiteSpace(request.DatiFirmati)
                                                             || string.IsNullOrWhiteSpace(request.Firma)
                                                             || string.IsNullOrWhiteSpace(request.CodiceContratto)
                                                             || string.IsNullOrWhiteSpace(request.FasciaVincita)
                                                             || string.IsNullOrWhiteSpace(request.IdRivenditore))
                {
                    throw new ParameterNotValid();
                }

                var utenteBrand = await _grattaEVinciDao.GetIdUtenteDaCodiceContratto(request.CodiceContratto);

                if (utenteBrand == null)
                    throw new ParameterNotValid("Utente non trovato");

                var idUtente = utenteBrand.IdUtente;
                utenteIdBrand = utenteBrand.IdBrand;

                var getTransazione = await _grattaEVinciDao.GetDatiReserve(request.IdTransazione);

                var verificaFirmeIn = _validator.VerificaFirmeCommitIn(request);

                if (!verificaFirmeIn)
                    throw new ParameterNotValid("Firme non valide");

                var importoVincitaInCentesimi =
                    Map.ImportoInCentesimiDiEuro(request.ImportoVincita);
                var importoAccreditoInCentesimi =
                    Map.ImportoInCentesimiDiEuro(request.ImportoAccredito);

                ResponseGetMovements? checkMovementAcquisto = null;
                ResponseGetMovements? checkMovementWinning = null;

                int idProdotto = await GetIdProdottoByCodiceGiocoPartner(request.IdGioco, request.IdTransazione);

                if (getTransazione?.IdStato != TipoOperazione.Commit.GetHashCode())
                {
                    await _accountManager.CheckSeamlessToken(request.Token, idUtente.ToString(), request.IdTransazione);
                    var res = await _grattaEVinciDao.CommitReserve(request.CodiceGiocata, request.IdTransazione,
                        importoVincitaInCentesimi, importoAccreditoInCentesimi, request.FasciaVincita,
                        Convert.ToDateTime(request.TimeStamp), idUtente, request.IdConcorso);
                    idBiglietto = res.IdBiglietto;
                    idMovimento = res.IdMovimento;
                }
                else
                {
                    var biglietto = await _grattaEVinciDao.GetDatiBiglietto(request.IdTransazione);
                    idBiglietto = biglietto.IdBiglietto;
                    idMovimento = biglietto.IdMovimento;

                    checkMovementAcquisto = await _backendCashierProxy.GetMovementsByTicket(new RequestGetIdTicket()
                    {
                        CodiceTransazione = correlationId,
                        IdBiglietto = idBiglietto.Value,
                        IdUtente = idUtente,
                        IdProdotto = idProdotto,
                        OperationCode = CodiceOperazione.AcquistoBiglietto.GetHashCode()
                    });

                    checkMovementWinning = await _backendCashierProxy.GetMovementsByTicket(new RequestGetIdTicket()
                    {
                        CodiceTransazione = correlationId,
                        IdBiglietto = idBiglietto.Value,
                        IdUtente = idUtente,
                        IdProdotto = idProdotto,
                        OperationCode = CodiceOperazione.VincitaBiglietto.GetHashCode()
                    });

                }

                //Commit del biglietto e comunicazione al cashier del codice ticket aams
                if (checkMovementAcquisto == null || !checkMovementAcquisto.Movimenti.Any())
                {
                    var resultJoinTicketToMovement = await _backendCashierProxy.JoinTicketToMovement(
                    new RequestAddTicketCodeToMovement()
                    {
                        CodiceTransazione = correlationId,
                        CodiceTicket = request.CodiceGiocata,
                        IdBiglietto = idBiglietto,
                        IdMovimento = idMovimento.Value,
                        IdCanale = (int)Source.GrattaEVinci,
                        IdUtente = idUtente,
                        //IpUtente = clientIpAddress
                    });
                    if (resultJoinTicketToMovement.ResultCode != ResultCode.Success)
                    {
                        throw new BackendResponseException(resultJoinTicketToMovement.Message);
                    }
                }

                if (checkMovementWinning == null || !checkMovementWinning.Movimenti.Any())
                {
                    if (importoAccreditoInCentesimi > 0 && request.FasciaVincita == FasciaVincita.Bassa)
                    {
                        var resultInsertWinningGamingMovement =
                            await _backendCashierProxy.InsertWinningGamingMovement(
                                new RequestInsertWinningMovements()
                                {
                                    CodiceTransazione = correlationId,
                                    ImportoRimborso = 0,
                                    ImportoVincita = importoAccreditoInCentesimi,
                                    ImportoRimborsoRealBonus = 0,
                                    IdBiglietto = idBiglietto.Value,
                                    IdProdotto = idProdotto,
                                    CodiceTicket = request.CodiceGiocata,
                                    IdCanale = (int)Source.GrattaEVinci,
                                    IdUtente = idUtente,
                                    ImportoParteJackpot = 0,
                                    //IpUtente = clientIpAddress,
                                });

                        if (resultInsertWinningGamingMovement.ResultCode != ResultCode.Success)
                        {
                            throw new InsertWinningMovementException(resultInsertWinningGamingMovement.Message);
                        }
                    }
                }

                var balance = await GetBalance(Convert.ToInt32(idUtente), correlationId);
                response.saldoContoGioco = Map.ImportInEuroDecimal((balance.Saldo + balance.SaldoBonus) ?? 0).ToString(CultureInfo.InvariantCulture).ReplaceSpecialCharToPoint();
                response.codiceRisultato = ((int)Result.Successo).ToString();
                response.descrizioneRisultato = Result.Successo.ToString().NormalizeResultCodeDescription();

            }
            catch (TokenNotValidException e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (BackendResponseException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato = Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
            }
            catch (InsertWinningMovementException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato = Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
            }
            catch (ParameterNotValid e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato =
                    Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (ImpossibileRecuperareSaldoUtenteExeception e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato = Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
            }
            catch (Exception e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");

                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            finally
            {
                response.datiFirmati = $"{request.CodiceContratto}{request.IdTransazione}{request.IdConcorso}{request.ImportoVincita}{request.ImportoAccredito}{request.FasciaVincita}{response.codiceRisultato}";
                response.firma = response.datiFirmati.FirmaStringa(utenteIdBrand, _appSettings);
            }

            return response;
        }

        public async Task<AddebitoResponse> RollbackAddebito(RollbackAddebitoRequestModel request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();
            AddebitoResponse response = new AddebitoResponse();
            Transaction? transazione = null;

            bool isRetry = false;
            var jsonRequest = JsonConvert.SerializeObject(request);
            long? idBiglietto = null;
            long? idMovimento = null;
            Brand utenteIdBrand = Brand.Goldbet;

            try
            {
                if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.IdTransazione)
                                                             || string.IsNullOrWhiteSpace(request.TimeStamp)
                                                             || string.IsNullOrWhiteSpace(request.IdConcorso)
                                                             || string.IsNullOrWhiteSpace(request.CostoGiocata)
                                                             || string.IsNullOrWhiteSpace(request.DatiFirmati)
                                                             || string.IsNullOrWhiteSpace(request.Firma)
                                                             || string.IsNullOrWhiteSpace(request.CodiceContratto)
                                                             || string.IsNullOrWhiteSpace(request.IdRivenditore))
                {
                    throw new ParameterNotValid();
                }

                var verificaFirmeIn = _validator.VerificaFirmeRollbackIn(request);

                if (!verificaFirmeIn)
                    throw new ParameterNotValid();

                var utenteBrand = await _grattaEVinciDao.GetIdUtenteDaCodiceContratto(request.CodiceContratto);

                if (utenteBrand == null)
                    throw new ParameterNotValid("Utente non trovato");

                var idUtente = utenteBrand.IdUtente;
                utenteIdBrand = utenteBrand.IdBrand;

                var transazioneEsistente =
                    TransactionManager.Get(request.IdTransazione, request.Token, CasinoOperationType.CancelWager);
                var datiReserve = (await _grattaEVinciDao.GetDatiReserve(request.IdTransazione));

                int saldo;
                if (datiReserve == null || datiReserve.IdStato == StatoBiglietto.Annullata.GetHashCode())
                {
                    var getBalance = await GetBalance(idUtente, correlationId);
                    saldo = getBalance.Saldo + getBalance.SaldoBonus ?? 0;
                    //response.result.saldoContoGioco = (Convert.ToDecimal(balance.Saldo) / 100).ToString("#.00");
                    response.saldoContoGioco = Map.ImportInEuroDecimal(saldo).ToString(CultureInfo.InvariantCulture)
                        .ReplaceSpecialCharToPoint();
                    response.codiceRisultato = ((int)Result.Successo).ToString();
                    response.descrizioneRisultato =
                        Result.Successo.ToString().NormalizeResultCodeDescription();
                    response.datiFirmati =
                        $"{request.CodiceContratto}{request.IdTransazione}{request.IdConcorso}{request.CostoGiocata}{response.codiceRisultato}";
                    response.firma = response.datiFirmati.FirmaStringa(utenteIdBrand, _appSettings);
                    return response;
                }

                if (datiReserve.IdStato == StatoBiglietto.Confermata.GetHashCode())
                    throw new Exception("Transazione in stato confermato");

                await _accountManager.CheckSeamlessToken(request.Token, idUtente.ToString(), request.IdTransazione);

                if (transazioneEsistente == null)
                {
                    transazione = new Transaction()
                    {
                        OperationType = CasinoOperationType.CancelWager,
                        JsonDataRequest = jsonRequest,
                        JsonDataResponse = JsonConvert.SerializeObject(response),
                        RelatedTransactionCode = request.Token,
                        Result = Convert.ToInt32(response.codiceRisultato),
                        SourceTransactionCode = request.IdTransazione,
                        Timestamp = DateTime.Now,
                        Request = request,
                        Response = response,
                        IdBiglietto = null,
                        IdMovimento = null,
                    };
                    await TransactionManager.SaveAsync(transazione);

                    //se il movimento non è stato associato alla reserve recupero l'idmovimento dall'IdComponenteBiglietto
                    datiReserve.IdMovimento ??= (await _backendCashierProxy.GetIdMovimentoByIdComponenteBiglietto(
                        new GetIdMovimentoByIdComponenteBigliettoRequest()
                        {
                            CodiceOperazione = CodiceOperazione.AcquistoBiglietto,
                            IdComponenteBiglietto = datiReserve.IdPrenotazioneBiglietto,
                            IdProdotto = await GetIdProdottoByCodiceGiocoPartner(request.IdGioco, request.IdTransazione),
                        })).IdMovimento.FirstOrDefault();

                    // storno importo acquisto
                    if (datiReserve.IdMovimento.HasValue && datiReserve.IdMovimento != 0)
                    {
                        var resultTransfer = await _backendCashierProxy.Transfer(new RequestTransferMovement()
                        {
                            CodiceTransazione = correlationId,
                            IdUtente = idUtente,
                            IdCanale = (int)Source.GrattaEVinci,
                            IdMovimento = datiReserve.IdMovimento.Value
                        });

                        if (resultTransfer.ResultCode != ResultCode.Success && resultTransfer.ResultCode != ResultCode.MovimentoGiaStornato)
                            throw new BackendResponseException(resultTransfer.Message);

                        idMovimento = datiReserve.IdMovimento.Value;
                    }

                    await _grattaEVinciDao.CancelReserve(request.IdTransazione);

                    var getBalance = await GetBalance(idUtente, correlationId);
                    saldo = getBalance.Saldo + getBalance.SaldoBonus ?? 0;
                    response.saldoContoGioco = Map.ImportInEuroDecimal(saldo).ToString(CultureInfo.InvariantCulture).ReplaceSpecialCharToPoint();
                    response.codiceRisultato = ((int)Result.Successo).ToString();
                    response.descrizioneRisultato = Result.Successo.ToString().NormalizeResultCodeDescription();

                    return response;
                }
                else
                {
                    isRetry = true;
                    response = (AddebitoResponse)transazioneEsistente.Response;
                    await TransactionManager.IncreaseRetryAsync(transazioneEsistente.SourceTransactionCode,
                         transazioneEsistente.RelatedTransactionCode, transazioneEsistente.OperationType);
                }
            }
            catch (ParameterNotValid e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (BackendResponseException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            catch (TokenNotValidException e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
            }
            catch (SqlException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato = Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
            }
            catch (Exception e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
            }
            finally
            {
                response.datiFirmati = $"{request.CodiceContratto}{request.IdTransazione}{request.IdConcorso}{request.CostoGiocata}{response.codiceRisultato}";
                response.firma = response.datiFirmati.FirmaStringa(utenteIdBrand, _appSettings);
            }

            var jsonResponse = JsonConvert.SerializeObject(response);
            if (!isRetry)
            {
                if (transazione != null)
                {
                    transazione.JsonDataResponse = jsonResponse;
                    transazione.Result = Convert.ToInt32(response.codiceRisultato);
                    transazione.Response = response;
                    transazione.LastUpdate = DateTime.Now;
                    transazione.IdBiglietto = idBiglietto;
                    transazione.IdMovimento = idMovimento;
                    await TransactionManager.UpdateAsync(transazione);
                }
            }
            return response;
        }

        public async Task<SaldoContoGiocoResponse> SaldoContoGioco(SaldoContoGiocoRequestModel request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            var correlationId = ScopeContext.GetAllProperties().FirstOrDefault(p => p.Key == "t").Value.ToString();
            SaldoContoGiocoResponse response = new SaldoContoGiocoResponse();

            try
            {
                if (string.IsNullOrEmpty(request.CodiceContratto) || string.IsNullOrEmpty(request.IdRivenditore) ||
                    string.IsNullOrEmpty(request.Token))
                {
                    throw new ParameterNotValid("Parametri incompleti");
                }

                var utenteBrand = await _grattaEVinciDao.GetIdUtenteDaCodiceContratto(request.CodiceContratto);

                if (utenteBrand == null)
                    throw new ParameterNotValid("Utente non trovato");

                var idUtente = utenteBrand.IdUtente;

                await _accountManager.CheckSeamlessToken(request.Token, idUtente.ToString());

                var infoSaldo = await GetBalance(idUtente, correlationId);

                if (infoSaldo.ResultCode != ResultCode.Success)
                    throw new ParameterNotValid(infoSaldo.Message);

                var saldo = infoSaldo.Saldo + infoSaldo.SaldoBonus ?? 0;

                response.saldoContoGioco = Map.ImportInEuroDecimal(saldo).ToString(CultureInfo.InvariantCulture).ReplaceSpecialCharToPoint();
                response.descrizioneRisultato = Result.Successo.ToString().NormalizeResultCodeDescription();
                response.codiceRisultato = ((int)Result.Successo).ToString();
            }
            catch (ParameterNotValid e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
            }
            catch (TokenNotValidException e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
            }
            catch (SqlException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato = Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
            }
            catch (Exception e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
            }

            return response;
        }

        public async Task<ChiaveContoGiocoResponse> ChiaveContoGioco(ChiaveContoGiocoRequestModel request)
        {
            var method = ExtensionMethod.GetActualAsyncMethodName();
            ChiaveContoGiocoResponse response = new ChiaveContoGiocoResponse();
            Brand utenteIdBrand = Brand.Goldbet;

            try
            {
                if (string.IsNullOrEmpty(request.IdRivenditore) || string.IsNullOrEmpty(request.Firma) ||
                    string.IsNullOrEmpty(request.DatiFirmati) || string.IsNullOrEmpty(request.CodiceContratto))
                    throw new ParameterNotValid("Parametri non corretti");

                var verificaFirmeIn = _validator.VerificaFirmeChiaveContoGiocoIn(request);

                if (!verificaFirmeIn)
                    throw new ParameterNotValid("Firme non congruenti");

                var utenteBrand = await _grattaEVinciDao.GetIdUtenteDaCodiceContratto(request.CodiceContratto);

                if (utenteBrand == null)
                    throw new ParameterNotValid("Utente non trovato");

                var idUtente = utenteBrand.IdUtente;
                utenteIdBrand = utenteBrand.IdBrand;

                var verificaVincitaFasciaAlta = await _grattaEVinciDao.VerificaVincitaFasciaAlta(idUtente);

                if (!verificaVincitaFasciaAlta)
                    throw new BackendResponseException("No vincite fascia alta");

                string chiaveContoGioco = await _grattaEVinciDao.GetChiaveContoGioco(idUtente);

                if (string.IsNullOrEmpty(chiaveContoGioco))
                    throw new UserNotFoundException("Chiave non presente");

                response.codiceRisultato = ((int)Result.Successo).ToString();
                response.descrizioneRisultato = Result.Successo.ToString().NormalizeResultCodeDescription();
                response.chiaveContoGioco = Firma.FirmaConCertificatoAdm(chiaveContoGioco, _appSettings);
                response.datiFirmati = $"{(int)Result.Successo}{response.chiaveContoGioco}";
            }
            catch (UserNotFoundException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)ResultCodeChiaveContoGioco.Chiave_Non_Disponibile).ToString();
                response.descrizioneRisultato = ResultCodeChiaveContoGioco.Chiave_Non_Disponibile.ToString()
                    .NormalizeResultCodeDescription();
                response.chiaveContoGioco = "";
                response.datiFirmati = $"{(int)ResultCodeChiaveContoGioco.Chiave_Non_Disponibile}";
            }
            catch (BackendResponseException e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)ResultCodeChiaveContoGioco.No_Vincite_Fascia_Alta).ToString();
                response.descrizioneRisultato = ResultCodeChiaveContoGioco.No_Vincite_Fascia_Alta.ToString().NormalizeResultCodeDescription();
                response.chiaveContoGioco = "";
                response.datiFirmati = $"{(int)ResultCodeChiaveContoGioco.No_Vincite_Fascia_Alta}";
            }
            catch (ParameterNotValid e)
            {
                logger.Warning($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Parametri_Errati).ToString();
                response.descrizioneRisultato = Result.Parametri_Errati.ToString().NormalizeResultCodeDescription();
                response.chiaveContoGioco = "";
                response.datiFirmati = $"{(int)Result.Parametri_Errati}";
            }
            catch (SqlException e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");

                response.codiceRisultato = ((int)Result.Sistema_Non_Disponibile).ToString();
                response.descrizioneRisultato = Result.Sistema_Non_Disponibile.ToString().NormalizeResultCodeDescription();
                response.datiFirmati = $"{(int)Result.Sistema_Non_Disponibile}";
            }
            catch (Exception e)
            {
                logger.Error($"Nome: {_class}.{method} - Errore: {e.Message}");
                response.codiceRisultato = ((int)Result.Errore_Di_Sistema).ToString();
                response.descrizioneRisultato = Result.Errore_Di_Sistema.ToString().NormalizeResultCodeDescription();
                response.chiaveContoGioco = "";
                response.datiFirmati = $"{(int)Result.Errore_Di_Sistema}";
            }
            finally
            {
                response.firma = response.datiFirmati.FirmaStringa(utenteIdBrand, _appSettings);
            }
            return response;
        }

        private async Task<int> GetIdProdottoByCodiceGiocoPartner(string idGioco, string idTransazione = null)
        => await _grattaEVinciDao.GetIdProdottoByCodiceGiocoPartner(idGioco, idTransazione) ?? (int)Prodotto.GrattaEVinci;

    }
}
