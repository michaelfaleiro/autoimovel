namespace AutoImovel.API.Features.Investidores.CadastrarInvestidor;

public sealed record CadastrarInvestidorRequest(
    string NomeCompleto,
    string CpfCnpj,
    string Email,
    string Telefone,
    string ChavePix,
    string Banco,
    string Agencia,
    string Conta,
    string TipoConta);
