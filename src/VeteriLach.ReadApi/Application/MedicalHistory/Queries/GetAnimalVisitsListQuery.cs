using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

/// <summary>
/// Query per obtenir la llista paginada de visites d'un animal
/// </summary>
public class GetAnimalVisitsListQuery : IRequest<PaginatedResult<VisitaResumatDto>>
{
    public Guid IdAnimal { get; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public DateTime? DataInici { get; set; }
    public DateTime? DataFi { get; set; }

    public GetAnimalVisitsListQuery(Guid idAnimal)
    {
        IdAnimal = idAnimal;
    }
}
