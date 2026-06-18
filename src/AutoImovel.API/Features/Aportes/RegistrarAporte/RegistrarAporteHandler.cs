using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Aportes.RegistrarAporte;

public static class RegistrarAporteHandler
{
    public static async Task<RegistrarAporteResponse> HandleAsync(
        AppDbContext db,
        RegistrarAporteRequest request,
        CancellationToken ct = default)
    {
        var investidorId = new InvestidorId(request.InvestidorId);

        var investidor = await db.Investidores
            .FirstOrDefaultAsync(i => i.Id == investidorId, ct)
            ?? throw new InvalidOperationException("Investidor não encontrado.");

        if (investidor.Status != StatusInvestidor.Aprovado)
            throw new InvalidOperationException("Investidor deve estar aprovado para registrar aporte.");

        var aporte = new Aporte
        {
            Id = AporteId.New(),
            InvestidorId = investidorId,
            ValorOriginal = request.Valor,
            SaldoEmLiquidez = request.Valor,
            Status = StatusAporte.EmLiquidez,
            DataAporte = DateTime.UtcNow,
            UrlContratoScp = request.UrlContratoScp
        };

        db.Aportes.Add(aporte);
        await db.SaveChangesAsync(ct);

        return new RegistrarAporteResponse(
            aporte.Id.Value,
            aporte.InvestidorId.Value,
            aporte.ValorOriginal,
            aporte.SaldoEmLiquidez,
            aporte.Status,
            aporte.DataAporte);
    }
}
