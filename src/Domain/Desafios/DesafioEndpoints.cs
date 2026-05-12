using System.Security.Claims;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Services;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios;

public class DesafioEndpoints : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/desafios", async (
            ClaimsPrincipal user,
            IDesafioService service,
            CancellationToken ct) =>
        {
            var unidadeId = Guid.Parse(user.FindFirstValue("unidade_id")!);
            var desafios = await service.ListarDesafiosPorUnidadeAsync(unidadeId, ct);
            
            return Results.Ok(desafios);
        }).RequireAuthorization();
    }
}
