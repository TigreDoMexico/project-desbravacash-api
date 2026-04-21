using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;

public class AtualizarStatusTransacaoRequest
{
    public StatusTransacao Status { get; set; }
}