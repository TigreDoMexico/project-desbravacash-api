using AutoBogus;
using NSubstitute;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Transacoes.Services;

public class TransacaoServiceTest
{
    private readonly ITransacaoRepository _repository = Substitute.For<ITransacaoRepository>();
    private readonly TransacaoService _service;

    public TransacaoServiceTest() => _service = new TransacaoService(_repository);

    [Fact]
    public async Task CriarTransacaoAsync_Deve_Chamar_AdicionarPendenteAsync_Com_Status_Pendente()
    {
        var usuarioId = Guid.NewGuid();
        var request = new AutoFaker<CriarTransacaoRequest>().Generate();

        await _service.CriarTransacaoAsync(usuarioId, request, CancellationToken.None);

        await _repository.Received(1).AdicionarPendenteAsync(
            Arg.Is<Transacao>(t =>
                t.Status == StatusTransacao.Pendente &&
                t.Valor == request.Valor &&
                t.Descricao == request.Descricao &&
                t.UnidadeId == request.UnidadeId &&
                t.Tipo == request.TipoTransacao &&
                t.CriadoPor == usuarioId),
            CancellationToken.None);
    }

    [Fact]
    public async Task AtualizarStatusTransacaoAsync_Deve_Atualizar_Status_Quando_Transacao_Existe()
    {
        var transacao = new AutoFaker<Transacao>().Generate();
        var novoStatus = StatusTransacao.Aprovado;

        _repository.ObterTransacaoPorIdAsync(transacao.Id, CancellationToken.None).Returns(transacao);

        await _service.AtualizarStatusTransacaoAsync(transacao.Id, novoStatus, CancellationToken.None);

        Assert.Equal(novoStatus, transacao.Status);
        await _repository.Received(1).AtualizarTransacaoAsync(transacao, CancellationToken.None);
    }

    [Fact]
    public async Task AtualizarStatusTransacaoAsync_Deve_Ignorar_Quando_Transacao_Nao_Existe()
    {
        var transacaoId = Guid.NewGuid();

        _repository.ObterTransacaoPorIdAsync(transacaoId, CancellationToken.None).Returns((Transacao?)null);

        await _service.AtualizarStatusTransacaoAsync(transacaoId, StatusTransacao.Aprovado, CancellationToken.None);

        await _repository.DidNotReceive().AtualizarTransacaoAsync(Arg.Any<Transacao>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ObterTransacoesPendentesAsync_Deve_Retornar_Transacoes_Pendentes()
    {
        var transacoes = new AutoFaker<Transacao>().Generate(3);

        _repository.ListarTransacoesPendentesAsync(CancellationToken.None).Returns(transacoes);

        var resultado = await _service.ObterTransacoesPendentesAsync(CancellationToken.None);

        Assert.Equal(transacoes.Count, resultado.Transacoes.Count);
    }

    [Fact]
    public async Task ObterTransacoesPorUnidadeAsync_Deve_Retornar_Transacoes_Da_Unidade()
    {
        var unidadeId = Guid.NewGuid();
        var transacoes = new AutoFaker<Transacao>().Generate(2);

        _repository.ListarTodasTransacoesPorUnidadeAsync(unidadeId, CancellationToken.None).Returns(transacoes);

        var resultado = await _service.ObterTransacoesPorUnidadeAsync(unidadeId, CancellationToken.None);

        Assert.Equal(transacoes.Count, resultado.Transacoes.Count);
    }
}
