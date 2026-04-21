using FluentValidation;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Requests;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Validators;

public class CriarTransacaoRequestValidator : AbstractValidator<CriarTransacaoRequest>
{
    public CriarTransacaoRequestValidator()
    {
        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("Valor deve ser maior que zero.");

        RuleFor(x => x.Descricao)
            .NotEmpty().NotNull()
            .WithMessage("Descrição é obrigatória.");
    }
}