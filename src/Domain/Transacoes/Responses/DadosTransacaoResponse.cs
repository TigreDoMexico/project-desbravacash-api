using System.Globalization;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Responses;

public class DadosTransacaoResponse
{
    public string Id { get; set; } = string.Empty;

    public string Valor { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Mes { get; set; } = string.Empty;

    public static implicit operator DadosTransacaoResponse(Transacao transacao)
    {
        var cultura = new CultureInfo("pt-BR");
        return new DadosTransacaoResponse
        {
            Id = transacao.Id.ToString(),
            Valor = transacao.Valor.ToString(),
            Descricao = transacao.Descricao,
            Tipo = transacao.Tipo.ToString(),
            Status = transacao.Status.ToString(),
            Mes = cultura.TextInfo.ToTitleCase(transacao.CriadoEm.ToString("MMMM", cultura)),
        };
    }
}