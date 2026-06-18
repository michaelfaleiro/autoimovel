using AutoImovel.API.Data;

namespace AutoImovel.API.Features.Dashboard.AdminFinanceiro;

public static class AdminFinanceiroEndpoint
{
    public static void MapAdminFinanceiro(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard/admin/financeiro", async (
            AppDbContext db,
            CancellationToken ct) =>
        {
            var result = await AdminFinanceiroHandler.HandleAsync(db, ct);
            return Results.Ok(result);
        })
        .WithName("ConsultarAdminFinanceiro")
        .Produces<AdminFinanceiroResponse>()
        .RequireAuthorization();
    }
}
