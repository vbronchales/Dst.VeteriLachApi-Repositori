using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface ISalesRepository
    {
        public Task<PaginatedResult<SaleDto>> GetSalesAsync(DateTime? startDate, DateTime? endDate, Guid? custormerId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
