namespace AutoImovel.API.Features.Dashboard.ExtratoRendimentos;

public sealed record ExtratoRendimentosResponse(
    List<RendimentoItem> Rendimentos,
    decimal TotalLucroLiquido);
