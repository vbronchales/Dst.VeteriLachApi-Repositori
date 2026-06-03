namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO per a vacunes administrades en una visita
/// </summary>
public record VacunaDto(Guid IdVacuna, string TipusVacuna, DateTime DiaVacuna, string? Observacions, bool NoRevacunar, int FrequenciaDies, DateTime? ProximaVacuna);
