using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.Fechamentos.FecharVenda;

public static class FecharVendaHandler
{
    private const decimal TaxaGestaoMensal = 0.02m;
    private const decimal TaxaInfraMensal = 0.06m;
    private const decimal TaxaPerformance = 0.20m;

    public static async Task<FecharVendaResponse> HandleAsync(
        AppDbContext db,
        FecharVendaRequest request,
        IConfiguration config,
        CancellationToken ct = default)
    {
        var veiculoId = new VeiculoId(request.VeiculoId);

        var veiculo = await db.Veiculos
            .Include(v => v.Alocacoes)
                .ThenInclude(a => a.Aporte)
                    .ThenInclude(ap => ap!.Investidor)
            .FirstOrDefaultAsync(v => v.Id == veiculoId, ct)
            ?? throw new InvalidOperationException("Veículo não encontrado.");

        if (veiculo.Status == StatusVeiculo.Vendido || veiculo.Status == StatusVeiculo.CicloEncerrado)
            throw new InvalidOperationException("Veículo já foi vendido ou ciclo encerrado.");

        veiculo.Status = StatusVeiculo.Vendido;
        veiculo.DataVenda = DateTime.UtcNow;
        veiculo.PrecoVendaReal = request.PrecoVendaReal;

        var custoTotalVeiculo = veiculo.PrecoCompra + veiculo.CustosPreparacao;
        var lucroBruto = request.PrecoVendaReal - custoTotalVeiculo;

        var alocacoesAtivas = veiculo.Alocacoes
            .Where(a => a.Status == StatusAlocacao.Ativo)
            .ToList();

        var totalAlocado = alocacoesAtivas.Sum(a => a.ValorAlocado);
        var fechamentos = new List<FechamentoLucroItem>();

        foreach (var alocacao in alocacoesAtivas)
        {
            var proporcao = totalAlocado > 0 ? alocacao.ValorAlocado / totalAlocado : 0;
            var lucroBrutoProporcional = lucroBruto * proporcao;

            var taxaGestao = lucroBrutoProporcional * TaxaGestaoMensal;
            var taxaInfra = lucroBrutoProporcional * TaxaInfraMensal;

            var lucroLiquidoBase = lucroBrutoProporcional - taxaGestao - taxaInfra;
            var cdiAnual = config.GetValue<decimal>("CdiAnualReferencia", 0.1475m);
            var lucroMinimoCdi = alocacao.ValorAlocado * (cdiAnual / 12);

            decimal taxaPerformance = 0;
            if (lucroLiquidoBase > lucroMinimoCdi)
            {
                var excesso = lucroLiquidoBase - lucroMinimoCdi;
                taxaPerformance = excesso * TaxaPerformance;
            }

            var lucroLiquido = lucroLiquidoBase - taxaPerformance;

            var investidor = alocacao.Aporte?.Investidor;
            var fechamento = new FechamentoLucro
            {
                Id = FechamentoLucroId.New(),
                InvestidorId = investidor?.Id ?? default,
                AlocacaoId = alocacao.Id,
                LucroBrutoProporcional = lucroBrutoProporcional,
                TaxaGestaoRetida = taxaGestao,
                TaxaInfraRetida = taxaInfra,
                TaxaPerformanceRetida = taxaPerformance,
                LucroLiquidoPago = lucroLiquido,
                DataFechamento = DateTime.UtcNow
            };

            db.FechamentosLucro.Add(fechamento);

            alocacao.Status = StatusAlocacao.Liquidado;
            alocacao.DataDesalocacao = DateTime.UtcNow;

            fechamentos.Add(new FechamentoLucroItem(
                investidor?.Id.Value ?? Guid.Empty,
                investidor?.NomeCompleto ?? "N/A",
                alocacao.ValorAlocado,
                proporcao,
                lucroBrutoProporcional,
                taxaGestao,
                taxaInfra,
                taxaPerformance,
                lucroLiquido));
        }

        await db.SaveChangesAsync(ct);

        return new FecharVendaResponse(
            veiculo.Id.Value,
            request.PrecoVendaReal,
            lucroBruto,
            custoTotalVeiculo,
            fechamentos);
    }
}
