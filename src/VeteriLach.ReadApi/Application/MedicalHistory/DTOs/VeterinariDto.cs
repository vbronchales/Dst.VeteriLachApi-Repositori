namespace VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

/// <summary>
/// DTO amb informació bàsica del veterinari
/// </summary>
public class VeterinariDto
{
    public Guid IdDoctor { get; set; }
    public string Nom { get; set; } = null!;
    public string? Especialitat { get; set; }
    public string? NumColegiat { get; set; }
}
