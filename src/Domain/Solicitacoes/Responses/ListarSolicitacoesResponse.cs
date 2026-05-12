using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Responses;

public class DadosSolicitacaoResponse
{
    public Guid Id { get; set; }
    
    public string Tipo { get; set; } = string.Empty;
    
    public string Status { get; set; } = string.Empty;
    
    public int Valor { get; set; }
    
    public string Descricao { get; set; } = string.Empty;
    
    public DateTimeOffset CriadoEm { get; set; }
    
    public Guid UnidadeId { get; set; }
    
    public string NomeUnidade { get; set; } = string.Empty;
    
    public string? NomeDesafio { get; set; }

    public static implicit operator DadosSolicitacaoResponse(Solicitacao s) => new()
    {
        Id = s.Id,
        Tipo = s.Tipo.ToString(),
        Status = s.Status.ToString(),
        Valor = s.Valor,
        Descricao = s.Descricao,
        CriadoEm = s.CriadoEm,
        UnidadeId = s.UnidadeId,
        NomeUnidade = s.Unidade.Nome,
        NomeDesafio = s.Desafio?.Descricao,
    };
}
