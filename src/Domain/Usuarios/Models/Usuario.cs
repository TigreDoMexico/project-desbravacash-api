using System.ComponentModel.DataAnnotations;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

public class Usuario
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(UsuarioConsts.MaxLengthNome)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(UsuarioConsts.MaxLengthTelefone)]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    [MaxLength(UsuarioConsts.MaxLengthSenha)]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;

    [Required]
    [MaxLength(UsuarioConsts.MaxLengthCargo)]
    public string Cargo { get; set; } = string.Empty;

    [Required]
    public UsuarioRole Role { get; set; }

    public ICollection<Transacao> Transacoes { get; set; } = [];
}