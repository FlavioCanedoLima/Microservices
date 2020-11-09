using Canedo.Identity.Api.Controllers.Bases;
using Canedo.Identity.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("create-claim")]
        public async Task<ActionResult> CreateClaimAsync(CreateClaimViewModel createClaim) 
        {
            var user = await _userManager.FindByEmailAsync(createClaim.UserEmail);

            if (user is null) 
            {
                return CustomResponse(error: "Usuário não encontrado");
            }

            var result = await _userManager.AddClaimAsync(user, new Claim(createClaim.Type, createClaim.Value));

            if (result.Succeeded == false) 
            {
                return CustomResponse(error: "Erro ao salvar");
            }

            return CustomResponse();
        }
    }
}
