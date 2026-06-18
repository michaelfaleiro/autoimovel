using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Veiculos.DetalheVeiculo;

public sealed record VeiculoDetalheResponse(
    Guid Id, string Placa, string MarcaModelo, string AnoFabricacaoModelo,
    int Km, decimal PrecoCompra, decimal CustosPreparacao,
    string Chassi, string Renavam, string Cor,
    StatusVeiculo Status, string UrlLaudoCautelar,
    DateTime DataCompra, DateTime? DataVenda, decimal? PrecoVendaReal,
    List<AlocacaoVeiculoItem> Alocacoes,
    FechamentoVeiculoItem? Fechamento
);

public sealed record AlocacaoVeiculoItem(
    Guid AlocacaoId, Guid InvestidorId, string NomeInvestidor,
    decimal ValorAlocado, decimal Porcentagem, StatusAlocacao Status, DateTime DataAlocacao
);

public sealed record FechamentoVeiculoItem(
    decimal LucroBruto, decimal TaxaGestao, decimal TaxaInfra,
    decimal TaxaPerformance, decimal LucroLiquido
);
