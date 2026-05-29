using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Sales.DTOs;
using VeteriLach.ReadApi.Application.Sales.Queries;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Sales.Handlers;

public class GetPaymentAdvancesQueryHandler : IRequestHandler<GetPaymentAdvancesQuery, List<PaymentAdvanceDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPaymentAdvancesQueryHandler> _logger;

    public GetPaymentAdvancesQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetPaymentAdvancesQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<PaymentAdvanceDto>> Handle(GetPaymentAdvancesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cercant pagaments a compte. StartDate={StartDate}, EndDate={EndDate}, CustomerId={CustomerId}",
            request.StartDate, request.EndDate, request.CustomerId);

        var query = _context.FacAcomptes
            .Include(a => a.IdClientNavigation)
            .Include(a => a.IdVenedorNavigation)
            .Include(a => a.IdCaixaNavigation)
            .Include(a => a.IdReferenciaNavigation)
            .AsQueryable();

        if (request.StartDate.HasValue)
            query = query.Where(a => a.DiaAcompte >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(a => a.DiaAcompte <= request.EndDate.Value);

        if (request.CustomerId.HasValue)
            query = query.Where(a => a.IdClient == request.CustomerId.Value);

        if (request.AnimalId.HasValue)
            query = query.Where(a => a.IdReferencia == request.AnimalId.Value);

        var advances = await query
            .OrderByDescending(a => a.DiaAcompte)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<PaymentAdvanceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Trobats {Count} pagaments a compte", advances.Count);

        return advances;
    }
}
