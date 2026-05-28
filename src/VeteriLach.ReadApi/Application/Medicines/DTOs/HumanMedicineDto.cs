namespace VeteriLach.ReadApi.Application.Medicines.DTOs;

/// <summary>
/// DTO per a medicaments humans (CIMA - Agencia Española de Medicamentos)
/// </summary>
public class HumanMedicineDto
{
    /// <summary>
    /// Codi Nacional del medicament
    /// </summary>
    public string CnCode { get; set; } = string.Empty;

    /// <summary>
    /// Nom del medicament
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Principi actiu o composició
    /// </summary>
    public string ActiveIngredient { get; set; } = string.Empty;

    /// <summary>
    /// Forma farmacèutica (comprimits, càpsules, solució, etc.)
    /// </summary>
    public string? PharmaceuticalForm { get; set; }

    /// <summary>
    /// Dosi o concentració
    /// </summary>
    public string? Dose { get; set; }

    /// <summary>
    /// Via d'administració (oral, intravenosa, tòpica, etc.)
    /// </summary>
    public string? AdministrationRoute { get; set; }

    /// <summary>
    /// Laboratori titular
    /// </summary>
    public string? Laboratory { get; set; }

    /// <summary>
    /// Estat d'autorització (autoritzat, suspès, revogat)
    /// </summary>
    public string? AuthorizationStatus { get; set; }

    /// <summary>
    /// Data d'autorització
    /// </summary>
    public DateTime? AuthorizationDate { get; set; }

    /// <summary>
    /// Indicacions terapèutiques
    /// </summary>
    public string? Indications { get; set; }

    /// <summary>
    /// Requereix prescripció mèdica
    /// </summary>
    public bool PrescriptionRequired { get; set; }

    /// <summary>
    /// És genèric
    /// </summary>
    public bool IsGeneric { get; set; }

    /// <summary>
    /// Preu de venda al públic (PVP) amb IVA
    /// </summary>
    public decimal? PricePvp { get; set; }

    /// <summary>
    /// Afectat per aportació reduïda (pensionistes)
    /// </summary>
    public bool? AffectedByReducedContribution { get; set; }

    /// <summary>
    /// URL de la fitxa tècnica
    /// </summary>
    public string? TechnicalDataSheetUrl { get; set; }

    /// <summary>
    /// URL del prospecte
    /// </summary>
    public string? PatientLeafletUrl { get; set; }

    /// <summary>
    /// Data de l'última actualització
    /// </summary>
    public DateTime? LastUpdated { get; set; }
}
