namespace VeteriLach.ReadApi.Domain;

/// <summary>
/// DTO per a informació del propietari
/// </summary>
public record PropietariDetailDto(Guid IdPropietari, string Nom, string? Cognom1, string? Cognom2, string? Nif, string? Email, string? Telefon, string? Adresa, string? CodiPostal, string? Poblacio)
    : PersonaBaseDto(IdPropietari, Nom, Cognom1, Cognom2, Nif, null, Email, false, Adresa, CodiPostal, Poblacio, null, null);
