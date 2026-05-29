using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

/// <summary>
/// Handler per cercar medicaments humans a CIMA
/// </summary>
public class SearchHumanMedicinesQueryHandler
    : IRequestHandler<SearchHumanMedicinesQuery, List<HumanMedicineDto>>
{
    private readonly ICimaService _cimaService;
    private readonly ILogger<SearchHumanMedicinesQueryHandler> _logger;

    public SearchHumanMedicinesQueryHandler(
        ICimaService cimaService,
        ILogger<SearchHumanMedicinesQueryHandler> logger)
    {
        _cimaService = cimaService;
        _logger = logger;
    }

    public async Task<List<HumanMedicineDto>> Handle(
        SearchHumanMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Executant SearchHumanMedicinesQuery. Query: {Query}",
            request.Query);

        var results = await _cimaService.SearchMedicinesAsync(
            request.Query,
            cancellationToken);

        _logger.LogInformation(
            "Completat SearchHumanMedicinesQuery. Trobats: {Count} medicaments",
            results.Count);

        return results;
    }
}
