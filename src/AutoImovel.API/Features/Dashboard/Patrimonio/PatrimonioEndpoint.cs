using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Dashboard.Patrimonio;

public static class PatrimonioEndpoint
{
    public static void MapPatrimonio(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard/patrimonio/{investidorId:guid}", async (
            Guid investidorId,
            AppDbContext db,
            CancellationToken ct) =>
        {
            if (investidorId == Guid.Empty)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "investidorId", new[] { "ID do investidor é obrigatório." } }
                });

            var result = await PatrimonioHandler.HandleAsync(db, investidorId, ct);
            return Results.Ok(result);
        })
        .WithName("ConsultarPatrimonio")
        .Produces<PatrimonioResponse>()
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
