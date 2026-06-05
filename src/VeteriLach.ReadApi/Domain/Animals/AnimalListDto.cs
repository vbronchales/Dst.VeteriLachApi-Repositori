namespace VeteriLach.ReadApi.Domain.Animals;

/// <summary>
/// DTO per a la llista d'animals (versió resumida)
/// </summary>
public record AnimalListDto(Guid IdAnimal, string Nom, int? Sexe, DateTime? DataNaixement, string Especie, string Rasa, string? Color, string? NumXip, bool Castrat);
