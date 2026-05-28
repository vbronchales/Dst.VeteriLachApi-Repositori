namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO amb el detall complet d'una visita clínica
/// </summary>
public class VisitaDetailDto
{
    public Guid IdVisita { get; set; }
    public Guid IdPacient { get; set; }
    public DateTime DiaVisita { get; set; }
    public VeterinariDto Veterinari { get; set; } = null!;
    public string? Resum { get; set; }
    public decimal? Pes { get; set; }
    public decimal? Alsada { get; set; }
    public int TipusVisita { get; set; }
    
    public List<TextVisitaDto> TextosClínics { get; set; } = new();
    public List<ProvaDto> Proves { get; set; } = new();
    public List<VacunaDto> Vacunes { get; set; } = new();
}
