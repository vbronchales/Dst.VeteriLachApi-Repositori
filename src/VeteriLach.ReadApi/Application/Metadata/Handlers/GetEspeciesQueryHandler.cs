using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Metadata.DTOs;
using VeteriLach.ReadApi.Application.Metadata.Queries;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Metadata.Handlers;

public class GetEspeciesQueryHandler : IRequestHandler<GetEspeciesQuery, List<EspecieDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly ILogger<GetEspeciesQueryHandler> _logger;

    public GetEspeciesQueryHandler(
        VeteriLachDbContext context,
        ILogger<GetEspeciesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<EspecieDto>> Handle(GetEspeciesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executant GetEspeciesQuery");

        var especies = await _context.VetEspecies
            .Select(e => new EspecieDto
            {
                IdEspecie = e.IdEspecie,
                Nom = e.Nom,
                TipusEspecie = e.TipusEspecie,
                TotalAnimals = e.VetRasas
                    .SelectMany(r => r.VetAnimals)
                    .Count()
            })
            .OrderByDescending(e => e.TotalAnimals)
            .ThenBy(e => e.Nom)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Trobades {Count} espècies", especies.Count);

        return especies;
    }
}
