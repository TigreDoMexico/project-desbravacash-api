using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;

public class TransacaoService(ITransacaoRepository repository) : ITransacaoService
{
    public async Task CriarTransacaoAsync(Guid usuarioId, CriarTransacaoRequest request, CancellationToken ct)
    {
        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            Tipo = request.Tipo,
            Status = StatusTransacao.Pendente,
            Valor = request.Valor,
            Descricao = request.Descricao,
            UnidadeId = request.UnidadeId,
            CriadoEm = DateTimeOffset.UtcNow,
            CriadoPor = usuarioId,
        };

        await repository.AdicionarAsync(transacao, ct);
    }

    public async Task<ListarDadosTransacaoResponse> ObterTransacoesPorUnidadeAsync(Guid unidadeId, CancellationToken ct)
    {
        var transacoes = await repository.ListarTodasTransacoesPorUnidadeAsync(unidadeId, ct);
        return new ListarDadosTransacaoResponse
        {
            Transacoes = transacoes.Select(t => (DadosTransacaoResponse)t).ToList()
        };
    }
}