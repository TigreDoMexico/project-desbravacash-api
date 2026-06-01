using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Responses;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Services;

public class UnidadeService(IUnidadeRepository repository, IUsuarioRepository usuarioRepository) : IUnidadeService
{
    public async Task<ListaUnidadesResponse> ListarAsync(CancellationToken ct = default)
    {
        var unidades = await repository.BuscarTodasAsync(ct);
        return new ListaUnidadesResponse
        {
            Unidades = unidades.Select(u => (DadosUnidadeResponse)u).ToList(),
        };
    }

    public async Task<ObterUnidadeDashResponse?> BuscarDashboardAsync(Guid usuarioId, Guid unidadeId,
        CancellationToken ct = default)
    {
        if (!await usuarioRepository.ExisteNaUnidadeAsync(usuarioId, unidadeId, ct))
        {
            return null;
        }

        var unidade = await repository.BuscarPorIdComTransacoesAsync(unidadeId, ct);
        if (unidade is null)
        {
            return null;
        }

        var saldo = unidade.Transacoes.Sum(t => t.Tipo == TipoTransacao.Credito ? t.Valor : -t.Valor);

        return new ObterUnidadeDashResponse
        {
            Unidade = unidade,
            Saldo = saldo.ToString(),
        };
    }
}