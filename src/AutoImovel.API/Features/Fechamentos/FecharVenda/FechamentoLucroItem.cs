namespace AutoImovel.API.Features.Fechamentos.FecharVenda;

public sealed record FechamentoLucroItem(
    Guid InvestidorId,
    string NomeInvestidor,
    decimal ValorAlocado,
    decimal Proporcao,
    decimal LucroBrutoProporcional,
    decimal TaxaGestaoRetida,
    decimal TaxaInfraRetida,
    decimal TaxaPerformanceRetida,
    decimal LucroLiquidoPago);
