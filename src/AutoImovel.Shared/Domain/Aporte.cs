using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.Shared.Domain;

public class Aporte
{
    public AporteId Id { get; set; }
    public InvestidorId InvestidorId { get; set; }
    public decimal ValorOriginal { get; set; }
    public decimal SaldoEmLiquidez { get; set; }
    public StatusAporte Status { get; set; } = StatusAporte.EmLiquidez;
    public DateTime DataAporte { get; set; } = DateTime.UtcNow;
    public string UrlContratoScp { get; set; } = string.Empty;

    public Investidor Investidor { get; set; } = null!;
    public ICollection<AlocacaoLastro> Alocacoes { get; set; } = new List<AlocacaoLastro>();
}
