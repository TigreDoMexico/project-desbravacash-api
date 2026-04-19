using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;
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
        builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    }
}