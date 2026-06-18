using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Aportes.RegistrarAporte;

public sealed record RegistrarAporteResponse(
    Guid Id,
    Guid InvestidorId,
    decimal ValorOriginal,
    decimal SaldoEmLiquidez,
    StatusAporte Status,
    DateTime DataAporte);
