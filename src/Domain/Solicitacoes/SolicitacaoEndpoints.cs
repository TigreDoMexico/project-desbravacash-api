using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Services;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes;

public class SolicitacaoEndpoints : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/solicitacoes", async (
            ISolicitacaoService service,
            CancellationToken ct) =>
            Results.Ok(await service.ListarTodasAsync(ct))
        ).RequireAuthorization(RolesConsts.Admin);

        endpoints.MapPost("/api/solicitacoes", async (
            DadosSolicitacaoRequest request,
            ClaimsPrincipal user,
            IValidator<DadosSolicitacaoRequest> validator,
            ISolicitacaoService service,
            CancellationToken ct) =>
        {
            var validation = await validator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var criadoPor = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

            try
            {
                await service.CriarAsync(request.UnidadeId, criadoPor, request.Descricao, request.Valor, null, ct);
                return Results.Created();
            }
            catch (SolicitacaoException ex)
            {
                return Results.BadRequest(new ApiError(ex.Message));
            }
            catch
            {
                return Results.BadRequest(new ApiError("Ocorreu um erro ao criar a solicitação. Tente novamente."));
            }
        }).RequireAuthorization();

        endpoints.MapPost("/api/solicitacoes/desafio/{desafioId:guid}", async (
            Guid desafioId,
            ClaimsPrincipal user,
            ISolicitacaoService service,
            CancellationToken ct) =>
        {
            var unidadeId = Guid.Parse(user.FindFirstValue("unidade_id")!);
            var criadoPor = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

            try
            {
                var sucesso = await service.CriarPorDesafioAsync(desafioId, unidadeId, criadoPor, ct);
                return sucesso ? Results.Created() : Results.NotFound();
            }
            catch (SolicitacaoException ex)
            {
                return Results.BadRequest(new ApiError(ex.Message));
            }
            catch
            {
                return Results.BadRequest(new ApiError("Ocorreu um erro ao processar a solicitação. Tente novamente."));
            }
        }).RequireAuthorization();

        endpoints.MapPut("/api/solicitacoes/{id:guid}/aprovar", async (
            Guid id,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] AprovarSolicitacaoRequest? request,
            ClaimsPrincipal user,
            ISolicitacaoService service,
            CancellationToken ct) =>
        {
            var aprovadoPor = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var sucesso = await service.AprovarAsync(id, aprovadoPor, request?.Valor, ct);
            return sucesso ? Results.NoContent() : Results.NotFound();
        }).RequireAuthorization(RolesConsts.Admin);

        endpoints.MapPut("/api/solicitacoes/{id:guid}/reprovar", async (
            Guid id,
            ISolicitacaoService service,
            CancellationToken ct) =>
        {
            var sucesso = await service.ReprovarAsync(id, ct);
            return sucesso ? Results.NoContent() : Results.NotFound();
        }).RequireAuthorization(RolesConsts.Admin);
    }
}