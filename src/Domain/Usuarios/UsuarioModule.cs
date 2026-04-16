using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios;

public class UsuarioModule : IModule
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        ConfigurarDependencias(builder);
    }
    
    public static void ConfigurarDependencias(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<IUsuarioService, UsuarioService>();
        builder.Services.AddScoped<IJwtService, JwtService>();
    }
}