namespace VeteriLach.ReadApi.Application.Animals.DTOs;

/// <summary>
/// DTO per a detall complet d'un animal
/// </summary>
public class AnimalDetailDto
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
    public string? Capa { get; set; }
    public string? Tatuatge { get; set; }
    public string? Caracter { get; set; }
    public PropietariDto? Propietari { get; set; }
}

/// <summary>
/// DTO per a informació del propietari
/// </summary>
public class PropietariDto
{
    public Guid IdPropietari { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Cognoms { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefon { get; set; }
    public string? Adresa { get; set; }
    public string? CodiPostal { get; set; }
    public string? Poblacio { get; set; }
}
