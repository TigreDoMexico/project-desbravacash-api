using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;

public class CriarTransacaoRequest
{
    public Guid UnidadeId { get; set; }

    public int Valor { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public TipoTransacao Tipo { get; set; }
}