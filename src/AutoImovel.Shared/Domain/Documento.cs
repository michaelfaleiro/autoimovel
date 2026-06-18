namespace AutoImovel.Shared.Domain;

public class Documento
{
    public Guid Id { get; set; }
    public VeiculoId? VeiculoId { get; set; }
    public InvestidorId? InvestidorId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = "Outro";
    public string Url { get; set; } = string.Empty;
    public DateTime DataUpload { get; set; } = DateTime.UtcNow;

    public Veiculo? Veiculo { get; set; }
    public Investidor? Investidor { get; set; }
}
