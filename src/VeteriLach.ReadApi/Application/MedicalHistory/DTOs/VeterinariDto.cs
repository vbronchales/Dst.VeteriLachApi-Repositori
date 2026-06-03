namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO amb informació bàsica del veterinari
/// </summary>
public record VeterinariDto(Guid IdDoctor, string Nom, string? Especialitat, string? NumColegiat);
