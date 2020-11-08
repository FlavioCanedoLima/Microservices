using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Canedo.Identity.Api.Controllers
{
    [Route("api/claim")]
    public class ClaimController : CustomController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ClaimController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("create-claim/{idUser}")]
        public async Task<ActionResult> CreateClaimAsync(Guid idUser, string type, string value) 
        {
            var user = await _userManager.FindByIdAsync(idUser.ToString());

            if (user is null) 
            {
                return CustomResponse(error: "Usuário não encontrado");
            }

            var result = await _userManager.AddClaimAsync(user, new Claim(type, value));

            if (result.Succeeded == false) 
            {
                return CustomResponse(error: "Erro ao salvar");
            }

            return CustomResponse("Sucesso");
        }
    }
}
