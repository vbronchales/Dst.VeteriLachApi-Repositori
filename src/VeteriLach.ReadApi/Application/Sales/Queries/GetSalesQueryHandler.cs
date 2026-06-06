using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure;

namespace VeteriLach.ReadApi.Application.Sales.Queries;

public record GetSalesQuery(DateTime? StartDate = null, DateTime? EndDate = null, Guid? CustomerId = null, int PageNumber = 1, int PageSize = 50) : IRequest<PaginatedResult<SaleDto>>;

public class GetSalesQueryHandler(ISalesRepository repository) : IRequestHandler<GetSalesQuery, PaginatedResult<SaleDto>>
{
    public async Task<PaginatedResult<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executant GetSalesQuery amb filtres: StartDate={StartDate}, EndDate={EndDate}, CustomerId={CustomerId}",
            request.StartDate, request.EndDate, request.CustomerId);

        var query = _context.FacVenda
            .Include(v => v.IdClientNavigation)
            .Include(v => v.IdVenedorNavigation)
            .Include(v => v.IdCaixaNavigation)
            .Include(v => v.IdReferenciaNavigation)
            .Include(v => v.FacArticleVenuts)
            .AsQueryable();

        // Filtres
        if (request.StartDate.HasValue)
            query = query.Where(v => v.DiaVenda >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(v => v.DiaVenda <= request.EndDate.Value);

        if (request.CustomerId.HasValue)
            query = query.Where(v => v.IdClient == request.CustomerId.Value);

        if (request.SellerId.HasValue)
            query = query.Where(v => v.IdVenedor == request.SellerId.Value);

        if (request.AnimalId.HasValue)
            query = query.Where(v => v.IdReferencia == request.AnimalId.Value);

        if (request.OnlyPending == true)
            query = query.Where(v => v.TotalPagat < v.TotalVenda);

        if (request.OnlyPaid == true)
            query = query.Where(v => v.TotalPagat >= v.TotalVenda);

        // Paginació
        var sales = await query
            .OrderByDescending(v => v.DiaVenda)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Trobades {Count} vendes", sales.Count);

        return sales;
    }
}
