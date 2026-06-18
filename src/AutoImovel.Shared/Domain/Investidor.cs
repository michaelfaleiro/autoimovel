using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.Shared.Domain;

public class Investidor
{
    public InvestidorId Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public StatusInvestidor Status { get; set; } = StatusInvestidor.Pendente;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public string ChavePix { get; set; } = string.Empty;
    public string Banco { get; set; } = string.Empty;
    public string Agencia { get; set; } = string.Empty;
    public string Conta { get; set; } = string.Empty;
    public string TipoConta { get; set; } = string.Empty;

    public ICollection<Aporte> Aportes { get; set; } = new List<Aporte>();
    public ICollection<FechamentoLucro> FechamentosLucro { get; set; } = new List<FechamentoLucro>();
}
