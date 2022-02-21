using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GetirTestApi.Application;
using GetirTestApi.Application.DTOs.V1;

using MediatR;

namespace GetirTestApi.Controllers.V1
{
    public class OperationsController : Controller
    {
        private readonly ILogger<OperationsController> _logger;
        private readonly IMediator _mediator;

        public OperationsController(ILogger<OperationsController> logger, IMediator mediator)
        {
            ArgValidator.NotNull(logger, nameof(logger));
            ArgValidator.NotNull(mediator, nameof(mediator));

            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Will persist brief information about customers
        /// </summary>
        [HttpPost("customers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequestDto body)
        {
            _logger.LogInformation($"INI {nameof(CreateCustomer)} method.");
            var commandResult = await _mediator.Send(new CreateCustomerCommand(body));
            _logger.LogInformation($"END {nameof(CreateCustomer)} method.");

            return Ok(commandResult);
        }

        [HttpPost("accounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequestDto body)
        {
            _logger.LogInformation($"INI {nameof(CreateAccount)} method.");
            var commandResult = await _mediator.Send(new CreateAccountCommand(body));
            _logger.LogInformation($"END {nameof(CreateAccount)} method.");
      
            return Ok(commandResult);
        }

        [HttpPost("accounts/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccountTransaction([FromBody] CreateAccountTransactionRequestDto body)
        {
            _logger.LogInformation($"INI {nameof(CreateAccountTransaction)} method.");
            var commandResult = await _mediator.Send(new CreateAccountTransactionCommand(body));
            _logger.LogInformation($"END {nameof(CreateAccountTransaction)} method.");
     
            return Ok(commandResult);
        }

        [HttpGet("accounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCustomerAccounts([FromQuery] GetCustomerAccountsRequestDto query)
        {
            _logger.LogInformation($"INI {nameof(GetCustomerAccounts)} method.");
            var commandResult = await _mediator.Send(new GetCustomerAccountsCommand(query));
            _logger.LogInformation($"END {nameof(GetCustomerAccounts)} method.");
      
            return Ok(commandResult);
        }

        [HttpGet("accounts/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountTransactions([FromQuery] GetAccountTransactionsRequestDto query)
        {
            _logger.LogInformation($"INI {nameof(GetAccountTransactions)} method.");
            var commandResult = await _mediator.Send(new GetAccountTransactionsCommand(query));
            _logger.LogInformation($"END {nameof(GetAccountTransactions)} method.");
      
            return Ok(commandResult);
        }
    }
}
