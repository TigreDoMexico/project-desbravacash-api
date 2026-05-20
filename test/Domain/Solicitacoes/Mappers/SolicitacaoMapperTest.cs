using AutoBogus;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Responses;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Solicitacoes.Mappers;

public class SolicitacaoMapperTest
{
    [Fact]
    public void Solicitacao_Para_DadosSolicitacaoResponse_Deve_MapearCorretamente()
    {
        // ARRANGE
        var solicitacao = new AutoFaker<Solicitacao>()
            .RuleFor(x => x.Unidade, new AutoFaker<Unidade>().Generate())
            .RuleFor(x => x.Desafio, new AutoFaker<Desafio>().Generate())
            .RuleFor(x => x.Tipo, TipoSolicitacao.Manual)
            .RuleFor(x => x.Status, StatusSolicitacao.Aprovado)
            .Generate();

        // ACT
        DadosSolicitacaoResponse response = solicitacao;

        // ASSERT
        Assert.Equal(solicitacao.Id, response.Id);
        Assert.Equal("Manual", response.Tipo);
        Assert.Equal("Aprovado", response.Status);
        Assert.Equal(solicitacao.Valor, response.Valor);
        Assert.Equal(solicitacao.Descricao, response.Descricao);
        Assert.Equal(solicitacao.CriadoEm, response.CriadoEm);
        Assert.Equal(solicitacao.UnidadeId, response.UnidadeId);
        Assert.Equal(solicitacao.UnidadeId, response.UnidadeId);
        Assert.Equal(solicitacao.Unidade.Nome, response.NomeUnidade);
        Assert.Equal(solicitacao.Desafio!.Descricao, response.NomeDesafio);
    }
}