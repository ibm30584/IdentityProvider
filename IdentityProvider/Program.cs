using IdentityProvider.Extentions;
using IdentityProvider.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Configurations.IdentityResources)
    .AddInMemoryApiScopes(Configurations.Scopes)
    .AddInMemoryClients(Configurations.Clients)
    .AddDeveloperSigningCredential();


var app = builder.Build();

app.UseIdentityServiceResponseFormatter();
app.UseIdentityServer();

app.Run();

