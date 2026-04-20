using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Responses;

public record AcessoResponse(string Token, UsuarioRole Role);
