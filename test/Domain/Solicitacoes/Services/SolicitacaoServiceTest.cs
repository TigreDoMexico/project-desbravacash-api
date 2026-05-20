using AutoBogus;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Services;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Solicitacoes.Services;

public class SolicitacaoServiceTest
{
    private readonly ISolicitacaoRepository _repository = Substitute.For<ISolicitacaoRepository>();
    private readonly IDesafioRepository _desafioRepository = Substitute.For<IDesafioRepository>();
    private readonly ILogger<SolicitacaoService> _logger = Substitute.For<ILogger<SolicitacaoService>>();
    private readonly SolicitacaoService _service;

    public SolicitacaoServiceTest() => _service = new SolicitacaoService(_repository, _desafioRepository, _logger);

    [Fact]
    public async Task CriarAsync_Deve_Criar_Solicitacao_Manual_Quando_SemDesafioId()
    {
        var unidadeId = Guid.NewGuid();
        var criadoPor = Guid.NewGuid();

        await _service.CriarAsync(unidadeId, criadoPor, "Descrição", 100, null, CancellationToken.None);

        await _repository.Received(1).CriarAsync(
            Arg.Is<Solicitacao>(s =>
                s.Tipo == TipoSolicitacao.Manual &&
                s.Status == StatusSolicitacao.Solicitado &&
                s.UnidadeId == unidadeId &&
                s.CriadoPor == criadoPor &&
                s.Valor == 100 &&
                s.DesafioId == null),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CriarAsync_Deve_Criar_Solicitacao_Desafio_Quando_ComDesafioId()
    {
        var desafioId = Guid.NewGuid();

        await _service.CriarAsync(Guid.NewGuid(), Guid.NewGuid(), "Descrição", 50, desafioId, CancellationToken.None);

        await _repository.Received(1).CriarAsync(
            Arg.Is<Solicitacao>(s =>
                s.Tipo == TipoSolicitacao.Desafio &&
                s.DesafioId == desafioId),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CriarPorDesafioAsync_Deve_Retornar_False_Quando_Desafio_Nao_Encontrado()
    {
        _desafioRepository.ObterPorIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Desafio?)null);

        var resultado = await _service.CriarPorDesafioAsync(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        Assert.False(resultado);
        await _repository.DidNotReceive().CriarAsync(Arg.Any<Solicitacao>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CriarPorDesafioAsync_Deve_Retornar_False_Quando_Desafio_NaoPodeSolicitar()
    {
        var desafio = new AutoFaker<Desafio>().RuleFor(d => d.PodeSolicitar, false).Generate();

        _desafioRepository.ObterPorIdAsync(desafio.Id, Arg.Any<CancellationToken>()).Returns(desafio);

        var resultado = await _service.CriarPorDesafioAsync(desafio.Id, Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        Assert.False(resultado);
        await _repository.DidNotReceive().CriarAsync(Arg.Any<Solicitacao>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CriarPorDesafioAsync_Deve_Criar_Solicitacao_E_Retornar_True_Quando_Desafio_Valido()
    {
        var desafio = new AutoFaker<Desafio>().RuleFor(d => d.PodeSolicitar, true).Generate();
        var unidadeId = Guid.NewGuid();
        var criadoPor = Guid.NewGuid();

        _desafioRepository.ObterPorIdAsync(desafio.Id, Arg.Any<CancellationToken>()).Returns(desafio);

        var resultado = await _service.CriarPorDesafioAsync(desafio.Id, unidadeId, criadoPor, CancellationToken.None);

        Assert.True(resultado);
        await _repository.Received(1).CriarAsync(
            Arg.Is<Solicitacao>(s =>
                s.Tipo == TipoSolicitacao.Desafio &&
                s.DesafioId == desafio.Id &&
                s.Valor == desafio.Pontuacao &&
                s.Descricao == desafio.Descricao &&
                s.UnidadeId == unidadeId &&
                s.CriadoPor == criadoPor),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ListarTodasAsync_Deve_Retornar_Lista_Mapeada()
    {
        var solicitacoes = new AutoFaker<Solicitacao>()
            .RuleFor(s => s.Unidade, new AutoFaker<TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models.Unidade>().Generate())
            .Generate(3);

        _repository.ListarTodasAsync(Arg.Any<CancellationToken>()).Returns(solicitacoes);

        var resultado = await _service.ListarTodasAsync(CancellationToken.None);

        Assert.Equal(3, resultado.Count);
    }

    [Fact]
    public async Task AprovarAsync_Deve_Retornar_False_Quando_Solicitacao_Nao_Encontrada()
    {
        _repository.ObterPorIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Solicitacao?)null);

        var resultado = await _service.AprovarAsync(Guid.NewGuid(), Guid.NewGuid(), null, CancellationToken.None);

        Assert.False(resultado);
        await _repository.DidNotReceive().AtualizarComTransacaoAsync(Arg.Any<Solicitacao>(), Arg.Any<Transacao>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AprovarAsync_Deve_Retornar_False_Quando_Status_Nao_Solicitado()
    {
        var solicitacao = new AutoFaker<Solicitacao>().RuleFor(s => s.Status, StatusSolicitacao.Aprovado).Generate();

        _repository.ObterPorIdAsync(solicitacao.Id, Arg.Any<CancellationToken>()).Returns(solicitacao);

        var resultado = await _service.AprovarAsync(solicitacao.Id, Guid.NewGuid(), null, CancellationToken.None);

        Assert.False(resultado);
        await _repository.DidNotReceive().AtualizarComTransacaoAsync(Arg.Any<Solicitacao>(), Arg.Any<Transacao>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AprovarAsync_Deve_Aprovar_Com_Valor_Da_Solicitacao_Quando_Valor_Nao_Informado()
    {
        var solicitacao = new AutoFaker<Solicitacao>().RuleFor(s => s.Status, StatusSolicitacao.Solicitado).Generate();
        var aprovadoPor = Guid.NewGuid();

        _repository.ObterPorIdAsync(solicitacao.Id, Arg.Any<CancellationToken>()).Returns(solicitacao);

        var resultado = await _service.AprovarAsync(solicitacao.Id, aprovadoPor, null, CancellationToken.None);

        Assert.True(resultado);
        Assert.Equal(StatusSolicitacao.Aprovado, solicitacao.Status);
        await _repository.Received(1).AtualizarComTransacaoAsync(
            Arg.Is<Solicitacao>(s => s.Status == StatusSolicitacao.Aprovado),
            Arg.Is<Transacao>(t =>
                t.Valor == solicitacao.Valor &&
                t.UnidadeId == solicitacao.UnidadeId &&
                t.CriadoPor == aprovadoPor &&
                t.Tipo == TipoTransacao.Credito),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AprovarAsync_Deve_Usar_Valor_Informado_Quando_Fornecido()
    {
        var solicitacao = new AutoFaker<Solicitacao>().RuleFor(s => s.Status, StatusSolicitacao.Solicitado).Generate();
        var valorOverride = 999;

        _repository.ObterPorIdAsync(solicitacao.Id, Arg.Any<CancellationToken>()).Returns(solicitacao);

        await _service.AprovarAsync(solicitacao.Id, Guid.NewGuid(), valorOverride, CancellationToken.None);

        await _repository.Received(1).AtualizarComTransacaoAsync(
            Arg.Any<Solicitacao>(),
            Arg.Is<Transacao>(t => t.Valor == valorOverride),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ReprovarAsync_Deve_Retornar_False_Quando_Solicitacao_Nao_Encontrada()
    {
        _repository.ObterPorIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Solicitacao?)null);

        var resultado = await _service.ReprovarAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.False(resultado);
        await _repository.DidNotReceive().AtualizarAsync(Arg.Any<Solicitacao>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ReprovarAsync_Deve_Retornar_False_Quando_Status_Nao_Solicitado()
    {
        var solicitacao = new AutoFaker<Solicitacao>().RuleFor(s => s.Status, StatusSolicitacao.Aprovado).Generate();

        _repository.ObterPorIdAsync(solicitacao.Id, Arg.Any<CancellationToken>()).Returns(solicitacao);

        var resultado = await _service.ReprovarAsync(solicitacao.Id, CancellationToken.None);

        Assert.False(resultado);
        await _repository.DidNotReceive().AtualizarAsync(Arg.Any<Solicitacao>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ReprovarAsync_Deve_Reprovar_E_Retornar_True_Quando_Solicitacao_Valida()
    {
        var solicitacao = new AutoFaker<Solicitacao>().RuleFor(s => s.Status, StatusSolicitacao.Solicitado).Generate();

        _repository.ObterPorIdAsync(solicitacao.Id, Arg.Any<CancellationToken>()).Returns(solicitacao);

        var resultado = await _service.ReprovarAsync(solicitacao.Id, CancellationToken.None);

        Assert.True(resultado);
        Assert.Equal(StatusSolicitacao.Rejeitado, solicitacao.Status);
        await _repository.Received(1).AtualizarAsync(
            Arg.Is<Solicitacao>(s => s.Status == StatusSolicitacao.Rejeitado),
            Arg.Any<CancellationToken>());
    }
}
