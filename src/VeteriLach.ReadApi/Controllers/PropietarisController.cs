using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Propietaris.DTOs;
using VeteriLach.ReadApi.Application.Propietaris.Queries;

namespace VeteriLach.ReadApi.Controllers;

/// <summary>
/// Controller per a la gestió de propietaris
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropietarisController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PropietarisController> _logger;

    public PropietarisController(IMediator mediator, ILogger<PropietarisController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obté una llista paginada de propietaris amb filtres opcionals
    /// </summary>
    /// <param name="query">Paràmetres de consulta (paginació i filtres)</param>
    /// <returns>Llista paginada de propietaris</returns>
    /// <response code="200">Llista de propietaris obtinguda correctament</response>
    /// <response code="400">Paràmetres de consulta invàlids</response>
    /// <response code="401">API Key no vàlida o absent</response>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPropietaris([FromQuery] GetPropietarisListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Obté el detall complet d'un propietari per ID
    /// </summary>
    /// <param name="id">ID del propietari</param>
    /// <returns>Detall del propietari amb animals i telèfons</returns>
    /// <response code="200">Propietari trobat</response>
    /// <response code="404">Propietari no trobat</response>
    /// <response code="401">API Key no vàlida o absent</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PropietariDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPropietari(Guid id)
    {
        var query = new GetPropietariByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Propietari {IdPropietari} no trobat", id);
            return NotFound(new { message = $"Propietari amb ID {id} no trobat" });
        }

        return Ok(result);
    }
}
