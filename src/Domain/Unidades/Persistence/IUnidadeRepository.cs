using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Persistence;

public interface IUnidadeRepository
{
    Task<Unidade?> BuscarPorIdComTransacoesAsync(Guid id, CancellationToken ct = default);
}
