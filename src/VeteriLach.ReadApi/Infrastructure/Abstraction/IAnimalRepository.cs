using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Application.Common.Models;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IAnimalRepository
    {
        public Task<AnimalDetailDto?> GetAnimalById(Guid idAnimal, CancellationToken cancellationToken);

        public Task<PaginatedResult<AnimalListDto>> GetAnimalsList(int pageNumber, int pageSize, string? searchTerm, Guid? idPropietari, Guid? idEspecie, CancellationToken cancellationToken);
    }
}
