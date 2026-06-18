namespace AutoImovel.API.Features.Aportes.RegistrarAporte;

public sealed record RegistrarAporteRequest(
    Guid InvestidorId,
    decimal Valor,
    string UrlContratoScp);
