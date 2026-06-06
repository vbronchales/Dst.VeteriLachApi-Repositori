using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.Animals;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IEspecieRepository
    {
        public Task<PaginatedResult<EspecieDto>> GetEspecies(CancellationToken cancellationToken);
    }
}
