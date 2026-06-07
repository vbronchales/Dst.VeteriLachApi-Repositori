namespace VeteriLach.ReadApi.Domain.Medicines;

/// <summary>
/// DTO per a medicaments humans (CIMA - Agencia Española de Medicamentos)
/// </summary>
public record HumanMedicineDto(string CnCode, string Name, string ActiveIngredient, string? PharmaceuticalForm, string? Dose, string? AdministrationRoute,
    string? Laboratory, string? AuthorizationStatus, DateTime? AuthorizationDate, string? Indications, bool PrescriptionRequired, bool IsGeneric, decimal? PricePvp,
    bool? AffectedByReducedContribution, string? TechnicalDataSheetUrl, string? PatientLeafletUrl, DateTime? LastUpdated)
{
    public HumanMedicineDto() : this(string.Empty, string.Empty, string.Empty, null, null, null, null, null, null,null, false, false, null, null, null, null, null)
    {
    }

    public string? CnCode{ get; set; } = CnCode;
    public string? Name{ get; set; } = Name;
    public string? ActiveIngredient { get; set; } = ActiveIngredient;    
    public string? Laboratory { get; set; } = Laboratory;
    public string? Dose { get; set; } = Dose;
    public string? PharmaceuticalForm { get; set; } = PharmaceuticalForm;
    public string? AdministrationRoute { get; set; } = AdministrationRoute;
    public bool PrescriptionRequired { get; set; } = PrescriptionRequired;
    public bool IsGeneric { get; set; } = IsGeneric;
    public DateTime? LastUpdated { get; set; } = LastUpdated;
}
