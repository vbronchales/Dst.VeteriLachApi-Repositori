using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Retry;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices;

/// <summary>
/// Servei per integració amb CIMA (Agencia Española de Medicamentos - medicaments humans)
/// Implementa retry policy amb Polly i cache de resultats
/// </summary>
public class CimaService : ICimaService
{
    private readonly ILogger<CimaService> _logger;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly AsyncRetryPolicy _retryPolicy;

    // Cache TTL: 7 dies per a informació de medicaments
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(7);
    // Cache TTL: 24 hores per a preus (canvien més sovint)
    private readonly TimeSpan _priceCacheDuration = TimeSpan.FromHours(24);

    public CimaService(
        ILogger<CimaService> logger,
        IMemoryCache cache,
        IConfiguration configuration)
    {
        _logger = logger;
        _cache = cache;
        _configuration = configuration;

        // Retry policy: 3 intents amb backoff exponencial
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        exception,
                        "Error cridant CIMA. Intent {RetryCount} de 3. Reintentant en {Seconds}s",
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

        try
        {
            // Executar amb retry policy
            var results = await _retryPolicy.ExecuteAsync(async () =>
            {
                // TODO: Implementar crida real al web service REST/SOAP de CIMA
                // Endpoint: https://cima.aemps.es/cima/rest/...
                
                // Per ara, retornem mock data per desenvolupament
                return await GetMockHumanMedicinesAsync(query, cancellationToken);
            });

            // Guardar a cache
            _cache.Set(cacheKey, results, _cacheDuration);

            _logger.LogInformation("Trobats {Count} medicaments humans per query: {Query}", results.Count, query);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cercant medicaments humans a CIMA per query: {Query}", query);
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

        try
        {
            var result = await _retryPolicy.ExecuteAsync(async () =>
            {
                // TODO: Implementar crida real al web service de CIMA
                return await GetMockMedicineByCodeAsync(cnCode, cancellationToken);
            });

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
            _logger.LogError(ex, "Error obtenint medicament humà {CnCode} de CIMA", cnCode);
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

        try
        {
            var results = await _retryPolicy.ExecuteAsync(async () =>
            {
                // TODO: Implementar crida real al web service de CIMA
                return await GetMockByActiveIngredientAsync(activeIngredient, cancellationToken);
            });

            _cache.Set(cacheKey, results, _cacheDuration);

            _logger.LogInformation("Trobats {Count} medicaments amb principi actiu: {ActiveIngredient}", results.Count, activeIngredient);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cercant medicaments per principi actiu {ActiveIngredient}", activeIngredient);
            throw;
        }
    }

    #region Mock Data (Temporal - reemplaçar amb integracions REST/SOAP reals)

    private Task<List<HumanMedicineDto>> GetMockHumanMedicinesAsync(
        string query,
        CancellationToken cancellationToken)
    {
        var mockData = new List<HumanMedicineDto>
        {
            new()
            {
                CnCode = "654321",
                Name = "AMOXICILINA/CLAVULÁNICO NORMON 875 mg/125 mg comprimidos recubiertos con película EFG",
                ActiveIngredient = "Amoxicilina + Ácido clavulánico",
                PharmaceuticalForm = "Comprimits recoberts",
                Dose = "875 mg + 125 mg",
                AdministrationRoute = "Via oral",
                Laboratory = "Normon S.A.",
                AuthorizationStatus = "Autoritzat",
                AuthorizationDate = new DateTime(2010, 5, 15),
                Indications = "Tractament d'infeccions bacterianes causades per microorganismes sensibles.",
                PrescriptionRequired = true,
                IsGeneric = true,
                PricePvp = 6.45m,
                AffectedByReducedContribution = true,
                TechnicalDataSheetUrl = "https://cima.aemps.es/cima/dochtml/ft/654321/FichaTecnica_654321.html",
                PatientLeafletUrl = "https://cima.aemps.es/cima/dochtml/p/654321/Prospecto_654321.html",
                LastUpdated = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                CnCode = "987654",
                Name = "PARACETAMOL KERN PHARMA 1 g comprimidos EFG",
                ActiveIngredient = "Paracetamol",
                PharmaceuticalForm = "Comprimits",
                Dose = "1000 mg",
                AdministrationRoute = "Via oral",
                Laboratory = "Kern Pharma S.L.",
                AuthorizationStatus = "Autoritzat",
                AuthorizationDate = new DateTime(2008, 3, 20),
                Indications = "Tractament del dolor lleu o moderat. Tractament de la febre.",
                PrescriptionRequired = false,
                IsGeneric = true,
                PricePvp = 2.85m,
                AffectedByReducedContribution = false,
                TechnicalDataSheetUrl = "https://cima.aemps.es/cima/dochtml/ft/987654/FichaTecnica_987654.html",
                PatientLeafletUrl = "https://cima.aemps.es/cima/dochtml/p/987654/Prospecto_987654.html",
                LastUpdated = DateTime.UtcNow.AddDays(-5)
            }
        };

        // Filtrar per query
        var filtered = mockData.Where(m =>
            m.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            m.ActiveIngredient.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            m.CnCode.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        return Task.FromResult(filtered);
    }

    private Task<HumanMedicineDto?> GetMockMedicineByCodeAsync(
        string cnCode,
        CancellationToken cancellationToken)
    {
        if (cnCode == "654321")
        {
            return Task.FromResult<HumanMedicineDto?>(new HumanMedicineDto
            {
                CnCode = "654321",
                Name = "AMOXICILINA/CLAVULÁNICO NORMON 875 mg/125 mg comprimidos recubiertos con película EFG",
                ActiveIngredient = "Amoxicilina + Ácido clavulánico",
                PharmaceuticalForm = "Comprimits recoberts",
                Dose = "875 mg + 125 mg",
                AdministrationRoute = "Via oral",
                Laboratory = "Normon S.A.",
                AuthorizationStatus = "Autoritzat",
                AuthorizationDate = new DateTime(2010, 5, 15),
                Indications = "Tractament d'infeccions bacterianes causades per microorganismes sensibles.",
                PrescriptionRequired = true,
                IsGeneric = true,
                PricePvp = 6.45m,
                AffectedByReducedContribution = true,
                TechnicalDataSheetUrl = "https://cima.aemps.es/cima/dochtml/ft/654321/FichaTecnica_654321.html",
                PatientLeafletUrl = "https://cima.aemps.es/cima/dochtml/p/654321/Prospecto_654321.html",
                LastUpdated = DateTime.UtcNow.AddDays(-10)
            });
        }

        return Task.FromResult<HumanMedicineDto?>(null);
    }

    private Task<List<HumanMedicineDto>> GetMockByActiveIngredientAsync(
        string activeIngredient,
        CancellationToken cancellationToken)
    {
        if (activeIngredient.Contains("paracetamol", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(new List<HumanMedicineDto>
            {
                new()
                {
                    CnCode = "987654",
                    Name = "PARACETAMOL KERN PHARMA 1 g comprimidos EFG",
                    ActiveIngredient = "Paracetamol",
                    PharmaceuticalForm = "Comprimits",
                    Dose = "1000 mg",
                    PrescriptionRequired = false,
                    IsGeneric = true,
                    Laboratory = "Kern Pharma S.L."
                }
            });
        }

        return Task.FromResult(new List<HumanMedicineDto>());
    }

    #endregion
}
