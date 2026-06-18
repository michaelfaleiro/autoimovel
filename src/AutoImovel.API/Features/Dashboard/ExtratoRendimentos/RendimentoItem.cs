namespace AutoImovel.API.Features.Dashboard.ExtratoRendimentos;

public sealed record RendimentoItem(
    Guid FechamentoId,
    string PlacaVeiculo,
    string MarcaModeloVeiculo,
    decimal LucroBrutoProporcional,
    decimal TaxaGestaoRetida,
    decimal TaxaInfraRetida,
    decimal TaxaPerformanceRetida,
    decimal LucroLiquidoPago,
    DateTime DataFechamento);
