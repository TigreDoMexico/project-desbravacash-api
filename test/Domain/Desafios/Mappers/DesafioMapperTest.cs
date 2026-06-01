using AutoBogus;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Desafios.Mappers;

public class DesafioMapperTest
{
    [Fact]
    public void Desafio_Para_ListarDesafiosResponse_Deve_RetornarCorreto()
    {
        // ARRANGE
        var desafio = new AutoFaker<Desafio>().Generate();

        // ACT
        ListarDesafiosResponse response = desafio;

        // ASSERT
        Assert.Equal(desafio.Id, response.Id);
        Assert.Equal(desafio.Descricao, response.Descricao);
        Assert.Equal(desafio.Pontuacao, response.Pontuacao);
        Assert.Equal(desafio.DataConclusao, response.DataConclusao);
        Assert.Equal(desafio.PodeSolicitar, response.PodeSolicitar);
        Assert.Equal(desafio.Solicitado, response.Solicitado);
        Assert.Equal(desafio.Concluido, response.Concluido);
    }
}