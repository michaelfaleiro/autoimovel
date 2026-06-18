using FluentValidation;

namespace AutoImovel.API.Features.Investidores.AprovarInvestidor;

public sealed class AprovarInvestidorValidator : AbstractValidator<AprovarInvestidorRequest>
{
    public AprovarInvestidorValidator()
    {
        RuleFor(x => x.InvestidorId)
            .NotEmpty()
            .WithMessage("ID do investidor é obrigatório.");
    }
}
