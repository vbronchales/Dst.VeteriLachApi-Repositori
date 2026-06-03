using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public record GetAnimalVisitsListQuery(Guid IdAnimal, int PageNumber = 1, int PageSize = 20, DateTime? DataInici = null, DateTime? DataFi = null) : IRequest<PaginatedResult<VisitaResumatDto>>;

public partial class GetAnimalVisitsListQueryHandler(IVisitesRepository repository, ILogger<GetAnimalVisitsListQueryHandler> logger) : IRequestHandler<GetAnimalVisitsListQuery, PaginatedResult<VisitaResumatDto>>
{

    public async Task<PaginatedResult<VisitaResumatDto>> Handle(GetAnimalVisitsListQuery request, CancellationToken cancellationToken)
    {
        LogObtenintLlistaDeVisitesPerAnimal(request.IdAnimal);

        return await repository.GetAnimalVisitsListAsync(request.IdAnimal, request.PageNumber, request.PageSize, request.DataInici, request.DataFi, cancellationToken);
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Obtenint llista de visites per animal {IdAnimal}")]
    partial void LogObtenintLlistaDeVisitesPerAnimal(Guid idAnimal);
}