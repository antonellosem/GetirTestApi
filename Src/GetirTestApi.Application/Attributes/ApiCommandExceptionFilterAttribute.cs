using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace GetirTestApi.Application.Attributes
{
    public class ApiCommandExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiCommandExceptionFilterAttribute> _logger;

        public ApiCommandExceptionFilterAttribute(ILogger<ApiCommandExceptionFilterAttribute> logger)
        {
            ArgValidator.NotNull(logger, nameof(logger));

            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            context.Result = new BadRequestResult();
            context.ExceptionHandled = true;
        }
    }
}
