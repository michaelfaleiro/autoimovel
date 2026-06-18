using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Features.VincularLastro;

public static class VincularLastroHandler
{
    public static async Task<VincularLastroResponse> HandleAsync(
        AppDbContext db,
        VincularLastroRequest request,
        CancellationToken ct = default)
    {
        var investidorId = new InvestidorId(request.InvestidorId);
        var aporteId = new AporteId(request.AporteId);
        var veiculoId = new VeiculoId(request.VeiculoId);

        var investidor = await db.Investidores
            .FirstOrDefaultAsync(i => i.Id == investidorId, ct)
            ?? throw new InvalidOperationException("Investidor não encontrado.");

        if (investidor.Status != StatusInvestidor.Aprovado)
            throw new InvalidOperationException("Investidor deve estar aprovado para vincular lastro.");

        var aporte = await db.Aportes
            .FirstOrDefaultAsync(a => a.Id == aporteId, ct)
            ?? throw new InvalidOperationException("Aporte não encontrado.");

        if (aporte.InvestidorId != investidorId)
            throw new InvalidOperationException("Aporte não pertence ao investidor informado.");

        if (aporte.SaldoEmLiquidez < request.ValorAlocado)
            throw new InvalidOperationException(
                $"Saldo em liquidez insuficiente. Saldo disponível: R$ {aporte.SaldoEmLiquidez:N2}.");

        var veiculo = await db.Veiculos
            .FirstOrDefaultAsync(v => v.Id == veiculoId, ct)
            ?? throw new InvalidOperationException("Veículo não encontrado.");

        if (veiculo.Status != StatusVeiculo.Disponivel)
            throw new InvalidOperationException("Veículo deve estar com status 'Disponível' para receber lastro.");

        var alocacao = new AlocacaoLastro
        {
            Id = AlocacaoLastroId.New(),
            AporteId = aporteId,
            VeiculoId = veiculoId,
            ValorAlocado = request.ValorAlocado,
            DataAlocacao = DateTime.UtcNow,
            Status = StatusAlocacao.Ativo
        };

        aporte.SaldoEmLiquidez -= request.ValorAlocado;
        if (aporte.SaldoEmLiquidez <= 0)
            aporte.Status = StatusAporte.Alocado;

        db.AlocacoesLastro.Add(alocacao);
        await db.SaveChangesAsync(ct);

        return new VincularLastroResponse(
            alocacao.Id.Value,
            alocacao.AporteId.Value,
            alocacao.VeiculoId.Value,
            alocacao.ValorAlocado,
            alocacao.Status,
            alocacao.DataAlocacao);
    }
}
