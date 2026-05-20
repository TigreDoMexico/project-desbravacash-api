using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Validators;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Solicitacoes.Validators;

public class DadosSolicitacaoRequestValidatorTest
{
    private readonly DadosSolicitacaoRequestValidator _validator = new();

    [Fact]
    public void Deve_Ser_Valido_Quando_Descricao_E_Valor_Preenchidos()
    {
        var request = new DadosSolicitacaoRequest { Descricao = "Solicitação teste", Valor = 100 };
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Deve_Ser_Invalido_Quando_Descricao_Vazia(string? descricao)
    {
        var request = new DadosSolicitacaoRequest { Descricao = descricao!, Valor = 100 };
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(DadosSolicitacaoRequest.Descricao));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Deve_Ser_Invalido_Quando_Valor_Menor_Ou_Igual_A_Zero(int valor)
    {
        var request = new DadosSolicitacaoRequest { Descricao = "Solicitação teste", Valor = valor };
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(DadosSolicitacaoRequest.Valor));
    }

    [Fact]
    public void Deve_Retornar_Dois_Erros_Quando_Descricao_E_Valor_Invalidos()
    {
        var request = new DadosSolicitacaoRequest();
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
    }
}
