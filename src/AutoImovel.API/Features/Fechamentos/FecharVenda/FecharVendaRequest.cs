namespace AutoImovel.API.Features.Fechamentos.FecharVenda;

public sealed record FecharVendaRequest(
    Guid VeiculoId,
    decimal PrecoVendaReal);
