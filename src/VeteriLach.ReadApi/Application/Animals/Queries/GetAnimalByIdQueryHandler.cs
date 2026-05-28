using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public class GetAnimalByIdQueryHandler : IRequestHandler<GetAnimalByIdQuery, AnimalDetailDto?>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAnimalByIdQueryHandler> _logger;

    public GetAnimalByIdQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetAnimalByIdQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AnimalDetailDto?> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtenint detall de l'animal {IdAnimal}", request.IdAnimal);

        var animal = await _context.VetAnimals
            .Include(a => a.IdAnimalNavigation)
                .ThenInclude(p => p.IdPacient1)
            .Include(a => a.IdRasaNavigation)
                .ThenInclude(r => r.IdEspecieNavigation)
            .Include(a => a.IdPropietariNavigation)
                .ThenInclude(p => p.IdPropietari1)
                    .ThenInclude(s => s.SlcTelefons)
            .Where(a => a.IdAnimal == request.IdAnimal)
            .FirstOrDefaultAsync(cancellationToken);

        if (animal == null)
        {
            _logger.LogWarning("Animal amb ID {IdAnimal} no trobat", request.IdAnimal);
            return null;
        }

        _logger.LogInformation("Animal {IdAnimal} recuperat correctament: {Nom}", 
            request.IdAnimal, animal.IdAnimalNavigation.IdPacient1.Nom);

        // Utilitzar AutoMapper per mapejar l'entitat al DTO
        return _mapper.Map<AnimalDetailDto>(animal);
    }
}
