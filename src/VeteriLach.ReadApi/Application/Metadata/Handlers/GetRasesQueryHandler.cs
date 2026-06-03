using MediatR;
using VeteriLach.ReadApi.Application.Metadata.DTOs;
using VeteriLach.ReadApi.Application.Metadata.Queries;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Metadata.Handlers;

public class GetRasesQueryHandler(IRacesRepository repository) : IRequestHandler<GetRasesQuery, List<RasaDto>>
{
    public async Task<List<RasaDto>> Handle(GetRasesQuery request, CancellationToken cancellationToken)
    {
        if(!Guid.TryParse(request.Especie, out var idEspecie))
        {
            return new List<RasaDto>();
        }
        var races = await repository.GetRaces(idEspecie, cancellationToken);
        return races.ToList();
    }
}
