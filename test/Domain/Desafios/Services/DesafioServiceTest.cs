using AutoBogus;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Services;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Desafios.Services;

public class DesafioServiceTest
{
    private readonly IDesafioRepository _repository = Substitute.For<IDesafioRepository>();
    private readonly ILogger<DesafioService> _logger = Substitute.For<ILogger<DesafioService>>();
    private readonly DesafioService _service;

    public DesafioServiceTest() => _service = new DesafioService(_repository, _logger);

    [Fact]
    public async Task ListarDesafiosPorUnidadeAsync_Deve_Retornar_Lista_Mapeada()
    {
        var unidadeId = Guid.NewGuid();
        var desafios = new AutoFaker<Desafio>().Generate(3);

        _repository.ListarDesafiosPorUnidadeAsync(unidadeId, Arg.Any<CancellationToken>()).Returns(desafios);

        var resultado = await _service.ListarDesafiosPorUnidadeAsync(unidadeId, CancellationToken.None);

        Assert.Equal(3, resultado.Count);
        Assert.All(resultado, r =>
        {
            var original = desafios.First(d => d.Id == r.Id);
            Assert.Equal(original.Descricao, r.Descricao);
            Assert.Equal(original.Pontuacao, r.Pontuacao);
            Assert.Equal(original.DataConclusao, r.DataConclusao);
            Assert.Equal(original.PodeSolicitar, r.PodeSolicitar);
            Assert.Equal(original.Solicitado, r.Solicitado);
            Assert.Equal(original.Concluido, r.Concluido);
        });
    }

    [Fact]
    public async Task ListarDesafiosPorUnidadeAsync_Deve_Retornar_Lista_Vazia_Quando_Nao_Ha_Desafios()
    {
        var unidadeId = Guid.NewGuid();

        _repository.ListarDesafiosPorUnidadeAsync(unidadeId, Arg.Any<CancellationToken>()).Returns([]);

        var resultado = await _service.ListarDesafiosPorUnidadeAsync(unidadeId, CancellationToken.None);

        Assert.Empty(resultado);
    }
}
