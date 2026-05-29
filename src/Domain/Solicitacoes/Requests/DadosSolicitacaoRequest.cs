namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Requests;

public class DadosSolicitacaoRequest
{
    public string Descricao { get; set; } = string.Empty;

    public int Valor { get; set; }
    
    public Guid UnidadeId { get; set; }

}