using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Aportes;

public static class ListarAportesEndpoint
{
    public static void MapListarAportes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/aportes", async (
            AppDbContext db,
            int page = 1,
            int size = 20,
            string? investidorId = null,
            string? status = null) =>
        {
            var query = db.Aportes.Include(a => a.Investidor).AsQueryable();

            if (Guid.TryParse(investidorId, out var invIdGuid))
            {
                var invId = new InvestidorId(invIdGuid);
                query = query.Where(a => a.InvestidorId == invId);
            }

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<StatusAporte>(status, true, out var statusFilter))
                query = query.Where(a => a.Status == statusFilter);

            var total = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(total / (double)size);

            var items = await query
                .OrderByDescending(a => a.DataAporte)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(a => new
                {
                    a.Id,
                    InvestidorId = a.InvestidorId.Value,
                    InvestidorNome = a.Investidor!.NomeCompleto,
                    a.ValorOriginal,
                    a.SaldoEmLiquidez,
                    a.Status,
                    a.DataAporte
                })
                .ToListAsync();

            return Results.Ok(new { items, total, totalPages, page, size });
        })
        .WithName("ListarAportes")
        .RequireAuthorization();
    }
}
