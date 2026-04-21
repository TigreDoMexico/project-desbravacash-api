using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Validators;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Transacoes.Validators;

public class CriarTransacaoRequestValidatorTest
{
    private readonly CriarTransacaoRequestValidator _validator = new();

    [Fact]
    public void Deve_Ser_Valido_Quando_Request_Correto()
    {
        var request = new CriarTransacaoRequest
        {
            Valor = 100,
            Descricao = "Pagamento",
            UnidadeId = Guid.NewGuid(),
            TipoTransacao = TipoTransacao.Credito
        };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Deve_Ser_Invalido_Quando_Valor_Menor_Ou_Igual_A_Zero(int valor)
    {
        var request = new CriarTransacaoRequest { Valor = valor, Descricao = "Pagamento" };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CriarTransacaoRequest.Valor));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Deve_Ser_Invalido_Quando_Descricao_Vazia(string? descricao)
    {
        var request = new CriarTransacaoRequest { Valor = 100, Descricao = descricao! };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CriarTransacaoRequest.Descricao));
    }

    [Fact]
    public void Deve_Retornar_Dois_Erros_Quando_Valor_E_Descricao_Invalidos()
    {
        var request = new CriarTransacaoRequest { Valor = 0, Descricao = "" };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
    }
}
