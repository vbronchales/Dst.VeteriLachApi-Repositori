using System.Xml;
using Microsoft.Extensions.Caching.Memory;
using VeteriLach.ReadApi.Application.Medicines.DTOs;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices.LocalDataFallback;

/// <summary>
/// Proveïdor de dades locals per medicaments humans (CIMA)
/// Llegeix del fitxer Prescripcion.xml com a fallback quan l'API REST falla
/// </summary>
public class CimaLocalDataProvider : ILocalMedicineDataProvider<HumanMedicineDto>
{
    private readonly ILogger<CimaLocalDataProvider> _logger;
    private readonly string _xmlFilePath;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(12);
    private const string CACHE_KEY_ALL = "cima_local_all_medicines";

    public CimaLocalDataProvider(
        ILogger<CimaLocalDataProvider> logger,
        IConfiguration configuration,
        IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
        
        // Path configurable als XMLs locals
        var basePath = configuration.GetValue<string>("LocalMedicineData:BasePath") 
                      ?? Path.Combine(AppContext.BaseDirectory, "LocalData");
        _xmlFilePath = Path.Combine(basePath, "MedicamentsHumana", "prescripcion", "Prescripcion.xml");
        
        _logger.LogInformation("CimaLocalDataProvider inicialitzat. XML Path: {XmlPath}", _xmlFilePath);
    }

    public bool IsAvailable()
    {
        return File.Exists(_xmlFilePath);
    }

    public async Task<List<HumanMedicineDto>> SearchByNameAsync(
        string query, 
        CancellationToken cancellationToken = default)
    {
        if (!IsAvailable())
        {
            _logger.LogWarning("XML local de CIMA no disponible: {Path}", _xmlFilePath);
            return new List<HumanMedicineDto>();
        }

        _logger.LogInformation("Cercant '{Query}' en XML local de CIMA", query);

        // Carregar tots els medicaments (cachejat)
        var allMedicines = await GetAllMedicinesAsync(cancellationToken);

        // Filtrar per query
        var results = allMedicines.Where(m =>
            m.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            m.ActiveIngredient.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).Take(25).ToList();

        _logger.LogInformation("Trobats {Count} medicaments en XML local per '{Query}'", results.Count, query);
        return results;
    }

    public async Task<HumanMedicineDto?> GetByRegistrationNumberAsync(
        string registrationNumber, 
        CancellationToken cancellationToken = default)
    {
        if (!IsAvailable())
        {
            _logger.LogWarning("XML local de CIMA no disponible: {Path}", _xmlFilePath);
            return null;
        }

        _logger.LogInformation("Buscant medicament {RegNumber} en XML local de CIMA", registrationNumber);

        var allMedicines = await GetAllMedicinesAsync(cancellationToken);
        var medicine = allMedicines.FirstOrDefault(m => 
            m.CnCode.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));

        if (medicine != null)
        {
            _logger.LogInformation("Medicament {RegNumber} trobat en XML local", registrationNumber);
        }

        return medicine;
    }

    public async Task<List<HumanMedicineDto>> SearchByActiveIngredientAsync(
        string activeIngredient, 
        CancellationToken cancellationToken = default)
    {
        if (!IsAvailable())
        {
            _logger.LogWarning("XML local de CIMA no disponible: {Path}", _xmlFilePath);
            return new List<HumanMedicineDto>();
        }

        _logger.LogInformation("Cercant per principi actiu '{ActiveIngredient}' en XML local de CIMA", activeIngredient);

        var allMedicines = await GetAllMedicinesAsync(cancellationToken);
        var results = allMedicines.Where(m =>
            m.ActiveIngredient.Contains(activeIngredient, StringComparison.OrdinalIgnoreCase)
        ).Take(25).ToList();

        _logger.LogInformation("Trobats {Count} medicaments per principi actiu '{ActiveIngredient}'", results.Count, activeIngredient);
        return results;
    }

    /// <summary>
    /// Carrega tots els medicaments del XML (amb cache)
    /// </summary>
    private async Task<List<HumanMedicineDto>> GetAllMedicinesAsync(CancellationToken cancellationToken)
    {
        // Comprovar cache
        if (_cache.TryGetValue<List<HumanMedicineDto>>(CACHE_KEY_ALL, out var cached) && cached != null)
        {
            _logger.LogDebug("Medicaments CIMA carregats de cache ({Count} medicaments)", cached.Count);
            return cached;
        }

        _logger.LogInformation("Carregant medicaments de CIMA des de XML local...");
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var medicines = new List<HumanMedicineDto>();

        await Task.Run(() =>
        {
            using var fileStream = new FileStream(_xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = XmlReader.Create(fileStream, new XmlReaderSettings
            {
                Async = true,
                IgnoreWhitespace = true,
                IgnoreComments = true
            });

            HumanMedicineDto? currentMedicine = null;
            string? currentElement = null;

            while (reader.Read())
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    currentElement = reader.Name;

                    if (reader.Name == "medicamento")
                    {
                        currentMedicine = new HumanMedicineDto();
                    }
                }
                else if (reader.NodeType == XmlNodeType.Text && currentMedicine != null)
                {
                    var value = reader.Value?.Trim();
                    if (string.IsNullOrEmpty(value))
                        continue;

                    switch (currentElement)
                    {
                        case "nregistro":
                            currentMedicine.CnCode = value;
                            break;
                        case "nombre":
                            currentMedicine.Name = value;
                            break;
                        case "pactivos":
                            currentMedicine.ActiveIngredient = value;
                            break;
                        case "labtitular":
                            currentMedicine.Laboratory = value;
                            break;
                        case "dosis":
                            currentMedicine.Dose = value;
                            break;
                        case "formaFarmaceutica":
                            currentMedicine.PharmaceuticalForm = value;
                            break;
                        case "viasAdministracion":
                            currentMedicine.AdministrationRoute = value;
                            break;
                        case "cpresc":
                            currentMedicine.PrescriptionRequired = value.Contains("Prescripción", StringComparison.OrdinalIgnoreCase);
                            break;
                        case "generico":
                            currentMedicine.IsGeneric = value.Equals("true", StringComparison.OrdinalIgnoreCase) || value == "1";
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "medicamento" && currentMedicine != null)
                {
                    // Validar que té almenys codi i nom
                    if (!string.IsNullOrEmpty(currentMedicine.CnCode) && !string.IsNullOrEmpty(currentMedicine.Name))
                    {
                        currentMedicine.LastUpdated = DateTime.UtcNow;
                        medicines.Add(currentMedicine);
                    }
                    currentMedicine = null;
                }
            }
        }, cancellationToken);

        stopwatch.Stop();
        _logger.LogInformation("Carregats {Count} medicaments de CIMA en {ElapsedMs}ms", 
            medicines.Count, stopwatch.ElapsedMilliseconds);

        // Guardar a cache
        _cache.Set(CACHE_KEY_ALL, medicines, _cacheDuration);

        return medicines;
    }
}
