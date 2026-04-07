using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;

public class UsuarioRepository(DesbravaCashDbContext db) : IUsuarioRepository
{
    public Task<Usuario?> BuscarPorTelefoneAsync(string telefone, CancellationToken ct = default) =>
        db.Usuarios.FirstOrDefaultAsync(u => u.Telefone == telefone, ct);
}
