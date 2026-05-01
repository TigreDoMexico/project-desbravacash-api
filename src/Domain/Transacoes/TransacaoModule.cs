using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes;

public class TransacaoModule : IModule
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITransacaoService, TransacaoService>();
    }
}