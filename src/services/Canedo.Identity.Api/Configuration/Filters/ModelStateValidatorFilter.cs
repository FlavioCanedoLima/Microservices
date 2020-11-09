using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Canedo.Identity.Api.Configuration.Filters
{
    public class ModelStateValidatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            if (actionExecutingContext.ModelState.IsValid == false)
            {
                var errors =
                    actionExecutingContext
                    .ModelState
                    .Values
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();


                actionExecutingContext.Result = new BadRequestObjectResult(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Messages", errors }
                }));
            }
        }
    }
}
