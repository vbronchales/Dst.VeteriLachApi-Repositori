using VeteriLach.ReadApi.Application.Medicines.DTOs;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

/// <summary>
/// Servei per integració amb CimaVet (medicaments veterinaris)
/// </summary>
public interface ICimaVetService
{
    /// <summary>
    /// Cerca medicaments veterinaris per nom, principi actiu o codi
    /// </summary>
    /// <param name="query">Text de cerca</param>
    /// <param name="species">Espècie animal (opcional)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de medicaments trobats</returns>
    Task<List<VeterinaryMedicineDto>> SearchMedicinesAsync(
        string query, 
        string? species = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obté informació detallada d'un medicament veterinari per codi nacional
    /// </summary>
    /// <param name="cnCode">Codi Nacional</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Informació del medicament o null si no es troba</returns>
    Task<VeterinaryMedicineDto?> GetMedicineByCodeAsync(
        string cnCode, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cerca medicaments per principi actiu
    /// </summary>
    /// <param name="activeIngredient">Principi actiu</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de medicaments amb aquest principi actiu</returns>
    Task<List<VeterinaryMedicineDto>> SearchByActiveIngredientAsync(
        string activeIngredient, 
        CancellationToken cancellationToken = default);
}
