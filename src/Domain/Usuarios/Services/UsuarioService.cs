using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;

public class UsuarioService(IUsuarioRepository repository) : IUsuarioService
{
    public async Task<Usuario?> AcessarAsync(string telefone, string senha, CancellationToken ct = default)
    {
        var usuario = await repository.BuscarPorTelefoneAsync(telefone, ct);
        if (usuario is null || !Hashing.SenhaValida(senha, usuario.Senha))
            return null;

        return usuario;
    }
}