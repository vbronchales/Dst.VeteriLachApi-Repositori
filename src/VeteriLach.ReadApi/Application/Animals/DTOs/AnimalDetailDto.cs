namespace VeteriLach.ReadApi.Application.Animals.DTOs;

/// <summary>
/// DTO per a detall complet d'un animal
/// </summary>
public record AnimalDetailDto(Guid IdAnimal, string Nom, int? Sexe, DateTime? DataNaixement, string Especie, string Rasa, string? Color, string? NumXip, bool Castrat, string? Capa, string? Tatuatge, string? Caracter, PropietariDto? Propietari);

/// <summary>
/// DTO per a informació del propietari
/// </summary>
public record PropietariDto(Guid IdPropietari, string Nom, string Cognoms, string? Email, string? Telefon, string? Adresa, string? CodiPostal, string? Poblacio);
