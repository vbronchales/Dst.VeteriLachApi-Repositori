using MediatR;
using VeteriLach.ReadApi.Application.Sales.DTOs;

namespace VeteriLach.ReadApi.Application.Sales.Queries;

/// <summary>
/// Query per obtenir detall complet d'una venda
/// </summary>
public class GetSaleDetailQuery : IRequest<SaleDetailDto?>
{
    public Guid SaleId { get; set; }
}
