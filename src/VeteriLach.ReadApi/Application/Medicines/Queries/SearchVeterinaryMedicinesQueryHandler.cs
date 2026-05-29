using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

/// <summary>
/// Handler per cercar medicaments veterinaris a CimaVet
/// </summary>
public class SearchVeterinaryMedicinesQueryHandler
    : IRequestHandler<SearchVeterinaryMedicinesQuery, List<VeterinaryMedicineDto>>
{
    private readonly ICimaVetService _cimaVetService;
    private readonly ILogger<SearchVeterinaryMedicinesQueryHandler> _logger;

    public SearchVeterinaryMedicinesQueryHandler(
        ICimaVetService cimaVetService,
        ILogger<SearchVeterinaryMedicinesQueryHandler> logger)
    {
        _cimaVetService = cimaVetService;
        _logger = logger;
    }

    public async Task<List<VeterinaryMedicineDto>> Handle(
        SearchVeterinaryMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Executant SearchVeterinaryMedicinesQuery. Query: {Query}, Espècie: {Species}",
            request.Query,
            request.Species);

        var results = await _cimaVetService.SearchMedicinesAsync(
            request.Query,
            request.Species,
            cancellationToken);

        _logger.LogInformation(
            "Completat SearchVeterinaryMedicinesQuery. Trobats: {Count} medicaments",
            results.Count);

        return results;
    }
}
