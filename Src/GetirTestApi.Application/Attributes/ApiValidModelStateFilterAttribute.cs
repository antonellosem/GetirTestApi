using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using GetirTestApi.CrossCutting;

namespace GetirTestApi.Application.Attributes
{
    public class ApiValidModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            // getting all validation errors
            string[] validationErrors = context.ModelState
                .Keys
                .SelectMany(x => context.ModelState[x].Errors)
                .Select(x => x.ErrorMessage)
                .ToArray();

            var httpError = ApiHttpError.CreateHttpValidationError(validationErrors);

            context.Result = new BadRequestObjectResult(httpError);
        }
    }
}
