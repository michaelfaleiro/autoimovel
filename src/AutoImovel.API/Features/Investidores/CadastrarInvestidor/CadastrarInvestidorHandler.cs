using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;

namespace AutoImovel.API.Features.Investidores.CadastrarInvestidor;

public static class CadastrarInvestidorHandler
{
    public static async Task<CadastrarInvestidorResponse> HandleAsync(
        AppDbContext db,
        CadastrarInvestidorRequest request,
        CancellationToken ct = default)
    {
        var investidor = new Investidor
        {
            Id = InvestidorId.New(),
            NomeCompleto = request.NomeCompleto,
            CpfCnpj = request.CpfCnpj,
            Email = request.Email,
            Telefone = request.Telefone,
            ChavePix = request.ChavePix,
            Banco = request.Banco,
            Agencia = request.Agencia,
            Conta = request.Conta,
            TipoConta = request.TipoConta,
            Status = StatusInvestidor.Pendente,
            DataCadastro = DateTime.UtcNow
        };

        db.Investidores.Add(investidor);
        await db.SaveChangesAsync(ct);

        return new CadastrarInvestidorResponse(
            investidor.Id.Value,
            investidor.NomeCompleto,
            investidor.CpfCnpj,
            investidor.Email,
            investidor.Status);
    }
}
