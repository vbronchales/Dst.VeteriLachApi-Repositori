using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.MedicalHistory;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public record GetRecentVisitsQuery(int Days, int PageNumber = 1, int PageSize = 20, DateTime? DataInici = null, DateTime? DataFi = null) : IRequest<PaginatedResult<VisitaResumDto>>;

public partial class GetRecentVisitsQueryHandler(IVisitesRepository repository, ILogger<GetRecentVisitsQueryHandler> logger) : IRequestHandler<GetRecentVisitsQuery, PaginatedResult<VisitaResumDto>>
{

    public async Task<PaginatedResult<VisitaResumDto>> Handle(GetRecentVisitsQuery request, CancellationToken cancellationToken)
    {
        LogObtenintLlistaDeVisitesRecents(request.Days);

        return await repository.GetRecentVisitsAsync(request.Days, request.PageNumber, request.PageSize, cancellationToken);
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Obtenint llista de visites recents dels darrers {Dies} dies")]
    partial void LogObtenintLlistaDeVisitesRecents(int dies);
}