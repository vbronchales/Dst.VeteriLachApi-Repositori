using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public class GetAnimalByIdQueryHandler : IRequestHandler<GetAnimalByIdQuery, AnimalDetailDto?>
{
    private readonly VeteriLachDbContext _context;
    private readonly ILogger<GetAnimalByIdQueryHandler> _logger;

    public GetAnimalByIdQueryHandler(VeteriLachDbContext context, ILogger<GetAnimalByIdQueryHandler> logger)
    {
        _context = context;
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
            .Select(a => new AnimalDetailDto
            {
                IdAnimal = a.IdAnimal,
                Nom = a.IdAnimalNavigation.IdPacient1.Nom ?? string.Empty,
                Sexe = a.IdAnimalNavigation.IdPacient1.Sexe,
                DataNaixement = a.IdAnimalNavigation.IdPacient1.Naixement,
                Especie = a.IdRasaNavigation.IdEspecieNavigation.Nom ?? string.Empty,
                Rasa = a.IdRasaNavigation.Nom ?? string.Empty,
                Color = a.Color,
                NumXip = a.NumXip,
                Castrat = a.Castrat,
                Capa = a.Capa,
                Tatuatge = a.Tatuatge,
                Caracter = a.Caracter,
                Propietari = new PropietariDto
                {
                    IdPropietari = a.IdPropietari,
                    Nom = a.IdPropietariNavigation.IdPropietari1.Nom ?? string.Empty,
                    Cognoms = (a.IdPropietariNavigation.IdPropietari1.Cognom1 ?? "") + " " + (a.IdPropietariNavigation.IdPropietari1.Cognom2 ?? ""),
                    Email = a.IdPropietariNavigation.IdPropietari1.Email,
                    Telefon = a.IdPropietariNavigation.IdPropietari1.SlcTelefons.FirstOrDefault() != null
                        ? a.IdPropietariNavigation.IdPropietari1.SlcTelefons.FirstOrDefault()!.Numero
                        : null,
                    Adresa = a.IdPropietariNavigation.IdPropietari1.Adresa,
                    CodiPostal = a.IdPropietariNavigation.IdPropietari1.CodiPostal,
                    Poblacio = a.IdPropietariNavigation.IdPropietari1.Poblacio
                }
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (animal == null)
        {
            _logger.LogWarning("Animal amb ID {IdAnimal} no trobat", request.IdAnimal);
            return null;
        }

        _logger.LogInformation("Animal {IdAnimal} recuperat correctament: {Nom}", request.IdAnimal, animal.Nom);
        return animal;
    }
}
