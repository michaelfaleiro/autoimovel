using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Fechamentos.FecharVenda;

public static class FecharVendaEndpoint
{
    public static void MapFecharVenda(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/fechamentos/fechar-venda", async (
            FecharVendaRequest request,
            AppDbContext db,
            IConfiguration config,
            CancellationToken ct) =>
        {
            var validator = new FecharVendaValidator();
            var validation = await validator.ValidateAsync(request, ct);

            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await FecharVendaHandler.HandleAsync(db, request, config, ct);
            return Results.Ok(result);
        })
        .WithName("FecharVenda")
        .Produces<FecharVendaResponse>()
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
