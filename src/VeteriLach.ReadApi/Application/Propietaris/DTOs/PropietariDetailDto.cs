namespace VeteriLach.ReadApi.Application.Propietaris.DTOs;

/// <summary>
/// DTO per a detall complet d'un propietari
/// </summary>
public record PropietariDetailDto(Guid IdPropietari,
    // Informació personal
    string Nom,
    string? Cognom1,
    string? Cognom2,
    string Cognoms,
    string? Nif,
    DateTime? DataNaixement,
    string? Email,
    bool AmbWhatsApp,
    string? Adresa,
    string? CodiPostal,
    string? Poblacio,
    string? Provincia,
    string? Pais)
{    
    // Telèfons
    public List<TelefonDto> Telefons { get; set; } = [];
    
    // Animals
    public List<AnimalResumatDto> Animals { get; set; } = [];
    
    // Estat
    public bool Actiu { get; set; }
    public string? Observacions { get; set; }
}

/// <summary>
/// DTO per a telèfon d'un propietari
/// </summary>
public record TelefonDto(string Numero, int TipusTelefon, string TipusTelefonDescripcio, int Ordre, string? Observacions);

/// <summary>
/// DTO resumit per a animals d'un propietari
/// </summary>
public record AnimalResumatDto(Guid IdAnimal, string Nom, string Especie, string? Rasa, string? Sexe, DateTime? DataNaixement, string? NumXip, bool Castrat);
