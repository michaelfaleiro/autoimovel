using FluentValidation;

namespace AutoImovel.API.Features.Aportes.RegistrarAporte;

public sealed class RegistrarAporteValidator : AbstractValidator<RegistrarAporteRequest>
{
    public RegistrarAporteValidator()
    {
        RuleFor(x => x.InvestidorId)
            .NotEmpty()
            .WithMessage("ID do investidor é obrigatório.");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("Valor do aporte deve ser maior que zero.")
            .GreaterThanOrEqualTo(100_000)
            .WithMessage("Aporte mínimo é de R$ 100.000,00.");

        RuleFor(x => x.UrlContratoScp)
            .NotEmpty()
            .WithMessage("URL do contrato SCP é obrigatória.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("URL do contrato SCP deve ser válida.");
    }
}
