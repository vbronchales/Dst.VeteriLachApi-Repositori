namespace VeteriLach.ReadApi.Application.Animals.DTOs;

/// <summary>
/// DTO per a la llista d'animals (versió resumida)
/// </summary>
public class AnimalListDto
{
    public Guid IdAnimal { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int? Sexe { get; set; }
    public DateTime? DataNaixement { get; set; }
    public string Especie { get; set; } = string.Empty;
    public string Rasa { get; set; } = string.Empty;
    public string? Color { get; set; }
    public string? NumXip { get; set; }
    public bool Castrat { get; set; }
}
