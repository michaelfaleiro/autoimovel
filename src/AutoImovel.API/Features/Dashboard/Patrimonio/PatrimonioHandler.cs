using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Dashboard.Patrimonio;

public static class PatrimonioHandler
{
    public static async Task<PatrimonioResponse> HandleAsync(
        AppDbContext db,
        Guid investidorId,
        CancellationToken ct = default)
    {
        var invId = new InvestidorId(investidorId);

        var investidor = await db.Investidores
            .FirstOrDefaultAsync(i => i.Id == invId, ct)
            ?? throw new InvalidOperationException("Investidor não encontrado.");

        var capitalEmLiquidez = await db.Aportes
            .Where(a => a.InvestidorId == invId)
            .SumAsync(a => a.SaldoEmLiquidez, ct);

        var capitalAlocado = await db.AlocacoesLastro
            .Where(a => a.Aporte!.InvestidorId == invId && a.Status == StatusAlocacao.Ativo)
            .SumAsync(a => a.ValorAlocado, ct);

        return new PatrimonioResponse(
            investidor.Id.Value,
            investidor.NomeCompleto,
            capitalEmLiquidez,
            capitalAlocado,
            capitalEmLiquidez + capitalAlocado);
    }
}
