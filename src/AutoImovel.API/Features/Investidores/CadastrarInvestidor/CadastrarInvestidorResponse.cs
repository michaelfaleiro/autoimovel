using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Investidores.CadastrarInvestidor;

public sealed record CadastrarInvestidorResponse(
    Guid Id,
    string NomeCompleto,
    string CpfCnpj,
    string Email,
    StatusInvestidor Status);
