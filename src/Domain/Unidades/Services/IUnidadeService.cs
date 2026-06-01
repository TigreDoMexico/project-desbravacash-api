using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Services;

public interface IUnidadeService
{
    Task<ListaUnidadesResponse> ListarAsync(CancellationToken ct = default);

    Task<ObterUnidadeDashResponse?> BuscarDashboardAsync(Guid usuarioId, Guid unidadeId, CancellationToken ct = default);
}
