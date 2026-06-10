using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public record GetPropietarisListQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 20, string? Poblacio = null) : IRequest<PaginatedResult<PropietariListDto>>;

/// <summary>
/// Handler per obtenir llista paginada de propietaris
/// </summary>
public class GetPropietarisListQueryHandler(IPropietariRepository repository) : IRequestHandler<GetPropietarisListQuery, PaginatedResult<PropietariListDto>>
{
    public async Task<PaginatedResult<PropietariListDto>> Handle(GetPropietarisListQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetPropietarisListAsync(request.SearchTerm, request.Poblacio, request.PageNumber, request.PageSize, cancellationToken);
        return result;
    }
}
