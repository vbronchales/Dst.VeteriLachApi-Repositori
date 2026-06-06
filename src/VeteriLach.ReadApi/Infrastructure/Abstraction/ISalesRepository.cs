using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain;

namespace VeteriLach.ReadApi.Infrastructure
{
    public interface ISalesRepository
    {
        public PaginatedResult<SaleDto> GetSales(DateTime? startDate = null, DateTime? endDate = null, Guid? customerId = null, Guid? sellerId = null, Guid? animalId = null, bool? onlyPending = null, bool? onlyPaid = null, int pageNumber = 1, int pageSize = 50);
    }
}
