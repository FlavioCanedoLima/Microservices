using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Canedo.Identity.Api.ViewModels
{
    public class CreateClaimViewModel
    {
        [DisplayName("E-Mail"),
         Required(ErrorMessage = "O campo {0} é obrigatório"),
         EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string UserEmail { get; set; }
        [DisplayName("Tipo"),
         Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Type { get; set; }
        [DisplayName("Valor"),
         Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Value { get; set; }
    }
}
