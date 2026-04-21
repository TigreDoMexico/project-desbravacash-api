using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Persistence;

public interface ITransacaoRepository
{
    public Task<Transacao?> ObterTransacaoPorIdAsync(Guid transacaoId, CancellationToken ct);

    public Task AdicionarPendenteAsync(Transacao transacao, CancellationToken ct);

    public Task AtualizarTransacaoAsync(Transacao transacao, CancellationToken ct);

    public Task<List<Transacao>> ListarTransacoesPendentesAsync(CancellationToken ct);

    public Task<List<Transacao>> ListarTodasTransacoesPorUnidadeAsync(Guid unidadeId, CancellationToken ct);
}