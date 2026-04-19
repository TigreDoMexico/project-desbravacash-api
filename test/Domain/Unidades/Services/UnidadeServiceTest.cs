using AutoBogus;
using NSubstitute;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Services;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Unidades.Services;

public class UnidadeServiceTest
{
    private readonly IUnidadeRepository _unidadeRepository = Substitute.For<IUnidadeRepository>();
    private readonly IUsuarioRepository _usuarioRepository = Substitute.For<IUsuarioRepository>();
    private readonly UnidadeService _service;

    public UnidadeServiceTest() => _service = new UnidadeService(_unidadeRepository, _usuarioRepository);

    [Fact]
    public async Task BuscarDashboardAsync_Deve_RetornarDadosCorretos()
    {
        // ARRANGE
        var valorTransacao = 100;
        var totalTransacoes = 5;

        var usuarioId = Guid.NewGuid();
        var unidade = new AutoFaker<Unidade>()
            .RuleFor(x => x.Transacoes, new AutoFaker<Transacao>()
                .RuleFor(x => x.Valor, valorTransacao)
                .RuleFor(x => x.Status, StatusTransacao.Aprovado)
                .RuleFor(x => x.Tipo, TipoTransacao.Credito)
                .Generate(totalTransacoes))
            .Generate();

        _unidadeRepository
            .BuscarPorIdComTransacoesAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(unidade);

        _usuarioRepository.ExisteNaUnidadeAsync(Arg.Is(usuarioId), Arg.Is(unidade.Id)).Returns(true);

        // ACT
        var result = await _service.BuscarDashboardAsync(usuarioId, unidade.Id, CancellationToken.None);

        // ASSERT
        await _unidadeRepository
            .Received(1)
            .BuscarPorIdComTransacoesAsync(Arg.Is(unidade.Id), Arg.Any<CancellationToken>());

        Assert.NotNull(result);
        Assert.Equal((valorTransacao * totalTransacoes).ToString(), result.Saldo);
        Assert.Equal(unidade.Nome, result.Unidade.Nome);
    }

    [Fact]
    public async Task Dado_TransacaoTipoPendente_Quando_BuscarDashboardAsync_Deve_RetornarSaldoZero()
    {
        // ARRANGE
        var valorTransacao = 100;
        var totalTransacoes = 5;

        var usuarioId = Guid.NewGuid();
        var unidade = new AutoFaker<Unidade>()
            .RuleFor(x => x.Transacoes, new AutoFaker<Transacao>()
                .RuleFor(x => x.Valor, valorTransacao)
                .RuleFor(x => x.Status, StatusTransacao.Pendente)
                .RuleFor(x => x.Tipo, TipoTransacao.Credito)
                .Generate(totalTransacoes))
            .Generate();

        _unidadeRepository
            .BuscarPorIdComTransacoesAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(unidade);

        _usuarioRepository.ExisteNaUnidadeAsync(Arg.Is(usuarioId), Arg.Is(unidade.Id)).Returns(true);

        // ACT
        var result = await _service.BuscarDashboardAsync(usuarioId, unidade.Id, CancellationToken.None);

        // ASSERT
        await _unidadeRepository
            .Received(1)
            .BuscarPorIdComTransacoesAsync(Arg.Is(unidade.Id), Arg.Any<CancellationToken>());

        Assert.NotNull(result);
        Assert.Equal("0", result.Saldo);
    }

    [Fact]
    public async Task Dado_TransacaoTipoDebito_Quando_BuscarDashboardAsync_Deve_RetornarSaldoNegativo()
    {
        // ARRANGE
        var valorTransacao = 100;
        var totalTransacoes = 5;

        var usuarioId = Guid.NewGuid();
        var unidade = new AutoFaker<Unidade>()
            .RuleFor(x => x.Transacoes, new AutoFaker<Transacao>()
                .RuleFor(x => x.Valor, valorTransacao)
                .RuleFor(x => x.Status, StatusTransacao.Aprovado)
                .RuleFor(x => x.Tipo, TipoTransacao.Debito)
                .Generate(totalTransacoes))
            .Generate();

        _unidadeRepository
            .BuscarPorIdComTransacoesAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(unidade);

        _usuarioRepository.ExisteNaUnidadeAsync(Arg.Is(usuarioId), Arg.Is(unidade.Id)).Returns(true);

        // ACT
        var result = await _service.BuscarDashboardAsync(usuarioId, unidade.Id, CancellationToken.None);

        // ASSERT
        await _unidadeRepository
            .Received(1)
            .BuscarPorIdComTransacoesAsync(Arg.Is(unidade.Id), Arg.Any<CancellationToken>());

        Assert.NotNull(result);
        Assert.Equal((valorTransacao * totalTransacoes * -1).ToString(), result.Saldo);
    }

    [Fact]
    public async Task Dado_UnidadeInexistente_Quando_BuscarDashboardAsync_Deve_RetornarNulo()
    {
        // ARRANGE
        var unidadeId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();

        _unidadeRepository
            .BuscarPorIdComTransacoesAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns((Unidade)null!);

        _usuarioRepository.ExisteNaUnidadeAsync(Arg.Is(usuarioId), Arg.Is(unidadeId)).Returns(true);

        // ACT
        var result = await _service.BuscarDashboardAsync(usuarioId, unidadeId, CancellationToken.None);

        // ASSERT
        await _unidadeRepository.Received(1)
            .BuscarPorIdComTransacoesAsync(Arg.Is(unidadeId), Arg.Any<CancellationToken>());

        Assert.Null(result);
    }

    [Fact]
    public async Task Dado_UsuarioNaoCompativelComUnidade_Quando_BuscarDashboardAsync_Deve_RetornarNulo()
    {
        // ARRANGE
        var unidadeId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();

        _usuarioRepository.ExisteNaUnidadeAsync(Arg.Is(usuarioId), Arg.Is(unidadeId)).Returns(false);

        // ACT
        var result = await _service.BuscarDashboardAsync(usuarioId, unidadeId, CancellationToken.None);

        // ASSERT
        await _unidadeRepository
            .DidNotReceive()
            .BuscarPorIdComTransacoesAsync(Arg.Is(unidadeId), Arg.Any<CancellationToken>());

        Assert.Null(result);
    }
}