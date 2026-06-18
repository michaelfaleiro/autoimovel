using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Investidores.CadastrarInvestidor;

public static class CadastrarInvestidorEndpoint
{
    public static void MapCadastrarInvestidor(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/investidores", async (
            CadastrarInvestidorRequest request,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var validator = new CadastrarInvestidorValidator();
            var validation = await validator.ValidateAsync(request, ct);

            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await CadastrarInvestidorHandler.HandleAsync(db, request, ct);
            return Results.Created($"/api/investidores/{result.Id}", result);
        })
        .WithName("CadastrarInvestidor")
        .Produces<CadastrarInvestidorResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
