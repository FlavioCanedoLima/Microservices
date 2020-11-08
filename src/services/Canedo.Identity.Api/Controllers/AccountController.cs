using Canedo.Identity.Api.Extensions;
using Canedo.Identity.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Canedo.Identity.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : CustomController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccountAsync(CreateAccountViewModel createAccount) 
        {
            if (ModelStateIsNotValid()) 
            {
                return CustomResponse(ModelState);
            }

            var user = new IdentityUser 
            {
                UserName = createAccount.Email,
                Email = createAccount.Email,
                EmailConfirmed = true
            };            

            var result = await _userManager.CreateAsync(user, createAccount.Password);

            if (result.Succeeded == false) 
            {
                result.Errors.InlineForEach(e => AddError(e.Description));

                return CustomResponse();
            }

            return CustomResponse();
        }

        [HttpPost("login-account")]
        public async Task<ActionResult> LoginAsync(LoginAccountViewModel loginAccount) 
        {
            if (ModelStateIsNotValid())
            {
                return CustomResponse(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(userName: loginAccount.Email,
                                                                  password: loginAccount.Password,
                                                                  isPersistent: false,
                                                                  lockoutOnFailure: true);

            if (result.Succeeded == false) 
            {
                if (result.IsLockedOut) 
                {
                    return CustomResponse(error: "Usuário temporariamente bloqueado por tentativas inválidas");
                }

                return CustomResponse(error: "Usuário ou senha inválidos");
            }

            return CustomResponse(await _userManager.FindByEmailAsync(loginAccount.Email));
        }
    }
}
