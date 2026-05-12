using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Services;

public class DesafioService(IDesafioRepository repository) : IDesafioService
{
    public async Task<List<ListarDesafiosResponse>> ListarDesafiosPorUnidadeAsync(Guid unidadeId, CancellationToken ct)
    {
        var desafios = await repository.ListarDesafiosPorUnidadeAsync(unidadeId, ct);
        return desafios.Select(r => (ListarDesafiosResponse)r).ToList();
    }
}