using VeteriLach.ReadApi.Application.Metadata.DTOs;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IRacesRepository
    {
        public Task<IEnumerable<RasaDto>> GetRaces(Guid IdEspecie, CancellationToken cancellationToken);
    }
}
