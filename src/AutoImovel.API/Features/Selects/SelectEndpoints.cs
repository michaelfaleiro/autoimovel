using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Selects;

public static class SelectEndpoints
{
    public static void MapSelectEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/select/investidores", async (AppDbContext db, string? status = null) =>
        {
            var query = db.Investidores.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<StatusInvestidor>(status, true, out var s))
                query = query.Where(i => i.Status == s);
            var items = await query
                .OrderBy(i => i.NomeCompleto)
                .Select(i => new { i.Id, i.NomeCompleto, i.CpfCnpj })
                .ToListAsync();
            return Results.Ok(items);
        })
        .WithName("SelectInvestidores")
        .RequireAuthorization();

        app.MapGet("/api/select/aportes", async (AppDbContext db, Guid? investidorId, string? status = null) =>
        {
            var query = db.Aportes.AsQueryable();
            if (investidorId.HasValue && investidorId.Value != Guid.Empty)
                query = query.Where(a => a.InvestidorId == new InvestidorId(investidorId.Value));
            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<StatusAporte>(status, true, out var s))
                query = query.Where(a => a.Status == s);
            var items = await query
                .OrderByDescending(a => a.DataAporte)
                .Select(a => new { a.Id, a.InvestidorId, a.ValorOriginal, a.SaldoEmLiquidez, a.Status })
                .ToListAsync();
            return Results.Ok(items);
        })
        .WithName("SelectAportes")
        .RequireAuthorization();

        app.MapGet("/api/select/veiculos", async (AppDbContext db, string? status = null) =>
        {
            var query = db.Veiculos.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
            {
                var statuses = status.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var parsed = statuses.Select(s => Enum.Parse<StatusVeiculo>(s, true)).ToList();
                query = query.Where(v => parsed.Contains(v.Status));
            }
            var items = await query
                .OrderByDescending(v => v.DataCompra)
                .Select(v => new { v.Id, v.Placa, v.MarcaModelo, v.PrecoCompra, v.Km, v.Status })
                .ToListAsync();
            return Results.Ok(items);
        })
        .WithName("SelectVeiculos")
        .RequireAuthorization();
    }
}
