using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Animals.Queries;

namespace VeteriLach.ReadApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnimalsController> _logger;

    public AnimalsController(IMediator mediator, ILogger<AnimalsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtenir una llista paginada d'animals
    /// </summary>
    /// <param name="pageNumber">Número de pàgina (per defecte: 1)</param>
    /// <param name="pageSize">Elements per pàgina (per defecte: 20, màxim: 50)</param>
    /// <param name="searchTerm">Terme de cerca (busca en nom i xip)</param>
    /// <param name="idPropietari">Filtrar per propietari</param>
    /// <param name="idEspecie">Filtrar per espècie</param>
    [HttpGet]
    public async Task<IActionResult> GetAnimals(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? searchTerm = null,
        [FromQuery] Guid? idPropietari = null,
        [FromQuery] Guid? idEspecie = null)
    {
        try
        {
            // Validar pageSize màxim
            if (pageSize > 50)
            {
                pageSize = 50;
            }

            var query = new GetAnimalsListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                IdPropietari = idPropietari,
                IdEspecie = idEspecie
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtenint llista d'animals");
            return StatusCode(500, new
            {
                success = false,
                error = "Error obtenint animals",
                message = ex.Message
            });
        }
    }

    /// <summary>
    /// Obtenir el detall d'un animal per ID
    /// </summary>
    /// <param name="id">ID de l'animal</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimal(Guid id)
    {
        try
        {
            var query = new GetAnimalByIdQuery(id);
            var animal = await _mediator.Send(query);

            if (animal == null)
            {
                return NotFound(new
                {
                    success = false,
                    error = "Animal no trobat",
                    id
                });
            }

            return Ok(new
            {
                success = true,
                data = animal
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtenint animal {Id}", id);
            return StatusCode(500, new
            {
                success = false,
                error = "Error obtenint animal",
                message = ex.Message
            });
        }
    }
}
