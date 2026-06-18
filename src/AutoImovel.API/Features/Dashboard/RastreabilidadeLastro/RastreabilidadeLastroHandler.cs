using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Dashboard.RastreabilidadeLastro;

public static class RastreabilidadeLastroHandler
{
    public static async Task<RastreabilidadeLastroResponse> HandleAsync(
        AppDbContext db,
        Guid investidorId,
        CancellationToken ct = default)
    {
        var invId = new InvestidorId(investidorId);

        var veiculos = await db.AlocacoesLastro
            .Where(a => a.Aporte!.InvestidorId == invId && a.Status == StatusAlocacao.Ativo)
            .Select(a => new VeiculoLastroItem(
                a.Veiculo!.Id.Value,
                a.Veiculo.Placa,
                a.Veiculo.MarcaModelo,
                a.Veiculo.AnoFabricacaoModelo,
                a.Veiculo.Km,
                a.ValorAlocado,
                a.Veiculo.Status,
                a.Veiculo.UrlLaudoCautelar,
                a.DataAlocacao))
            .ToListAsync(ct);

        return new RastreabilidadeLastroResponse(veiculos);
    }
}
