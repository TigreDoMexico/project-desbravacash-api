using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Requests;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Validators;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Usuarios.Validators;

public class AcessoRequestValidatorTest
{
    private readonly AcessoRequestValidator _validator = new();

    [Fact]
    public void Deve_Ser_Valido_Quando_Telefone_E_Senha_Preenchidos()
    {
        var request = new AcessoRequest { Telefone = "11999999999", Senha = "senha123" };
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("", "senha123")]
    [InlineData(null, "senha123")]
    public void Deve_Ser_Invalido_Quando_Telefone_Vazio(string? telefone, string senha)
    {
        var request = new AcessoRequest { Telefone = telefone!, Senha = senha };
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(AcessoRequest.Telefone));
    }

    [Theory]
    [InlineData("11999999999", "")]
    [InlineData("11999999999", null)]
    public void Deve_Ser_Invalido_Quando_Senha_Vazia(string telefone, string? senha)
    {
        var request = new AcessoRequest { Telefone = telefone, Senha = senha! };
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(AcessoRequest.Senha));
    }

    [Fact]
    public void Deve_Retornar_Dois_Erros_Quando_Telefone_E_Senha_Vazios()
    {
        var request = new AcessoRequest();
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
    }
}
