using FluentValidation;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Requests;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Validators;

public class DadosSolicitacaoRequestValidator : AbstractValidator<DadosSolicitacaoRequest>
{
    public DadosSolicitacaoRequestValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("A descrição é obrigatória.");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("O valor deve ser maior que zero.");
    }
}