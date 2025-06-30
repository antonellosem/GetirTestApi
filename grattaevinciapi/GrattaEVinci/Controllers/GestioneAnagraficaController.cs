using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using Gamenet.Logger;
using GestioneAnagraficaReference;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Contract.Responses;
using GrattaEVinci.Common.Utility;
using GrattaEVinci.Manager;
using Newtonsoft.Json;
using NLog;

namespace GrattaEVinci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestioneAnagraficaController : BaseController
    {
        private readonly AccountManager _accountManager;
        private readonly ILog _logger;
        
        public GestioneAnagraficaController(AccountManager accountManager, ILog logger)
        {
            _accountManager = accountManager;
            _logger = logger;
        }

        [Route("registracontogioco")]
        [HttpPost]
        public ResponseBase RegistraContoGioco(RegistraModificaContoRequest request)
        {
            ResponseBase response;
            string method = MethodBase.GetCurrentMethod()?.Name;
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

        [Route("modificacontogioco")]
        [HttpPost]
        public ResponseBase ModificaContoGioco(RegistraModificaContoRequest request)
        {
            ResponseBase response;
            string method = MethodBase.GetCurrentMethod()?.Name;
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
