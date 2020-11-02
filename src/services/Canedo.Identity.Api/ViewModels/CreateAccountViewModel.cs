using System.ComponentModel.DataAnnotations;

namespace Canedo.Identity.Api.ViewModels
{
    public class CreateAccountViewModel : LoginAccountViewModel
    {
        [Compare("Password", ErrorMessage = "Senhas diferentes")]
        public string ConfirmPassword { get; set; }
    }
}
