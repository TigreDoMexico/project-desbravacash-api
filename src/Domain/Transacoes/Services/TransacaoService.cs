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
            Tipo = request.TipoTransacao,
            Status = StatusTransacao.Pendente,
            Valor = request.Valor,
            Descricao = request.Descricao,
            UnidadeId = request.UnidadeId,
            CriadoEm = DateTimeOffset.UtcNow,
            CriadoPor = usuarioId,
        };

        await repository.AdicionarPendenteAsync(transacao, ct);
    }

    public async Task AtualizarStatusTransacaoAsync(Guid transacaoId, StatusTransacao novoStatus, CancellationToken ct)
    {
        var transacao = await repository.ObterTransacaoPorIdAsync(transacaoId, ct);
        if (transacao is null) return;

        transacao.Status = novoStatus;
        await repository.AtualizarTransacaoAsync(transacao, ct);
    }

    public async Task<ListarDadosTransacaoResponse> ObterTransacoesPendentesAsync(CancellationToken ct)
    {
        var transacoes = await repository.ListarTransacoesPendentesAsync(ct);
        return new ListarDadosTransacaoResponse
        {
            Transacoes = transacoes.Select(t => (DadosTransacaoResponse)t).ToList()
        };
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