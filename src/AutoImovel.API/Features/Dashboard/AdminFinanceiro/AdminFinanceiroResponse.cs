namespace AutoImovel.API.Features.Dashboard.AdminFinanceiro;

public sealed record AdminFinanceiroResponse(
    decimal TotalInvestido,
    decimal TotalEmLiquidez,
    decimal TotalAlocado,
    int QuantidadeInvestidores,
    int QuantidadeVeiculosTotal,
    int QuantidadeVeiculosDisponiveis,
    int QuantidadeVeiculosAlocados,
    int QuantidadeVeiculosVendidos,
    decimal ValorTotalVendas,
    decimal TotalRendimentosDistribuidos,
    List<AtividadeRecenteItem> AtividadesRecentes
);

public sealed record AtividadeRecenteItem(
    string Tipo,
    string Descricao,
    decimal Valor,
    DateTime Data
);
