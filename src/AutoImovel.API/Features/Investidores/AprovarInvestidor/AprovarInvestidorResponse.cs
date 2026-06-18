using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Investidores.AprovarInvestidor;

public sealed record AprovarInvestidorResponse(Guid Id, string NomeCompleto, StatusInvestidor Status);
