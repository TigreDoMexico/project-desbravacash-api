using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Services;

public interface ISolicitacaoService
{
    Task CriarAsync(Guid unidadeId, Guid criadoPor, string descricao, int valor, Guid? desafioId, CancellationToken ct);
    Task<bool> CriarPorDesafioAsync(Guid desafioId, Guid unidadeId, Guid criadoPor, CancellationToken ct);
    Task<List<DadosSolicitacaoResponse>> ListarTodasAsync(CancellationToken ct);
    Task<bool> AprovarAsync(Guid id, Guid aprovadoPor, int? valor, CancellationToken ct);
    Task<bool> ReprovarAsync(Guid id, CancellationToken ct);
}