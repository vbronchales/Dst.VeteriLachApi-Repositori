namespace VeteriLach.ReadApi.Domain.MedicalHistory;

/// <summary>
/// DTO per a la llista paginada de visites d'un animal
/// </summary>
public record VisitaResumDto(Guid IdVisita, DateTime DiaVisita, string Veterinari, string? Resum, decimal? Pes, int TotalTextos, int TotalProves, int TotalVacunes);
