using System.Security.Claims;
using FluentValidation;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;
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

        endpoints.MapGet("/api/transacoes/pendentes", async (
            ITransacaoService service,
            CancellationToken ct) =>
        {
            return Results.Ok(await service.ObterTransacoesPendentesAsync(ct));
        }).RequireAuthorization(RolesConsts.Admin);

        endpoints.MapPost("/api/transacoes", async (
            CriarTransacaoRequest request,
            IValidator<CriarTransacaoRequest> validator,
            ClaimsPrincipal user,
            ITransacaoService service,
            CancellationToken ct) =>
        {
            var validation = await validator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var usuarioId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await service.CriarTransacaoAsync(usuarioId, request, ct);
            return Results.Created();
        }).RequireAuthorization(RolesConsts.Tesoureiro);

        endpoints.MapPut("/api/transacoes/{id}/status", async (
            Guid id,
            AtualizarStatusTransacaoRequest request,
            ITransacaoService service,
            CancellationToken ct) =>
        {
            await service.AtualizarStatusTransacaoAsync(id, request.Status, ct);
            return Results.Created();
        }).RequireAuthorization(RolesConsts.Admin);
    }
}