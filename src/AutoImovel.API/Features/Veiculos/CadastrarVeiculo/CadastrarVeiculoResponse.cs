using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Veiculos.CadastrarVeiculo;

public sealed record CadastrarVeiculoResponse(
    Guid Id,
    string Placa,
    string MarcaModelo,
    StatusVeiculo Status,
    DateTime DataCompra);
