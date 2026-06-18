using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Dashboard.RastreabilidadeLastro;

public static class RastreabilidadeLastroEndpoint
{
    public static void MapRastreabilidadeLastro(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard/rastreabilidade/{investidorId:guid}", async (
            Guid investidorId,
            AppDbContext db,
            CancellationToken ct) =>
        {
            if (investidorId == Guid.Empty)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "investidorId", new[] { "ID do investidor é obrigatório." } }
                });

            var result = await RastreabilidadeLastroHandler.HandleAsync(db, investidorId, ct);
            return Results.Ok(result);
        })
        .WithName("ConsultarRastreabilidadeLastro")
        .Produces<RastreabilidadeLastroResponse>()
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
