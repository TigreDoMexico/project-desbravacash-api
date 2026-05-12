using System.Security.Claims;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes;

public class TransacaoEndpoints : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/transacoes", async (
            ClaimsPrincipal user,
            ITransacaoService service,
            CancellationToken ct) =>
        {
            var unidadeId = Guid.Parse(user.FindFirstValue("unidade_id")!);
            return Results.Ok(await service.ObterTransacoesPorUnidadeAsync(unidadeId, ct));
        }).RequireAuthorization();
    }
}