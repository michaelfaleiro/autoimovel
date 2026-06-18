using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Dashboard.RastreabilidadeLastro;

public sealed record VeiculoLastroItem(
    Guid VeiculoId,
    string Placa,
    string MarcaModelo,
    string AnoFabricacaoModelo,
    int Km,
    decimal ValorAlocado,
    StatusVeiculo StatusVeiculo,
    string UrlLaudoCautelar,
    DateTime DataAlocacao);
