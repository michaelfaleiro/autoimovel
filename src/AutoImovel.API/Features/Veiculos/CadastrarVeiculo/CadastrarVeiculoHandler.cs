using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Veiculos.CadastrarVeiculo;

public static class CadastrarVeiculoHandler
{
    public static async Task<CadastrarVeiculoResponse> HandleAsync(
        AppDbContext db,
        CadastrarVeiculoRequest request,
        CancellationToken ct = default)
    {
        var placaExistente = await db.Veiculos
            .AnyAsync(v => v.Placa == request.Placa, ct);

        if (placaExistente)
            throw new InvalidOperationException("Já existe um veículo cadastrado com esta placa.");

        var veiculo = new Veiculo
        {
            Id = VeiculoId.New(),
            Placa = request.Placa.ToUpperInvariant(),
            MarcaModelo = request.MarcaModelo,
            AnoFabricacaoModelo = request.AnoFabricacaoModelo,
            Km = request.Km,
            PrecoCompra = request.PrecoCompra,
            CustosPreparacao = request.CustosPreparacao,
            Chassi = request.Chassi,
            Renavam = request.Renavam,
            Cor = request.Cor,
            Status = StatusVeiculo.Compra,
            UrlLaudoCautelar = request.UrlLaudoCautelar,
            DataCompra = DateTime.UtcNow
        };

        db.Veiculos.Add(veiculo);
        await db.SaveChangesAsync(ct);

        return new CadastrarVeiculoResponse(
            veiculo.Id.Value,
            veiculo.Placa,
            veiculo.MarcaModelo,
            veiculo.Status,
            veiculo.DataCompra);
    }
}
