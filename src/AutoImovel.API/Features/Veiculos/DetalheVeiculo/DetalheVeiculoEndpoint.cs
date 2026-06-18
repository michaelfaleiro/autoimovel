using AutoImovel.API.Data;

namespace AutoImovel.API.Features.Veiculos.DetalheVeiculo;

public static class DetalheVeiculoEndpoint
{
    public static void MapDetalheVeiculo(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos/{id:guid}", async (
            AppDbContext db,
            Guid id,
            CancellationToken ct) =>
        {
            var response = await DetalheVeiculoHandler.HandleAsync(db, id, ct);
            return Results.Ok(response);
        })
        .WithName("DetalheVeiculo")
        .RequireAuthorization();
    }
}
