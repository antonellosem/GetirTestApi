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
using Microsoft.AspNetCore.Mvc;

namespace GrattaEVinci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeskService_v2 : BaseController
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

        [Route("checkToken")]
        [HttpPost]
        public async Task<SaldoContoGiocoResponse> CheckToken(CheckTokenRequestModel request)
        {
            var stopWatch = Stopwatch.StartNew();
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
                    _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                    response = await _accountManager.CheckToken(request);
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

        [Route("commitAddebito")]
        [HttpPost]
        public async Task<AddebitoResponse> CommitAddebito(CommitAddebitoRequestModel request)
        {
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
                    _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                    response = await _cashierManager.CommitAddebito(request);
                    _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Nome: {_class}.{method} - Errore: {e.Message.Trim()}");

                    var transazioneEsistente = _transactionManager.GetAsync(request.IdTransazione, request.Token, CasinoOperationType.ConfirmWager).GetAwaiter().GetResult();
                    _transactionManager.DeleteAsync(transazioneEsistente).GetAwaiter().GetResult();

                    throw;
                }

                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }
            return response;
        }

        [Route("reserveAddebito")]
        [HttpPost]
        public async Task<AddebitoResponse> ReserveAddebito(ReserveAddebitoRequestModel request)
        {
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
                    _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                    response = await _cashierManager.ReserveAddebito(request);
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

        [Route("rollbackAddebito")]
        [HttpPost]
        public async Task<AddebitoResponse> RollbackAddebito(RollbackAddebitoRequestModel request)
        {
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
                    _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                    response = await _cashierManager.RollbackAddebito(request);
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

        [Route("saldoContoGioco")]
        [HttpPost]
        public async Task<SaldoContoGiocoResponse> SaldoContoGioco(SaldoContoGiocoRequestModel request)
        {
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var stopWatch = Stopwatch.StartNew();
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
                    _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                    response = await _cashierManager.SaldoContoGioco(request);
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

        [Route("chiaveContoGioco")]
        [HttpPost]
        public async Task<ChiaveContoGiocoResponse> ChiaveContoGioco(ChiaveContoGiocoRequestModel request)
        {
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var stopWatch = Stopwatch.StartNew();

            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };

            ChiaveContoGiocoResponse response;
            using (ScopeContext.PushProperties(items))
            {
                try
                {
                    _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                    response = await _cashierManager.ChiaveContoGioco(request);
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
