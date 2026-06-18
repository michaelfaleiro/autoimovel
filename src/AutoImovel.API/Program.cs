using System.Text;
using System.Text.Json.Serialization.Metadata;
using AutoImovel.API.Data;
using AutoImovel.API.Features.Aportes;
using AutoImovel.API.Features.Aportes.DetalheAporte;
using AutoImovel.API.Features.Aportes.RegistrarAporte;
using AutoImovel.API.Features.Auth;
using AutoImovel.API.Features.Dashboard.AdminFinanceiro;
using AutoImovel.API.Features.Dashboard.ExtratoRendimentos;
using AutoImovel.API.Features.Dashboard.Patrimonio;
using AutoImovel.API.Features.Dashboard.RastreabilidadeLastro;
using AutoImovel.API.Features.Fechamentos.FecharVenda;
using AutoImovel.API.Features.Investidores;
using AutoImovel.API.Features.Investidores.AprovarInvestidor;
using AutoImovel.API.Features.Investidores.CadastrarInvestidor;
using AutoImovel.API.Features.Investidores.DetalheInvestidor;
using AutoImovel.API.Features.Veiculos;
using AutoImovel.API.Features.Veiculos.CadastrarVeiculo;
using AutoImovel.API.Features.Veiculos.DetalheVeiculo;
using AutoImovel.API.Features.Documentos;
using AutoImovel.API.Features.Selects;
using AutoImovel.API.Features.VincularLastro;
using AutoImovel.API.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddCors();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "AutoImovel",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "AutoImovel",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "SuperSecretKeyForAutoImovel@2026!ChangeMe"))
        };
    });

builder.Services.AddAuthorization();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolver = JsonTypeInfoResolver.Combine(
        AppJsonContext.Default,
        new DefaultJsonTypeInfoResolver());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "AutoImovel API is running.");

app.MapLogin();
app.MapAprovarInvestidor();
app.MapRegistrarAporte();
app.MapCadastrarVeiculo();
app.MapVincularLastro();
app.MapFecharVenda();
app.MapPatrimonio();
app.MapRastreabilidadeLastro();
app.MapExtratoRendimentos();
app.MapAdminFinanceiro();
app.MapListarInvestidores();
app.MapDetalheInvestidor();
app.MapCadastrarInvestidor();
app.MapListarAportes();
app.MapDetalheAporte();
app.MapListarVeiculos();
app.MapDetalheVeiculo();
app.MapAtualizarStatusVeiculo();
app.MapDocumentos();
app.MapSelectEndpoints();

using (var scope = app.Services.CreateScope())
{
    await SeedData.InitializeAsync(app.Services);
}

app.Run();
