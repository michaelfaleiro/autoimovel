using System.Net.Http.Headers;
using System.Net.Http.Json;
using AutoImovel.Shared.Domain;
using AutoImovel.Shared.Domain.Enums;
using Blazored.LocalStorage;

namespace AutoImovel.Admin.Services;

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
        var token = await _storage.GetItemAsync<string>("autoimovel-admin-token");
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

    public async Task<Investidor> AprovarInvestidorAsync(Guid investidorId)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/investidores/aprovar", new { InvestidorId = investidorId });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Investidor>() ?? throw new Exception("Erro ao aprovar investidor.");
    }

    public async Task<RegistrarAporteResponse> RegistrarAporteAsync(RegistrarAporteRequest request)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/aportes", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RegistrarAporteResponse>() ?? throw new Exception("Erro ao registrar aporte.");
    }

    public async Task<CadastrarVeiculoResponse> CadastrarVeiculoAsync(CadastrarVeiculoRequest request)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/veiculos", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CadastrarVeiculoResponse>() ?? throw new Exception("Erro ao cadastrar veículo.");
    }

    public async Task<VincularLastroResponse> VincularLastroAsync(VincularLastroRequest request)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/vincular-lastro", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<VincularLastroResponse>() ?? throw new Exception("Erro ao vincular lastro.");
    }

    public async Task<FecharVendaResponse> FecharVendaAsync(FecharVendaRequest request)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/fechamentos/fechar-venda", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<FecharVendaResponse>() ?? throw new Exception("Erro ao fechar venda.");
    }

    public async Task<PagedResult<InvestidorItem>> GetInvestidoresAsync(int page = 1, int size = 20, string? status = null, string? search = null)
    {
        await AttachToken();
        var query = $"/api/investidores?page={page}&size={size}";
        if (!string.IsNullOrEmpty(status)) query += $"&status={status}";
        if (!string.IsNullOrEmpty(search)) query += $"&search={Uri.EscapeDataString(search)}";
        return await _http.GetFromJsonAsync<PagedResult<InvestidorItem>>(query) ?? new();
    }

    public async Task<PagedResult<AporteItem>> GetAportesAsync(int page = 1, int size = 20, string? investidorId = null, string? status = null)
    {
        await AttachToken();
        var query = $"/api/aportes?page={page}&size={size}";
        if (!string.IsNullOrEmpty(investidorId)) query += $"&investidorId={investidorId}";
        if (!string.IsNullOrEmpty(status)) query += $"&status={status}";
        return await _http.GetFromJsonAsync<PagedResult<AporteItem>>(query) ?? new();
    }

    public async Task<PagedResult<VeiculoItem>> GetVeiculosAsync(int page = 1, int size = 20, string? status = null, string? search = null)
    {
        await AttachToken();
        var query = $"/api/veiculos?page={page}&size={size}";
        if (!string.IsNullOrEmpty(status)) query += $"&status={status}";
        if (!string.IsNullOrEmpty(search)) query += $"&search={Uri.EscapeDataString(search)}";
        return await _http.GetFromJsonAsync<PagedResult<VeiculoItem>>(query) ?? new();
    }

    public async Task<List<SelectInvestidor>> GetInvestidoresSelectAsync(string? status = null)
    {
        await AttachToken();
        var query = "/api/select/investidores";
        if (!string.IsNullOrEmpty(status)) query += $"?status={status}";
        return await _http.GetFromJsonAsync<List<SelectInvestidor>>(query) ?? new();
    }

    public async Task<List<SelectAporte>> GetAportesSelectAsync(Guid? investidorId = null, string? status = null)
    {
        await AttachToken();
        var query = "/api/select/aportes?";
        if (investidorId.HasValue) query += $"investidorId={investidorId.Value}&";
        if (!string.IsNullOrEmpty(status)) query += $"status={status}";
        return await _http.GetFromJsonAsync<List<SelectAporte>>(query) ?? new();
    }

    public async Task<CadastrarInvestidorResponse> CadastrarInvestidorAsync(CadastrarInvestidorRequest request)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/investidores", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CadastrarInvestidorResponse>() ?? throw new Exception("Erro ao cadastrar investidor.");
    }

    public async Task<List<SelectVeiculo>> GetVeiculosSelectAsync(string? status = null)
    {
        await AttachToken();
        var query = "/api/select/veiculos";
        if (!string.IsNullOrEmpty(status)) query += $"?status={status}";
        return await _http.GetFromJsonAsync<List<SelectVeiculo>>(query) ?? new();
    }

    public async Task AtualizarStatusVeiculoAsync(Guid id, StatusVeiculo status)
    {
        await AttachToken();
        var response = await _http.PatchAsJsonAsync($"/api/veiculos/{id}/status", new { status = status.ToString() });
        response.EnsureSuccessStatusCode();
    }

    public async Task<VeiculoDetalheResponse> GetVeiculoDetalheAsync(Guid id)
    {
        await AttachToken();
        return await _http.GetFromJsonAsync<VeiculoDetalheResponse>($"/api/veiculos/{id}")
            ?? throw new Exception("Veículo não encontrado.");
    }

    public async Task<AporteDetalheResponse> GetAporteDetalheAsync(Guid id)
    {
        await AttachToken();
        return await _http.GetFromJsonAsync<AporteDetalheResponse>($"/api/aportes/{id}")
            ?? throw new Exception("Aporte não encontrado.");
    }

    public async Task<InvestidorDetalheResponse> GetInvestidorDetalheAsync(Guid id)
    {
        await AttachToken();
        return await _http.GetFromJsonAsync<InvestidorDetalheResponse>($"/api/investidores/{id}")
            ?? throw new Exception("Investidor não encontrado.");
    }

    public async Task<AdminFinanceiroResponse> GetAdminFinanceiroAsync()
    {
        await AttachToken();
        return await _http.GetFromJsonAsync<AdminFinanceiroResponse>("/api/dashboard/admin/financeiro")
            ?? new AdminFinanceiroResponse(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new());
    }

    public async Task<List<DocumentoItem>> GetDocumentosAsync(VeiculoId? veiculoId = null, InvestidorId? investidorId = null)
    {
        await AttachToken();
        var query = new List<string>();
        if (veiculoId.HasValue) query.Add($"veiculoId={veiculoId.Value}");
        if (investidorId.HasValue) query.Add($"investidorId={investidorId.Value}");

        var queryString = query.Any() ? "?" + string.Join("&", query) : "";
        return await _http.GetFromJsonAsync<List<DocumentoItem>>($"/api/documentos{queryString}")
            ?? new();
    }

    public async Task<DocumentoItem> CreateDocumentoAsync(DocumentoRequest request)
    {
        await AttachToken();
        var response = await _http.PostAsJsonAsync("/api/documentos", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DocumentoItem>()
            ?? throw new Exception("Erro ao criar documento.");
    }

    public async Task DeleteDocumentoAsync(Guid id)
    {
        await AttachToken();
        var response = await _http.DeleteAsync($"/api/documentos/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao remover documento.");
    }
}

