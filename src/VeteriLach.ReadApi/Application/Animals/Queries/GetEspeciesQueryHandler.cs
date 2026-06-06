using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.Animals;
using VeteriLach.ReadApi.Infrastructure;


namespace VeteriLach.ReadApi.Application.Animals.Queries;

public record GetEspeciesQuery : IRequest<PaginatedResult<EspecieDto>>;

public class GetEspeciesQueryHandler(IEspecieRepository repository) : IRequestHandler<GetEspeciesQuery, PaginatedResult<EspecieDto>>
{
    public async Task<PaginatedResult<EspecieDto>> Handle(GetEspeciesQuery request, CancellationToken cancellationToken)
    {
        var especies = await repository.GetEspecies(cancellationToken);

        return especies;
    }
}
