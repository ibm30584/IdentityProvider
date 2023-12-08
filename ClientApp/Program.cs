using ClientApp.Configuration;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var client = new HttpClient();
var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));

var identityServerBaseUrl = configuration["IdentityServerBaseUrl"];
var discovery = await client.GetDiscoveryDocumentAsync(
    identityServerBaseUrl,
    cancellationTokenSource.Token);

if (discovery.IsError)
{
    Console.WriteLine(discovery.Error);
    return;
}

var section = configuration.GetSection("ClientCredentialConfig");
var clientCredentialConfig = section.Get<ClientCredentialConfig>()!;
var token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = discovery.TokenEndpoint,
    ClientId = clientCredentialConfig.ClientId,
    ClientSecret = clientCredentialConfig.ClientSecret,
    Scope = string.Join(" ", clientCredentialConfig.Scopes)
});

if (token.IsError)
{
    Console.WriteLine(token.Error);
    return;
}

Console.WriteLine(token.Json);