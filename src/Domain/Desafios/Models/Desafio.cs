using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;

public class Desafio
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(DesafioConsts.MaxLengthDescricao)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public int Pontuacao { get; set; }

    [Required]
    public DateTime DataConclusao { get; set; }

    [Required]
    public bool PodeSolicitar { get; set; }

    [NotMapped]
    public bool Solicitado { get; set; }

    [NotMapped]
    public bool Concluido { get; set; }
}