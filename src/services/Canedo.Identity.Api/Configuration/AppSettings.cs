namespace Canedo.Identity.Api.Configuration
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHour { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
    }
}
