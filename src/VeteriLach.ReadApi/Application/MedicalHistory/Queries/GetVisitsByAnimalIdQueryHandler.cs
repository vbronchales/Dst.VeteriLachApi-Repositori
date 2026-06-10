using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.MedicalHistory;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public record GetVisitsByAnimalIdQuery(Guid IdAnimal, int PageNumber = 1, int PageSize = 20, DateTime? DataInici = null, DateTime? DataFi = null) : IRequest<PaginatedResult<VisitaResumDto>>;

public partial class GetVisitsByAnimalIdQueryHandler(IVisitesRepository repository, ILogger<GetVisitsByAnimalIdQueryHandler> logger) : IRequestHandler<GetVisitsByAnimalIdQuery, PaginatedResult<VisitaResumDto>>
{

    public async Task<PaginatedResult<VisitaResumDto>> Handle(GetVisitsByAnimalIdQuery request, CancellationToken cancellationToken)
    {
        LogObtenintLlistaDeVisitesPerAnimal(request.IdAnimal);

        return await repository.GetVisitsByIdAnimalAsync(request.IdAnimal, request.PageNumber, request.PageSize, request.DataInici, request.DataFi, cancellationToken);
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Obtenint llista de visites per animal {IdAnimal}")]
    partial void LogObtenintLlistaDeVisitesPerAnimal(Guid idAnimal);
}