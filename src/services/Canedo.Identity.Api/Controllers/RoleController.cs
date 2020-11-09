using Canedo.Identity.Api.Controllers.Bases;
using Canedo.Identity.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Canedo.Identity.Api.Controllers
{
    [Route("api/role")]
    public class RoleController : CustomController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public RoleController(RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("create-role")]
        public async Task<ActionResult> CreateRuleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded == false)
            {
                return CustomResponse(error: "Erro ao criar role");
            }

            return CustomResponse();
        }

        [HttpGet("list-role")]
        public async Task<ActionResult> ListRolesAsync()
        {
            var result = _applicationDbContext.Roles;

            return CustomResponse(result);
        }

        [HttpPut("edit-role/{id}")]
        public async Task<ActionResult> EditRoleAsync(Guid id, string name) 
        {
            var resultDb = await _roleManager.FindByIdAsync(id.ToString());

            if (resultDb is null) 
            {
                return CustomResponse(error: "Role não encontrada para edição");
            }

            resultDb.Name = name;

            var result = await _roleManager.UpdateAsync(resultDb);

            if (result.Succeeded == false) 
            {
                return CustomResponse(error: "Erro ao editar");
            }

            return CustomResponse();
        }
    }
}
