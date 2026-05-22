using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;

public class DesafioRepository(DesbravaCashDbContext db) : IDesafioRepository
{
    public async Task<List<Desafio>> ListarDesafiosPorUnidadeAsync(Guid unidadeId, CancellationToken ct)
        => await db.Desafios
            .Select(d => new Desafio
            {
                Id = d.Id,
                Descricao = d.Descricao,
                Pontuacao = d.Pontuacao,
                DataConclusao = d.DataConclusao,
                PodeSolicitar = d.PodeSolicitar,

                Solicitado = db.Solicitacoes.Any(s =>
                    s.UnidadeId == unidadeId &&
                    s.DesafioId == d.Id),

                Concluido = db.Solicitacoes.Any(s =>
                    s.UnidadeId == unidadeId &&
                    s.DesafioId == d.Id &&
                    s.Status == StatusSolicitacao.Aprovado)
            })
            .OrderBy(d => d.DataConclusao)
            .ToListAsync(ct);

    public async Task<Desafio?> ObterPorIdAsync(Guid id, CancellationToken ct)
        => await db.Desafios.FindAsync([id], ct);
}