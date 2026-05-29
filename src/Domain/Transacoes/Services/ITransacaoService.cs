using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;

public interface ITransacaoService
{
    public Task<ListarDadosTransacaoResponse> ObterTransacoesPorUnidadeAsync(Guid unidadeId, CancellationToken ct);
}