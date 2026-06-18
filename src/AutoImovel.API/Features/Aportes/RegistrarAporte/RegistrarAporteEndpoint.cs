using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Aportes.RegistrarAporte;

public static class RegistrarAporteEndpoint
{
    public static void MapRegistrarAporte(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/aportes", async (
            RegistrarAporteRequest request,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var validator = new RegistrarAporteValidator();
            var validation = await validator.ValidateAsync(request, ct);

            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await RegistrarAporteHandler.HandleAsync(db, request, ct);
            return Results.Created($"/api/aportes/{result.Id}", result);
        })
        .WithName("RegistrarAporte")
        .Produces<RegistrarAporteResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
