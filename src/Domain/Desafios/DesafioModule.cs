using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Services;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios;

public class DesafioModule : IModule
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDesafioService, DesafioService>();
        builder.Services.AddScoped<IDesafioRepository, DesafioRepository>();
    }
}