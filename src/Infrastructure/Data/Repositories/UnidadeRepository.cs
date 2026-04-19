using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Persistence;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;

public class UnidadeRepository(DesbravaCashDbContext db) : IUnidadeRepository
{
    public Task<Unidade?> BuscarPorIdComTransacoesAsync(Guid id, CancellationToken ct = default) =>
        db.Unidades
            .Include(u => u.Transacoes)
            .FirstOrDefaultAsync(u => u.Id == id, ct);
}
