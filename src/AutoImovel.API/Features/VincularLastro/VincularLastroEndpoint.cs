using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.VincularLastro;

public static class VincularLastroEndpoint
{
    public static void MapVincularLastro(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/vincular-lastro", async (
            VincularLastroRequest request,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var validator = new VincularLastroValidator();
            var validation = await validator.ValidateAsync(request, ct);

            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await VincularLastroHandler.HandleAsync(db, request, ct);
            return Results.Ok(result);
        })
        .WithName("VincularLastro")
        .Produces<VincularLastroResponse>()
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
