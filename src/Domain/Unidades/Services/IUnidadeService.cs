using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Services;

public interface IUnidadeService
{
    Task<ObterUnidadeDashResponse?> BuscarDashboardAsync(Guid usuarioId, Guid unidadeId, CancellationToken ct = default);
}
