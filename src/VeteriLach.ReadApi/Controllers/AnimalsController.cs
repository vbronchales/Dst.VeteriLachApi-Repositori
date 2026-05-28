using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly VeteriLachDbContext _context;
    private readonly ILogger<AnimalsController> _logger;

    public AnimalsController(VeteriLachDbContext context, ILogger<AnimalsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Obtenir els primers 10 animals de la base de dades (prova)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAnimals()
    {
        try
        {
            var animals = await _context.VetAnimals
                .Include(a => a.IdAnimalNavigation)
                    .ThenInclude(p => p.IdPacient1)
                .Include(a => a.IdRasaNavigation)
                    .ThenInclude(r => r.IdEspecieNavigation)
                .Take(10)
                .Select(a => new
                {
                    a.IdAnimal,
                    Nom = a.IdAnimalNavigation.IdPacient1.Nom,
                    Sexe = a.IdAnimalNavigation.IdPacient1.Sexe,
                    DataNaixement = a.IdAnimalNavigation.IdPacient1.Naixement,
                    Especie = a.IdRasaNavigation.IdEspecieNavigation.Nom,
                    Rasa = a.IdRasaNavigation.Nom,
                    a.Color,
                    a.NumXip,
                    a.Castrat
                })
                .ToListAsync();

            _logger.LogInformation("S'han recuperat {Count} animals de la base de dades", animals.Count);

            return Ok(new
            {
                success = true,
                count = animals.Count,
                data = animals
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error recuperant animals de la base de dades");
            return StatusCode(500, new
            {
                success = false,
                error = "Error recuperant animals",
                message = ex.Message
            });
        }
    }

    /// <summary>
    /// Obtenir un animal per ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimal(Guid id)
    {
        try
        {
            var animal = await _context.VetAnimals
                .Include(a => a.IdAnimalNavigation)
                    .ThenInclude(p => p.IdPacient1)
                .Include(a => a.IdRasaNavigation)
                    .ThenInclude(r => r.IdEspecieNavigation)
                .Include(a => a.IdPropietariNavigation)
                    .ThenInclude(p => p.IdPropietari1)
                .Where(a => a.IdAnimal == id)
                .Select(a => new
                {
                    a.IdAnimal,
                    Nom = a.IdAnimalNavigation.IdPacient1.Nom,
                    Sexe = a.IdAnimalNavigation.IdPacient1.Sexe,
                    DataNaixement = a.IdAnimalNavigation.IdPacient1.Naixement,
                    Especie = a.IdRasaNavigation.IdEspecieNavigation.Nom,
                    Rasa = a.IdRasaNavigation.Nom,
                    a.Color,
                    a.NumXip,
                    a.Castrat,
                    Propietari = new
                    {
                        a.IdPropietari,
                        Nom = a.IdPropietariNavigation.IdPropietari1.Nom,
                        Cognoms = a.IdPropietariNavigation.IdPropietari1.Cognom1 + " " + a.IdPropietariNavigation.IdPropietari1.Cognom2,
                        Email = a.IdPropietariNavigation.IdPropietari1.Email,
                        Telefon = a.IdPropietariNavigation.IdPropietari1.SlcTelefons.FirstOrDefault()
                    }
                })
                .FirstOrDefaultAsync();

            if (animal == null)
            {
                _logger.LogWarning("Animal amb ID {Id} no trobat", id);
                return NotFound(new
                {
                    success = false,
                    error = "Animal no trobat",
                    id
                });
            }

            _logger.LogInformation("Animal {Id} recuperat correctament", id);

            return Ok(new
            {
                success = true,
                data = animal
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error recuperant animal {Id}", id);
            return StatusCode(500, new
            {
                success = false,
                error = "Error recuperant animal",
                message = ex.Message
            });
        }
    }
}
