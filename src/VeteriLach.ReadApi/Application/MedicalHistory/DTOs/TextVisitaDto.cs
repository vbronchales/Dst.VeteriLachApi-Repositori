namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO per a textos clínics d'una visita
/// </summary>
public class TextVisitaDto
{
    public int IndexText { get; set; }
    public string TextPla { get; set; } = null!;
    
    /// <summary>
    /// Seccions estructurades del text (si es detecten)
    /// </summary>
    public SeccioTextVisitaDto? Seccions { get; set; }
}
