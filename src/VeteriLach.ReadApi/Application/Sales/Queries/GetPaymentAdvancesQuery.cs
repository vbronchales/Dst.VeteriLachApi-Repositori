using MediatR;
using VeteriLach.ReadApi.Application.Sales.DTOs;

namespace VeteriLach.ReadApi.Application.Sales.Queries;

/// <summary>
/// Query per obtenir pagaments a compte
/// </summary>
public class GetPaymentAdvancesQuery : IRequest<List<PaymentAdvanceDto>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? AnimalId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
