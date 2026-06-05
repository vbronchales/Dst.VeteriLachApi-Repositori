namespace VeteriLach.ReadApi.Domain;

/// <summary>
/// DTO per a detall complet d'un animal
/// </summary>
public record AnimalDetailDto(Guid IdAnimal, string Nom, int? Sexe, DateTime? DataNaixement, string Especie, string Rasa, string? Color, string? NumXip, bool Castrat, string? Capa, string? Tatuatge, string? Caracter, PropietariDetailDto? Propietari);
