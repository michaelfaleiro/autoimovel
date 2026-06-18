using AutoImovel.API.Data;
using FluentValidation;

namespace AutoImovel.API.Features.Veiculos.CadastrarVeiculo;

public static class CadastrarVeiculoEndpoint
{
    public static void MapCadastrarVeiculo(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/veiculos", async (
            CadastrarVeiculoRequest request,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var validator = new CadastrarVeiculoValidator();
            var validation = await validator.ValidateAsync(request, ct);

            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await CadastrarVeiculoHandler.HandleAsync(db, request, ct);
            return Results.Created($"/api/veiculos/{result.Id}", result);
        })
        .WithName("CadastrarVeiculo")
        .Produces<CadastrarVeiculoResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
}
