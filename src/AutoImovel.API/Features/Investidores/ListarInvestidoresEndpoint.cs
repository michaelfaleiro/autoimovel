using AutoImovel.API.Data;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Investidores;

public static class ListarInvestidoresEndpoint
{
    public static void MapListarInvestidores(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/investidores", async (
            AppDbContext db,
            int page = 1,
            int size = 20,
            string? status = null,
            string? search = null) =>
        {
            var query = db.Investidores.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<StatusInvestidor>(status, true, out var statusFilter))
                query = query.Where(i => i.Status == statusFilter);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.ToLower();
                query = query.Where(i =>
                    i.NomeCompleto.ToLower().Contains(term) ||
                    i.CpfCnpj.Contains(term) ||
                    i.Email.ToLower().Contains(term));
            }

            var total = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(total / (double)size);

            var items = await query
                .OrderByDescending(i => i.DataCadastro)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(i => new
                {
                    i.Id,
                    i.NomeCompleto,
                    i.CpfCnpj,
                    i.Email,
                    i.Status,
                    i.DataCadastro,
                    i.ChavePix,
                    i.Banco,
                    i.Agencia,
                    i.Conta,
                    i.TipoConta
                })
                .ToListAsync();

            return Results.Ok(new { items, total, totalPages, page, size });
        })
        .WithName("ListarInvestidores")
        .RequireAuthorization();
    }
}
