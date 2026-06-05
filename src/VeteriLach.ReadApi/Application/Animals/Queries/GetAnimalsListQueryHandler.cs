using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.Animals;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public record GetAnimalsListQuery(int PageNumber, int PageSize, string? SearchTerm, Guid? IdPropietari, Guid? IdEspecie) : IRequest<PaginatedResult<AnimalListDto>>;

public partial class GetAnimalsListQueryHandler(IAnimalRepository repository, ILogger<GetAnimalsListQueryHandler> logger) : IRequestHandler<GetAnimalsListQuery, PaginatedResult<AnimalListDto>>
{
    public async Task<PaginatedResult<AnimalListDto>> Handle(GetAnimalsListQuery request, CancellationToken cancellationToken)
    {
        LogGettingAnimalsList(request.PageNumber, request.PageSize, request.SearchTerm);

        var result = await repository.GetAnimalsList(request.PageNumber, request.PageSize, request.SearchTerm, request.IdPropietari, request.IdEspecie, cancellationToken);

        LogRetrievedAnimalsList(result.Data.Count, result.Pagination.TotalItems);
        return result;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Obtenint llista d'animals. Page: {PageNumber}, Size: {PageSize}, Search: {SearchTerm}")]
    partial void LogGettingAnimalsList(int pageNumber, int pageSize, string? searchTerm);
    [LoggerMessage(Level = LogLevel.Information, Message = "S'han recuperat {Count} animals de {TotalItems} totals")]
    partial void LogRetrievedAnimalsList(int count, int totalItems);
}
