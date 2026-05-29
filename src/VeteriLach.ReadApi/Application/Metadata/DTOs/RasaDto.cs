namespace VeteriLach.ReadApi.Application.Metadata.DTOs;

/// <summary>
/// DTO per a races amb informació d'espècie i comptador
/// </summary>
public class RasaDto
{
    public Guid IdRasa { get; set; }
    public string Nom { get; set; } = string.Empty;
    public Guid IdEspecie { get; set; }
    public string NomEspecie { get; set; } = string.Empty;
    public int TotalAnimals { get; set; }
    public int TamanyRelatiu { get; set; }
}
