using FluentValidation;

namespace AutoImovel.API.Features.VincularLastro;

public sealed class VincularLastroValidator : AbstractValidator<VincularLastroRequest>
{
    public VincularLastroValidator()
    {
        RuleFor(x => x.InvestidorId)
            .NotEmpty()
            .WithMessage("ID do investidor é obrigatório.");

        RuleFor(x => x.AporteId)
            .NotEmpty()
            .WithMessage("ID do aporte é obrigatório.");

        RuleFor(x => x.VeiculoId)
            .NotEmpty()
            .WithMessage("ID do veículo é obrigatório.");

        RuleFor(x => x.ValorAlocado)
            .GreaterThan(0)
            .WithMessage("Valor alocado deve ser maior que zero.");
    }
}
