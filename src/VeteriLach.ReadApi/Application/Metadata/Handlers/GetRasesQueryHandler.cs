using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Metadata.DTOs;
using VeteriLach.ReadApi.Application.Metadata.Queries;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Metadata.Handlers;

public class GetRasesQueryHandler : IRequestHandler<GetRasesQuery, List<RasaDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly ILogger<GetRasesQueryHandler> _logger;

    public GetRasesQueryHandler(
        VeteriLachDbContext context,
        ILogger<GetRasesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<RasaDto>> Handle(GetRasesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executant GetRasesQuery amb filtre Especie={Especie}", request.Especie);

        var query = _context.VetRasas
            .Include(r => r.IdEspecieNavigation)
            .AsQueryable();

        // Aplicar filtre per espècie si s'especifica
        if (!string.IsNullOrWhiteSpace(request.Especie))
        {
            // Intentar parsejar com a GUID
            if (Guid.TryParse(request.Especie, out var especieGuid))
            {
                query = query.Where(r => r.IdEspecie == especieGuid);
            }
            else
            {
                // Cercar per nom d'espècie (case-insensitive)
                query = query.Where(r => r.IdEspecieNavigation.Nom.ToLower().Contains(request.Especie.ToLower()));
            }
        }

        var rases = await query
            .Select(r => new RasaDto
            {
                IdRasa = r.IdRasa,
                Nom = r.Nom,
                IdEspecie = r.IdEspecie,
                NomEspecie = r.IdEspecieNavigation.Nom,
                TotalAnimals = r.VetAnimals.Count,
                TamanyRelatiu = r.TamanyRelatiu
            })
            .OrderBy(r => r.NomEspecie)
            .ThenByDescending(r => r.TotalAnimals)
            .ThenBy(r => r.Nom)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Trobades {Count} races", rases.Count);

        return rases;
    }
}
