using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Sales.DTOs;
using VeteriLach.ReadApi.Application.Sales.Queries;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Sales.Handlers;

public class GetSaleDetailQueryHandler : IRequestHandler<GetSaleDetailQuery, SaleDetailDto?>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleDetailQueryHandler> _logger;

    public GetSaleDetailQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetSaleDetailQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleDetailDto?> Handle(GetSaleDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtenint detall de venda {SaleId}", request.SaleId);

        var sale = await _context.FacVenda
            .Include(v => v.IdClientNavigation)
                .ThenInclude(c => c.IdClientNavigation)
                    .ThenInclude(p => p.SlcTelefons)
            .Include(v => v.IdVenedorNavigation)
                .ThenInclude(v => v.IdVenedorNavigation)
            .Include(v => v.IdCaixaNavigation)
                .ThenInclude(c => c.IdEntitatBancariaNavigation)
            .Include(v => v.IdReferenciaNavigation)
                .ThenInclude(a => a!.IdAnimalNavigation)
                    .ThenInclude(p => p.IdPacient1)
            .Include(v => v.IdReferenciaNavigation)
                .ThenInclude(a => a!.IdRasaNavigation)
                    .ThenInclude(r => r.IdEspecieNavigation)
            .Include(v => v.FacArticleVenuts.OrderBy(a => a.Ordre))
                .ThenInclude(a => a.IdArticleNavigation)
            .FirstOrDefaultAsync(v => v.IdVenda == request.SaleId, cancellationToken);

        if (sale == null)
        {
            _logger.LogWarning("Venda {SaleId} no trobada", request.SaleId);
            return null;
        }

        var saleDetail = _mapper.Map<SaleDetailDto>(sale);

        _logger.LogInformation("Detall de venda {SaleId} obtingut amb {ItemCount} articles",
            request.SaleId, saleDetail.Items.Count);

        return saleDetail;
    }
}
