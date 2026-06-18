using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.Shared.Domain;

public class AlocacaoLastro
{
    public AlocacaoLastroId Id { get; set; }
    public AporteId AporteId { get; set; }
    public VeiculoId VeiculoId { get; set; }
    public decimal ValorAlocado { get; set; }
    public DateTime DataAlocacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataDesalocacao { get; set; }
    public StatusAlocacao Status { get; set; } = StatusAlocacao.Ativo;

    public Aporte Aporte { get; set; } = null!;
    public Veiculo Veiculo { get; set; } = null!;
}
