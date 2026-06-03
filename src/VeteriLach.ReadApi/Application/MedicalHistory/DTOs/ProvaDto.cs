namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO per a proves diagnòstiques realitzades en una visita
/// </summary>
public record ProvaDto(Guid IdProva, string TipusProva, int Ordre, string? CodiMostra, string? Observacions, List<DetallProvaDto> Resultats);

/// <summary>
/// DTO per a resultats de paràmetres d'una prova
/// </summary>
public record DetallProvaDto(string Parametre, string Valor, string? Observacions);
