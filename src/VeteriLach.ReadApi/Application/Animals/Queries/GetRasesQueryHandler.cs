using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.Animals;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public record GetRasesQuery(string? Especie) : IRequest<PaginatedResult<RasaDto>>;

public class GetRasesQueryHandler(IRacesRepository repository) : IRequestHandler<GetRasesQuery, PaginatedResult<RasaDto>>
{
    public async Task<PaginatedResult<RasaDto>> Handle(GetRasesQuery request, CancellationToken cancellationToken)
    {
        if(!Guid.TryParse(request.Especie, out var idEspecie))
        {
            return new PaginatedResult<RasaDto>(new List<RasaDto>(), 0, 0, 0);
        }
        return await repository.GetRaces(idEspecie, cancellationToken);
    }
}
