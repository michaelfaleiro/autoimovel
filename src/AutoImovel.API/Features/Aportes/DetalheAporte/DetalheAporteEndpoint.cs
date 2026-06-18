using AutoImovel.API.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Aportes.DetalheAporte;

public static class DetalheAporteEndpoint
{
    public static void MapDetalheAporte(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/aportes/{id:guid}", async (
            AppDbContext db,
            Guid id,
            CancellationToken ct) =>
        {
            var response = await DetalheAporteHandler.HandleAsync(db, id, ct);
            return Results.Ok(response);
        })
        .WithName("DetalheAporte")
        .RequireAuthorization();
    }
}
