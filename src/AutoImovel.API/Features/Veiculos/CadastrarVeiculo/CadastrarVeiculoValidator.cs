using FluentValidation;

namespace AutoImovel.API.Features.Veiculos.CadastrarVeiculo;

public sealed class CadastrarVeiculoValidator : AbstractValidator<CadastrarVeiculoRequest>
{
    public CadastrarVeiculoValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty()
            .WithMessage("Placa é obrigatória.")
            .MaximumLength(10)
            .WithMessage("Placa deve ter no máximo 10 caracteres.");

        RuleFor(x => x.MarcaModelo)
            .NotEmpty()
            .WithMessage("Marca/Modelo é obrigatório.")
            .MaximumLength(200)
            .WithMessage("Marca/Modelo deve ter no máximo 200 caracteres.");

        RuleFor(x => x.AnoFabricacaoModelo)
            .NotEmpty()
            .WithMessage("Ano/Fabricação/Modelo é obrigatório.")
            .MaximumLength(20)
            .WithMessage("Ano/Fabricação/Modelo deve ter no máximo 20 caracteres.");

        RuleFor(x => x.Km)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quilometragem não pode ser negativa.");

        RuleFor(x => x.PrecoCompra)
            .GreaterThan(0)
            .WithMessage("Preço de compra deve ser maior que zero.");

        RuleFor(x => x.CustosPreparacao)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Custos de preparação não podem ser negativos.");

        RuleFor(x => x.Chassi)
            .NotEmpty()
            .WithMessage("Chassi é obrigatório.")
            .MaximumLength(17)
            .WithMessage("Chassi deve ter no máximo 17 caracteres.");

        RuleFor(x => x.Renavam)
            .NotEmpty()
            .WithMessage("Renavam é obrigatório.")
            .MaximumLength(11)
            .WithMessage("Renavam deve ter no máximo 11 caracteres.")
            .Matches(@"^\d{11}$")
            .WithMessage("Renavam deve conter 11 dígitos numéricos.");

        RuleFor(x => x.Cor)
            .NotEmpty()
            .WithMessage("Cor é obrigatória.")
            .MaximumLength(20)
            .WithMessage("Cor deve ter no máximo 20 caracteres.");

        RuleFor(x => x.UrlLaudoCautelar)
            .NotEmpty()
            .WithMessage("URL do laudo cautelar é obrigatória.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("URL do laudo cautelar deve ser válida.");
    }
}
