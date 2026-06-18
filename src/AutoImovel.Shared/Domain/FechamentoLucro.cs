namespace AutoImovel.Shared.Domain;

public class FechamentoLucro
{
    public FechamentoLucroId Id { get; set; }
    public InvestidorId InvestidorId { get; set; }
    public AlocacaoLastroId AlocacaoId { get; set; }
    public decimal LucroBrutoProporcional { get; set; }
    public decimal TaxaGestaoRetida { get; set; }
    public decimal TaxaInfraRetida { get; set; }
    public decimal TaxaPerformanceRetida { get; set; }
    public decimal LucroLiquidoPago { get; set; }
    public DateTime DataFechamento { get; set; } = DateTime.UtcNow;

    public Investidor Investidor { get; set; } = null!;
    public AlocacaoLastro Alocacao { get; set; } = null!;
}
