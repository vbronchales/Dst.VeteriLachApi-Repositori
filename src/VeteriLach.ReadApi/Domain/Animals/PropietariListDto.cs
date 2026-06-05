namespace VeteriLach.ReadApi.Domain;

/// <summary>
/// DTO per a llista paginada de propietaris
/// </summary>
public record PropietariListDto(Guid IdPropietari, string Nom, string? Cognom1, string? Cognom2, string? Email, string? Telefon, string? Poblacio, string? CodiPostal, int TotalAnimals, bool Actiu)
    : PersonaBaseDto(IdPropietari, Nom, Cognom1, Cognom2, null, null, Email, false, null, CodiPostal, Poblacio, null, null);
