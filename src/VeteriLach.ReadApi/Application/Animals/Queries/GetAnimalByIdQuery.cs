using MediatR;
using VeteriLach.ReadApi.Application.Animals.DTOs;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

/// <summary>
/// Query per obtenir el detall d'un animal per ID
/// </summary>
public class GetAnimalByIdQuery : IRequest<AnimalDetailDto?>
{
    public Guid IdAnimal { get; set; }

    public GetAnimalByIdQuery(Guid idAnimal)
    {
        IdAnimal = idAnimal;
    }
}
