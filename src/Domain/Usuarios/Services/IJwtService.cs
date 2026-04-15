using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Services;

public interface IJwtService
{
    string GerarToken(Usuario usuario);
}
