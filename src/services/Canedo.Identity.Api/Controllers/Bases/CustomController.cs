using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Canedo.Identity.Api.Controllers.Bases
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

        protected bool HasErrors() => _errors.Any();

        protected void AddError(string error) => _errors.Add(error);

        protected void ClearErrors() => _errors.Clear();
    }
}
