using MediatR;
using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public record GetAnimalByIdQuery(Guid IdAnimal) : IRequest<AnimalDetailDto?>;

public partial class GetAnimalByIdQueryHandler( IAnimalRepository repository, ILogger<GetAnimalByIdQueryHandler> logger) : IRequestHandler<GetAnimalByIdQuery, AnimalDetailDto?>
{
    public async Task<AnimalDetailDto?> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        LogGettingAnimalDetail(request.IdAnimal);

        var animal = await repository.GetAnimalById(request.IdAnimal, cancellationToken);
        
        if (animal == null)
        {
            LogAnimalNotFound(request.IdAnimal);
            return null;
        }

        LogAnimalRetrieved(request.IdAnimal, animal.Nom);
        return animal;
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Getting details for animal {IdAnimal}")]
    private partial void LogGettingAnimalDetail(Guid IdAnimal);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Animal {IdAnimal} not found")]
    private partial void LogAnimalNotFound(Guid IdAnimal);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Animal {IdAnimal} retrieved successfully: {Nom}")]
    private partial void LogAnimalRetrieved(Guid IdAnimal, string Nom);
}
