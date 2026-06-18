using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Dashboard.ExtratoRendimentos;

public static class ExtratoRendimentosHandler
{
    public static async Task<ExtratoRendimentosResponse> HandleAsync(
        AppDbContext db,
        Guid investidorId,
        CancellationToken ct = default)
    {
        var invId = new InvestidorId(investidorId);

        var rendimentos = await db.FechamentosLucro
            .Where(f => f.InvestidorId == invId)
            .OrderByDescending(f => f.DataFechamento)
            .Select(f => new RendimentoItem(
                f.Id.Value,
                f.Alocacao!.Veiculo!.Placa,
                f.Alocacao.Veiculo.MarcaModelo,
                f.LucroBrutoProporcional,
                f.TaxaGestaoRetida,
                f.TaxaInfraRetida,
                f.TaxaPerformanceRetida,
                f.LucroLiquidoPago,
                f.DataFechamento))
            .ToListAsync(ct);

        var totalLucroLiquido = rendimentos.Sum(r => r.LucroLiquidoPago);

        return new ExtratoRendimentosResponse(rendimentos, totalLucroLiquido);
    }
}
