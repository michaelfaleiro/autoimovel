using System.Net.Http.Headers;
using System.Net.Http.Json;
using AutoImovel.Shared.Domain.Enums;
using Blazored.LocalStorage;

namespace AutoImovel.Dashboard.Services;

public class ApiClient
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _storage;

    public ApiClient(HttpClient http, ILocalStorageService storage)
    {
        _http = http;
        _storage = storage;
    }

    private async Task AttachToken()
    {
        var token = await _storage.GetItemAsync<string>("auth_token");
        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public void SetToken(string token)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/auth/login", new { email, password });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LoginResponse>() ?? throw new Exception("Login failed");
    }

    public async Task<PatrimonioResponse> GetPatrimonioAsync(Guid investidorId)
    {
        await AttachToken();
        return await _http.GetFromJsonAsync<PatrimonioResponse>($"/api/dashboard/patrimonio/{investidorId}")
            ?? throw new Exception("Erro ao consultar patrimônio.");
    }

    public async Task<RastreabilidadeLastroResponse> GetRastreabilidadeLastroAsync(Guid investidorId)
    {
        await AttachToken();
        return await _http.GetFromJsonAsync<RastreabilidadeLastroResponse>($"/api/dashboard/rastreabilidade/{investidorId}")
            ?? throw new Exception("Erro ao consultar rastreabilidade.");
    }

    public async Task<ExtratoRendimentosResponse> GetExtratoRendimentosAsync(Guid investidorId)
    {
        await AttachToken();
        return await _http.GetFromJsonAsync<ExtratoRendimentosResponse>($"/api/dashboard/extrato/{investidorId}")
            ?? throw new Exception("Erro ao consultar extrato.");
    }

    public async Task<DashboardData> GetDashboardDataAsync(Guid investidorId)
    {
        await AttachToken();
        var patrimonio = await GetPatrimonioAsync(investidorId);
        var lastro = await GetRastreabilidadeLastroAsync(investidorId);
        var extrato = await GetExtratoRendimentosAsync(investidorId);

        return new DashboardData
        {
            Patrimonio = patrimonio,
            Veiculos = lastro.Veiculos,
            Rendimentos = extrato.Rendimentos,
            TotalLucroLiquido = extrato.TotalLucroLiquido
        };
    }

    public async Task<List<DocumentoItem>> GetDocumentosAsync(Guid? veiculoId = null, Guid? investidorId = null)
    {
        await AttachToken();
        var query = new List<string>();
        if (veiculoId.HasValue) query.Add($"veiculoId={veiculoId.Value}");
        if (investidorId.HasValue) query.Add($"investidorId={investidorId.Value}");

        var queryString = query.Any() ? "?" + string.Join("&", query) : "";
        return await _http.GetFromJsonAsync<List<DocumentoItem>>($"/api/documentos{queryString}")
            ?? new();
    }
}

public record LoginResponse(string Token, string Email, string Role, Guid? InvestidorId);

public class DashboardData
{
    public PatrimonioResponse Patrimonio { get; set; } = null!;
    public List<VeiculoLastroItemResponse> Veiculos { get; set; } = new();
    public List<RendimentoItemResponse> Rendimentos { get; set; } = new();
    public decimal TotalLucroLiquido { get; set; }
}

public record PatrimonioResponse(Guid InvestidorId, string NomeInvestidor, decimal CapitalEmLiquidez, decimal CapitalAlocado, decimal PatrimonioTotal);

public record RastreabilidadeLastroResponse(List<VeiculoLastroItemResponse> Veiculos);
public record VeiculoLastroItemResponse(Guid VeiculoId, string Placa, string MarcaModelo, string AnoFabricacaoModelo, int Km, decimal ValorAlocado, StatusVeiculo StatusVeiculo, string UrlLaudoCautelar, DateTime DataAlocacao);

public record ExtratoRendimentosResponse(List<RendimentoItemResponse> Rendimentos, decimal TotalLucroLiquido);
public record RendimentoItemResponse(Guid FechamentoId, string PlacaVeiculo, string MarcaModeloVeiculo, decimal LucroBrutoProporcional, decimal TaxaGestaoRetida, decimal TaxaInfraRetida, decimal TaxaPerformanceRetida, decimal LucroLiquidoPago, DateTime DataFechamento);

public record DocumentoItem(
    Guid Id, string Nome, string Tipo, string Url,
    Guid? VeiculoId, Guid? InvestidorId, DateTime DataUpload)
{
    // Adicionando propriedades para facilitar a exibição no dashboard
    public VeiculoSimples? Veiculo { get; set; }
}

public record VeiculoSimples(string Placa, string MarcaModelo);

public record DocumentoRequest(
    string Nome, string Tipo, string Url,
    Guid? VeiculoId, Guid? InvestidorId);
