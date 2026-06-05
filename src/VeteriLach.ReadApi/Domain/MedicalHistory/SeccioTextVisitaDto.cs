namespace VeteriLach.ReadApi.Domain.MedicalHistory;

/// <summary>
/// Seccions estructurades del text d'una visita
/// </summary>
public record struct SeccioTextVisitaDto(Guid IdVisita, string? Motiu, string? Exploracio, string? Diagnostic, string? Tractament, string? Observacions)
{
    public SeccioTextVisitaDto() : this(Guid.Empty, null, null, null, null, null) {}
}
