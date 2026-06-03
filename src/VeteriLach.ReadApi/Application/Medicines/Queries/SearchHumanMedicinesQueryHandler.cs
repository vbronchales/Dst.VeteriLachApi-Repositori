using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;


public record SearchHumanMedicinesQuery(string Query) : IRequest<List<HumanMedicineDto>>;
/// <summary>
/// Handler per cercar medicaments humans a CIMA
/// </summary>
public partial class SearchHumanMedicinesQueryHandler(ICimaService cimaService, ILogger<SearchHumanMedicinesQueryHandler> logger)
    : IRequestHandler<SearchHumanMedicinesQuery, List<HumanMedicineDto>>
{
    public async Task<List<HumanMedicineDto>> Handle(
        SearchHumanMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        LogExecutingSearchHumanMedicinesQuery(request.Query);
        
        var results = await cimaService.SearchMedicinesAsync(
            request.Query,
            cancellationToken);

        LogCompletedSearchHumanMedicinesQuery(results.Count);

        return results;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Executant SearchHumanMedicinesQuery. Query: {Query}")]
    partial void LogExecutingSearchHumanMedicinesQuery(string query);
    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Completat SearchHumanMedicinesQuery. Trobats: {Count} medicaments")]
    partial void LogCompletedSearchHumanMedicinesQuery(int count);
}
