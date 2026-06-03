using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface IVisitesRepository
    {
        public Task<PaginatedResult<VisitaResumatDto>> GetAnimalVisitsListAsync(Guid idAnimal, int pageNumber, int pageSize, DateTime? dataInici, DateTime? dataFi, CancellationToken cancellationToken);
        public Task<VisitaDetailDto?> GetVisitByIdAsync(Guid idVisita, CancellationToken cancellationToken);
    }
}
