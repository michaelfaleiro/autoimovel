namespace AutoImovel.API.Features.Dashboard.Patrimonio;

public sealed record PatrimonioResponse(
    Guid InvestidorId,
    string NomeInvestidor,
    decimal CapitalEmLiquidez,
    decimal CapitalAlocado,
    decimal PatrimonioTotal);
