namespace AutoImovel.API.Features.Veiculos.CadastrarVeiculo;

public sealed record CadastrarVeiculoRequest(
    string Placa,
    string MarcaModelo,
    string AnoFabricacaoModelo,
    int Km,
    decimal PrecoCompra,
    decimal CustosPreparacao,
    string Chassi,
    string Renavam,
    string Cor,
    string UrlLaudoCautelar);
