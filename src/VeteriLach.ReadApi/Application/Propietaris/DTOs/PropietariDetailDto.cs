namespace VeteriLach.ReadApi.Application.Propietaris.DTOs;

/// <summary>
/// DTO per a detall complet d'un propietari
/// </summary>
public class PropietariDetailDto
{
    public Guid IdPropietari { get; set; }
    
    // Informació personal
    public string Nom { get; set; } = string.Empty;
    public string? Cognom1 { get; set; }
    public string? Cognom2 { get; set; }
    public string Cognoms { get; set; } = string.Empty;
    public string? Nif { get; set; }
    public DateTime? DataNaixement { get; set; }
    public string? Email { get; set; }
    public bool AmbWhatsApp { get; set; }
    
    // Adreça
    public string? Adresa { get; set; }
    public string? CodiPostal { get; set; }
    public string? Poblacio { get; set; }
    public string? Provincia { get; set; }
    public string? Pais { get; set; }
    
    // Telèfons
    public List<TelefonDto> Telefons { get; set; } = new();
    
    // Animals
    public List<AnimalResumatDto> Animals { get; set; } = new();
    
    // Estat
    public bool Actiu { get; set; }
    public string? Observacions { get; set; }
}

/// <summary>
/// DTO per a telèfon d'un propietari
/// </summary>
public class TelefonDto
{
    public string Numero { get; set; } = string.Empty;
    public int TipusTelefon { get; set; }
    public string TipusTelefonDescripcio { get; set; } = string.Empty;
    public int Ordre { get; set; }
    public string? Observacions { get; set; }
}

/// <summary>
/// DTO resumit per a animals d'un propietari
/// </summary>
public class AnimalResumatDto
{
    public Guid IdAnimal { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string? Rasa { get; set; }
    public string? Sexe { get; set; }
    public DateTime? DataNaixement { get; set; }
    public string? NumXip { get; set; }
    public bool Castrat { get; set; }
}
