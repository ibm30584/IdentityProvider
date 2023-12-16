using IdentityProvider.Api.Components;
using IdentityProvider.Application.Models;
using IdentityProvider.Infrastructures.Services.Blazor;
using IdentityProvider.Infrastructures.Services.IdentityCore;
using IdentityProvider.Infrastructures.Services.IdentityServer4;
using IdentityProvider.Infrastructures.Services.Persistence;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebApplication.CreateBuilder(args);

var identityServerConfig = builder.Configuration
           .GetSection("IdentityServerConfig")
           .Get<IdentityServerConfig>();
var validatedIdentityServerConfig = IdentityServerConfig.Validate(identityServerConfig);

builder.Services.AddBlazor();
builder.Services.AddDatabaseContext(
        validatedIdentityServerConfig.ConnectionString);
builder.Services.AddIdentityCore();
builder.Services.AddIdentityServer4(
        validatedIdentityServerConfig.Certificate);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseIdentityCore();
app.UseIdentityServer4();
app.UseBlazor<App>(typeof(IdentityProvider.UI._Imports).Assembly);

app.Run();