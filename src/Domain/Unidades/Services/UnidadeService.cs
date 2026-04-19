using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Responses;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Services;

public class UnidadeService(IUnidadeRepository repository, IUsuarioRepository usuarioRepository) : IUnidadeService
{
    public async Task<ObterUnidadeDashResponse?> BuscarDashboardAsync(Guid usuarioId, Guid unidadeId, CancellationToken ct = default)
    {
        if (!await usuarioRepository.ExisteNaUnidadeAsync(usuarioId, unidadeId, ct))
            return null;

        var unidade = await repository.BuscarPorIdComTransacoesAsync(unidadeId, ct);
        if (unidade is null) return null;

        var transacoesAprovadas = unidade.Transacoes.Where(t => t.Status == StatusTransacao.Aprovado);
        var saldo = transacoesAprovadas.Sum(t => t.Tipo == TipoTransacao.Credito ? t.Valor : -t.Valor);

        return new ObterUnidadeDashResponse
        {
            Unidade = new DadosUnidadeResponse { Nome = unidade.Nome },
            Saldo = saldo.ToString(),
        };
    }
}
