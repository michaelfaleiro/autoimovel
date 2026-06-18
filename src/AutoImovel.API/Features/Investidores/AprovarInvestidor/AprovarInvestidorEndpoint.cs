using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Investidores.AprovarInvestidor;

public static class AprovarInvestidorEndpoint
{
    public static void MapAprovarInvestidor(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/investidores/aprovar", async (
            AprovarInvestidorRequest request,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var validator = new AprovarInvestidorValidator();
            var validation = await validator.ValidateAsync(request, ct);

            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await AprovarInvestidorHandler.HandleAsync(db, request, ct);
            return Results.Ok(result);
        })
        .WithName("AprovarInvestidor")
        .Produces<AprovarInvestidorResponse>()
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
