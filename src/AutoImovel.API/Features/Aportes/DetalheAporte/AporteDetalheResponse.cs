using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Aportes.DetalheAporte;

public sealed record AporteDetalheResponse(
    Guid Id,
    Guid InvestidorId,
    string NomeInvestidor,
    decimal ValorOriginal,
    decimal SaldoEmLiquidez,
    decimal TotalAlocado,
    StatusAporte Status,
    DateTime DataAporte,
    string UrlContratoScp,
    List<AlocacaoAporteItem> Alocacoes
);

public sealed record AlocacaoAporteItem(
    Guid AlocacaoId,
    Guid VeiculoId,
    string PlacaVeiculo,
    string MarcaModelo,
    decimal ValorAlocado,
    StatusAlocacao Status,
    DateTime DataAlocacao
);
