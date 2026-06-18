using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoImovel.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Investidores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeCompleto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CpfCnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investidores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Placa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MarcaModelo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AnoFabricacaoModelo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Km = table.Column<int>(type: "integer", nullable: false),
                    PrecoCompra = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CustosPreparacao = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    UrlLaudoCautelar = table.Column<string>(type: "text", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PrecoVendaReal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestidorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorOriginal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaldoEmLiquidez = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataAporte = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UrlContratoScp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aportes_Investidores_InvestidorId",
                        column: x => x.InvestidorId,
                        principalTable: "Investidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlocacoesLastro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AporteId = table.Column<Guid>(type: "uuid", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorAlocado = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DataAlocacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataDesalocacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlocacoesLastro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlocacoesLastro_Aportes_AporteId",
                        column: x => x.AporteId,
                        principalTable: "Aportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlocacoesLastro_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FechamentosLucro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestidorId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlocacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    LucroBrutoProporcional = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxaGestaoRetida = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxaInfraRetida = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxaPerformanceRetida = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LucroLiquidoPago = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechamentosLucro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FechamentosLucro_AlocacoesLastro_AlocacaoId",
                        column: x => x.AlocacaoId,
                        principalTable: "AlocacoesLastro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FechamentosLucro_Investidores_InvestidorId",
                        column: x => x.InvestidorId,
                        principalTable: "Investidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlocacoesLastro_AporteId",
                table: "AlocacoesLastro",
                column: "AporteId");

            migrationBuilder.CreateIndex(
                name: "IX_AlocacoesLastro_VeiculoId",
                table: "AlocacoesLastro",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aportes_InvestidorId",
                table: "Aportes",
                column: "InvestidorId");

            migrationBuilder.CreateIndex(
                name: "IX_FechamentosLucro_AlocacaoId",
                table: "FechamentosLucro",
                column: "AlocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FechamentosLucro_InvestidorId",
                table: "FechamentosLucro",
                column: "InvestidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Investidores_CpfCnpj",
                table: "Investidores",
                column: "CpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investidores_Email",
                table: "Investidores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_Placa",
                table: "Veiculos",
                column: "Placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FechamentosLucro");

            migrationBuilder.DropTable(
                name: "AlocacoesLastro");

            migrationBuilder.DropTable(
                name: "Aportes");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Investidores");
        }
    }
}
