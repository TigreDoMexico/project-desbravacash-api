using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Responses;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Services;

public class SolicitacaoService(ISolicitacaoRepository repository, IDesafioRepository desafioRepository) : ISolicitacaoService
{
    public async Task CriarAsync(Guid unidadeId, Guid criadoPor, string descricao, int valor, Guid? desafioId, CancellationToken ct)
    {
        var solicitacao = new Solicitacao
        {
            Id = Guid.NewGuid(),
            Tipo = desafioId.HasValue ? TipoSolicitacao.Desafio : TipoSolicitacao.Manual,
            Status = StatusSolicitacao.Solicitado,
            UnidadeId = unidadeId,
            CriadoPor = criadoPor,
            CriadoEm = DateTimeOffset.UtcNow,
            Descricao = descricao,
            Valor = valor,
            DesafioId = desafioId,
        };

        await repository.CriarAsync(solicitacao, ct);
    }

    public async Task<bool> CriarPorDesafioAsync(Guid desafioId, Guid unidadeId, Guid criadoPor, CancellationToken ct)
    {
        var desafio = await desafioRepository.ObterPorIdAsync(desafioId, ct);
        if (desafio is null || !desafio.PodeSolicitar)
            return false;

        await CriarAsync(unidadeId, criadoPor, desafio.Descricao, desafio.Pontuacao, desafio.Id, ct);
        return true;
    }
    public async Task<List<DadosSolicitacaoResponse>> ListarTodasAsync(CancellationToken ct)
    {
        var solicitacoes = await repository.ListarTodasAsync(ct);
        return solicitacoes.Select(s => (DadosSolicitacaoResponse)s).ToList();
    }

    public async Task<bool> AprovarAsync(Guid id, Guid aprovadoPor, int? valor, CancellationToken ct)
    {
        var solicitacao = await repository.ObterPorIdAsync(id, ct);
        if (solicitacao is null || solicitacao.Status != StatusSolicitacao.Solicitado)
        {
            return false;
        }
        
        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            Tipo = TipoTransacao.Credito,
            Valor = valor ?? solicitacao.Valor,
            Descricao = solicitacao.Descricao,
            UnidadeId = solicitacao.UnidadeId,
            CriadoPor = aprovadoPor,
            CriadoEm = DateTimeOffset.UtcNow,
        };

        solicitacao.Status = StatusSolicitacao.Aprovado;
        solicitacao.TransacaoId = transacao.Id;

        await repository.AtualizarComTransacaoAsync(solicitacao, transacao, ct);
        return true;
    }

    public async Task<bool> ReprovarAsync(Guid id, CancellationToken ct)
    {
        var solicitacao = await repository.ObterPorIdAsync(id, ct);
        if (solicitacao is null || solicitacao.Status != StatusSolicitacao.Solicitado)
            return false;

        solicitacao.Status = StatusSolicitacao.Rejeitado;
        await repository.AtualizarAsync(solicitacao, ct);
        return true;
    }
}