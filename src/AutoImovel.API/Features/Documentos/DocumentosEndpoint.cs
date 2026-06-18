using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Documentos;

public static class DocumentosEndpoint
{
    public static void MapDocumentos(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/documentos", async (
            [FromQuery] Guid? veiculoId,
            [FromQuery] Guid? investidorId,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var query = db.Documentos.AsQueryable();

            if (veiculoId.HasValue)
            {
                var vId = new VeiculoId(veiculoId.Value);
                query = query.Where(d => d.VeiculoId == vId);
            }

            if (investidorId.HasValue)
            {
                var iId = new InvestidorId(investidorId.Value);
                query = query.Where(d => d.InvestidorId == iId);
            }

            var items = await query
                .OrderByDescending(d => d.DataUpload)
                .Select(d => new DocumentoItem(
                    d.Id, d.Nome, d.Tipo, d.Url, d.VeiculoId, d.InvestidorId, d.DataUpload))
                .ToListAsync(ct);

            return Results.Ok(items);
        })
        .WithName("ListarDocumentos")
        .RequireAuthorization();

        app.MapPost("/api/documentos", async (
            DocumentoRequest request,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var doc = new Documento
            {
                Id = Guid.NewGuid(),
                VeiculoId = request.VeiculoId,
                InvestidorId = request.InvestidorId,
                Nome = request.Nome,
                Tipo = request.Tipo,
                Url = request.Url,
                DataUpload = DateTime.UtcNow
            };

            db.Documentos.Add(doc);
            await db.SaveChangesAsync(ct);

            return Results.Created($"/api/documentos/{doc.Id}", new DocumentoItem(
                doc.Id, doc.Nome, doc.Tipo, doc.Url, doc.VeiculoId, doc.InvestidorId, doc.DataUpload));
        })
        .WithName("CriarDocumento")
        .RequireAuthorization();

        app.MapDelete("/api/documentos/{id:guid}", async (
            Guid id,
            AppDbContext db,
            CancellationToken ct) =>
        {
            var doc = await db.Documentos.FindAsync(new object[] { id }, ct);
            if (doc is null) return Results.NotFound();
            db.Documentos.Remove(doc);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        })
        .WithName("RemoverDocumento")
        .RequireAuthorization();
    }
}

public sealed record DocumentoItem(
    Guid Id, string Nome, string Tipo, string Url,
    VeiculoId? VeiculoId, InvestidorId? InvestidorId, DateTime DataUpload);

public sealed record DocumentoRequest(
    string Nome, string Tipo, string Url,
    VeiculoId? VeiculoId, InvestidorId? InvestidorId);
