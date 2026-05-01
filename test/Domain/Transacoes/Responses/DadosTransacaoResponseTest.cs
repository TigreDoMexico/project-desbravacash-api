using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Responses;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Transacoes.Responses;

public class DadosTransacaoResponseTest
{
    private readonly Transacao _transacao = new()
    {
        Id = Guid.NewGuid(),
        Valor = 250,
        Descricao = "Pagamento mensal",
        Tipo = TipoTransacao.Credito,
        Status = StatusTransacao.Aprovado,
        CriadoEm = new DateTimeOffset(2025, 1, 15, 0, 0, 0, TimeSpan.Zero),
    };

    [Fact]
    public void Deve_Mapear_Id_Como_String()
    {
        DadosTransacaoResponse response = _transacao;
        Assert.Equal(_transacao.Id.ToString(), response.Id);
    }

    [Fact]
    public void Deve_Mapear_Valor_Como_String()
    {
        DadosTransacaoResponse response = _transacao;
        Assert.Equal(_transacao.Valor.ToString(), response.Valor);
    }

    [Fact]
    public void Deve_Mapear_Descricao()
    {
        DadosTransacaoResponse response = _transacao;
        Assert.Equal(_transacao.Descricao, response.Descricao);
    }

    [Fact]
    public void Deve_Mapear_Tipo_Como_String()
    {
        DadosTransacaoResponse response = _transacao;
        Assert.Equal(_transacao.Tipo.ToString(), response.Tipo);
    }

    [Fact]
    public void Deve_Mapear_Status_Como_String()
    {
        DadosTransacaoResponse response = _transacao;
        Assert.Equal(_transacao.Status.ToString(), response.Status);
    }

    [Theory]
    [InlineData(1, "Janeiro")]
    [InlineData(6, "Junho")]
    [InlineData(12, "Dezembro")]
    public void Deve_Mapear_Mes_Em_Portugues_Com_Inicial_Maiuscula(int mes, string mesEsperado)
    {
        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            CriadoEm = new DateTimeOffset(2025, mes, 1, 0, 0, 0, TimeSpan.Zero)
        };

        DadosTransacaoResponse response = transacao;

        Assert.Equal(mesEsperado, response.Mes);
    }
}
