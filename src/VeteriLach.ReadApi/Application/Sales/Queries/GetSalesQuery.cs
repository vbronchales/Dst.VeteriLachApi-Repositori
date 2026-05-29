using MediatR;
using VeteriLach.ReadApi.Application.Sales.DTOs;

namespace VeteriLach.ReadApi.Application.Sales.Queries;

/// <summary>
/// Query per obtenir llista de vendes amb filtres i paginació
/// </summary>
public class GetSalesQuery : IRequest<List<SaleDto>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? SellerId { get; set; }
    public Guid? AnimalId { get; set; }
    public bool? OnlyPending { get; set; }
    public bool? OnlyPaid { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
