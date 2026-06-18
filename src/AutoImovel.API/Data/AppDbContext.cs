using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoImovel.API.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Investidor> Investidores => Set<Investidor>();
    public DbSet<Aporte> Aportes => Set<Aporte>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<AlocacaoLastro> AlocacoesLastro => Set<AlocacaoLastro>();
    public DbSet<FechamentoLucro> FechamentosLucro => Set<FechamentoLucro>();
    public DbSet<Documento> Documentos => Set<Documento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Investidor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasConversion(id => id.Value, value => new InvestidorId(value));
            entity.Property(e => e.NomeCompleto).HasMaxLength(200).IsRequired();
            entity.Property(e => e.CpfCnpj).HasMaxLength(14).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Telefone).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Status)
                .HasConversion(status => status.ToString(), value => Enum.Parse<StatusInvestidor>(value));
            entity.Property(e => e.ChavePix).HasMaxLength(100);
            entity.Property(e => e.Banco).HasMaxLength(50);
            entity.Property(e => e.Agencia).HasMaxLength(10);
            entity.Property(e => e.Conta).HasMaxLength(20);
            entity.Property(e => e.TipoConta).HasMaxLength(20);
            entity.HasIndex(e => e.CpfCnpj).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Aporte>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasConversion(id => id.Value, value => new AporteId(value));
            entity.Property(e => e.InvestidorId)
                .HasConversion(id => id.Value, value => new InvestidorId(value));
            entity.Property(e => e.ValorOriginal).HasPrecision(18, 2);
            entity.Property(e => e.SaldoEmLiquidez).HasPrecision(18, 2);
            entity.Property(e => e.Status)
                .HasConversion(status => status.ToString(), value => Enum.Parse<StatusAporte>(value));
            entity.HasOne(e => e.Investidor)
                .WithMany(i => i.Aportes)
                .HasForeignKey(e => e.InvestidorId);
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasConversion(id => id.Value, value => new VeiculoId(value));
            entity.Property(e => e.Placa).HasMaxLength(10).IsRequired();
            entity.Property(e => e.MarcaModelo).HasMaxLength(200).IsRequired();
            entity.Property(e => e.AnoFabricacaoModelo).HasMaxLength(20).IsRequired();
            entity.Property(e => e.PrecoCompra).HasPrecision(18, 2);
            entity.Property(e => e.CustosPreparacao).HasPrecision(18, 2);
            entity.Property(e => e.PrecoVendaReal).HasPrecision(18, 2);
            entity.Property(e => e.Chassi).HasMaxLength(17);
            entity.Property(e => e.Renavam).HasMaxLength(11);
            entity.Property(e => e.Cor).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasConversion(status => status.ToString(), value => Enum.Parse<StatusVeiculo>(value));
            entity.HasIndex(e => e.Placa).IsUnique();
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(500).IsRequired();

            entity.Property(e => e.VeiculoId)
                .HasConversion(id => id.HasValue ? id.Value.Value : (Guid?)null, value => value.HasValue ? new VeiculoId(value.Value) : (VeiculoId?)null);

            entity.Property(e => e.InvestidorId)
                .HasConversion(id => id.HasValue ? id.Value.Value : (Guid?)null, value => value.HasValue ? new InvestidorId(value.Value) : (InvestidorId?)null);

            entity.HasOne(e => e.Veiculo)
                .WithMany()
                .HasForeignKey(e => e.VeiculoId)
                .HasPrincipalKey(e => e.Id);
            entity.HasOne(e => e.Investidor)
                .WithMany()
                .HasForeignKey(e => e.InvestidorId)
                .HasPrincipalKey(e => e.Id);
        });

        modelBuilder.Entity<AlocacaoLastro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasConversion(id => id.Value, value => new AlocacaoLastroId(value));
            entity.Property(e => e.AporteId)
                .HasConversion(id => id.Value, value => new AporteId(value));
            entity.Property(e => e.VeiculoId)
                .HasConversion(id => id.Value, value => new VeiculoId(value));
            entity.Property(e => e.ValorAlocado).HasPrecision(18, 2);
            entity.Property(e => e.Status)
                .HasConversion(status => status.ToString(), value => Enum.Parse<StatusAlocacao>(value));
            entity.HasOne(e => e.Aporte)
                .WithMany(a => a.Alocacoes)
                .HasForeignKey(e => e.AporteId);
            entity.HasOne(e => e.Veiculo)
                .WithMany(v => v.Alocacoes)
                .HasForeignKey(e => e.VeiculoId);
        });

        modelBuilder.Entity<FechamentoLucro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasConversion(id => id.Value, value => new FechamentoLucroId(value));
            entity.Property(e => e.InvestidorId)
                .HasConversion(id => id.Value, value => new InvestidorId(value));
            entity.Property(e => e.AlocacaoId)
                .HasConversion(id => id.Value, value => new AlocacaoLastroId(value));
            entity.Property(e => e.LucroBrutoProporcional).HasPrecision(18, 2);
            entity.Property(e => e.TaxaGestaoRetida).HasPrecision(18, 2);
            entity.Property(e => e.TaxaInfraRetida).HasPrecision(18, 2);
            entity.Property(e => e.TaxaPerformanceRetida).HasPrecision(18, 2);
            entity.Property(e => e.LucroLiquidoPago).HasPrecision(18, 2);
            entity.HasOne(e => e.Investidor)
                .WithMany(i => i.FechamentosLucro)
                .HasForeignKey(e => e.InvestidorId);
            entity.HasOne(e => e.Alocacao)
                .WithMany()
                .HasForeignKey(e => e.AlocacaoId);
        });
    }
}
