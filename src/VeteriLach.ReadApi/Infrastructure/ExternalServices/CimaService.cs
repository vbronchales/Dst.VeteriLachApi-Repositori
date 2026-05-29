using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Retry;
using System.Text.Json;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.LocalDataFallback;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Models;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices;

/// <summary>
/// Servei per integració amb CIMA (Agencia Española de Medicamentos - medicaments humans)
/// Implementa retry policy amb Polly i cache de resultats
/// Utilitza l'API REST pública de CIMA: https://cima.aemps.es/cima/rest/
/// Si l'API falla o retorna 0 resultats, utilitza dades locals (XMLs) com a fallback
/// </summary>
public class CimaService : ICimaService
{
    private readonly ILogger<CimaService> _logger;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILocalMedicineDataProvider<HumanMedicineDto> _localDataProvider;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly string _baseUrl;

    // Cache TTL: 7 dies per a informació de medicaments
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(7);
    // Cache TTL: 24 hores per a preus (canvien més sovint)
    private readonly TimeSpan _priceCacheDuration = TimeSpan.FromHours(24);

    public CimaService(
        ILogger<CimaService> logger,
        IMemoryCache cache,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        ILocalMedicineDataProvider<HumanMedicineDto> localDataProvider)
    {
        _logger = logger;
        _cache = cache;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _localDataProvider = localDataProvider;
        _baseUrl = configuration.GetValue<string>("ExternalServices:Cima:BaseUrl") 
                   ?? "https://cima.aemps.es/cima/rest";

        // Retry policy: 3 intents amb backoff exponencial
        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .Or<JsonException>()
            .Or<TaskCanceledException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        exception,
                        "Error cridant CIMA API. Intent {RetryCount} de 3. Reintentant en {Seconds}s",
                        retryCount,
                        timeSpan.TotalSeconds);
                });
    }

    public async Task<List<HumanMedicineDto>> SearchMedicinesAsync(
        string query,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"cima_search_{query}";

        // Intentar obtenir de cache
        if (_cache.TryGetValue<List<HumanMedicineDto>>(cacheKey, out var cachedResult) && cachedResult != null)
        {
            _logger.LogInformation("Resultats de CIMA obtinguts de cache per query: {Query}", query);
            return cachedResult;
        }

        _logger.LogInformation("Cercant medicaments humans a CIMA: {Query}", query);

        List<HumanMedicineDto> results;

        try
        {
            // Executar amb retry policy
            results = await _retryPolicy.ExecuteAsync(async () =>
            {
                return await SearchMedicinesFromApiAsync(query, cancellationToken);
            });

            // Si API retorna 0 resultats, provar amb dades locals
            if (results.Count == 0 && _localDataProvider.IsAvailable())
            {
                _logger.LogWarning("CIMA API retornà 0 resultats per '{Query}'. Provant amb dades locals XML", query);
                results = await _localDataProvider.SearchByNameAsync(query, cancellationToken);
                
                if (results.Count > 0)
                {
                    _logger.LogInformation("Trobats {Count} medicaments humans en dades locals XML", results.Count);
                }
            }

            // Guardar a cache només si hi ha resultats
            if (results.Count > 0)
            {
                _cache.Set(cacheKey, results, _cacheDuration);
            }

            _logger.LogInformation("Trobats {Count} medicaments humans per query: {Query}", results.Count, query);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cercant medicaments humans a CIMA per query: {Query}. Provant fallback a XML local", query);
            
            // Fallback complet a dades locals en cas d'error
            if (_localDataProvider.IsAvailable())
            {
                _logger.LogInformation("Utilitzant dades locals XML com a fallback per '{Query}'", query);
                results = await _localDataProvider.SearchByNameAsync(query, cancellationToken);
                
                if (results.Count > 0)
                {
                    _logger.LogInformation("Recuperats {Count} medicaments de dades locals XML", results.Count);
                    _cache.Set(cacheKey, results, _cacheDuration);
                    return results;
                }
            }

            throw;
        }
    }

    public async Task<HumanMedicineDto?> GetMedicineByCodeAsync(
        string cnCode,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"cima_code_{cnCode}";

        // Intentar obtenir de cache
        if (_cache.TryGetValue<HumanMedicineDto>(cacheKey, out var cachedResult) && cachedResult != null)
        {
            _logger.LogInformation("Medicament humà {CnCode} obtingut de cache", cnCode);
            return cachedResult;
        }

        _logger.LogInformation("Obtenint medicament humà de CIMA per codi: {CnCode}", cnCode);

        HumanMedicineDto? result;

        try
        {
            result = await _retryPolicy.ExecuteAsync(async () =>
            {
                return await GetMedicineDetailFromApiAsync(cnCode, cancellationToken);
            });

            // Si API no retorna res, provar amb dades locals
            if (result == null && _localDataProvider.IsAvailable())
            {
                _logger.LogWarning("CIMA API no retornà medicament {CnCode}. Provant amb dades locals XML", cnCode);
                result = await _localDataProvider.GetByRegistrationNumberAsync(cnCode, cancellationToken);
                
                if (result != null)
                {
                    _logger.LogInformation("Medicament {CnCode} trobat en dades locals XML", cnCode);
                }
            }

            if (result != null)
            {
                // Preus amb cache més curt
                var cacheDuration = result.PricePvp.HasValue ? _priceCacheDuration : _cacheDuration;
                _cache.Set(cacheKey, result, cacheDuration);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtenint medicament humà {CnCode} de CIMA. Provant fallback", cnCode);
            
            if (_localDataProvider.IsAvailable())
            {
                result = await _localDataProvider.GetByRegistrationNumberAsync(cnCode, cancellationToken);
                if (result != null)
                {
                    _logger.LogInformation("Medicament {CnCode} recuperat de dades locals XML", cnCode);
                    _cache.Set(cacheKey, result, _cacheDuration);
                    return result;
                }
            }

            throw;
        }
    }

    public async Task<List<HumanMedicineDto>> SearchByActiveIngredientAsync(
        string activeIngredient,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"cima_active_{activeIngredient}";

        if (_cache.TryGetValue<List<HumanMedicineDto>>(cacheKey, out var cachedResult) && cachedResult != null)
        {
            _logger.LogInformation("Medicaments amb principi actiu {ActiveIngredient} obtinguts de cache", activeIngredient);
            return cachedResult;
        }

        _logger.LogInformation("Cercant medicaments humans per principi actiu: {ActiveIngredient}", activeIngredient);

        List<HumanMedicineDto> results;

        try
        {
            results = await _retryPolicy.ExecuteAsync(async () =>
            {
                // Cercar pel nom del principi actiu (l'API no té endpoint específic per principis actius)
                return await SearchMedicinesFromApiAsync(activeIngredient, cancellationToken);
            });

            // Si API retorna 0 resultats, provar amb dades locals
            if (results.Count == 0 && _localDataProvider.IsAvailable())
            {
                _logger.LogWarning("CIMA API retornà 0 resultats per principi actiu '{ActiveIngredient}'. Provant XML local", activeIngredient);
                results = await _localDataProvider.SearchByActiveIngredientAsync(activeIngredient, cancellationToken);
                
                if (results.Count > 0)
                {
                    _logger.LogInformation("Trobats {Count} medicaments per principi actiu en dades locals", results.Count);
                }
            }

            if (results.Count > 0)
            {
                _cache.Set(cacheKey, results, _cacheDuration);
            }

            _logger.LogInformation("Trobats {Count} medicaments amb principi actiu: {ActiveIngredient}", results.Count, activeIngredient);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cercant medicaments per principi actiu {ActiveIngredient}. Provant fallback", activeIngredient);
            
            if (_localDataProvider.IsAvailable())
            {
                results = await _localDataProvider.SearchByActiveIngredientAsync(activeIngredient, cancellationToken);
                if (results.Count > 0)
                {
                    _logger.LogInformation("Recuperats {Count} medicaments de dades locals", results.Count);
                    _cache.Set(cacheKey, results, _cacheDuration);
                    return results;
                }
            }

            throw;
        }
    }

    #region Private API Methods

    /// <summary>
    /// Cerca medicaments a través de l'API REST de CIMA
    /// </summary>
    private async Task<List<HumanMedicineDto>> SearchMedicinesFromApiAsync(
        string query,
        CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var url = $"{_baseUrl}/medicamentos?nombre={Uri.EscapeDataString(query)}";

        _logger.LogDebug("Cridant CIMA API: {Url}", url);

        var response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var searchResponse = JsonSerializer.Deserialize<CimaSearchResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (searchResponse?.Results == null || searchResponse.Results.Count == 0)
        {
            _logger.LogInformation("Cap resultat trobat a CIMA per query: {Query}", query);
            return new List<HumanMedicineDto>();
        }

        _logger.LogDebug("API retornat {Count} resultats de {Total} totals", 
            searchResponse.Results.Count, searchResponse.TotalRows);

        // Mapejat de resultats de cerca a DTOs
        var dtos = searchResponse.Results.Select(MapSearchResultToDto).ToList();

        return dtos;
    }

    /// <summary>
    /// Obté el detall complet d'un medicament per número de registre
    /// </summary>
    private async Task<HumanMedicineDto?> GetMedicineDetailFromApiAsync(
        string registrationNumber,
        CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var url = $"{_baseUrl}/medicamento?nregistro={Uri.EscapeDataString(registrationNumber)}";

        _logger.LogDebug("Cridant CIMA API per detall: {Url}", url);

        var response = await httpClient.GetAsync(url, cancellationToken);
        
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Medicament {RegistrationNumber} no trobat a CIMA", registrationNumber);
            return null;
        }

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var detail = JsonSerializer.Deserialize<CimaMedicineDetail>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (detail == null)
        {
            _logger.LogWarning("No s'ha pogut deserialitzar el detall del medicament {RegistrationNumber}", registrationNumber);
            return null;
        }

        return MapDetailToDto(detail);
    }

    /// <summary>
    /// Mapeja un resultat de cerca resumit a DTO
    /// </summary>
    private HumanMedicineDto MapSearchResultToDto(CimaMedicineSearchResult result)
    {
        return new HumanMedicineDto
        {
            CnCode = result.RegistrationNumber,
            Name = result.Name,
            ActiveIngredient = result.Vtm?.Name ?? string.Empty,
            PharmaceuticalForm = result.PharmaceuticalForm?.Name,
            Dose = result.Dose,
            AdministrationRoute = result.AdministrationRoutes.FirstOrDefault()?.Name,
            Laboratory = result.Laboratory,
            AuthorizationStatus = result.IsCommercialized ? "Comercialitzat" : "No comercialitzat",
            PrescriptionRequired = result.RequiresPrescription,
            IsGeneric = result.IsGeneric,
            TechnicalDataSheetUrl = result.Documents.FirstOrDefault(d => d.Type == 1)?.UrlHtml,
            PatientLeafletUrl = result.Documents.FirstOrDefault(d => d.Type == 2)?.UrlHtml,
            LastUpdated = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Mapeja un detall complet a DTO
    /// </summary>
    private HumanMedicineDto MapDetailToDto(CimaMedicineDetail detail)
    {
        DateTime? authDate = null;
        if (detail.State?.AuthorizationTimestamp.HasValue == true)
        {
            authDate = DateTimeOffset.FromUnixTimeMilliseconds(detail.State.AuthorizationTimestamp.Value).DateTime;
        }

        // Construir cadena d'indicacions a partir dels ATCs si no hi ha camp específic
        var indications = detail.Atcs.Count > 0 
            ? string.Join("; ", detail.Atcs.Select(atc => atc.Name))
            : null;

        // Combinar principis actius amb quantitats
        var activeIngredients = detail.ActivePrinciples.Count > 0
            ? string.Join(" + ", detail.ActivePrinciples
                .OrderBy(p => p.Order)
                .Select(p => $"{p.Name} {p.Quantity} {p.Unit}"))
            : detail.ActiveIngredients;

        return new HumanMedicineDto
        {
            CnCode = detail.RegistrationNumber,
            Name = detail.Name,
            ActiveIngredient = activeIngredients,
            PharmaceuticalForm = detail.PharmaceuticalForm?.Name,
            Dose = detail.Dose,
            AdministrationRoute = string.Join(", ", detail.AdministrationRoutes.Select(r => r.Name)),
            Laboratory = detail.Laboratory,
            AuthorizationStatus = detail.IsCommercialized ? "Comercialitzat" : "No comercialitzat",
            AuthorizationDate = authDate,
            Indications = indications,
            PrescriptionRequired = detail.RequiresPrescription,
            IsGeneric = detail.IsGeneric,
            PricePvp = null, // L'API REST no proporciona preus directament
            AffectedByReducedContribution = null,
            TechnicalDataSheetUrl = detail.Documents.FirstOrDefault(d => d.Type == 1)?.UrlHtml 
                                   ?? detail.Documents.FirstOrDefault(d => d.Type == 1)?.Url,
            PatientLeafletUrl = detail.Documents.FirstOrDefault(d => d.Type == 2)?.UrlHtml
                               ?? detail.Documents.FirstOrDefault(d => d.Type == 2)?.Url,
            LastUpdated = DateTime.UtcNow
        };
    }

    #endregion
}
