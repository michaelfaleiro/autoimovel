using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Dashboard.ExtratoRendimentos;

public static class ExtratoRendimentosEndpoint
{
    public static void MapExtratoRendimentos(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard/extrato/{investidorId:guid}", async (
            Guid investidorId,
            AppDbContext db,
            CancellationToken ct) =>
        {
            if (investidorId == Guid.Empty)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "investidorId", new[] { "ID do investidor é obrigatório." } }
                });

            var result = await ExtratoRendimentosHandler.HandleAsync(db, investidorId, ct);
            return Results.Ok(result);
        })
        .WithName("ConsultarExtratoRendimentos")
        .Produces<ExtratoRendimentosResponse>()
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
