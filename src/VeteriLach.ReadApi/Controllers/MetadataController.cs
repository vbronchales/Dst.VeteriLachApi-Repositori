using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Metadata.DTOs;
using VeteriLach.ReadApi.Application.Metadata.Queries;

namespace VeteriLach.ReadApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MetadataController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MetadataController> _logger;

    public MetadataController(IMediator mediator, ILogger<MetadataController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obté totes les espècies disponibles amb comptador d'animals
    /// </summary>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista d'espècies ordenades per nombre d'animals</returns>
    [HttpGet("especies")]
    [ProducesResponseType(typeof(List<EspecieDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEspecies(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obtenint llistat d'espècies");

        var query = new GetEspeciesQuery();
        var especies = await _mediator.Send(query, cancellationToken);

        return Ok(especies);
    }

    /// <summary>
    /// Obté totes les races, opcionalment filtrades per espècie
    /// </summary>
    /// <param name="especie">Filtre per espècie (GUID o nom parcial)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de races</returns>
    [HttpGet("rases")]
    [ProducesResponseType(typeof(List<RasaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRases(
        [FromQuery] string? especie = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obtenint llistat de races amb filtre={Especie}", especie);

        var query = new GetRasesQuery { Especie = especie };
        var rases = await _mediator.Send(query, cancellationToken);

        return Ok(rases);
    }
}
