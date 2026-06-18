using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.VincularLastro;

public sealed record VincularLastroResponse(
    Guid Id,
    Guid AporteId,
    Guid VeiculoId,
    decimal ValorAlocado,
    StatusAlocacao Status,
    DateTime DataAlocacao);
