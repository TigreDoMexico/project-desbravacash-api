using System.ComponentModel.DataAnnotations;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

public class Transacao
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public TipoTransacao Tipo { get; set; }

    [Required]
    public StatusTransacao Status { get; set; }

    [Required]
    public int Valor { get; set; }

    [Required]
    [MaxLength(TransacaoConsts.MaxLengthDescricao)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;

    [Required]
    public DateTimeOffset CriadoEm { get; set; }

    [Required]
    public Guid CriadoPor { get; set; }
    public Usuario CriadoPorUsuario { get; set; } = null!;
}