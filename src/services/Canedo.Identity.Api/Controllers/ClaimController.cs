using Canedo.Identity.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Canedo.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/claim")]
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ClaimController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpPost("create-claim/{idUser}")]
        public async Task<ActionResult> CreateClaimAsync(Guid idUser, string type, string value) 
        {
            var user = await _userManager.FindByIdAsync(idUser.ToString());

            if (user is null) 
            {
                return BadRequest("Usuário não encontrado");
            }

            var result = await _userManager.AddClaimAsync(user, new Claim(type, value));

            if (result.Succeeded == false) 
            {
                return BadRequest("Erro ao salvar");
            }

            return Ok("Sucesso");
        }
    }
}
