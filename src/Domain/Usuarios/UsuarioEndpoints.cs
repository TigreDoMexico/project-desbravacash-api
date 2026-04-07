using FluentValidation;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Requests;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios;

public class UsuarioEndpoints : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/usuario/acessar", async (AcessoRequest request, IValidator<AcessoRequest> validator) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var usuario = new Usuario
            {
                Telefone = request.Telefone,
                Senha = request.Senha
            };

            return Results.Ok(usuario);
        });
    }
}
