using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;

public class SolicitacaoRepository(DesbravaCashDbContext db) : ISolicitacaoRepository
{
    public async Task CriarAsync(Solicitacao solicitacao, CancellationToken ct)
    {
        db.Solicitacoes.Add(solicitacao);
        await db.SaveChangesAsync(ct);
    }
    
    public async Task<List<Solicitacao>> ListarTodasAsync(CancellationToken ct)
        => await db.Solicitacoes
            .Include(s => s.Unidade)
            .Include(s => s.Desafio)
            .OrderByDescending(s => s.CriadoEm)
            .ToListAsync(ct);

    public async Task<Solicitacao?> ObterPorIdAsync(Guid id, CancellationToken ct)
        => await db.Solicitacoes.FindAsync([id], ct);

    public async Task AtualizarComTransacaoAsync(Solicitacao solicitacao, Transacao transacao, CancellationToken ct)
    {
        db.Transacoes.Add(transacao);
        db.Solicitacoes.Update(solicitacao);
        
        await db.SaveChangesAsync(ct);
    }

    public async Task AtualizarAsync(Solicitacao solicitacao, CancellationToken ct)
    {
        db.Solicitacoes.Update(solicitacao);
        await db.SaveChangesAsync(ct);
    }

    public async Task<bool> ExisteSolicitacaoAtivaAsync(Guid unidadeId, Guid desafioId, CancellationToken ct)
        => await db.Solicitacoes.AnyAsync(
            s => s.UnidadeId == unidadeId
              && s.DesafioId == desafioId
              && s.Status != StatusSolicitacao.Rejeitado,
            ct);
}
