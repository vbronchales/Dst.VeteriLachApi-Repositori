using System.Xml;
using Microsoft.Extensions.Caching.Memory;
using VeteriLach.ReadApi.Application.Medicines.DTOs;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices.LocalDataFallback;

/// <summary>
/// Proveïdor de dades locals per medicaments veterinaris (CIMAVet)
/// Llegeix del fitxer PrescripcionVET.xml com a fallback quan l'API REST falla
/// </summary>
public class CimaVetLocalDataProvider : ILocalMedicineDataProvider<VeterinaryMedicineDto>
{
    private readonly ILogger<CimaVetLocalDataProvider> _logger;
    private readonly string _xmlFilePath;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(12);
    private const string CACHE_KEY_ALL = "cimavet_local_all_medicines";

    public CimaVetLocalDataProvider(
        ILogger<CimaVetLocalDataProvider> logger,
        IConfiguration configuration,
        IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
        
        var basePath = configuration.GetValue<string>("LocalMedicineData:BasePath") 
                      ?? Path.Combine(AppContext.BaseDirectory, "LocalData");
        _xmlFilePath = Path.Combine(basePath, "MedicamentsVeterinaris", "prescripcionVET", "PrescripcionVET.xml");
        
        _logger.LogInformation("CimaVetLocalDataProvider inicialitzat. XML Path: {XmlPath}", _xmlFilePath);
    }

    public bool IsAvailable()
    {
        return File.Exists(_xmlFilePath);
    }

    public async Task<List<VeterinaryMedicineDto>> SearchByNameAsync(
        string query, 
        CancellationToken cancellationToken = default)
    {
        if (!IsAvailable())
        {
            _logger.LogWarning("XML local de CIMAVet no disponible: {Path}", _xmlFilePath);
            return new List<VeterinaryMedicineDto>();
        }

        _logger.LogInformation("Cercant '{Query}' en XML local de CIMAVet", query);

        var allMedicines = await GetAllMedicinesAsync(cancellationToken);
        var results = allMedicines.Where(m =>
            m.CommercialName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            m.ActiveIngredient.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).Take(25).ToList();

        _logger.LogInformation("Trobats {Count} medicaments veterinaris en XML local per '{Query}'", results.Count, query);
        return results;
    }

    public async Task<VeterinaryMedicineDto?> GetByRegistrationNumberAsync(
        string registrationNumber, 
        CancellationToken cancellationToken = default)
    {
        if (!IsAvailable())
        {
            _logger.LogWarning("XML local de CIMAVet no disponible: {Path}", _xmlFilePath);
            return null;
        }

        _logger.LogInformation("Buscant medicament veterinari {RegNumber} en XML local", registrationNumber);

        var allMedicines = await GetAllMedicinesAsync(cancellationToken);
        var medicine = allMedicines.FirstOrDefault(m => 
            m.CnCode.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));

        if (medicine != null)
        {
            _logger.LogInformation("Medicament veterinari {RegNumber} trobat en XML local", registrationNumber);
        }

        return medicine;
    }

    public async Task<List<VeterinaryMedicineDto>> SearchByActiveIngredientAsync(
        string activeIngredient, 
        CancellationToken cancellationToken = default)
    {
        if (!IsAvailable())
        {
            _logger.LogWarning("XML local de CIMAVet no disponible: {Path}", _xmlFilePath);
            return new List<VeterinaryMedicineDto>();
        }

        _logger.LogInformation("Cercant per principi actiu '{ActiveIngredient}' en XML local de CIMAVet", activeIngredient);

        var allMedicines = await GetAllMedicinesAsync(cancellationToken);
        var results = allMedicines.Where(m =>
            m.ActiveIngredient.Contains(activeIngredient, StringComparison.OrdinalIgnoreCase)
        ).Take(25).ToList();

        _logger.LogInformation("Trobats {Count} medicaments veterinaris per principi actiu", results.Count);
        return results;
    }

    private async Task<List<VeterinaryMedicineDto>> GetAllMedicinesAsync(CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue<List<VeterinaryMedicineDto>>(CACHE_KEY_ALL, out var cached) && cached != null)
        {
            _logger.LogDebug("Medicaments CIMAVet carregats de cache ({Count} medicaments)", cached.Count);
            return cached;
        }

        _logger.LogInformation("Carregant medicaments veterinaris des de XML local...");
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var medicines = new List<VeterinaryMedicineDto>();

        await Task.Run(() =>
        {
            using var fileStream = new FileStream(_xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = XmlReader.Create(fileStream, new XmlReaderSettings
            {
                Async = true,
                IgnoreWhitespace = true,
                IgnoreComments = true
            });

            VeterinaryMedicineDto? currentMedicine = null;
            string? currentElement = null;
            List<string> currentSpecies = new();

            while (reader.Read())
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    currentElement = reader.Name;

                    if (reader.Name == "medicamento")
                    {
                        currentMedicine = new VeterinaryMedicineDto();
                        currentSpecies = new List<string>();
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
                            currentMedicine.CommercialName = value;
                            break;
                        case "pactivos":
                            currentMedicine.ActiveIngredient = value;
                            break;
                        case "concentracion":
                            currentMedicine.Concentration = value;
                            break;
                        case "labtitular":
                            currentMedicine.Laboratory = value;
                            break;
                        case "formaFarmaceutica":
                            currentMedicine.PharmaceuticalForm = value;
                            break;
                        case "especieDestino":
                            if (!currentSpecies.Contains(value))
                                currentSpecies.Add(value);
                            break;
                        case "indicaciones":
                            currentMedicine.TherapeuticIndications = value;
                            break;
                        case "dosis":
                            currentMedicine.Dosage = value;
                            break;
                        case "contraIndicaciones":
                            currentMedicine.Contraindications = value;
                            break;
                        case "cpresc":
                            currentMedicine.PrescriptionRequired = value.Contains("Prescripción", StringComparison.OrdinalIgnoreCase);
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "medicamento" && currentMedicine != null)
                {
                    if (!string.IsNullOrEmpty(currentMedicine.CnCode) && !string.IsNullOrEmpty(currentMedicine.CommercialName))
                    {
                        currentMedicine.TargetSpecies = currentSpecies;
                        currentMedicine.LastUpdated = DateTime.UtcNow;
                        medicines.Add(currentMedicine);
                    }
                    currentMedicine = null;
                    currentSpecies = new List<string>();
                }
            }
        }, cancellationToken);

        stopwatch.Stop();
        _logger.LogInformation("Carregats {Count} medicaments veterinaris en {ElapsedMs}ms", 
            medicines.Count, stopwatch.ElapsedMilliseconds);

        _cache.Set(CACHE_KEY_ALL, medicines, _cacheDuration);

        return medicines;
    }
}
