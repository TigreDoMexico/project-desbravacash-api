using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;

public interface IDesafioRepository
{
    Task<List<Desafio>> ListarDesafiosPorUnidadeAsync(Guid unidadeId, CancellationToken ct);
    Task<Desafio?> ObterPorIdAsync(Guid id, CancellationToken ct);
}