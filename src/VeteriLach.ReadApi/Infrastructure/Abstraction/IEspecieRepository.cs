using VeteriLach.ReadApi.Application.Metadata.DTOs;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IEspecieRepository
    {
        public Task<IEnumerable<EspecieDto>> GetEspecies(CancellationToken cancellationToken);
    }
}
