using IdentityProvider.Extentions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityServerServices(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();
app.UseIdentityServerMiddlewares();

app.Run();
