using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Canedo.Identity.Api.ViewModels
{
    public class LoginAccountViewModel
    {
        [DisplayName("E-Mail"),
         Required(ErrorMessage = "O campo {0} é obrigatório"),
         EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [DisplayName("Senha"),
         Required(ErrorMessage = "O campo {0} é obrigatório"),
         StringLength(maximumLength: 100, MinimumLength = 6, ErrorMessage = "O campo {0} deve estar entre {2} e {1} caracteres")]
        public string Password { get; set; }
    }
}
