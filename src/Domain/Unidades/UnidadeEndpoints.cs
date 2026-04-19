using System.Security.Claims;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Services;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades;

public class UnidadeEndpoints : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/unidade/dashboard", async (
            ClaimsPrincipal user,
            IUnidadeService service,
            CancellationToken ct) =>
        {
            var usuarioId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var unidadeId = Guid.Parse(user.FindFirstValue("unidade_id")!);

            var unidade = await service.BuscarDashboardAsync(usuarioId, unidadeId, ct);
            return unidade is null ? Results.NotFound() : Results.Ok(unidade);
        }).RequireAuthorization();
    }
}