using System.ComponentModel.DataAnnotations;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;

public class Unidade
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(UnidadeConsts.MaxLengthNome)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Transacao> Transacoes { get; set; } = [];
    public ICollection<Usuario> Usuarios { get; set; } = [];
}