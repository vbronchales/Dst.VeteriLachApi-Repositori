namespace VeteriLach.ReadApi.Domain.MedicalHistory;

/// <summary>
/// DTO per a textos clínics d'una visita
/// </summary>
public record TextVisitaDto(int IndexText, string TextPla, SeccioTextVisitaDto? Seccions);

 