using FluentValidation;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Requests;

namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Validators;

public class AcessoRequestValidator : AbstractValidator<AcessoRequest>
{
    public AcessoRequestValidator()
    {
        RuleFor(x => x.Telefone).NotEmpty();
        
        RuleFor(x => x.Senha).NotEmpty();
    }
}
