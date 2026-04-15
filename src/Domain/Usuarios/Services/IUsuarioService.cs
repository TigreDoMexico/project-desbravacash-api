using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;

public interface IUsuarioService
{
    Task<Usuario?> AcessarAsync(string telefone, string senha, CancellationToken ct = default);
}