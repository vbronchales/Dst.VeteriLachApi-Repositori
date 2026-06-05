namespace VeteriLach.ReadApi.Domain.MedicalHistory;

/// <summary>
/// DTO amb el detall complet d'una visita clínica
/// </summary>
public record VisitaDetailDto(Guid IdVisita, Guid IdPacient, DateTime DiaVisita, VeterinariDto Veterinari, string? Resum, decimal? Pes, decimal? Alsada, int TipusVisita)
{
    public List<TextVisitaDto> TextosClínics { get; set; } = [];
    public List<ProvaDto> Proves { get; set; } = [];
    public List<VacunaDto> Vacunes { get; set; } = [];
    public string? Resum { get; set; } = Resum;
}
