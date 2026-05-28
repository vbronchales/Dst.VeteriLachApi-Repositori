namespace VeteriLach.ReadApi.Application.Propietaris.DTOs;

/// <summary>
/// DTO per a llista paginada de propietaris
/// </summary>
public class PropietariListDto
{
    public Guid IdPropietari { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Cognoms { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefon { get; set; }
    public string? Poblacio { get; set; }
    public string? CodiPostal { get; set; }
    public int TotalAnimals { get; set; }
    public bool Actiu { get; set; }
}
