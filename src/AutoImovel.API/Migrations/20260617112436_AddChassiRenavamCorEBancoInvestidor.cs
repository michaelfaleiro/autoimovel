using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoImovel.API.Migrations
{
    /// <inheritdoc />
    public partial class AddChassiRenavamCorEBancoInvestidor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chassi",
                table: "Veiculos",
                type: "character varying(17)",
                maxLength: 17,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Veiculos",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Renavam",
                table: "Veiculos",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Agencia",
                table: "Investidores",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Banco",
                table: "Investidores",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChavePix",
                table: "Investidores",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Conta",
                table: "Investidores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoConta",
                table: "Investidores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chassi",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Renavam",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Agencia",
                table: "Investidores");

            migrationBuilder.DropColumn(
                name: "Banco",
                table: "Investidores");

            migrationBuilder.DropColumn(
                name: "ChavePix",
                table: "Investidores");

            migrationBuilder.DropColumn(
                name: "Conta",
                table: "Investidores");

            migrationBuilder.DropColumn(
                name: "TipoConta",
                table: "Investidores");
        }
    }
}
