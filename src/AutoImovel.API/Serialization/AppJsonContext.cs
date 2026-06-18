using System.Text.Json.Serialization;
using AutoImovel.API.Features.Aportes.DetalheAporte;
using AutoImovel.API.Features.Aportes.RegistrarAporte;
using AutoImovel.API.Features.Auth;
using AutoImovel.API.Features.Dashboard.AdminFinanceiro;
using AutoImovel.API.Features.Documentos;
using AutoImovel.API.Features.Dashboard.ExtratoRendimentos;
using AutoImovel.API.Features.Dashboard.Patrimonio;
using AutoImovel.API.Features.Dashboard.RastreabilidadeLastro;
using AutoImovel.API.Features.Fechamentos.FecharVenda;
using AutoImovel.API.Features.Investidores.AprovarInvestidor;
using AutoImovel.API.Features.Investidores.CadastrarInvestidor;
using AutoImovel.API.Features.Investidores.DetalheInvestidor;
using AutoImovel.API.Features.Veiculos.CadastrarVeiculo;
using AutoImovel.API.Features.Veiculos.DetalheVeiculo;
using AutoImovel.API.Features.VincularLastro;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Serialization;

[JsonSerializable(typeof(Investidor))]
[JsonSerializable(typeof(Aporte))]
[JsonSerializable(typeof(Veiculo))]
[JsonSerializable(typeof(AlocacaoLastro))]
[JsonSerializable(typeof(FechamentoLucro))]
[JsonSerializable(typeof(StatusInvestidor))]
[JsonSerializable(typeof(StatusAporte))]
[JsonSerializable(typeof(StatusVeiculo))]
[JsonSerializable(typeof(StatusAlocacao))]
[JsonSerializable(typeof(LoginRequest))]
[JsonSerializable(typeof(LoginResponse))]
[JsonSerializable(typeof(AprovarInvestidorRequest))]
[JsonSerializable(typeof(AprovarInvestidorResponse))]
[JsonSerializable(typeof(RegistrarAporteRequest))]
[JsonSerializable(typeof(RegistrarAporteResponse))]
[JsonSerializable(typeof(CadastrarVeiculoRequest))]
[JsonSerializable(typeof(CadastrarVeiculoResponse))]
[JsonSerializable(typeof(VeiculoDetalheResponse))]
[JsonSerializable(typeof(AlocacaoVeiculoItem))]
[JsonSerializable(typeof(FechamentoVeiculoItem))]
[JsonSerializable(typeof(VincularLastroRequest))]
[JsonSerializable(typeof(VincularLastroResponse))]
[JsonSerializable(typeof(FecharVendaRequest))]
[JsonSerializable(typeof(FecharVendaResponse))]
[JsonSerializable(typeof(FechamentoLucroItem))]
[JsonSerializable(typeof(CadastrarInvestidorRequest))]
[JsonSerializable(typeof(CadastrarInvestidorResponse))]
[JsonSerializable(typeof(PatrimonioResponse))]
[JsonSerializable(typeof(RastreabilidadeLastroResponse))]
[JsonSerializable(typeof(VeiculoLastroItem))]
[JsonSerializable(typeof(AdminFinanceiroResponse))]
[JsonSerializable(typeof(AtividadeRecenteItem))]
[JsonSerializable(typeof(ExtratoRendimentosResponse))]
[JsonSerializable(typeof(RendimentoItem))]
[JsonSerializable(typeof(AporteDetalheResponse))]
[JsonSerializable(typeof(AlocacaoAporteItem))]
[JsonSerializable(typeof(InvestidorDetalheResponse))]
[JsonSerializable(typeof(PortfolioSummary))]
[JsonSerializable(typeof(AporteItemResponse))]
[JsonSerializable(typeof(AlocacaoInvestidorItem))]
[JsonSerializable(typeof(RendimentoRecebidoItem))]
[JsonSerializable(typeof(DocumentoItem))]
[JsonSerializable(typeof(DocumentoRequest))]
public partial class AppJsonContext : JsonSerializerContext;
