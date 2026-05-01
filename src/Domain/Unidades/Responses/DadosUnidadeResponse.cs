using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Responses;

public class DadosUnidadeResponse
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;

    public static implicit operator DadosUnidadeResponse(Unidade unidade)
        => new()
        {
            Id = unidade.Id.ToString(),
            Nome = unidade.Nome
        };
}