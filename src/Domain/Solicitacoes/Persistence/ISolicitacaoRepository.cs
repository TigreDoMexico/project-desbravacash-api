using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Persistence;

public interface ISolicitacaoRepository
{
    Task CriarAsync(Solicitacao solicitacao, CancellationToken ct);
    
    Task<List<Solicitacao>> ListarTodasAsync(CancellationToken ct);
    
    Task<Solicitacao?> ObterPorIdAsync(Guid id, CancellationToken ct);
    
    Task AtualizarComTransacaoAsync(Solicitacao solicitacao, Transacao transacao, CancellationToken ct);
    
    Task AtualizarAsync(Solicitacao solicitacao, CancellationToken ct);
}