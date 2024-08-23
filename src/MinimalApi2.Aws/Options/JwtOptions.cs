namespace MinimalApi2.Aws.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string ExpiryMinutes { get; set; }
        public string ExpireMinuteRefToken { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
