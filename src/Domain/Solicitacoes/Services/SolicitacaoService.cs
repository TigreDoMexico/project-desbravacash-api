using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Responses;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Services;

public class SolicitacaoService(
    ISolicitacaoRepository repository,
    IDesafioRepository desafioRepository,
    ILogger<SolicitacaoService> logger)
    : ISolicitacaoService
{
    public async Task CriarAsync(
        Guid unidadeId,
        Guid criadoPor,
        string descricao,
        int valor,
        Guid? desafioId,
        CancellationToken ct)
    {
        logger.LogInformation(
            "Criando Solicitacao: Unidade {unidadeId}, Valor {valor}, Descricao {descricao}",
            unidadeId,
            valor,
            descricao);

        try
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
        catch (SolicitacaoException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar solicitacao. {ex}", ex);
            throw new SolicitacaoException("Ocorreu um erro ao criar a solicitação. Tente novamente.");
        }

        logger.LogInformation("Solicitacao criada com sucesso!");
    }

    public async Task<bool> CriarPorDesafioAsync(Guid desafioId, Guid unidadeId, Guid criadoPor, CancellationToken ct)
    {
        logger.LogInformation(
            "Criando Solicitacao para Desafio: Desafio {desafioId}, Unidade {unidadeId}",
            desafioId,
            unidadeId);

        Desafio? desafio;

        try
        {
            desafio = await desafioRepository.ObterPorIdAsync(desafioId, ct);
            if (desafio is null)
            {
                logger.LogWarning("Desafio {desafioId} não encontrado", desafioId);
                return false;
            }

            if (!desafio.PodeSolicitar)
            {
                logger.LogWarning("Desafio {desafioId} não pode ser solicitado", desafioId);
                throw new SolicitacaoException("Este desafio não está disponível para solicitação.");
            }

            var jaExiste = await repository.ExisteSolicitacaoAtivaAsync(unidadeId, ct);
            if (jaExiste)
            {
                logger.LogWarning("Unidade {unidadeId} já possui solicitação ativa para o desafio {desafioId}", unidadeId, desafioId);
                throw new SolicitacaoException("Já existe uma solicitação ativa para esta unidade.");
            }
        }
        catch (SolicitacaoException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar desafio. {ex}", ex);
            throw new SolicitacaoException("Ocorreu um erro ao processar a solicitação. Tente novamente.");
        }

        await CriarAsync(unidadeId, criadoPor, desafio.Descricao, desafio.Pontuacao, desafio.Id, ct);
        return true;
    }

    public async Task<List<DadosSolicitacaoResponse>> ListarTodasAsync(CancellationToken ct)
    {
        logger.LogInformation("Listando todas as solicitacoes");

        var solicitacoes = await repository.ListarTodasAsync(ct);
        return solicitacoes.Select(s => (DadosSolicitacaoResponse)s).ToList();
    }

    public async Task<bool> AprovarAsync(Guid id, Guid aprovadoPor, int? valor, CancellationToken ct)
    {
        logger.LogInformation("Aprovando solicitação {id}", id);

        try
        {
            var solicitacao = await repository.ObterPorIdAsync(id, ct);
            if (solicitacao is null || solicitacao.Status != StatusSolicitacao.Solicitado)
            {
                logger.LogWarning("Solicitação {id} não encontrada ou não está solicitada", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao aprovar solicitacao. {ex}", ex);
            throw;
        }
    }

    public async Task<bool> ReprovarAsync(Guid id, CancellationToken ct)
    {
        logger.LogInformation("Reprovando solicitação {id}", id);

        try
        {
            var solicitacao = await repository.ObterPorIdAsync(id, ct);
            if (solicitacao is null || solicitacao.Status != StatusSolicitacao.Solicitado)
            {
                logger.LogWarning("Solicitação {id} não encontrada ou não está solicitada", id);
                return false;
            }

            solicitacao.Status = StatusSolicitacao.Rejeitado;
            await repository.AtualizarAsync(solicitacao, ct);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao reprovar solicitacao. {ex}", ex);
            throw;
        }

    }
}