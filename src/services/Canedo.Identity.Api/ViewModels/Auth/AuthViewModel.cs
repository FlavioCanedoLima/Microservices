namespace Canedo.Identity.Api.ViewModels
{
    public class AuthViewModel 
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }
}
