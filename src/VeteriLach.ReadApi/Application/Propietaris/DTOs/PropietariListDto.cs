namespace VeteriLach.ReadApi.Application.Propietaris.DTOs;

/// <summary>
/// DTO per a llista paginada de propietaris
/// </summary>
public record PropietariListDto(Guid IdPropietari, string Nom, string Cognoms, string? Email, string? Telefon, string? Poblacio, string? CodiPostal, int TotalAnimals, bool Actiu);
