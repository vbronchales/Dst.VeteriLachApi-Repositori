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
        var result  = await repository.GetSalesAsync(request.StartDate, request.EndDate, request.CustomerId, request.PageNumber, request.PageSize, cancellationToken);
        return result;
    }
}
