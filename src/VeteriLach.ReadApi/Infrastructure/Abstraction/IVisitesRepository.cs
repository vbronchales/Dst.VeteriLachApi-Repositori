using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain.MedicalHistory;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IVisitesRepository
    {
        public Task<PaginatedResult<VisitaResumDto>> GetVisitsByIdAnimalAsync(Guid idAnimal, int pageNumber, int pageSize, DateTime? dataInici, DateTime? dataFi, CancellationToken cancellationToken);
        public Task<VisitaDetailDto?> GetVisitByIdAsync(Guid idVisita, CancellationToken cancellationToken);

        public Task<PaginatedResult<VisitaResumDto>> GetRecentVisitsAsync(int days, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
