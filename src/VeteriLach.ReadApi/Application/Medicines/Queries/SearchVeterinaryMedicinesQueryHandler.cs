using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;


public record SearchVeterinaryMedicinesQuery(string Query, string? Species = null) : IRequest<List<VeterinaryMedicineDto>>;

/// <summary>
/// Handler per cercar medicaments veterinaris a CimaVet
/// </summary>
public partial class SearchVeterinaryMedicinesQueryHandler(ICimaVetService cimaVetService, ILogger<SearchVeterinaryMedicinesQueryHandler> logger)
    : IRequestHandler<SearchVeterinaryMedicinesQuery, List<VeterinaryMedicineDto>>
{
    public async Task<List<VeterinaryMedicineDto>> Handle(
        SearchVeterinaryMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        LogExecutingSearchVeterinaryMedicinesQuery(request.Query, request.Species);

        var results = await cimaVetService.SearchMedicinesAsync(
            request.Query,
            request.Species,
            cancellationToken);

        LogCompletedSearchVeterinaryMedicinesQuery(results.Count);

        return results;
    }
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Executant SearchVeterinaryMedicinesQuery. Query: {Query}, Espècie: {Species}")]
    partial void LogExecutingSearchVeterinaryMedicinesQuery(string query, string? species);
    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Completat SearchVeterinaryMedicinesQuery. Trobats: {Count} medicaments")]
    partial void LogCompletedSearchVeterinaryMedicinesQuery(int count);
}
