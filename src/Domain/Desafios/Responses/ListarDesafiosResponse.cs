using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Responses;

public class ListarDesafiosResponse
{
    public Guid Id { get; set; }
    
    public string Descricao { get; set; } = string.Empty;
    
    public int Pontuacao { get; set; }
    
    public DateTime DataConclusao { get; set; }
    
    public bool PodeSolicitar { get; set; }
    
    public bool Solicitado { get; set; }
    
    public bool Concluido { get; set; }
    
    public static implicit operator ListarDesafiosResponse(Desafio desafio)
    {
        return new ListarDesafiosResponse
        {
            Id = desafio.Id,
            Descricao = desafio.Descricao,
            Pontuacao = desafio.Pontuacao,
            DataConclusao = desafio.DataConclusao,
            PodeSolicitar = desafio.PodeSolicitar,
            Solicitado = desafio.Solicitado,
            Concluido = desafio.Concluido
        };
    }
}