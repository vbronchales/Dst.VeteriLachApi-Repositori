namespace VeteriLach.ReadApi.Application.Metadata.DTOs;

/// <summary>
/// DTO per a races amb informació d'espècie i comptador
/// </summary>
public record RasaDto(Guid IdRasa, string Nom, Guid IdEspecie, string NomEspecie, int TotalAnimals, int TamanyRelatiu);
