using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.Propietaris.DTOs;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IPropietariRepository
    {
        public Task<PropietariDetailDto?> GetPropietariByIdAsync(Guid idPropietari, CancellationToken cancellationToken);
        public Task<PaginatedResult<PropietariListDto>> GetPropietarisListAsync(string? searchTerm, string? poblacio, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
