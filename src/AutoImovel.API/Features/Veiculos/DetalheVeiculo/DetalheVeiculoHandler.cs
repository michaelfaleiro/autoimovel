using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Veiculos.DetalheVeiculo;

public static class DetalheVeiculoHandler
{
    public static async Task<VeiculoDetalheResponse> HandleAsync(
        AppDbContext db,
        Guid id,
        CancellationToken ct = default)
    {
        var veiculoId = new VeiculoId(id);

        var veiculo = await db.Veiculos
            .FirstOrDefaultAsync(v => v.Id == veiculoId, ct)
            ?? throw new InvalidOperationException("Veículo não encontrado.");

        var alocacoes = await db.AlocacoesLastro
            .Include(a => a.Aporte)
                .ThenInclude(ap => ap.Investidor)
            .Where(a => a.VeiculoId == veiculoId)
            .ToListAsync(ct);

        var totalAlocado = alocacoes.Sum(a => a.ValorAlocado);

        var alocacaoItems = alocacoes.Select(a => new AlocacaoVeiculoItem(
            a.Id.Value,
            a.Aporte!.Investidor.Id.Value,
            a.Aporte.Investidor.NomeCompleto,
            a.ValorAlocado,
            totalAlocado > 0 ? Math.Round(a.ValorAlocado / totalAlocado * 100, 2) : 0,
            a.Status,
            a.DataAlocacao
        )).ToList();

        FechamentoVeiculoItem? fechamento = null;

        if (veiculo.Status == StatusVeiculo.Vendido)
        {
            var alocacaoIds = alocacoes.Select(a => a.Id).ToList();

            var fechamentos = await db.FechamentosLucro
                .Where(f => alocacaoIds.Contains(f.AlocacaoId))
                .ToListAsync(ct);

            if (fechamentos.Count != 0)
            {
                fechamento = new FechamentoVeiculoItem(
                    fechamentos.Sum(f => f.LucroBrutoProporcional),
                    fechamentos.Sum(f => f.TaxaGestaoRetida),
                    fechamentos.Sum(f => f.TaxaInfraRetida),
                    fechamentos.Sum(f => f.TaxaPerformanceRetida),
                    fechamentos.Sum(f => f.LucroLiquidoPago)
                );
            }
        }

        return new VeiculoDetalheResponse(
            veiculo.Id.Value,
            veiculo.Placa,
            veiculo.MarcaModelo,
            veiculo.AnoFabricacaoModelo,
            veiculo.Km,
            veiculo.PrecoCompra,
            veiculo.CustosPreparacao,
            veiculo.Chassi,
            veiculo.Renavam,
            veiculo.Cor,
            veiculo.Status,
            veiculo.UrlLaudoCautelar,
            veiculo.DataCompra,
            veiculo.DataVenda,
            veiculo.PrecoVendaReal,
            alocacaoItems,
            fechamento
        );
    }
}
