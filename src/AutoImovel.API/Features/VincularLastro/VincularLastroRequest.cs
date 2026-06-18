namespace AutoImovel.API.Features.VincularLastro;

public sealed record VincularLastroRequest(
    Guid InvestidorId,
    Guid AporteId,
    Guid VeiculoId,
    decimal ValorAlocado);
