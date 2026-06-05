using MediatR;
using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public record GetPropietariByIdQuery(Guid IdPropietari) : IRequest<PropietariDetailDto?>;

/// <summary>
/// Handler per obtenir detall complet d'un propietari
/// </summary>
public class GetPropietariByIdQueryHandler(IPropietariRepository repository) : IRequestHandler<GetPropietariByIdQuery, PropietariDetailDto?>
{
    public async Task<PropietariDetailDto?> Handle(GetPropietariByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetPropietariByIdAsync(request.IdPropietari, cancellationToken);
        return result;
    }
}
