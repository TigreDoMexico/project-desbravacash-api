using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Services;

public interface IDesafioService
{
    Task<List<ListarDesafiosResponse>> ListarDesafiosPorUnidadeAsync(Guid unidadeId, CancellationToken ct);
}