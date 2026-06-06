namespace VeteriLach.ReadApi.Domain.Medicines;

/// <summary>
/// DTO per a medicaments humans (CIMA - Agencia Española de Medicamentos)
/// </summary>
public record struct HumanMedicineDto(string CnCode, string Name, string ActiveIngredient, string? PharmaceuticalForm, string? Dose, string? AdministrationRoute,
    string? Laboratory, string? AuthorizationStatus, DateTime? AuthorizationDate, string? Indications, bool PrescriptionRequired, bool IsGeneric, decimal? PricePvp,
    bool? AffectedByReducedContribution, string? TechnicalDataSheetUrl, string? PatientLeafletUrl, DateTime? LastUpdated)
{
    public HumanMedicineDto() : this(string.Empty, string.Empty, string.Empty, null, null, null, null, null, null,null, false, false, null, null, null, null, null)
    {
    }
}
