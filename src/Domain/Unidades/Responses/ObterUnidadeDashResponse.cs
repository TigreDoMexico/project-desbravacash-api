namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Responses;

public class ObterUnidadeDashResponse
{
    public DadosUnidadeResponse Unidade { get; set; } = new();

    public string Saldo { get; set; } = "0";
}