using VeteriLach.ReadApi.Application.Medicines.DTOs;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

/// <summary>
/// Servei per integració amb CIMA (Agencia Española de Medicamentos - medicaments humans)
/// </summary>
public interface ICimaService
{
    /// <summary>
    /// Cerca medicaments humans per nom, principi actiu o codi
    /// </summary>
    /// <param name="query">Text de cerca</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de medicaments trobats</returns>
    Task<List<HumanMedicineDto>> SearchMedicinesAsync(
        string query, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obté informació detallada d'un medicament humà per codi nacional
    /// </summary>
    /// <param name="cnCode">Codi Nacional</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Informació del medicament o null si no es troba</returns>
    Task<HumanMedicineDto?> GetMedicineByCodeAsync(
        string cnCode, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cerca medicaments per principi actiu
    /// </summary>
    /// <param name="activeIngredient">Principi actiu</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de medicaments amb aquest principi actiu</returns>
    Task<List<HumanMedicineDto>> SearchByActiveIngredientAsync(
        string activeIngredient, 
        CancellationToken cancellationToken = default);
}
