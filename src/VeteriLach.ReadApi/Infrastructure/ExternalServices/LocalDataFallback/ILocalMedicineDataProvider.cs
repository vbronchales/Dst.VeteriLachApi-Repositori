namespace VeteriLach.ReadApi.Infrastructure.ExternalServices.LocalDataFallback;

/// <summary>
/// Interfície per proveïdors de dades locals (XMLs)
/// Utilitzat com a fallback quan les APIs externes fallen
/// </summary>
public interface ILocalMedicineDataProvider<TDto> where TDto : class
{
    /// <summary>
    /// Cerca medicaments per nom al fitxer XML local
    /// </summary>
    Task<List<TDto>> SearchByNameAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obté un medicament per codi de registre del fitxer XML local
    /// </summary>
    Task<TDto?> GetByRegistrationNumberAsync(string registrationNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cerca medicaments per principi actiu al fitxer XML local
    /// </summary>
    Task<List<TDto>> SearchByActiveIngredientAsync(string activeIngredient, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si els fitxers XML locals estan disponibles
    /// </summary>
    bool IsAvailable();
}
