using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.Animals;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IRacesRepository
    {
        public Task<PaginatedResult<RasaDto>> GetRaces(Guid IdEspecie, CancellationToken cancellationToken);
    }
}
