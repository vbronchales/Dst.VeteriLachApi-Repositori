using MediatR;
using VeteriLach.ReadApi.Application.Propietaris.DTOs;

namespace VeteriLach.ReadApi.Application.Propietaris.Queries;

/// <summary>
/// Query per obtenir detall complet d'un propietari
/// </summary>
public class GetPropietariByIdQuery : IRequest<PropietariDetailDto?>
{
    public Guid IdPropietari { get; }

    public GetPropietariByIdQuery(Guid idPropietari)
    {
        IdPropietari = idPropietari;
    }
}
