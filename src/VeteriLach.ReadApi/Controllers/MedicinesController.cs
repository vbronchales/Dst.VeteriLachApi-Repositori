using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Medicines.Queries;

namespace VeteriLach.ReadApi.Controllers;

/// <summary>
/// Controller per a consultes de medicaments (veterinaris i humans)
/// </summary>
[ApiController]
[Route("api/medicines")]
[Produces("application/json")]
public class MedicinesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MedicinesController> _logger;

    public MedicinesController(
        IMediator mediator,
        ILogger<MedicinesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Cerca medicaments veterinaris a CimaVet
    /// </summary>
    /// <param name="query">Text de cerca (nom, principi actiu, codi)</param>
    /// <param name="species">Espècie animal (opcional)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de medicaments veterinaris trobats</returns>
    /// <response code="200">Retorna la llista de medicaments trobats</response>
    /// <response code="400">Paràmetres de cerca invàlids</response>
    /// <response code="401">API Key mancant o invàlida</response>
    /// <response code="503">Servei extern no disponible</response>
    [HttpGet]
    [Route("veterinary/search")]
    [ProducesResponseType(typeof(List<Application.Medicines.DTOs.VeterinaryMedicineDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> SearchVeterinaryMedicines(
        [FromQuery] string query,
        [FromQuery] string? species = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
        {
            return BadRequest(new
            {
                error = "Query invàlid",
                message = "El text de cerca ha de tenir almenys 2 caràcters"
            });
        }

        _logger.LogInformation("Cercant medicaments veterinaris: {Query}, Espècie: {Species}", query, species);

        try
        {
            var results = await _mediator.Send(new SearchVeterinaryMedicinesQuery
            {
                Query = query,
                Species = species
            }, cancellationToken);

            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cercant medicaments veterinaris");
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CimaVet. Torneu a intentar-ho més tard."
            });
        }
    }

    /// <summary>
    /// Obté informació detallada d'un medicament veterinari per codi nacional
    /// </summary>
    /// <param name="cnCode">Codi Nacional del medicament</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Informació detallada del medicament</returns>
    /// <response code="200">Retorna la informació del medicament</response>
    /// <response code="404">Medicament no trobat</response>
    /// <response code="401">API Key mancant o invàlida</response>
    /// <response code="503">Servei extern no disponible</response>
    [HttpGet]
    [Route("veterinary/{cnCode}")]
    [ProducesResponseType(typeof(Application.Medicines.DTOs.VeterinaryMedicineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetVeterinaryMedicineByCode(
        string cnCode,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(cnCode))
        {
            return BadRequest(new
            {
                error = "Codi invàlid",
                message = "El codi nacional no pot estar buit"
            });
        }

        _logger.LogInformation("Obtenint medicament veterinari amb codi: {CnCode}", cnCode);

        try
        {
            var result = await _mediator.Send(new GetVeterinaryMedicineByCodeQuery
            {
                CnCode = cnCode
            }, cancellationToken);

            if (result == null)
            {
                return NotFound(new
                {
                    error = "Medicament no trobat",
                    message = $"No s'ha trobat cap medicament veterinari amb el codi {cnCode}"
                });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtenint medicament veterinari {CnCode}", cnCode);
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CimaVet. Torneu a intentar-ho més tard."
            });
        }
    }

    /// <summary>
    /// Cerca medicaments humans a CIMA
    /// </summary>
    /// <param name="query">Text de cerca (nom, principi actiu, codi)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de medicaments humans trobats</returns>
    /// <response code="200">Retorna la llista de medicaments trobats</response>
    /// <response code="400">Paràmetres de cerca invàlids</response>
    /// <response code="401">API Key mancant o invàlida</response>
    /// <response code="503">Servei extern no disponible</response>
    [HttpGet]
    [Route("human/search")]
    [ProducesResponseType(typeof(List<Application.Medicines.DTOs.HumanMedicineDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> SearchHumanMedicines(
        [FromQuery] string query,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
        {
            return BadRequest(new
            {
                error = "Query invàlid",
                message = "El text de cerca ha de tenir almenys 2 caràcters"
            });
        }

        _logger.LogInformation("Cercant medicaments humans: {Query}", query);

        try
        {
            var results = await _mediator.Send(new SearchHumanMedicinesQuery
            {
                Query = query
            }, cancellationToken);

            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cercant medicaments humans");
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CIMA. Torneu a intentar-ho més tard."
            });
        }
    }

    /// <summary>
    /// Obté informació detallada d'un medicament humà per codi nacional
    /// </summary>
    /// <param name="cnCode">Codi Nacional del medicament</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Informació detallada del medicament</returns>
    /// <response code="200">Retorna la informació del medicament</response>
    /// <response code="404">Medicament no trobat</response>
    /// <response code="401">API Key mancant o invàlida</response>
    /// <response code="503">Servei extern no disponible</response>
    [HttpGet]
    [Route("human/{cnCode}")]
    [ProducesResponseType(typeof(Application.Medicines.DTOs.HumanMedicineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetHumanMedicineByCode(
        string cnCode,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(cnCode))
        {
            return BadRequest(new
            {
                error = "Codi invàlid",
                message = "El codi nacional no pot estar buit"
            });
        }

        _logger.LogInformation("Obtenint medicament humà amb codi: {CnCode}", cnCode);

        try
        {
            var result = await _mediator.Send(new GetHumanMedicineByCodeQuery
            {
                CnCode = cnCode
            }, cancellationToken);

            if (result == null)
            {
                return NotFound(new
                {
                    error = "Medicament no trobat",
                    message = $"No s'ha trobat cap medicament humà amb el codi {cnCode}"
                });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtenint medicament humà {CnCode}", cnCode);
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CIMA. Torneu a intentar-ho més tard."
            });
        }
    }
}
