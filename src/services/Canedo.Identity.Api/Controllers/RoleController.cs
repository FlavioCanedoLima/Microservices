using Canedo.Identity.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Canedo.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("create-role")]
        public async Task<ActionResult> CreateRuleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("list-role")]
        public async Task<ActionResult> ListRolesAsync()
        {
            var result = _roleManager.Roles;

            return Ok(result);
        }
    }
}
