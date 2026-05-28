namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// Seccions estructurades del text d'una visita
/// </summary>
public class SeccioTextVisitaDto
{
    /// <summary>
    /// Motiu de consulta
    /// </summary>
    public string? Motiu { get; set; }
    
    /// <summary>
    /// Exploració física realitzada
    /// </summary>
    public string? Exploracio { get; set; }
    
    /// <summary>
    /// Diagnòstic o impressió clínica
    /// </summary>
    public string? Diagnostic { get; set; }
    
    /// <summary>
    /// Tractament prescrit o recomanacions
    /// </summary>
    public string? Tractament { get; set; }
    
    /// <summary>
    /// Observacions generals o altres notes
    /// </summary>
    public string? Observacions { get; set; }
}
