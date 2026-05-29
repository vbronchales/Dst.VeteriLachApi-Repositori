using System.Net.Http.Json;
using System.Text.Json;
using VeteriLach.McpServer.Models;

namespace VeteriLach.McpServer.Services;

public class VeteriLachApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public VeteriLachApiClient(string baseUrl, string apiKey, int timeoutSeconds = 30)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl),
            Timeout = TimeSpan.FromSeconds(timeoutSeconds)
        };
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<Dictionary<string, object>> HealthCheckAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Dictionary<string, object>>("/api/health", _jsonOptions);
        return response ?? new Dictionary<string, object>();
    }

    public async Task<PaginatedResponse<SaleDto>> GetSalesAsync(
        int? pageNumber = null,
        int? pageSize = null,
        string? startDate = null,
        string? endDate = null,
        string? customerId = null,
        string? sellerId = null,
        string? animalId = null,
        bool? onlyUnpaid = null)
    {
        var queryParams = new List<string>();
        if (pageNumber.HasValue) queryParams.Add($"pageNumber={pageNumber}");
        if (pageSize.HasValue) queryParams.Add($"pageSize={pageSize}");
        if (!string.IsNullOrEmpty(startDate)) queryParams.Add($"startDate={startDate}");
        if (!string.IsNullOrEmpty(endDate)) queryParams.Add($"endDate={endDate}");
        if (!string.IsNullOrEmpty(customerId)) queryParams.Add($"customerId={customerId}");
        if (!string.IsNullOrEmpty(sellerId)) queryParams.Add($"sellerId={sellerId}");
        if (!string.IsNullOrEmpty(animalId)) queryParams.Add($"animalId={animalId}");
        if (onlyUnpaid.HasValue) queryParams.Add($"onlyUnpaid={onlyUnpaid.Value.ToString().ToLower()}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<SaleDto>>($"/api/sales{query}", _jsonOptions);
        return response ?? new PaginatedResponse<SaleDto>();
    }

    public async Task<SaleDetailDto?> GetSaleByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<SaleDetailDto>($"/api/sales/{id}", _jsonOptions);
    }

    public async Task<PaginatedResponse<SaleDto>> GetSalesByCustomerAsync(
        string customerId,
        int? pageNumber = null,
        int? pageSize = null,
        string? startDate = null,
        string? endDate = null)
    {
        var queryParams = new List<string>();
        if (pageNumber.HasValue) queryParams.Add($"pageNumber={pageNumber}");
        if (pageSize.HasValue) queryParams.Add($"pageSize={pageSize}");
        if (!string.IsNullOrEmpty(startDate)) queryParams.Add($"startDate={startDate}");
        if (!string.IsNullOrEmpty(endDate)) queryParams.Add($"endDate={endDate}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<SaleDto>>($"/api/sales/customer/{customerId}{query}", _jsonOptions);
        return response ?? new PaginatedResponse<SaleDto>();
    }

    public async Task<PaginatedResponse<DebtDto>> GetDebtsAsync(
        int? pageNumber = null,
        int? pageSize = null,
        string? customerId = null,
        int? minimumDays = null,
        decimal? minimumAmount = null)
    {
        var queryParams = new List<string>();
        if (pageNumber.HasValue) queryParams.Add($"pageNumber={pageNumber}");
        if (pageSize.HasValue) queryParams.Add($"pageSize={pageSize}");
        if (!string.IsNullOrEmpty(customerId)) queryParams.Add($"customerId={customerId}");
        if (minimumDays.HasValue) queryParams.Add($"minimumDays={minimumDays}");
        if (minimumAmount.HasValue) queryParams.Add($"minimumAmount={minimumAmount}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<DebtDto>>($"/api/sales/debts{query}", _jsonOptions);
        return response ?? new PaginatedResponse<DebtDto>();
    }

    public async Task<PaginatedResponse<PaymentAdvanceDto>> GetPaymentAdvancesAsync(
        int? pageNumber = null,
        int? pageSize = null,
        string? customerId = null,
        string? startDate = null,
        string? endDate = null)
    {
        var queryParams = new List<string>();
        if (pageNumber.HasValue) queryParams.Add($"pageNumber={pageNumber}");
        if (pageSize.HasValue) queryParams.Add($"pageSize={pageSize}");
        if (!string.IsNullOrEmpty(customerId)) queryParams.Add($"customerId={customerId}");
        if (!string.IsNullOrEmpty(startDate)) queryParams.Add($"startDate={startDate}");
        if (!string.IsNullOrEmpty(endDate)) queryParams.Add($"endDate={endDate}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<PaymentAdvanceDto>>($"/api/sales/advances{query}", _jsonOptions);
        return response ?? new PaginatedResponse<PaymentAdvanceDto>();
    }
}
