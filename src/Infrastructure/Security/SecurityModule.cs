using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;

public class SecurityModule : IModule
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtService, JwtService>();
    }
}