using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Persistence;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;

public class TransacaoRepository(DesbravaCashDbContext db)  : ITransacaoRepository
{
    public async Task<Transacao?> ObterTransacaoPorIdAsync(Guid transacaoId, CancellationToken ct)
        => await db.Transacoes.FindAsync([transacaoId], ct);

    public async Task AdicionarPendenteAsync(Transacao transacao, CancellationToken ct)
    {
        db.Transacoes.Add(transacao);
        await db.SaveChangesAsync(ct);
    }

    public async Task AtualizarTransacaoAsync(Transacao transacao, CancellationToken ct)
    {
        db.Transacoes.Update(transacao);
        await db.SaveChangesAsync(ct);
    }

    public async Task<List<Transacao>> ListarTransacoesPendentesAsync(CancellationToken ct)
        => await db.Transacoes
            .Include(t => t.Unidade)
            .Where(t => t.Status == StatusTransacao.Pendente)
            .OrderByDescending(t => t.CriadoEm)
            .ToListAsync(ct);

    public async Task<List<Transacao>> ListarTodasTransacoesPorUnidadeAsync(Guid unidadeId, CancellationToken ct)
        => await db.Transacoes
            .Where(t => t.UnidadeId == unidadeId)
            .OrderByDescending(t => t.CriadoEm)
            .ToListAsync(ct);
}