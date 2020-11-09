using System.Collections.Generic;

namespace Canedo.Identity.Api.ViewModels
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimViewModel> Claims { get; set; }
    }
}