public record LoginResponse(string Token, string Email, string Role, Guid? InvestidorId);

public record PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
}

public record InvestidorItem(Guid Id, string NomeCompleto, string CpfCnpj, string Email, StatusInvestidor Status, DateTime DataCadastro, string ChavePix, string Banco, string Agencia, string Conta, string TipoConta);

public record AporteItem(Guid Id, Guid InvestidorId, string InvestidorNome, decimal ValorOriginal, decimal SaldoEmLiquidez, StatusAporte Status, DateTime DataAporte);

public record VeiculoItem(Guid Id, string Placa, string MarcaModelo, string AnoFabricacaoModelo, int Km, decimal PrecoCompra, decimal CustosPreparacao, string Chassi, string Renavam, string Cor, StatusVeiculo Status, DateTime DataCompra, DateTime? DataVenda, decimal? PrecoVendaReal);

public record RegistrarAporteRequest(Guid InvestidorId, decimal Valor, string UrlContratoScp);
public record RegistrarAporteResponse(Guid Id, Guid InvestidorId, decimal ValorOriginal, decimal SaldoEmLiquidez, StatusAporte Status, DateTime DataAporte);

public record CadastrarVeiculoRequest(string Placa, string MarcaModelo, string AnoFabricacaoModelo, int Km, decimal PrecoCompra, decimal CustosPreparacao, string Chassi, string Renavam, string Cor, string UrlLaudoCautelar);
public record CadastrarVeiculoResponse(Guid Id, string Placa, string MarcaModelo, StatusVeiculo Status, DateTime DataCompra);

public record VincularLastroRequest(Guid InvestidorId, Guid AporteId, Guid VeiculoId, decimal ValorAlocado);
public record VincularLastroResponse(Guid Id, Guid AporteId, Guid VeiculoId, decimal ValorAlocado, StatusAlocacao Status, DateTime DataAlocacao);

public record FecharVendaRequest(Guid VeiculoId, decimal PrecoVendaReal);
public record FecharVendaResponse(Guid VeiculoId, decimal PrecoVendaReal, decimal LucroBruto, decimal CustoTotalVeiculo, List<FechamentoLucroItemResponse> Fechamentos);
public record FechamentoLucroItemResponse(Guid InvestidorId, string NomeInvestidor, decimal ValorAlocado, decimal Proporcao, decimal LucroBrutoProporcional, decimal TaxaGestaoRetida, decimal TaxaInfraRetida, decimal TaxaPerformanceRetida, decimal LucroLiquidoPago);

