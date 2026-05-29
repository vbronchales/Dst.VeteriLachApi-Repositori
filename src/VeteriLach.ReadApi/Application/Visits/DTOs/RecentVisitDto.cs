namespace VeteriLach.ReadApi.Application.Visits.DTOs;

/// <summary>
/// DTO per a visites recents amb informació d'animal i propietari
/// </summary>
public class RecentVisitDto
{
    public Guid IdVisita { get; set; }
    public DateTime DiaVisita { get; set; }
    public string? Resum { get; set; }
    public decimal? Pes { get; set; }
    public decimal? Alsada { get; set; }
    public int TipusVisita { get; set; }
    
    // Informació del pacient (animal)
    public Guid IdPacient { get; set; }
    public string NomAnimal { get; set; } = string.Empty;
    public string? Especie { get; set; }
    public string? Rasa { get; set; }
    public string? NumXip { get; set; }
    
    // Informació del propietari
    public Guid IdPropietari { get; set; }
    public string NomPropietari { get; set; } = string.Empty;
    public string CognomsPropietari { get; set; } = string.Empty;
    public string? TelefonPropietari { get; set; }
    
    // Informació del doctor
    public Guid IdDoctor { get; set; }
    public string? NomDoctor { get; set; }
}
