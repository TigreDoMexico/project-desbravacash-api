using System.ComponentModel.DataAnnotations;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;

public class Solicitacao
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public TipoSolicitacao Tipo { get; set; }

    [Required]
    public StatusSolicitacao Status { get; set; }

    [Required]
    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;

    [Required]
    public Guid CriadoPor { get; set; }
    public Usuario CriadoPorUsuario { get; set; } = null!;

    [Required]
    public DateTimeOffset CriadoEm { get; set; }

    [Required]
    public int Valor { get; set; }

    [Required]
    [MaxLength(SolicitacaoConsts.MaxLengthDescricao)]
    public string Descricao { get; set; } = string.Empty;

    public Guid? DesafioId { get; set; }
    public Desafio? Desafio { get; set; }

    public Guid? TransacaoId { get; set; }
    public Transacao? Transacao { get; set; }
}