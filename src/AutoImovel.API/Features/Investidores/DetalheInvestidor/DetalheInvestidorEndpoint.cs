using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Investidores.DetalheInvestidor;

public static class DetalheInvestidorEndpoint
{
    public static void MapDetalheInvestidor(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/investidores/{id:guid}", async (
            Guid id,
            AppDbContext db,
            CancellationToken ct) =>
        {
            if (id == Guid.Empty)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "id", new[] { "ID do investidor é obrigatório." } }
                });

            var result = await DetalheInvestidorHandler.HandleAsync(db, id, ct);
            return Results.Ok(result);
        })
        .WithName("DetalheInvestidor")
        .Produces<InvestidorDetalheResponse>()
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
