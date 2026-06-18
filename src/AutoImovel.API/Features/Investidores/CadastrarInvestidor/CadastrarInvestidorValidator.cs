using FluentValidation;

namespace AutoImovel.API.Features.Investidores.CadastrarInvestidor;

public sealed class CadastrarInvestidorValidator : AbstractValidator<CadastrarInvestidorRequest>
{
    public CadastrarInvestidorValidator()
    {
        RuleFor(x => x.NomeCompleto)
            .NotEmpty().WithMessage("Nome completo é obrigatório.")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");

        RuleFor(x => x.CpfCnpj)
            .NotEmpty().WithMessage("CPF/CNPJ é obrigatório.")
            .MaximumLength(14).WithMessage("CPF/CNPJ deve ter no máximo 14 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório.")
            .MaximumLength(200).WithMessage("E-mail deve ter no máximo 200 caracteres.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório.")
            .MaximumLength(20).WithMessage("Telefone deve ter no máximo 20 caracteres.");

        RuleFor(x => x.ChavePix)
            .MaximumLength(100).WithMessage("Chave Pix deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Banco)
            .MaximumLength(50).WithMessage("Banco deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Agencia)
            .MaximumLength(10).WithMessage("Agência deve ter no máximo 10 caracteres.");

        RuleFor(x => x.Conta)
            .MaximumLength(20).WithMessage("Conta deve ter no máximo 20 caracteres.");

        RuleFor(x => x.TipoConta)
            .MaximumLength(20).WithMessage("Tipo de conta deve ter no máximo 20 caracteres.");
    }
}
