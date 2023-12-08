namespace ClientApp.Configuration
{
    internal class ClientCredentialConfig
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string[] Scopes { get; set; } = null!;
    }
}
