using System.Diagnostics;
using System.Reflection;
using Gamenet.Logger;
using GestioneAnagraficaReference;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Contract.Responses;
using GrattaEVinci.Manager;
using Newtonsoft.Json;
using NLog;

namespace GrattaEVinci.Service
{
    public class GestioneAnagrafica : IGestioneAnagrafica
    {
        private readonly AccountManager _accountManager;
        private readonly ILog _logger;
        public GestioneAnagrafica(AccountManager accountManager, ILog logger)
        {
            _accountManager = accountManager;
            _logger = logger;
        }

        public ResponseBase RegistraContoGioco(RegistraModificaContoRequest request)
        {
            ResponseBase response;
            string method = MethodBase.GetCurrentMethod().Name;
            var stopWatch = Stopwatch.StartNew();

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));
            
            using (ScopeContext.PushProperties(items))
            {
                _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                response = _accountManager.RegistraConto(request).Result;
                _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }
            return response;
        }

        public ResponseBase ModificaContoGioco(RegistraModificaContoRequest request)
        {
            ResponseBase response;
            string method = MethodBase.GetCurrentMethod().Name;
            var stopWatch = Stopwatch.StartNew();

            var items = new List<KeyValuePair<string, object>>();
            items.Add(new KeyValuePair<string, object>("t", Guid.NewGuid()));
            items.Add(new KeyValuePair<string, object>("callsite", method));
            
            using (ScopeContext.PushProperties(items))
            {
                _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                response = _accountManager.ModificaConto(request).Result;
                _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }
            return response;
        }
    }
}
