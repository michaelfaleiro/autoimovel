using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Aportes.DetalheAporte;

public static class DetalheAporteHandler
{
    public static async Task<AporteDetalheResponse> HandleAsync(
        AppDbContext db,
        Guid id,
        CancellationToken ct = default)
    {
        var aporteId = new AporteId(id);

        var aporte = await db.Aportes
            .Include(a => a.Investidor)
            .FirstOrDefaultAsync(a => a.Id == aporteId, ct)
            ?? throw new KeyNotFoundException("Aporte não encontrado.");

        var alocacoes = await db.AlocacoesLastro
            .Include(a => a.Veiculo)
            .Where(a => a.AporteId == aporteId)
            .ToListAsync(ct);

        var totalAlocado = alocacoes.Sum(a => a.ValorAlocado);

        return new AporteDetalheResponse(
            aporte.Id.Value,
            aporte.InvestidorId.Value,
            aporte.Investidor.NomeCompleto,
            aporte.ValorOriginal,
            aporte.SaldoEmLiquidez,
            totalAlocado,
            aporte.Status,
            aporte.DataAporte,
            aporte.UrlContratoScp,
            alocacoes.Select(a => new AlocacaoAporteItem(
                a.Id.Value,
                a.VeiculoId.Value,
                a.Veiculo.Placa,
                a.Veiculo.MarcaModelo,
                a.ValorAlocado,
                a.Status,
                a.DataAlocacao
            )).ToList()
        );
    }
}
