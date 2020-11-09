using Canedo.Identity.Api.Controllers.Bases;
using Canedo.Identity.Api.Data;
using Canedo.Identity.Api.ViewModels;
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
        public async Task<ActionResult> CreateRuleAsync(CreateRoleViewModel createRole)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(createRole.RoleName));

            if (result.Succeeded == false)
            {
                return CustomResponse(error: "Erro ao criar role");
            }

            return CustomResponse();
        }

        [HttpGet("list-role")]
        public async Task<ActionResult<IdentityRole>> ListRolesAsync()
        {
            var result = _applicationDbContext.Roles;

            return CustomResponse(result);
        }

        [HttpPut("edit-role/{id}")]
        public async Task<ActionResult> EditRoleAsync(Guid id, EditRoleViewModel editRole) 
        {
            var resultDb = await _roleManager.FindByIdAsync(id.ToString());

            if (resultDb is null) 
            {
                return CustomResponse(error: "Role não encontrada para edição");
            }

            resultDb.Name = editRole.RoleName;

            var result = await _roleManager.UpdateAsync(resultDb);

            if (result.Succeeded == false) 
            {
                return CustomResponse(error: "Erro ao editar");
            }

            return CustomResponse();
        }
    }
}
