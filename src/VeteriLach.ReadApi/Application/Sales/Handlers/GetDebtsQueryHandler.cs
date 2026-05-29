using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Sales.DTOs;
using VeteriLach.ReadApi.Application.Sales.Queries;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Sales.Handlers;

public class GetDebtsQueryHandler : IRequestHandler<GetDebtsQuery, List<DebtDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetDebtsQueryHandler> _logger;

    public GetDebtsQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetDebtsQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<DebtDto>> Handle(GetDebtsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cercant deutes pendents. CustomerId={CustomerId}, MinDays={MinDays}",
            request.CustomerId, request.MinimumDays);

        var query = _context.FacVenda
            .Include(v => v.IdClientNavigation)
                .ThenInclude(c => c.IdClientNavigation)
                    .ThenInclude(p => p.SlcTelefons)
            .Include(v => v.IdReferenciaNavigation)
                .ThenInclude(a => a!.IdAnimalNavigation)
                    .ThenInclude(p => p.IdPacient1)
            .Where(v => v.TotalPagat < v.TotalVenda) // Només vendes no pagades completament
            .AsQueryable();

        if (request.CustomerId.HasValue)
            query = query.Where(v => v.IdClient == request.CustomerId.Value);

        if (request.MinimumAmount.HasValue)
        {
            query = query.Where(v => (v.TotalVenda - v.TotalPagat) >= request.MinimumAmount.Value);
        }

        var debts = await query
            .OrderBy(v => v.DiaVenda) // Més antics primer
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<DebtDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        // Filtrar per dies pendents després de la projecció (ja que DaysPending és calculat)
        if (request.MinimumDays.HasValue)
        {
            debts = debts.Where(d => d.DaysPending >= request.MinimumDays.Value).ToList();
        }

        _logger.LogInformation("Trobats {Count} deutes pendents", debts.Count);

        return debts;
    }
}
