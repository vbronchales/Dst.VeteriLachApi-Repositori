namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO per a la llista paginada de visites d'un animal
/// </summary>
public class VisitaResumatDto
{
    public Guid IdVisita { get; set; }
    public DateTime DiaVisita { get; set; }
    public string Veterinari { get; set; } = null!;
    public string? Resum { get; set; }
    public decimal? Pes { get; set; }
    public int TotalTextos { get; set; }
    public int TotalProves { get; set; }
    public int TotalVacunes { get; set; }
}
