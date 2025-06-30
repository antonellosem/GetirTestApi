using System.Diagnostics;
using System.Reflection;
using Backend.Common.Contracts.Base;
using Gamenet.Logger;
using GrattaEVinci.Common.Contract.Requests;
using GrattaEVinci.Common.Contract.Responses;
using GrattaEVinci.Common.Utility;
using GrattaEVinci.Manager;
using GrattaEVinciReference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;

namespace GrattaEVinci.Controllers
{
    [ApiController]
    [Route("AccountJson.svc")]
    public class MobileController : BaseController
    {
        private AccountManager _accountManager;
        private CashierManager _cashierManager;
        private ILog _logger;

        public MobileController(AccountManager accountManager, ILog logger, CashierManager cashierManager)
        {
            _accountManager = accountManager;
            _logger = logger;
            _cashierManager = cashierManager;
        }
        
        [Route("login")]
        [HttpPost] 
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/xml")]
        public async Task<login> Login([FromForm]LoginRequest? request)
        {
            var stopWatch = Stopwatch.StartNew();
            login response;
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };
            var ipUtente = HttpContext.Request.Headers["X-Forwarded-For"];

            _logger.Debug($"Ip da X-Forwarded-For: {HttpContext.Request.Headers["X-Forwarded-For"]}");

            if (request.idRivenditore == null)
            {
                request = new LoginRequest()
                {
                    
                    idRivenditore = HttpContext.Request.Query["idrivenditore"],
                    username= HttpContext.Request.Query["username"],
                    password= HttpContext.Request.Query["password"]
                };
            }

            using (ScopeContext.PushProperties(items))
            {
                _logger.Information($"Request: {request.ToLoggableRequest()}");
                response = await _accountManager.Login(request, ipUtente);
                _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }

            return response;
        }

        [Route("logout")]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/xml")]
        public async Task<logout> Logout([FromForm] LogoutRequest? request)
        {
            var stopWatch = Stopwatch.StartNew();
            logout response;
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };

            if (request.idRivenditore == null)
            {
                request = new LogoutRequest()
                {
                    idRivenditore = HttpContext.Request.Query["idrivenditore"],
                    IdContoGioco = HttpContext.Request.Query["IdContoGioco"],
                    token = HttpContext.Request.Query["token"]
                };
            }

            using (ScopeContext.PushProperties(items))
            {
                _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                response = await _accountManager.Logout(request);
                _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }
            return response;
        }

        [Route("saldocontogioco")]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/xml")]
        public async Task<SaldoContoGiocoResponse> SaldoContoGioco([FromForm] SaldoContoGiocoRequestModel? request)
        {
            var stopWatch = Stopwatch.StartNew();
            SaldoContoGiocoResponse response;
            string method = ExtensionMethod.GetActualAsyncMethodName();
            var items = new List<KeyValuePair<string, object>>
            {
                new("t", Guid.NewGuid()),
                new("callsite", method)
            };

            if (request.IdRivenditore == null)
            {
                request = new SaldoContoGiocoRequestModel()
                {
                    CodiceContratto = HttpContext.Request.Query["CodiceContratto"],
                    IdRivenditore = HttpContext.Request.Query["IdRivenditore"],
                    Token = HttpContext.Request.Query["token"]
                };
            }

            using (ScopeContext.PushProperties(items))
            {
                _logger.Information($"Request: {JsonConvert.SerializeObject(request)}");
                var resSaldo = await _cashierManager.SaldoContoGioco(request);

                response = new SaldoContoGiocoResponse()
                {
                    codiceRisultato = resSaldo.codiceRisultato,
                    descrizioneRisultato = resSaldo.descrizioneRisultato,
                    saldoContoGioco = resSaldo.saldoContoGioco,
                };

                _logger.Information($"Response: {JsonConvert.SerializeObject(response)}");
                _logger.Information($"EXECUTION_TIME: {stopWatch.ElapsedMilliseconds}ms");
            }
            return response;
        }

    }
}
