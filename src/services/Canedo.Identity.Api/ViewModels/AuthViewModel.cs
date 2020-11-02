using System.Collections.Generic;

namespace Canedo.Identity.Api.ViewModels
{
    public class AuthViewModel 
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }

        public class UserTokenViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public IEnumerable<UserClaimViewModel> Claims { get; set; }

            public class UserClaimViewModel 
            {
                public string Value { get; set; }
                public string Type { get; set; }
            }
        }
    }

    public class LoginAuthViewModel : LoginAccountViewModel
    {

    }
}
