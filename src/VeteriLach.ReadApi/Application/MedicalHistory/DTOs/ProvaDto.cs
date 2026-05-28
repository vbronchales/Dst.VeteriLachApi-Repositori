namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO per a proves diagnòstiques realitzades en una visita
/// </summary>
public class ProvaDto
{
    public Guid IdProva { get; set; }
    public string TipusProva { get; set; } = null!;
    public int Ordre { get; set; }
    public string? CodiMostra { get; set; }
    public string? Observacions { get; set; }
    public List<DetallProvaDto> Resultats { get; set; } = new();
}

/// <summary>
/// DTO per a resultats de paràmetres d'una prova
/// </summary>
public class DetallProvaDto
{
    public string Parametre { get; set; } = null!;
    public string Valor { get; set; } = null!;
    public string? Observacions { get; set; }
}
