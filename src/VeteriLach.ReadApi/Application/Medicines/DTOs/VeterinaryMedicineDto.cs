namespace VeteriLach.ReadApi.Application.Medicines.DTOs;

/// <summary>
/// DTO per a medicaments veterinaris (CimaVet)
/// </summary>
public class VeterinaryMedicineDto
{
    /// <summary>
    /// Codi Nacional del medicament
    /// </summary>
    public string CnCode { get; set; } = string.Empty;

    /// <summary>
    /// Nom comercial del medicament
    /// </summary>
    public string CommercialName { get; set; } = string.Empty;

    /// <summary>
    /// Principi actiu
    /// </summary>
    public string ActiveIngredient { get; set; } = string.Empty;

    /// <summary>
    /// Concentració del principi actiu
    /// </summary>
    public string? Concentration { get; set; }

    /// <summary>
    /// Forma farmacèutica (comprimits, suspensió oral, injecció, etc.)
    /// </summary>
    public string? PharmaceuticalForm { get; set; }

    /// <summary>
    /// Espècies animals a les quals va dirigit
    /// </summary>
    public List<string> TargetSpecies { get; set; } = new();

    /// <summary>
    /// Indicacions terapèutiques
    /// </summary>
    public string? TherapeuticIndications { get; set; }

    /// <summary>
    /// Posologia i via d'administració
    /// </summary>
    public string? Dosage { get; set; }

    /// <summary>
    /// Contraindicacions
    /// </summary>
    public string? Contraindications { get; set; }

    /// <summary>
    /// Laboratori fabricant
    /// </summary>
    public string? Laboratory { get; set; }

    /// <summary>
    /// Requereix prescripció veterinària
    /// </summary>
    public bool PrescriptionRequired { get; set; }

    /// <summary>
    /// Temps d'espera abans del sacrifici (per a animals de consum)
    /// </summary>
    public WithdrawalPeriodDto? WithdrawalPeriod { get; set; }

    /// <summary>
    /// Mides de presentació disponibles
    /// </summary>
    public List<string> PackageSizes { get; set; } = new();

    /// <summary>
    /// Data de l'última actualització de la informació
    /// </summary>
    public DateTime? LastUpdated { get; set; }
}

/// <summary>
/// Temps d'espera per a animals de consum
/// </summary>
public class WithdrawalPeriodDto
{
    /// <summary>
    /// Dies d'espera per a carn
    /// </summary>
    public int? MeatDays { get; set; }

    /// <summary>
    /// Dies d'espera per a llet
    /// </summary>
    public int? MilkDays { get; set; }

    /// <summary>
    /// Dies d'espera per a ous
    /// </summary>
    public int? EggsDays { get; set; }
}
