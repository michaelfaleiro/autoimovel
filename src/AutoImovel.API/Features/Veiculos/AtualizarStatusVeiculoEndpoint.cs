using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Veiculos;

public static class AtualizarStatusVeiculoEndpoint
{
    public static void MapAtualizarStatusVeiculo(this IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/veiculos/{id:guid}/status", async (
            Guid id,
            AtualizarStatusRequest request,
            AppDbContext db) =>
        {
            var veiculoId = new VeiculoId(id);
            var veiculo = await db.Veiculos.FirstOrDefaultAsync(v => v.Id == veiculoId)
                ?? throw new InvalidOperationException("Veículo não encontrado.");

            if (!Enum.TryParse<StatusVeiculo>(request.Status, true, out var novoStatus))
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "status", new[] { $"Status inválido: {request.Status}" } }
                });

            veiculo.Status = novoStatus;
            await db.SaveChangesAsync();

            return Results.Ok(new { veiculo.Id.Value, veiculo.Placa, veiculo.Status });
        })
        .WithName("AtualizarStatusVeiculo")
        .RequireAuthorization();
    }
}

public record AtualizarStatusRequest(string Status);
