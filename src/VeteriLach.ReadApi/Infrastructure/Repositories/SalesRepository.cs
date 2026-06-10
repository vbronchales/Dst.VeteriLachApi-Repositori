using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure.Data;
using VeteriLach.ReadApi.Mapper;

namespace VeteriLach.ReadApi.Infrastructure
{
    public partial class SalesRepository(VeteriLachDbContext context, ILogger<SalesRepository> logger) : ISalesRepository
    {
        public async Task<PaginatedResult<SaleDto>> GetSalesAsync(DateTime? startDate, DateTime? endDate, Guid? customerId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            LogGetSalesAsyncFilters(startDate, endDate);
            
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
                query = query.Where(v => v.TotalPagat >= v.TotalVenda);

            // Paginació
            var sales = await query
                .OrderByDescending(v => v.DiaVenda)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(v => v.ToSalesDto())
                .ToListAsync();
            
            LogGetSalesAsyncResult(sales.Count);
            return new PaginatedResult<SaleDto>(sales, sales.Count, pageNumber, pageSize);
        }

        [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Executant GetSalesAsync amb filtres: StartDate={StartDate}, EndDate={EndDate}")]
        partial void LogGetSalesAsyncFilters(DateTime? startDate, DateTime? endDate);
        [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Trobades {Count} vendes")]
        partial void LogGetSalesAsyncResult(int count); 
    }
}
