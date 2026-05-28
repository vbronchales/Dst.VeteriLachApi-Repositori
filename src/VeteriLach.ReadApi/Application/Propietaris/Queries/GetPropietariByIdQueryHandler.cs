using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeteriLach.ReadApi.Application.Propietaris.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Propietaris.Queries;

/// <summary>
/// Handler per obtenir detall complet d'un propietari
/// </summary>
public class GetPropietariByIdQueryHandler : IRequestHandler<GetPropietariByIdQuery, PropietariDetailDto?>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPropietariByIdQueryHandler> _logger;

    public GetPropietariByIdQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetPropietariByIdQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PropietariDetailDto?> Handle(GetPropietariByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtenint detall del propietari {IdPropietari}", request.IdPropietari);

        var propietari = await _context.VetPropietaris
            .Include(p => p.IdPropietari1)
                .ThenInclude(sp => sp.SlcTelefons)
            .Include(p => p.VetAnimals)
                .ThenInclude(a => a.IdAnimalNavigation)
                    .ThenInclude(pac => pac.IdPacient1)
            .Include(p => p.VetAnimals)
                .ThenInclude(a => a.IdRasaNavigation)
                    .ThenInclude(r => r.IdEspecieNavigation)
            .FirstOrDefaultAsync(p => p.IdPropietari == request.IdPropietari, cancellationToken);

        if (propietari == null)
        {
            _logger.LogWarning("Propietari {IdPropietari} no trobat", request.IdPropietari);
            return null;
        }

        var result = _mapper.Map<PropietariDetailDto>(propietari);

        _logger.LogInformation("Propietari {IdPropietari} trobat amb {TotalAnimals} animals", 
            request.IdPropietari, result.Animals.Count);

        return result;
    }
}
