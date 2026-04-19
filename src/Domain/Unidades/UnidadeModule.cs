using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Services;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades;

public class UnidadeModule : IModule
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnidadeService, UnidadeService>();
    }
}