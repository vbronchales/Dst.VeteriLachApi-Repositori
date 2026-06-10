namespace VeteriLach.ReadApi.Domain.Medicines;

/// <summary>
/// DTO per a medicaments veterinaris (CimaVet)
/// </summary>
public record VeterinaryMedicineDto(string CnCode, string CommercialName, string ActiveIngredient, string? Concentration, string? PharmaceuticalForm,
    List<string> TargetSpecies, string? TherapeuticIndications, string? Dosage, string? Contraindications, string? Laboratory,
    bool PrescriptionRequired, WithdrawalPeriodDto? WithdrawalPeriod, List<string> PackageSizes, DateTime? LastUpdated)
{
    public VeterinaryMedicineDto() : this(string.Empty, string.Empty, string.Empty, null, null, new List<string>(), null, null, null, null, false, null, new List<string>(), null)
    {
    }

    public string? CnCode { get; set; } = CnCode;
    public string? CommercialName { get; set; } = CommercialName;
    public string? ActiveIngredient { get; set; } = ActiveIngredient;
    public string? Concentration { get; set; } = Concentration;
    public string? Laboratory { get; set; } = Laboratory;
    public string? PharmaceuticalForm { get; set; } = PharmaceuticalForm;
    public string? TherapeuticIndications { get; set; } = TherapeuticIndications;
    public string? Dosage { get; set; } = Dosage;
    public string? Contraindications { get; set; } = Contraindications;
    public bool PrescriptionRequired { get; set; } = PrescriptionRequired;
    public WithdrawalPeriodDto? WithdrawalPeriod { get; set; } = WithdrawalPeriod;
    public List<string> PackageSizes { get; set; } = PackageSizes;
    public DateTime? LastUpdated { get; set; } = LastUpdated;
    public List<string> TargetSpecies { get; set; } = TargetSpecies;
}

/// <summary>
/// Temps d'espera per a animals de consum
/// </summary>
public record WithdrawalPeriodDto(int? MeatDays, int? MilkDays, int? EggsDays);
