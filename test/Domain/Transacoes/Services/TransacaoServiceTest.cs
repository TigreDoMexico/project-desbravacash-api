using AutoBogus;
using NSubstitute;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Services;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Transacoes.Services;

public class TransacaoServiceTest
{
    private readonly ITransacaoRepository _repository = Substitute.For<ITransacaoRepository>();
    private readonly TransacaoService _service;

    public TransacaoServiceTest() => _service = new TransacaoService(_repository);

    [Fact]
    public async Task ObterTransacoesPorUnidadeAsync_Deve_Retornar_Transacoes_Da_Unidade()
    {
        var unidadeId = Guid.NewGuid();
        var transacoes = new AutoFaker<Transacao>()
            .RuleFor(x => x.Unidade, _ => null!)
            .RuleFor(x => x.CriadoPorUsuario, _ => null!)
            .Generate(3);

        _repository.ListarTodasTransacoesPorUnidadeAsync(unidadeId, Arg.Any<CancellationToken>()).Returns(transacoes);

        var resultado = await _service.ObterTransacoesPorUnidadeAsync(unidadeId, CancellationToken.None);

        Assert.Equal(3, resultado.Transacoes.Count);
    }

    [Fact]
    public async Task ObterTransacoesPorUnidadeAsync_Deve_Retornar_Lista_Vazia_Quando_Nao_Ha_Transacoes()
    {
        var unidadeId = Guid.NewGuid();

        _repository.ListarTodasTransacoesPorUnidadeAsync(unidadeId, Arg.Any<CancellationToken>()).Returns([]);

        var resultado = await _service.ObterTransacoesPorUnidadeAsync(unidadeId, CancellationToken.None);

        Assert.Empty(resultado.Transacoes);
    }
}
