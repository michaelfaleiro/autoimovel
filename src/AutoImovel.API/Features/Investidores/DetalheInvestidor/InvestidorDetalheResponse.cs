using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Investidores.DetalheInvestidor;

public sealed record InvestidorDetalheResponse(
    Guid Id,
    string NomeCompleto,
    string CpfCnpj,
    string Email,
    string Telefone,
    StatusInvestidor Status,
    DateTime DataCadastro,
    string ChavePix,
    string Banco,
    string Agencia,
    string Conta,
    string TipoConta,
    PortfolioSummary Portfolio,
    List<AporteItemResponse> Aportes,
    List<AlocacaoInvestidorItem> AlocacoesAtivas,
    List<RendimentoRecebidoItem> Rendimentos);

public sealed record PortfolioSummary(
    decimal TotalInvestido,
    decimal TotalEmLiquidez,
    decimal TotalAlocado,
    decimal TotalRendimentos);

public sealed record AporteItemResponse(
    Guid Id,
    decimal ValorOriginal,
    decimal SaldoEmLiquidez,
    StatusAporte Status,
    DateTime DataAporte);

public sealed record AlocacaoInvestidorItem(
    Guid AlocacaoId,
    Guid VeiculoId,
    string PlacaVeiculo,
    string MarcaModelo,
    decimal ValorAlocado,
    decimal PorcentagemDoAporte,
    StatusAlocacao Status,
    DateTime DataAlocacao);

public sealed record RendimentoRecebidoItem(
    Guid FechamentoId,
    string PlacaVeiculo,
    string MarcaModelo,
    decimal LucroBrutoProporcional,
    decimal TaxaGestaoRetida,
    decimal TaxaInfraRetida,
    decimal TaxaPerformanceRetida,
    decimal LucroLiquidoPago,
    DateTime DataFechamento);
