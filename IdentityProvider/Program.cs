using IdentityProvider.Extentions;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServerServices(builder.Configuration);
builder.Services.AddBlazor();

//builder.Services
//    .AddAuthentication(
//    x =>
//    {
//        x.DefaultScheme = IdentityServerConstants.schem.AuthenticationScheme;
//        x.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//    })
//    .AddCookie(
//    CookieAuthenticationDefaults.AuthenticationScheme, 
//    x=>
//    {
//        x.Cookie.Name = "id4-cookie";
//        x.LoginPath = "/login";
//        x.LogoutPath = "/logout";
//        x.SlidingExpiration = TimeSpan.FromSeconds(60);
//    })
//    .AddOpenIdConnect(
//    OpenIdConnectDefaults.AuthenticationScheme,
//    x =>
//    {
//        x.Authority = "https://localhost:7083";
//        x.ClientId 
//    });


WebApplication app = builder.Build();

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
app.UseIdentityServerMiddlewares();
app.UseBlazor();

app.Run();

//var blazorBuilder = WebAssemblyHostBuilder.CreateDefault(args);
//var blazorApp = blazorBuilder.Build();

//await Task.WhenAll(
//    app.RunAsync(),
//    blazorApp.RunAsync()
//    );