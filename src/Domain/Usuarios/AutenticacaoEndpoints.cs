using FluentValidation;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Responses;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios;

public class AutenticacaoEndpoints : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/auth/login",
            async (
                AcessoRequest request,
                IValidator<AcessoRequest> validator,
                IUsuarioService service,
                IJwtService jwt,
                CancellationToken ct) =>
            {
                var validation = await validator.ValidateAsync(request, ct);
                if (!validation.IsValid)
                    return Results.ValidationProblem(validation.ToDictionary());

                var usuario = await service.AcessarAsync(request.Telefone, request.Senha, ct);
                return usuario is null
                    ? Results.Unauthorized()
                    : Results.Ok(new AcessoResponse(jwt.GerarToken(usuario)));
            });
    }
}