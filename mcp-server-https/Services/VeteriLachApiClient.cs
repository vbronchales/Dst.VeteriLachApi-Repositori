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

    // ===== Propietaris (Clients) =====

    public async Task<PaginatedResponse<PropietariListDto>> GetPropietarisAsync(
        int? pageNumber = null,
        int? pageSize = null,
        string? searchTerm = null,
        string? poblacio = null,
        string? codiPostal = null,
        bool? nomes_actius = null)
    {
        var queryParams = new List<string>();
        if (pageNumber.HasValue) queryParams.Add($"pageNumber={pageNumber}");
        if (pageSize.HasValue) queryParams.Add($"pageSize={pageSize}");
        if (!string.IsNullOrEmpty(searchTerm)) queryParams.Add($"searchTerm={searchTerm}");
        if (!string.IsNullOrEmpty(poblacio)) queryParams.Add($"poblacio={poblacio}");
        if (!string.IsNullOrEmpty(codiPostal)) queryParams.Add($"codiPostal={codiPostal}");
        if (nomes_actius.HasValue) queryParams.Add($"nomes_actius={nomes_actius.Value.ToString().ToLower()}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<PropietariListDto>>($"/api/propietaris{query}", _jsonOptions);
        return response ?? new PaginatedResponse<PropietariListDto>();
    }

    public async Task<PropietariDetailDto?> GetPropietariByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<PropietariDetailDto>($"/api/propietaris/{id}", _jsonOptions);
    }

    // ===== Animals (Mascotes) =====

    public async Task<PaginatedResponse<AnimalListDto>> GetAnimalsAsync(
        int? pageNumber = null,
        int? pageSize = null,
        string? searchTerm = null,
        string? idPropietari = null,
        string? idEspecie = null)
    {
        var queryParams = new List<string>();
        if (pageNumber.HasValue) queryParams.Add($"pageNumber={pageNumber}");
        if (pageSize.HasValue) queryParams.Add($"pageSize={pageSize}");
        if (!string.IsNullOrEmpty(searchTerm)) queryParams.Add($"searchTerm={searchTerm}");
        if (!string.IsNullOrEmpty(idPropietari)) queryParams.Add($"idPropietari={idPropietari}");
        if (!string.IsNullOrEmpty(idEspecie)) queryParams.Add($"idEspecie={idEspecie}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<AnimalListDto>>($"/api/animals{query}", _jsonOptions);
        return response ?? new PaginatedResponse<AnimalListDto>();
    }

    public async Task<AnimalDetailDto?> GetAnimalByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<Dictionary<string, object>>($"/api/animals/{id}", _jsonOptions);
            if (response != null && response.ContainsKey("data"))
            {
                var dataJson = JsonSerializer.Serialize(response["data"]);
                return JsonSerializer.Deserialize<AnimalDetailDto>(dataJson, _jsonOptions);
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    // ===== Medical History (Visites) =====

    public async Task<PaginatedResponse<VisitaResumatDto>> GetAnimalVisitsAsync(
        string idAnimal,
        int? pageNumber = null,
        int? pageSize = null,
        string? dataInici = null,
        string? dataFi = null)
    {
        var queryParams = new List<string>();
        if (pageNumber.HasValue) queryParams.Add($"pageNumber={pageNumber}");
        if (pageSize.HasValue) queryParams.Add($"pageSize={pageSize}");
        if (!string.IsNullOrEmpty(dataInici)) queryParams.Add($"dataInici={dataInici}");
        if (!string.IsNullOrEmpty(dataFi)) queryParams.Add($"dataFi={dataFi}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        
        try
        {
            var response = await _httpClient.GetFromJsonAsync<Dictionary<string, object>>($"/api/animals/{idAnimal}/visits{query}", _jsonOptions);
            if (response != null && response.ContainsKey("data"))
            {
                var dataJson = JsonSerializer.Serialize(response["data"]);
                var items = JsonSerializer.Deserialize<List<VisitaResumatDto>>(dataJson, _jsonOptions) ?? new List<VisitaResumatDto>();
                
                var paginationJson = response.ContainsKey("pagination") ? JsonSerializer.Serialize(response["pagination"]) : "{}";
                var paginationInfo = JsonSerializer.Deserialize<PaginationInfo>(paginationJson, _jsonOptions);
                
                return new PaginatedResponse<VisitaResumatDto>
                {
                    Items = items,
                    Pagination = paginationInfo
                };
            }
            return new PaginatedResponse<VisitaResumatDto>();
        }
        catch
        {
            return new PaginatedResponse<VisitaResumatDto>();
        }
    }

    public async Task<VisitaDetailDto?> GetVisitByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<VisitaDetailDto>($"/api/visits/{id}", _jsonOptions);
    }

    // ===== Medicines (Medicaments) =====

    public async Task<List<VeterinaryMedicineDto>> SearchVeterinaryMedicinesAsync(
        string query,
        string? species = null)
    {
        var queryParams = new List<string> { $"query={Uri.EscapeDataString(query)}" };
        if (!string.IsNullOrEmpty(species)) queryParams.Add($"species={Uri.EscapeDataString(species)}");

        var queryString = "?" + string.Join("&", queryParams);
        var response = await _httpClient.GetFromJsonAsync<List<VeterinaryMedicineDto>>($"/api/medicines/veterinary/search{queryString}", _jsonOptions);
        return response ?? new List<VeterinaryMedicineDto>();
    }

    public async Task<VeterinaryMedicineDto?> GetVeterinaryMedicineByCodeAsync(string cnCode)
    {
        return await _httpClient.GetFromJsonAsync<VeterinaryMedicineDto>($"/api/medicines/veterinary/{cnCode}", _jsonOptions);
    }

    public async Task<List<HumanMedicineDto>> SearchHumanMedicinesAsync(string query)
    {
        var queryString = $"?query={Uri.EscapeDataString(query)}";
        var response = await _httpClient.GetFromJsonAsync<List<HumanMedicineDto>>($"/api/medicines/human/search{queryString}", _jsonOptions);
        return response ?? new List<HumanMedicineDto>();
    }

    public async Task<HumanMedicineDto?> GetHumanMedicineByCodeAsync(string cnCode)
    {
        return await _httpClient.GetFromJsonAsync<HumanMedicineDto>($"/api/medicines/human/{cnCode}", _jsonOptions);
    }
}
