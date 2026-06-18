using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Investidores.AprovarInvestidor;

public static class AprovarInvestidorHandler
{
    public static async Task<AprovarInvestidorResponse> HandleAsync(
        AppDbContext db,
        AprovarInvestidorRequest request,
        CancellationToken ct = default)
    {
        var investidorId = new InvestidorId(request.InvestidorId);

        var investidor = await db.Investidores
            .FirstOrDefaultAsync(i => i.Id == investidorId, ct)
            ?? throw new InvalidOperationException("Investidor não encontrado.");

        if (investidor.Status == StatusInvestidor.Aprovado)
            throw new InvalidOperationException("Investidor já está aprovado.");

        investidor.Status = StatusInvestidor.Aprovado;
        await db.SaveChangesAsync(ct);

        return new AprovarInvestidorResponse(
            investidor.Id.Value,
            investidor.NomeCompleto,
            investidor.Status);
    }
}
