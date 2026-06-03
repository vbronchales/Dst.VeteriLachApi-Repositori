namespace VeteriLach.ReadApi.Application.Metadata.DTOs;

/// <summary>
/// DTO per a espècies amb comptador d'animals
/// </summary>
public record EspecieDto(Guid IdEspecie, string Nom, int TotalAnimals, int TipusEspecie);
