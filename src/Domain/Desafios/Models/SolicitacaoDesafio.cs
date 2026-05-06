using System.ComponentModel.DataAnnotations;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;

public class SolicitacaoDesafio
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;
    
    [Required]
    public Guid DesafioId { get; set; }
    public Desafio Desafio { get; set; } = null!;

    [Required]
    public StatusDesafio Status { get; set; }

    [Required]
    public DateTimeOffset CriadoEm { get; set; }

    [Required]
    public Guid CriadoPor { get; set; }
    public Usuario CriadoPorUsuario { get; set; } = null!;

    public Guid? TransacaoId { get; set; }
    public Transacao? Transacao { get; set; }
}