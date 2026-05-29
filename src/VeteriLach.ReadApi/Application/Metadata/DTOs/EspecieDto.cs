namespace VeteriLach.ReadApi.Application.Metadata.DTOs;

/// <summary>
/// DTO per a espècies amb comptador d'animals
/// </summary>
public class EspecieDto
{
    public Guid IdEspecie { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int TotalAnimals { get; set; }
    public int TipusEspecie { get; set; }
}
