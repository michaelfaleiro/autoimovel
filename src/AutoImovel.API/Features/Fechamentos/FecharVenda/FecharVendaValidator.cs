using FluentValidation;

namespace AutoImovel.API.Features.Fechamentos.FecharVenda;

public sealed class FecharVendaValidator : AbstractValidator<FecharVendaRequest>
{
    public FecharVendaValidator()
    {
        RuleFor(x => x.VeiculoId)
            .NotEmpty()
            .WithMessage("ID do veículo é obrigatório.");

        RuleFor(x => x.PrecoVendaReal)
            .GreaterThan(0)
            .WithMessage("Preço de venda deve ser maior que zero.");
    }
}
