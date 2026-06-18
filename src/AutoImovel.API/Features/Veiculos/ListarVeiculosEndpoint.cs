using AutoImovel.API.Data;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Veiculos;

public static class ListarVeiculosEndpoint
{
    public static void MapListarVeiculos(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos", async (
            AppDbContext db,
            int page = 1,
            int size = 20,
            string? status = null,
            string? search = null) =>
        {
            var query = db.Veiculos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<StatusVeiculo>(status, true, out var statusFilter))
                query = query.Where(v => v.Status == statusFilter);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.ToLower();
                query = query.Where(v =>
                    v.Placa.ToLower().Contains(term) ||
                    v.MarcaModelo.ToLower().Contains(term));
            }

            var total = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(total / (double)size);

            var items = await query
                .OrderByDescending(v => v.DataCompra)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(v => new
                {
                    v.Id,
                    v.Placa,
                    v.MarcaModelo,
                    v.AnoFabricacaoModelo,
                    v.Km,
                    v.PrecoCompra,
                    v.CustosPreparacao,
                    v.Chassi,
                    v.Renavam,
                    v.Cor,
                    v.Status,
                    v.DataCompra,
                    v.DataVenda,
                    v.PrecoVendaReal
                })
                .ToListAsync();

            return Results.Ok(new { items, total, totalPages, page, size });
        })
        .WithName("ListarVeiculos")
        .RequireAuthorization();
    }
}
