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
public partial class MedicinesController(IMediator mediator, ILogger<MedicinesController> logger) : ControllerBase
{
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

        LogInformationSearchingVeterinaryMedicines(query, species);

        try
        {
            var results = await mediator.Send(new SearchVeterinaryMedicinesQuery(query,species), cancellationToken);

            return Ok(results);
        }
        catch (Exception ex)
        {
            LogErrorSearchingVeterinaryMedicines(ex);
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CimaVet. Torneu a intentar-ho més tard."
            });
        }
    }

    [LoggerMessage(EventId = 1000, Level = LogLevel.Information, Message = "Cercant medicaments veterinaris: {Query}, Espècie: {Species}")]
    partial void LogInformationSearchingVeterinaryMedicines(string query, string? species);

    [LoggerMessage(EventId = 1001,Level = LogLevel.Error,Message = "Error cercant medicaments veterinaris")]
    partial void LogErrorSearchingVeterinaryMedicines(Exception ex);

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
    public async Task<IActionResult> GetVeterinaryMedicineByCode(string cnCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(cnCode))
        {
            return BadRequest(new
            {
                error = "Codi invàlid",
                message = "El codi nacional no pot estar buit"
            });
        }

        LogInformationGettingVeterinaryMedicine(cnCode);

        try
        {
            var result = await mediator.Send(new GetVeterinaryMedicineByCodeQuery(cnCode), cancellationToken);

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
            LogErrorGettingVeterinaryMedicine(cnCode, ex);
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CimaVet. Torneu a intentar-ho més tard."
            });
        }
    }
    [LoggerMessage(EventId=1004, Level =LogLevel.Information, Message = "Obtenint medicament veterinari amb codi: {CnCode}")]
    partial void LogInformationGettingVeterinaryMedicine(string cnCode);
    [LoggerMessage(EventId = 1005, Level = LogLevel.Error, Message = "Error obtenint medicament veterinari {CnCode}")]
    partial void LogErrorGettingVeterinaryMedicine(string cnCode, Exception ex);

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

        LogInformationSearchingHumanMedicines(query);

        try
        {
            var results = await mediator.Send(new SearchHumanMedicinesQuery(query), cancellationToken);

            return Ok(results);
        }
        catch (Exception ex)
        {
            LogErrorSearchingHumanMedicines(ex);
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CIMA. Torneu a intentar-ho més tard."
            });
        }
    }

    [LoggerMessage(EventId = 1002, Level = LogLevel.Information, Message = "Cercant medicaments humans: {Query}")]
    partial void LogInformationSearchingHumanMedicines(string query);
    [LoggerMessage(EventId = 1003, Level = LogLevel.Error, Message = "Error cercant medicaments humans")]
    partial void LogErrorSearchingHumanMedicines(Exception ex);

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

        LogInformationGettingHumanMedicineByCode(cnCode);

        try
        {
            var result = await mediator.Send(new GetHumanMedicineByCodeQuery(cnCode), cancellationToken);

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
            LogErrorGettingHumanMedicineByCode(ex, cnCode);
            return StatusCode(503, new
            {
                error = "Servei extern no disponible",
                message = "No s'ha pogut connectar amb CIMA. Torneu a intentar-ho més tard."
            });
        }
    }
    [LoggerMessage(EventId = 1006, Level = LogLevel.Information, Message = "Obtenint medicament humà amb codi: {CnCode}")]
    partial void LogInformationGettingHumanMedicineByCode(string cnCode);
    [LoggerMessage(EventId = 1007, Level = LogLevel.Error, Message = "Error obtenint medicament humà {CnCode}")]
    partial void LogErrorGettingHumanMedicineByCode(Exception ex, string cnCode);

}
