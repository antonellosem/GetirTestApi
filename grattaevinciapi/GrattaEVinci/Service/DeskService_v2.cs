using System.Diagnostics;
using Gamenet.Common.Enum;
using Gamenet.Common.Interface;
using Gamenet.Logger;
using GrattaEVinci.Common;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Utility;
using GrattaEVinciReference;
using GrattaEVinci.Manager;
using NLog;
using Newtonsoft.Json;

namespace GrattaEVinci.Service
{
    public class DeskService_v2 : Desk
    {

        private readonly CashierManager _cashierManager;
        private readonly AccountManager _accountManager;
        private readonly ILog _logger;
        private readonly string? _class;
        private readonly ITransaction<Transaction> _transactionManager;

        public DeskService_v2(CashierManager cashierManager, AccountManager accountManager, ILog logger, ITransaction<Transaction> transactionManager)
        {
            _cashierManager = cashierManager;
            _accountManager = accountManager;
            _logger = logger;
            _transactionManager = transactionManager;
            _class = GetType().FullName;
        }

        public SaldoContoGiocoResponse checkToken(string token, string codiceContratto,
            string idTransazione)
        {
            var stopWatch = Stopwatch.StartNew();
            var reqMapp = new CheckTokenRequestModel()
            {
                Token = token,
                CodiceContratto = codiceContratto,
                IdTransazione = idTransazione,
            };
            string method = ExtensionMethod.GetActualAsyncMethodName();

            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };

            SaldoContoGiocoResponse response;
            using (ScopeContext.PushProperties(items))
            {
                try
                {
                    _logger.Information($"Request: {JsonConvert.SerializeObject(reqMapp)}");
                    response = _accountManager.CheckToken(reqMapp).GetAwaiter().GetResult();
                    _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");
                    throw;
                }
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }

            return response;
        }

        public AddebitoResponse commitAddebito(string token, string codiceContratto, string idRivenditore,
            string idConcorso, string idTransazione, string codiceGiocata, string importoVincita, string importoAccredito, string fasciaVincita,
            string timeStamp, string datiFirmati, string firma, string idGioco)
        {
            var reqMapp = new CommitAddebitoRequestModel()
            {
                Token = token,
                CodiceContratto = codiceContratto,
                IdRivenditore = idRivenditore,
                IdConcorso = idConcorso,
                IdTransazione = idTransazione,
                CodiceGiocata = codiceGiocata,
                ImportoVincita = importoVincita,
                ImportoAccredito = importoAccredito,
                FasciaVincita = fasciaVincita,
                TimeStamp = timeStamp,
                DatiFirmati = datiFirmati,
                Firma = firma,
                IdGioco = idGioco,
            };
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var stopWatch = Stopwatch.StartNew();
            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };
            AddebitoResponse response;
            using (ScopeContext.PushProperties(items))
            {
                try
                {
                    _logger.Information($"Request: {JsonConvert.SerializeObject(reqMapp)}");
                    response = _cashierManager.CommitAddebito(reqMapp).GetAwaiter().GetResult();
                    _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");

                    var transazioneEsistente = _transactionManager.GetAsync(reqMapp.IdTransazione, reqMapp.Token, CasinoOperationType.ConfirmWager).GetAwaiter().GetResult();
                    _transactionManager.DeleteAsync(transazioneEsistente).GetAwaiter().GetResult();

                    throw;
                }
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }
            return response;
        }

        public AddebitoResponse reserveAddebito(string token, string codiceContratto, string idRivenditore,
            string idConcorso, string idTransazione,
            string costoGiocata, string timeStamp, string datiFirmati, string firma, string idGioco)
        {
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var stopWatch = Stopwatch.StartNew();
            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };
            var reqMapp = new ReserveAddebitoRequestModel()
            {
                Token = token,
                CodiceContratto = codiceContratto,
                IdRivenditore = idRivenditore,
                IdConcorso = idConcorso,
                IdTransazione = idTransazione,
                CostoGiocata = costoGiocata,
                TimeStamp = timeStamp,
                DatiFirmati = datiFirmati,
                Firma = firma,
                IdGioco = idGioco,
            };
            AddebitoResponse response;
            using (ScopeContext.PushProperties(items))
            {
                try
                {
                    _logger.Information($"Request: {JsonConvert.SerializeObject(reqMapp)}");
                    response = _cashierManager.ReserveAddebito(reqMapp).GetAwaiter().GetResult();
                    _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");
                    throw;
                }

                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }

            return response;
        }

        public AddebitoResponse rollbackAddebito(string token, string codiceContratto, string idRivenditore,
            string idConcorso, string idTransazione, string costoGiocata, string timeStamp, string datiFirmati, string firma, string idGioco)
        {
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var stopWatch = Stopwatch.StartNew();
            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };

            var reqMapp = new RollbackAddebitoRequestModel()
            {
                Token = token,
                CodiceContratto = codiceContratto,
                IdRivenditore = idRivenditore,
                IdConcorso = idConcorso,
                IdTransazione = idTransazione,
                CostoGiocata = costoGiocata,
                TimeStamp = timeStamp,
                DatiFirmati = datiFirmati,
                Firma = firma,
                IdGioco = idGioco,
            };
            AddebitoResponse response;
            using (ScopeContext.PushProperties(items))
            {
                try
                {
                    _logger.Information($"Request: {JsonConvert.SerializeObject(reqMapp)}");
                    response = _cashierManager.RollbackAddebito(reqMapp).GetAwaiter().GetResult();
                    _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");
                    throw;
                }
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }

            return response;
        }

        public  SaldoContoGiocoResponse saldoContoGioco(string token, string codiceContratto,
            string idRivenditore)
        {
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var stopWatch = Stopwatch.StartNew();
            
            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };
            var reqMapp = new SaldoContoGiocoRequestModel()
            {
                Token = token,
                CodiceContratto = codiceContratto,
                IdRivenditore = idRivenditore,
            };
            SaldoContoGiocoResponse response;
            using (ScopeContext.PushProperties(items))
            {
                try
                {
                    _logger.Information($"Request: {JsonConvert.SerializeObject(reqMapp)}");
                    response = _cashierManager.SaldoContoGioco(reqMapp).GetAwaiter().GetResult();
                    _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");
                    throw;
                }
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }
            return response;
        }

        public ChiaveContoGiocoResponse chiaveContoGioco(string codiceContratto, string idRivenditore,
            string datiFirmati, string firma)
        {
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var stopWatch = Stopwatch.StartNew();

            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };
            var reqMapp = new ChiaveContoGiocoRequestModel()
            {
                CodiceContratto = codiceContratto,
                IdRivenditore = idRivenditore,
                DatiFirmati = datiFirmati,
                Firma = firma,
            };
            ChiaveContoGiocoResponse response;
            using (ScopeContext.PushProperties(items))
            {
                try
                {
                    _logger.Information($"Request: {JsonConvert.SerializeObject(reqMapp)}");
                    response = _cashierManager.ChiaveContoGioco(reqMapp).GetAwaiter().GetResult();
                    _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");
                    throw;
                }

                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }

            return response;
        }
    }
}
