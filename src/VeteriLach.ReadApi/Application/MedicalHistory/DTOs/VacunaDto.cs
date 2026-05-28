namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO per a vacunes administrades en una visita
/// </summary>
public class VacunaDto
{
    public Guid IdVacuna { get; set; }
    public string TipusVacuna { get; set; } = null!;
    public DateTime DiaVacuna { get; set; }
    public string? Observacions { get; set; }
    public bool NoRevacunar { get; set; }
    public int FrequenciaDies { get; set; }
    public DateTime? ProximaVacuna { get; set; }
}
