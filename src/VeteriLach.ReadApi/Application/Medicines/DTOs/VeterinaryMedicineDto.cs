namespace VeteriLach.ReadApi.Application.Medicines.DTOs;

/// <summary>
/// DTO per a medicaments veterinaris (CimaVet)
/// </summary>
public record VeterinaryMedicineDto(string CnCode, string CommercialName, string ActiveIngredient, string? Concentration, string? PharmaceuticalForm,
    List<string> TargetSpecies, string? TherapeuticIndications, string? Dosage, string? Contraindications, string? Laboratory,
    bool PrescriptionRequired, WithdrawalPeriodDto? WithdrawalPeriod, List<string> PackageSizes, DateTime? LastUpdated);

/// <summary>
/// Temps d'espera per a animals de consum
/// </summary>
public record WithdrawalPeriodDto(int? MeatDays, int? MilkDays, int? EggsDays);
