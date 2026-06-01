using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Services;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes;

public class SolicitacaoModule : IModule
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISolicitacaoService, SolicitacaoService>();
        builder.Services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();
    }
}