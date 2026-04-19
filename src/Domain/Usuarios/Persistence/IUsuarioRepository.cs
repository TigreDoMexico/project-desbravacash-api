using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;

public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorTelefoneAsync(string telefone, CancellationToken ct = default);

    Task<bool> ExisteNaUnidadeAsync(Guid usuarioId, Guid unidadeId, CancellationToken ct = default);
}