using AutoBogus;
using NSubstitute;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;

namespace TigreDoMexico.DesbravaCash.Api.Test.Domain.Usuarios.Services;

public class UsuarioServiceTest
{
    private readonly IUsuarioRepository _repository = Substitute.For<IUsuarioRepository>();
    private readonly UsuarioService _service;

    public UsuarioServiceTest()
    {
        _service = new UsuarioService(_repository);
    }

    [Fact(Skip = "Teste muito demorado. Ver forma de tornar mais simples")]
    public async Task AcessarAsync_Deve_Retornar_Usuario_Quando_Credenciais_Validas()
    {
        var senhaPlana = "senha123";
        var usuario = new AutoFaker<Usuario>()
            .RuleFor(u => u.Senha, _ => Hashing.HashSenha(senhaPlana))
            .Generate();

        _repository.BuscarPorTelefoneAsync(usuario.Telefone).Returns(usuario);

        var resultado = await _service.AcessarAsync(usuario.Telefone, senhaPlana);

        Assert.Equal(usuario, resultado);
    }
    
    [Fact(Skip = "Teste muito demorado. Ver forma de tornar mais simples")]
    public async Task AcessarAsync_Deve_Retornar_Null_Quando_Usuario_Nao_Encontrado()
    {
        var faker = new AutoFaker<Usuario>().Generate();

        _repository.BuscarPorTelefoneAsync(faker.Telefone).Returns((Usuario?)null);

        var resultado = await _service.AcessarAsync(faker.Telefone, faker.Senha);

        Assert.Null(resultado);
    }

    [Fact(Skip = "Teste muito demorado. Ver forma de tornar mais simples")]
    public async Task AcessarAsync_Deve_Retornar_Null_Quando_Senha_Invalida()
    {
        var usuario = new AutoFaker<Usuario>()
            .RuleFor(u => u.Senha, _ => Hashing.HashSenha("senhaCorreta"))
            .Generate();

        _repository.BuscarPorTelefoneAsync(usuario.Telefone).Returns(usuario);

        var resultado = await _service.AcessarAsync(usuario.Telefone, "senhaErrada");

        Assert.Null(resultado);
    }
}
