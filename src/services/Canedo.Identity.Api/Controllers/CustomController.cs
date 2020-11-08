using Canedo.Identity.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Canedo.Identity.Api.Controllers
{
    [ApiController]
    public abstract class CustomController : Controller
    {
        protected readonly ICollection<string> _errors;

        public CustomController()
        {
            _errors = new List<string>();
        }

        protected ActionResult CustomResponse(object result = default, string error = default) 
        {
            if (string.IsNullOrEmpty(error) == false) 
            {
                AddError(error);
            }

            if (HasErrors()) 
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> 
                {
                    { "Messages", _errors.ToArray() }
                }));
            }

            return Ok(result);
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState) 
        {
            modelState
                .Values
                .SelectMany(e => e.Errors)
                .InlineForEach(e => AddError(e.ErrorMessage));

            return CustomResponse();
        }

        protected bool HasErrors() => _errors.Any();

        protected void AddError(string error) => _errors.Add(error);

        protected void ClearErrors() => _errors.Clear();

        protected bool ModelStateIsNotValid() => ModelState.IsValid == false;
    }
}
