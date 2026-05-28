using MediatR;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

/// <summary>
/// Query per obtenir el detall complet d'una visita
/// </summary>
public class GetVisitByIdQuery : IRequest<VisitaDetailDto?>
{
    public Guid IdVisita { get; }

    public GetVisitByIdQuery(Guid idVisita)
    {
        IdVisita = idVisita;
    }
}
