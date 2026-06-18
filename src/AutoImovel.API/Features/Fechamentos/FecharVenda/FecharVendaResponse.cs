namespace AutoImovel.API.Features.Fechamentos.FecharVenda;

public sealed record FecharVendaResponse(
    Guid VeiculoId,
    decimal PrecoVendaReal,
    decimal LucroBruto,
    decimal CustoTotalVeiculo,
    List<FechamentoLucroItem> Fechamentos);
