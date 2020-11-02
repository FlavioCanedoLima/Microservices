using Canedo.Identity.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Canedo.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
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
            if (ModelState.IsValid == false) 
            {
                return BadRequest();
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
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("login-account")]
        public async Task<ActionResult> LoginAsync(LoginAccountViewModel loginAccount) 
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(userName: loginAccount.Email,
                                                                  password: loginAccount.Password,
                                                                  isPersistent: false,
                                                                  lockoutOnFailure: true);

            if (result.Succeeded == false) 
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
