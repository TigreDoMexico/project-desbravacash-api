using TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;

namespace TigreDoMexico.DesbravaCash.Api.Test.Infrastructure.Security;

public class HashingTests
{
    [Fact]
    public void HashSenha_DeveRetornarHashNoFormatoCorreto()
    {
        var hash = Hashing.HashSenha("senha123");

        var parts = hash.Split(':');
        Assert.Equal(3, parts.Length);
    }

    [Fact]
    public void HashSenha_MesmasSenhasDevemGerarHashesDiferentes()
    {
        var hash1 = Hashing.HashSenha("senha123");
        var hash2 = Hashing.HashSenha("senha123");

        Assert.NotEqual(hash1, hash2);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HashSenha_SenhaInvalida_DeveLancarExcecao(string? senha)
    {
        Assert.Throws<ArgumentException>(() => Hashing.HashSenha(senha!));
    }
    
    [Fact]
    public void HashSenha_SenhaNula_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentNullException>(() => Hashing.HashSenha(null!));
    }

    [Fact]
    public void SenhaValida_SenhaCorreta_DeveRetornarTrue()
    {
        var hash = Hashing.HashSenha("senha123");

        Assert.True(Hashing.SenhaValida("senha123", hash));
    }

    [Fact]
    public void SenhaValida_SenhaErrada_DeveRetornarFalse()
    {
        var hash = Hashing.HashSenha("senha123");

        Assert.False(Hashing.SenhaValida("senhaErrada", hash));
    }

    [Theory]
    [InlineData("hash_invalido")]
    [InlineData("a:b")]
    [InlineData("nao:e:base64!@#")]
    public void SenhaValida_HashMalFormado_DeveRetornarFalse(string hashInvalido)
    {
        Assert.False(Hashing.SenhaValida("senha123", hashInvalido));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void SenhaValida_SenhaInvalida_DeveLancarExcecao(string? senha)
    {
        Assert.Throws<ArgumentException>(() => Hashing.SenhaValida(senha!, "hash:qualquer:coisa"));
    }
    
    [Fact]
    public void SenhaValida_SenhaNula_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentNullException>(() => Hashing.SenhaValida(null!, "hash:qualquer:coisa"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void SenhaValida_HashInvalida_DeveLancarExcecao(string? hash)
    {
        Assert.Throws<ArgumentException>(() => Hashing.SenhaValida("senha123", hash!));
    }
    
    [Fact]
    public void SenhaValida_HashNula_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentNullException>(() => Hashing.SenhaValida("senha123", null!));
    }
}