public record Investidor(Guid Id, string NomeCompleto, string CpfCnpj, string Email, string Telefone, StatusInvestidor Status, DateTime DataCadastro);

public record CadastrarInvestidorRequest(string NomeCompleto, string CpfCnpj, string Email, string Telefone, string ChavePix, string Banco, string Agencia, string Conta, string TipoConta);
public record CadastrarInvestidorResponse(Guid Id, string NomeCompleto, string CpfCnpj, string Email, StatusInvestidor Status);

public record SelectInvestidor(Guid Id, string NomeCompleto, string CpfCnpj);
public record SelectAporte(Guid Id, decimal SaldoEmLiquidez);
public record SelectVeiculo(Guid Id, string Placa, string MarcaModelo, decimal PrecoCompra);

public record VeiculoDetalheResponse(
    Guid Id, string Placa, string MarcaModelo, string AnoFabricacaoModelo,
    int Km, decimal PrecoCompra, decimal CustosPreparacao,
    string Chassi, string Renavam, string Cor,
    StatusVeiculo Status, string UrlLaudoCautelar,
    DateTime DataCompra, DateTime? DataVenda, decimal? PrecoVendaReal,
    List<AlocacaoVeiculoItem> Alocacoes,
    FechamentoVeiculoItem? Fechamento
);

public record AlocacaoVeiculoItem(
    Guid AlocacaoId, Guid InvestidorId, string NomeInvestidor,
    decimal ValorAlocado, decimal Porcentagem, StatusAlocacao Status, DateTime DataAlocacao
);

public record FechamentoVeiculoItem(
    decimal LucroBruto, decimal TaxaGestao, decimal TaxaInfra,
    decimal TaxaPerformance, decimal LucroLiquido
);

public record AporteDetalheResponse(
    Guid Id, Guid InvestidorId, string NomeInvestidor,
    decimal ValorOriginal, decimal SaldoEmLiquidez, decimal TotalAlocado,
    StatusAporte Status, DateTime DataAporte, string UrlContratoScp,
    List<AlocacaoAporteItem> Alocacoes
);

public record AlocacaoAporteItem(
    Guid AlocacaoId, Guid VeiculoId, string PlacaVeiculo, string MarcaModelo,
    decimal ValorAlocado, StatusAlocacao Status, DateTime DataAlocacao
);

public record InvestidorDetalheResponse(
    Guid Id, string NomeCompleto, string CpfCnpj, string Email, string Telefone,
    StatusInvestidor Status, DateTime DataCadastro,
    string ChavePix, string Banco, string Agencia, string Conta, string TipoConta,
    PortfolioSummary Portfolio,
    List<AporteItemResponse> Aportes,
    List<AlocacaoInvestidorItem> AlocacoesAtivas,
    List<RendimentoRecebidoItem> Rendimentos
);

public record PortfolioSummary(
    decimal TotalInvestido, decimal TotalEmLiquidez, decimal TotalAlocado,
    decimal TotalRendimentos
);

public record AporteItemResponse(
    Guid Id, decimal ValorOriginal, decimal SaldoEmLiquidez,
    StatusAporte Status, DateTime DataAporte
);

public record AlocacaoInvestidorItem(
    Guid AlocacaoId, Guid VeiculoId, string PlacaVeiculo, string MarcaModelo,
    decimal ValorAlocado, decimal PorcentagemDoAporte,
    StatusAlocacao Status, DateTime DataAlocacao
);

public record RendimentoRecebidoItem(
    Guid FechamentoId, string PlacaVeiculo, string MarcaModelo,
    decimal LucroBrutoProporcional, decimal TaxaGestaoRetida, decimal TaxaInfraRetida,
    decimal TaxaPerformanceRetida, decimal LucroLiquidoPago, DateTime DataFechamento
);

public record AdminFinanceiroResponse(
    decimal TotalInvestido, decimal TotalEmLiquidez, decimal TotalAlocado,
    int QuantidadeInvestidores,
    int QuantidadeVeiculosTotal, int QuantidadeVeiculosDisponiveis,
    int QuantidadeVeiculosAlocados, int QuantidadeVeiculosVendidos,
    decimal ValorTotalVendas, decimal TotalRendimentosDistribuidos,
    List<AtividadeRecenteItem> AtividadesRecentes
);

public record AtividadeRecenteItem(
    string Tipo, string Descricao, decimal Valor, DateTime Data
);

public record DocumentoItem(
    Guid Id, string Nome, string Tipo, string Url,
    VeiculoId? VeiculoId, InvestidorId? InvestidorId, DateTime DataUpload);

public record DocumentoRequest(
    string Nome, string Tipo, string Url,
    VeiculoId? VeiculoId, InvestidorId? InvestidorId);
