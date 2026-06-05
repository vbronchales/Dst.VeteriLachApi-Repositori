using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.Sales.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Infrastructure
{
    public partial class SalesRepository(VeteriLachDbContext context, ILogger<SalesRepository> logger) : ISalesRepository
    {
        public async Task<PaginatedResult<SaleDto>> GetSalesAsync(DateTime? startDate, DateTime? endDate, Guid? customerId, Guid? sellerId, Guid? animalId, bool? onlyPending, bool? onlyPaid, int pageNumber, int pageSize)
        {
            LogGetSalesAsyncFilters(startDate, endDate, customerId);
            var query = context.FacVenda
                .Include(v => v.IdClientNavigation)
                .Include(v => v.IdVenedorNavigation)
                .Include(v => v.IdCaixaNavigation)
                .Include(v => v.IdReferenciaNavigation)
                .Include(v => v.FacArticleVenuts)
                .AsQueryable();
            // Filtres
            if (startDate.HasValue)
                query = query.Where(v => v.DiaVenda >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(v => v.DiaVenda <= endDate.Value);
            if (customerId.HasValue)
                query = query.Where(v => v.IdClient == customerId.Value);
            if (sellerId.HasValue)
                query = query.Where(v => v.IdVenedor == sellerId.Value);
            if (animalId.HasValue)
                query = query.Where(v => v.IdReferencia == animalId.Value);
            if (onlyPending == true)
                query = query.Where(v => v.TotalPagat < v.TotalVenda);
            if (onlyPaid == true)
                query = query.Where(v => v.TotalPagat >= v.TotalVenda);
            // Paginació
            var totalCount = await query.CountAsync();
            var sales = await query
                .OrderByDescending(v => v.DiaVenda)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => s.ToSaleDto())
                .ToListAsync();
            logger.LogInformation("Trobades {Count} vendes", sales.Count);
            return new PaginatedResult<SaleDto>(sales, totalCount, pageNumber, pageSize);
        }

        [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Executant GetSalesAsync amb filtres: StartDate={StartDate}, EndDate={EndDate}, CustomerId={CustomerId}")]
        partial void LogGetSalesAsyncFilters(DateTime? startDate, DateTime? endDate, Guid? customerId);
    }
}
