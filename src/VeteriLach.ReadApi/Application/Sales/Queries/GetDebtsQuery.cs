using MediatR;
using VeteriLach.ReadApi.Application.Sales.DTOs;

namespace VeteriLach.ReadApi.Application.Sales.Queries;

/// <summary>
/// Query per obtenir deutes pendents
/// </summary>
public class GetDebtsQuery : IRequest<List<DebtDto>>
{
    public Guid? CustomerId { get; set; }
    public int? MinimumDays { get; set; }
    public decimal? MinimumAmount { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
