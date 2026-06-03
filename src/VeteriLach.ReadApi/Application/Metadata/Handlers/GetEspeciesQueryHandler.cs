using MediatR;
using VeteriLach.ReadApi.Application.Metadata.DTOs;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Metadata.Handlers;

public record GetEspeciesQuery : IRequest<List<EspecieDto>>;

public class GetEspeciesQueryHandler(IEspecieRepository repository) : IRequestHandler<GetEspeciesQuery, List<EspecieDto>>
{
    public async Task<List<EspecieDto>> Handle(GetEspeciesQuery request, CancellationToken cancellationToken)
    {
        var especies = await repository.GetEspecies(cancellationToken);

        return especies.ToList();
    }
}
