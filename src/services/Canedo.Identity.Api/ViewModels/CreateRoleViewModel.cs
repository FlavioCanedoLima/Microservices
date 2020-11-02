using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Canedo.Identity.Api.ViewModels
{
    public class CreateRoleViewModel 
    {
        [DisplayName("NomePapel"),
         Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RoleName { get; set; }
    }
}
