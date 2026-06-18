using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

        await db.Database.MigrateAsync();

        // Ensure roles exist first
        string[] roles = ["Admin", "Investidor"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                logger.LogInformation("Role '{Role}' created.", role);
            }
        }

        // Seed Admin
        var adminEmail = "admin@autoimovel.com";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(admin, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
                logger.LogInformation("Admin user created.");
            }
            else
            {
                logger.LogWarning("Admin user creation failed: {Errors}",
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // Seed Investor
        var investorEmail = "investidor@autoimovel.com";
        if (await userManager.FindByEmailAsync(investorEmail) is null)
        {
            var investor = new IdentityUser
            {
                UserName = investorEmail,
                Email = investorEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(investor, "Inv@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(investor, "Investidor");
                logger.LogInformation("Investor user created.");
            }
            else
            {
                logger.LogWarning("Investor user creation failed: {Errors}",
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // Seed Investidores, Aportes, Veiculos, Alocacoes e Fechamentos
        if (!db.Investidores.Any())
        {
            var joaoId = InvestidorId.New();
            var mariaId = InvestidorId.New();
            var carlosId = InvestidorId.New();

            var joao = new Investidor
            {
                Id = joaoId,
                NomeCompleto = "João Silva",
                CpfCnpj = "111.111.111-11",
                Email = "investidor@autoimovel.com",
                Telefone = "(11) 99999-1111",
                Status = StatusInvestidor.Aprovado,
                DataCadastro = DateTime.UtcNow.AddDays(-90),
                ChavePix = "joao@email.com",
                Banco = "Banco Inter",
                Agencia = "0001",
                Conta = "12345-6",
                TipoConta = "Corrente"
            };

            var maria = new Investidor
            {
                Id = mariaId,
                NomeCompleto = "Maria Santos",
                CpfCnpj = "222.222.222-22",
                Email = "maria@email.com",
                Telefone = "(11) 98888-2222",
                Status = StatusInvestidor.Aprovado,
                DataCadastro = DateTime.UtcNow.AddDays(-60),
                ChavePix = "maria@email.com",
                Banco = "Nubank",
                Agencia = "0001",
                Conta = "67890-1",
                TipoConta = "Corrente"
            };

            var carlos = new Investidor
            {
                Id = carlosId,
                NomeCompleto = "Carlos Pereira",
                CpfCnpj = "333.333.333-33",
                Email = "carlos@email.com",
                Telefone = "(11) 97777-3333",
                Status = StatusInvestidor.Pendente,
                DataCadastro = DateTime.UtcNow.AddDays(-30),
                ChavePix = "carlos@email.com",
                Banco = "Itaú",
                Agencia = "1234",
                Conta = "54321-0",
                TipoConta = "Poupança"
            };

            db.Investidores.AddRange(joao, maria, carlos);
            await db.SaveChangesAsync();

            // Aportes
            var aporte1Id = AporteId.New();
            var aporte1 = new Aporte
            {
                Id = aporte1Id,
                InvestidorId = joaoId,
                ValorOriginal = 200_000m,
                SaldoEmLiquidez = 120_000m,
                Status = StatusAporte.EmLiquidez,
                DataAporte = DateTime.UtcNow.AddDays(-80),
                UrlContratoScp = "https://drive.google.com/contrato-joao-1"
            };

            var aporte2Id = AporteId.New();
            var aporte2 = new Aporte
            {
                Id = aporte2Id,
                InvestidorId = carlosId,
                ValorOriginal = 150_000m,
                SaldoEmLiquidez = 150_000m,
                Status = StatusAporte.EmLiquidez,
                DataAporte = DateTime.UtcNow.AddDays(-50),
                UrlContratoScp = "https://drive.google.com/contrato-carlos-1"
            };

            var aporte3Id = AporteId.New();
            var aporte3 = new Aporte
            {
                Id = aporte3Id,
                InvestidorId = mariaId,
                ValorOriginal = 80_000m,
                SaldoEmLiquidez = 80_000m,
                Status = StatusAporte.EmLiquidez,
                DataAporte = DateTime.UtcNow.AddDays(-40),
                UrlContratoScp = "https://drive.google.com/contrato-maria-1"
            };

            var aporte4Id = AporteId.New();
            var aporte4 = new Aporte
            {
                Id = aporte4Id,
                InvestidorId = joaoId,
                ValorOriginal = 100_000m,
                SaldoEmLiquidez = 0,
                Status = StatusAporte.Alocado,
                DataAporte = DateTime.UtcNow.AddDays(-70),
                UrlContratoScp = "https://drive.google.com/contrato-joao-2"
            };

            db.Aportes.AddRange(aporte1, aporte2, aporte3, aporte4);
            await db.SaveChangesAsync();

            // Veiculos
            var hb20Id = VeiculoId.New();
            var corollaId = VeiculoId.New();
            var onixId = VeiculoId.New();

            var hb20 = new Veiculo
            {
                Id = hb20Id,
                Placa = "HBP2025",
                MarcaModelo = "Hyundai HB20",
                AnoFabricacaoModelo = "2022 / 2022 / SENSE",
                Km = 38000,
                PrecoCompra = 60_000m,
                CustosPreparacao = 3_000m,
                Chassi = "9BHBG51D0BP012345",
                Renavam = "12345678901",
                Cor = "Prata",
                Status = StatusVeiculo.Disponivel,
                UrlLaudoCautelar = "https://drive.google.com/laudo-hb20",
                DataCompra = DateTime.UtcNow.AddDays(-45)
            };

            var corolla = new Veiculo
            {
                Id = corollaId,
                Placa = "COR7894",
                MarcaModelo = "Toyota Corolla",
                AnoFabricacaoModelo = "2023 / 2024 / ALTIS",
                Km = 15000,
                PrecoCompra = 95_000m,
                CustosPreparacao = 5_000m,
                Chassi = "9BRBU42E0CX678901",
                Renavam = "23456789012",
                Cor = "Preto",
                Status = StatusVeiculo.Preparacao,
                UrlLaudoCautelar = "https://drive.google.com/laudo-corolla",
                DataCompra = DateTime.UtcNow.AddDays(-20)
            };

            var onix = new Veiculo
            {
                Id = onixId,
                Placa = "ONX3344",
                MarcaModelo = "Chevrolet Onix",
                AnoFabricacaoModelo = "2021 / 2021 / LT",
                Km = 52000,
                PrecoCompra = 55_000m,
                CustosPreparacao = 2_500m,
                Chassi = "9BGSE48N0DZ345678",
                Renavam = "34567890123",
                Cor = "Branco",
                Status = StatusVeiculo.Vendido,
                UrlLaudoCautelar = "https://drive.google.com/laudo-onix",
                DataCompra = DateTime.UtcNow.AddDays(-90),
                DataVenda = DateTime.UtcNow.AddDays(-10),
                PrecoVendaReal = 72_000m
            };

            db.Veiculos.AddRange(hb20, corolla, onix);
            await db.SaveChangesAsync();

            // Alocacoes
            var aloc1Id = AlocacaoLastroId.New();
            var aloc1 = new AlocacaoLastro
            {
                Id = aloc1Id,
                AporteId = aporte1Id,
                VeiculoId = hb20Id,
                ValorAlocado = 50_000m,
                DataAlocacao = DateTime.UtcNow.AddDays(-40),
                Status = StatusAlocacao.Ativo
            };

            var aloc2Id = AlocacaoLastroId.New();
            var aloc2 = new AlocacaoLastro
            {
                Id = aloc2Id,
                AporteId = aporte4Id,
                VeiculoId = onixId,
                ValorAlocado = 50_000m,
                DataAlocacao = DateTime.UtcNow.AddDays(-80),
                DataDesalocacao = DateTime.UtcNow.AddDays(-10),
                Status = StatusAlocacao.Liquidado
            };

            var aloc3Id = AlocacaoLastroId.New();
            var aloc3 = new AlocacaoLastro
            {
                Id = aloc3Id,
                AporteId = aporte2Id,
                VeiculoId = onixId,
                ValorAlocado = 30_000m,
                DataAlocacao = DateTime.UtcNow.AddDays(-40),
                DataDesalocacao = DateTime.UtcNow.AddDays(-10),
                Status = StatusAlocacao.Liquidado
            };

            // Vincula o restante do aporte4 (R$ 50k) ao HB20
            var aloc4Id = AlocacaoLastroId.New();
            var aloc4 = new AlocacaoLastro
            {
                Id = aloc4Id,
                AporteId = aporte4Id,
                VeiculoId = hb20Id,
                ValorAlocado = 30_000m,
                DataAlocacao = DateTime.UtcNow.AddDays(-35),
                Status = StatusAlocacao.Ativo
            };

            db.AlocacoesLastro.AddRange(aloc1, aloc2, aloc3, aloc4);
            await db.SaveChangesAsync();

            // Fechamento do Onix: PrecoVenda 72.000 - (55.000 + 2.500) = 14.500 lucro bruto
            // Total alocado no Onix: 80.000 (50k João + 30k Carlos)
            // João: 50/80 = 62.5% → lucro bruto prop = 9.062,50
            //   - taxa gestao = 181,25
            //   - taxa infra = 543,75
            //   - lucro base = 8.337,50
            //   - CDI periodo (80 dias) = 50.000 * (0,1475 / 365 * 80) = 1.616,44
            //   - excesso = 6.721,06 → performance = 1.344,21
            //   - liquido = 6.993,29
            // Carlos: 30/80 = 37.5% → lucro bruto prop = 5.437,50
            //   - taxa gestao = 108,75
            //   - taxa infra = 326,25
            //   - lucro base = 5.002,50
            //   - CDI periodo (40 dias) = 30.000 * (0,1475 / 365 * 40) = 484,93
            //   - excesso = 4.517,57 → performance = 903,51
            //   - liquido = 4.098,99
            db.FechamentosLucro.AddRange(
                new FechamentoLucro
                {
                    Id = FechamentoLucroId.New(),
                    InvestidorId = joaoId,
                    AlocacaoId = aloc2Id,
                    LucroBrutoProporcional = 9_062.50m,
                    TaxaGestaoRetida = 181.25m,
                    TaxaInfraRetida = 543.75m,
                    TaxaPerformanceRetida = 1_344.21m,
                    LucroLiquidoPago = 6_993.29m,
                    DataFechamento = DateTime.UtcNow.AddDays(-10)
                },
                new FechamentoLucro
                {
                    Id = FechamentoLucroId.New(),
                    InvestidorId = carlosId,
                    AlocacaoId = aloc3Id,
                    LucroBrutoProporcional = 5_437.50m,
                    TaxaGestaoRetida = 108.75m,
                    TaxaInfraRetida = 326.25m,
                    TaxaPerformanceRetida = 903.51m,
                    LucroLiquidoPago = 4_098.99m,
                    DataFechamento = DateTime.UtcNow.AddDays(-10)
                });

            await db.SaveChangesAsync();

            logger.LogInformation("Seed data expanded: 3 investidores, 4 aportes, 3 veiculos, 4 alocacoes, 2 fechamentos.");
        }
    }
}
