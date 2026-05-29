using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

/// <summary>
/// Query per obtenir informació detallada d'un medicament humà per codi
/// </summary>
public class GetHumanMedicineByCodeQuery : IRequest<HumanMedicineDto?>
{
    /// <summary>
    /// Codi Nacional del medicament
    /// </summary>
    public string CnCode { get; set; } = string.Empty;
}
