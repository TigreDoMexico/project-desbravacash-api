using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;

public class TransacaoService(ITransacaoRepository repository) : ITransacaoService
{
    public async Task<ListarDadosTransacaoResponse> ObterTransacoesPorUnidadeAsync(Guid unidadeId, CancellationToken ct)
    {
        var transacoes = await repository.ListarTodasTransacoesPorUnidadeAsync(unidadeId, ct);
        return new ListarDadosTransacaoResponse
        {
            Transacoes = transacoes.Select(t => (DadosTransacaoResponse)t).ToList()
        };
    }
}