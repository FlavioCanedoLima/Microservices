using Canedo.Identity.Api.Configuration;
using Canedo.Identity.Api.Extensions;
using Canedo.Identity.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Canedo.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult> AuthAsync(LoginAccountViewModel loginAccount) 
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

            var user = await _userManager.FindByEmailAsync(loginAccount.Email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ConfigureUserClaimsAsync(user, claims);
            var encodedToken = GenerateToken(identityClaims);

            var response = new AuthViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHour).TotalSeconds,
                UserToken = new AuthViewModel.UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new AuthViewModel.UserTokenViewModel.UserClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return Ok(response);
        }

        private string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidOn,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHour),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private async Task<ClaimsIdentity> ConfigureUserClaimsAsync(IdentityUser user, IList<Claim> claims) 
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var userRoles = await _userManager.GetRolesAsync(user);

            userRoles.ForEach(roleValue => claims.Add(new Claim("role", roleValue)));

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(year: 1970, month: 1, day: 1, hour: 0 , minute: 0, second: 0, offset: TimeSpan.Zero)).TotalSeconds);
        }
    }
}
