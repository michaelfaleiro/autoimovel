using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.Shared.Domain;

public class Veiculo
{
    public VeiculoId Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string MarcaModelo { get; set; } = string.Empty;
    public string AnoFabricacaoModelo { get; set; } = string.Empty;
    public int Km { get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal CustosPreparacao { get; set; }
    public string Chassi { get; set; } = string.Empty;
    public string Renavam { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public StatusVeiculo Status { get; set; } = StatusVeiculo.Compra;
    public string UrlLaudoCautelar { get; set; } = string.Empty;
    public DateTime DataCompra { get; set; } = DateTime.UtcNow;
    public DateTime? DataVenda { get; set; }
    public decimal? PrecoVendaReal { get; set; }

    public ICollection<AlocacaoLastro> Alocacoes { get; set; } = new List<AlocacaoLastro>();
}
