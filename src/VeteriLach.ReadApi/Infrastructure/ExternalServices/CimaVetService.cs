using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Retry;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices;

/// <summary>
/// Servei per integració amb CimaVet (medicaments veterinaris)
/// Implementa retry policy amb Polly i cache de resultats
/// </summary>
public class CimaVetService : ICimaVetService
{
    private readonly ILogger<CimaVetService> _logger;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly AsyncRetryPolicy _retryPolicy;

    // Cache TTL: 7 dies per a informació de medicaments (canvia poc)
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(7);

    public CimaVetService(
        ILogger<CimaVetService> logger,
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
                        "Error cridant CimaVet. Intent {RetryCount} de 3. Reintentant en {Seconds}s",
                        retryCount,
                        timeSpan.TotalSeconds);
                });
    }

    public async Task<List<VeterinaryMedicineDto>> SearchMedicinesAsync(
        string query,
        string? species = null,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"cimavet_search_{query}_{species}";

        // Intentar obtenir de cache
        if (_cache.TryGetValue<List<VeterinaryMedicineDto>>(cacheKey, out var cachedResult) && cachedResult != null)
        {
            _logger.LogInformation("Resultats de CimaVet obtinguts de cache per query: {Query}", query);
            return cachedResult;
        }

        _logger.LogInformation("Cercant medicaments veterinaris a CimaVet: {Query}, Espècie: {Species}", query, species ?? "Totes");

        try
        {
            // Executar amb retry policy
            var results = await _retryPolicy.ExecuteAsync(async () =>
            {
                // TODO: Implementar crida real al web service SOAP de CimaVet
                // Endpoint SOAP: https://www.cimavet.aemps.es/cimavet/...
                
                // Per ara, retornem mock data per desenvolupament
                return await GetMockVeterinaryMedicinesAsync(query, species, cancellationToken);
            });

            // Guardar a cache
            _cache.Set(cacheKey, results, _cacheDuration);

            _logger.LogInformation("Trobats {Count} medicaments veterinaris per query: {Query}", results.Count, query);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cercant medicaments veterinaris a CimaVet per query: {Query}", query);
            throw;
        }
    }

    public async Task<VeterinaryMedicineDto?> GetMedicineByCodeAsync(
        string cnCode,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"cimavet_code_{cnCode}";

        // Intentar obtenir de cache
        if (_cache.TryGetValue<VeterinaryMedicineDto>(cacheKey, out var cachedResult) && cachedResult != null)
        {
            _logger.LogInformation("Medicament veterinari {CnCode} obtingut de cache", cnCode);
            return cachedResult;
        }

        _logger.LogInformation("Obtenint medicament veterinari de CimaVet per codi: {CnCode}", cnCode);

        try
        {
            var result = await _retryPolicy.ExecuteAsync(async () =>
            {
                // TODO: Implementar crida real al web service SOAP de CimaVet
                return await GetMockMedicineByCodeAsync(cnCode, cancellationToken);
            });

            if (result != null)
            {
                _cache.Set(cacheKey, result, _cacheDuration);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtenint medicament veterinari {CnCode} de CimaVet", cnCode);
            throw;
        }
    }

    public async Task<List<VeterinaryMedicineDto>> SearchByActiveIngredientAsync(
        string activeIngredient,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"cimavet_active_{activeIngredient}";

        if (_cache.TryGetValue<List<VeterinaryMedicineDto>>(cacheKey, out var cachedResult) && cachedResult != null)
        {
            _logger.LogInformation("Medicaments amb principi actiu {ActiveIngredient} obtinguts de cache", activeIngredient);
            return cachedResult;
        }

        _logger.LogInformation("Cercant medicaments veterinaris per principi actiu: {ActiveIngredient}", activeIngredient);

        try
        {
            var results = await _retryPolicy.ExecuteAsync(async () =>
            {
                // TODO: Implementar crida real al web service SOAP de CimaVet
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

    #region Mock Data (Temporal - reemplaçar amb integracions SOAP reals)

    private Task<List<VeterinaryMedicineDto>> GetMockVeterinaryMedicinesAsync(
        string query,
        string? species,
        CancellationToken cancellationToken)
    {
        var mockData = new List<VeterinaryMedicineDto>
        {
            new()
            {
                CnCode = "123456",
                CommercialName = "Metacam 5mg/ml Suspensió Oral per a Gossos",
                ActiveIngredient = "Meloxicam",
                Concentration = "5 mg/ml",
                PharmaceuticalForm = "Suspensió oral",
                TargetSpecies = new List<string> { "Gos" },
                TherapeuticIndications = "Alleujament del dolor i la inflamació en trastorns musculoesquelètics aguts i crònics.",
                Dosage = "Dosi inicial: 0.2 mg/kg pes corporal. Dosi de manteniment: 0.1 mg/kg un cop al dia.",
                Contraindications = "No utilitzar en animals amb hipersensibilitat al meloxicam. No utilitzar en gats.",
                Laboratory = "Boehringer Ingelheim",
                PrescriptionRequired = true,
                PackageSizes = new List<string> { "15 ml", "32 ml", "100 ml" },
                LastUpdated = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                CnCode = "789012",
                CommercialName = "Frontline Combo Gos",
                ActiveIngredient = "Fipronil + (S)-metoprè",
                Concentration = "Fipronil 100 mg/ml + (S)-metoprè 90 mg/ml",
                PharmaceuticalForm = "Solució spot-on",
                TargetSpecies = new List<string> { "Gos" },
                TherapeuticIndications = "Tractament i prevenció d'infestacions per puces i paparres.",
                Dosage = "Aplicar una pipeta segons el pes de l'animal. Repetir mensualment.",
                Contraindications = "No utilitzar en animals malalts o convalescents.",
                Laboratory = "Boehringer Ingelheim",
                PrescriptionRequired = false,
                PackageSizes = new List<string> { "XS (2-10kg)", "S (10-20kg)", "M (20-40kg)", "L (40-60kg)" },
                LastUpdated = DateTime.UtcNow.AddDays(-15)
            }
        };

        // Filtrar per query i espècie
        var filtered = mockData.Where(m =>
            m.CommercialName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            m.ActiveIngredient.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            m.CnCode.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        if (!string.IsNullOrEmpty(species))
        {
            filtered = filtered.Where(m =>
                m.TargetSpecies.Any(s => s.Contains(species, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        return Task.FromResult(filtered);
    }

    private Task<VeterinaryMedicineDto?> GetMockMedicineByCodeAsync(
        string cnCode,
        CancellationToken cancellationToken)
    {
        if (cnCode == "123456")
        {
            return Task.FromResult<VeterinaryMedicineDto?>(new VeterinaryMedicineDto
            {
                CnCode = "123456",
                CommercialName = "Metacam 5mg/ml Suspensió Oral per a Gossos",
                ActiveIngredient = "Meloxicam",
                Concentration = "5 mg/ml",
                PharmaceuticalForm = "Suspensió oral",
                TargetSpecies = new List<string> { "Gos" },
                TherapeuticIndications = "Alleujament del dolor i la inflamació en trastorns musculoesquelètics aguts i crònics.",
                Dosage = "Dosi inicial: 0.2 mg/kg pes corporal el primer dia. Dosi de manteniment: 0.1 mg/kg un cop al dia.",
                Contraindications = "No utilitzar en animals amb hipersensibilitat al meloxicam. No utilitzar en gats.",
                Laboratory = "Boehringer Ingelheim",
                PrescriptionRequired = true,
                PackageSizes = new List<string> { "15 ml", "32 ml", "100 ml" },
                LastUpdated = DateTime.UtcNow.AddDays(-30)
            });
        }

        return Task.FromResult<VeterinaryMedicineDto?>(null);
    }

    private Task<List<VeterinaryMedicineDto>> GetMockByActiveIngredientAsync(
        string activeIngredient,
        CancellationToken cancellationToken)
    {
        if (activeIngredient.Contains("meloxicam", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(new List<VeterinaryMedicineDto>
            {
                new()
                {
                    CnCode = "123456",
                    CommercialName = "Metacam 5mg/ml Suspensió Oral per a Gossos",
                    ActiveIngredient = "Meloxicam",
                    Concentration = "5 mg/ml",
                    PharmaceuticalForm = "Suspensió oral",
                    TargetSpecies = new List<string> { "Gos" },
                    PrescriptionRequired = true,
                    Laboratory = "Boehringer Ingelheim"
                }
            });
        }

        return Task.FromResult(new List<VeterinaryMedicineDto>());
    }

    #endregion
}
