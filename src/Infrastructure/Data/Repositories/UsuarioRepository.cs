using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;

public class UsuarioRepository(DesbravaCashDbContext db) : IUsuarioRepository
{
    public Task<Usuario?> BuscarPorTelefoneAsync(string telefone, CancellationToken ct = default) =>
        db.Usuarios.FirstOrDefaultAsync(u => u.Telefone == telefone, ct);

    public Task<bool> ExisteNaUnidadeAsync(Guid usuarioId, Guid unidadeId, CancellationToken ct = default) =>
        db.Usuarios.AnyAsync(u => u.Id == usuarioId && u.UnidadeId == unidadeId, ct);
}
