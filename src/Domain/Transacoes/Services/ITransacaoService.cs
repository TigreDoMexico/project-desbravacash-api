using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;

public interface ITransacaoService
{
    public Task CriarTransacaoAsync(Guid usuarioId, CriarTransacaoRequest request, CancellationToken ct);

    public Task AtualizarStatusTransacaoAsync(Guid transacaoId, StatusTransacao novoStatus, CancellationToken ct);

    public Task<ListarDadosTransacaoResponse> ObterTransacoesPendentesAsync(CancellationToken ct);

    public Task<ListarDadosTransacaoResponse> ObterTransacoesPorUnidadeAsync(Guid unidadeId, CancellationToken ct);
}