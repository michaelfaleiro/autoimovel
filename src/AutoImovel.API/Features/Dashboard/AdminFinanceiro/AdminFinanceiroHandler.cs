using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Dashboard.AdminFinanceiro;

public static class AdminFinanceiroHandler
{
    public static async Task<AdminFinanceiroResponse> HandleAsync(
        AppDbContext db,
        CancellationToken ct = default)
    {
        var totalInvestido = await db.Aportes
            .SumAsync(a => a.ValorOriginal, ct);

        var totalEmLiquidez = await db.Aportes
            .SumAsync(a => a.SaldoEmLiquidez, ct);

        var totalAlocado = await db.AlocacoesLastro
            .Where(a => a.Status == StatusAlocacao.Ativo)
            .SumAsync(a => a.ValorAlocado, ct);

        var quantidadeInvestidores = await db.Investidores
            .CountAsync(ct);

        var quantidadeVeiculosTotal = await db.Veiculos
            .CountAsync(ct);

        var quantidadeVeiculosDisponiveis = await db.Veiculos
            .Where(v => v.Status == StatusVeiculo.Disponivel)
            .CountAsync(ct);

        var quantidadeVeiculosAlocados = await db.AlocacoesLastro
            .Where(a => a.Status == StatusAlocacao.Ativo)
            .Select(a => a.VeiculoId)
            .Distinct()
            .CountAsync(ct);

        var quantidadeVeiculosVendidos = await db.Veiculos
            .Where(v => v.Status == StatusVeiculo.Vendido)
            .CountAsync(ct);

        var valorTotalVendas = await db.Veiculos
            .SumAsync(v => v.PrecoVendaReal ?? 0, ct);

        var totalRendimentosDistribuidos = await db.FechamentosLucro
            .SumAsync(f => f.LucroLiquidoPago, ct);

        var recentes = await db.AlocacoesLastro
            .Include(a => a.Aporte!.Investidor)
            .Include(a => a.Veiculo)
            .OrderByDescending(a => a.DataAlocacao)
            .Take(10)
            .ToListAsync(ct);

        var atividades = recentes.Select(a =>
        {
            var descricao = $"R$ {a.ValorAlocado:F2} alocado no veículo {a.Veiculo.Placa} ({a.Aporte!.Investidor.NomeCompleto})";
            return new AtividadeRecenteItem("Alocacao", descricao, a.ValorAlocado, a.DataAlocacao);
        }).ToList();

        return new AdminFinanceiroResponse(
            totalInvestido,
            totalEmLiquidez,
            totalAlocado,
            quantidadeInvestidores,
            quantidadeVeiculosTotal,
            quantidadeVeiculosDisponiveis,
            quantidadeVeiculosAlocados,
            quantidadeVeiculosVendidos,
            valorTotalVendas,
            totalRendimentosDistribuidos,
            atividades
        );
    }
}
