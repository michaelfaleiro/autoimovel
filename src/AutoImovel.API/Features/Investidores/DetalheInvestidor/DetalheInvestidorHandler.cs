using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Investidores.DetalheInvestidor;

public static class DetalheInvestidorHandler
{
    public static async Task<InvestidorDetalheResponse> HandleAsync(
        AppDbContext db,
        Guid investidorId,
        CancellationToken ct = default)
    {
        var invId = new InvestidorId(investidorId);

        var investidor = await db.Investidores
            .FirstOrDefaultAsync(i => i.Id == invId, ct)
            ?? throw new InvalidOperationException("Investidor não encontrado.");

        var aportes = await db.Aportes
            .Where(a => a.InvestidorId == invId)
            .OrderByDescending(a => a.DataAporte)
            .ToListAsync(ct);

        var alocacoesAtivas = await db.AlocacoesLastro
            .Include(a => a.Veiculo)
            .Include(a => a.Aporte)
            .Where(a => a.Aporte!.InvestidorId == invId && a.Status == StatusAlocacao.Ativo)
            .ToListAsync(ct);

        var fechamentos = await db.FechamentosLucro
                .Include(f => f.Alocacao)
                    .ThenInclude(a => a.Veiculo)
            .Where(f => f.InvestidorId == invId)
            .ToListAsync(ct);

        var totalInvestido = aportes.Sum(a => a.ValorOriginal);
        var totalEmLiquidez = aportes.Sum(a => a.SaldoEmLiquidez);
        var totalAlocado = alocacoesAtivas.Sum(a => a.ValorAlocado);
        var totalRendimentos = fechamentos.Sum(f => f.LucroLiquidoPago);

        return new InvestidorDetalheResponse(
            investidor.Id.Value,
            investidor.NomeCompleto,
            investidor.CpfCnpj,
            investidor.Email,
            investidor.Telefone,
            investidor.Status,
            investidor.DataCadastro,
            investidor.ChavePix,
            investidor.Banco,
            investidor.Agencia,
            investidor.Conta,
            investidor.TipoConta,
            new PortfolioSummary(totalInvestido, totalEmLiquidez, totalAlocado, totalRendimentos),
            aportes.Select(a => new AporteItemResponse(
                a.Id.Value,
                a.ValorOriginal,
                a.SaldoEmLiquidez,
                a.Status,
                a.DataAporte)).ToList(),
            alocacoesAtivas.Select(a =>
            {
                var percentual = a.Aporte is { ValorOriginal: > 0 }
                    ? Math.Round(a.ValorAlocado / a.Aporte.ValorOriginal * 100, 2)
                    : 0;
                return new AlocacaoInvestidorItem(
                    a.Id.Value,
                    a.Veiculo.Id.Value,
                    a.Veiculo.Placa,
                    a.Veiculo.MarcaModelo,
                    a.ValorAlocado,
                    percentual,
                    a.Status,
                    a.DataAlocacao);
            }).ToList(),
            fechamentos.Select(f => new RendimentoRecebidoItem(
                f.Id.Value,
                f.Alocacao?.Veiculo?.Placa ?? "",
                f.Alocacao?.Veiculo?.MarcaModelo ?? "",
                f.LucroBrutoProporcional,
                f.TaxaGestaoRetida,
                f.TaxaInfraRetida,
                f.TaxaPerformanceRetida,
                f.LucroLiquidoPago,
                f.DataFechamento)).ToList());
    }
}
