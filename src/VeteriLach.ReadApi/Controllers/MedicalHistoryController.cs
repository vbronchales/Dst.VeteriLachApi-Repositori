using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;
using VeteriLach.ReadApi.Application.MedicalHistory.Queries;

namespace VeteriLach.ReadApi.Controllers;

/// <summary>
/// Controlador per gestionar les visites mèdiques dels animals, incloent la consulta de l'historial mèdic i el detall de cada visita.
/// </summary>
/// <param name="mediator"></param>
/// <param name="logger"></param>
[ApiController]
[Route("api/animals/{idAnimal}/visits")]
[Route("api/visits")]
[Produces("application/json")]
public partial class MedicalHistoryController(IMediator mediator, ILogger<MedicalHistoryController> logger) : ControllerBase
{
    /// <summary>
    /// Obté la llista paginada de visites d'un animal
    /// </summary>
    /// <param name="idAnimal">ID de l'animal</param>
    /// <param name="pageNumber">Número de pàgina (per defecte 1)</param>
    /// <param name="pageSize">Mida de pàgina (per defecte 20, màxim 100)</param>
    /// <param name="dataInici">Data d'inici del filtre (opcional)</param>
    /// <param name="dataFi">Data de fi del filtre (opcional)</param>
    /// <returns>Llista paginada de visites</returns>
    [HttpGet]
    [Route("/api/animals/{idAnimal}/visits")]
    [ProducesResponseType(typeof(PaginatedResult<VisitaResumatDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAnimalVisits(
        Guid idAnimal,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] DateTime? dataInici = null,
        [FromQuery] DateTime? dataFi = null)
    {
        var query = new GetAnimalVisitsListQuery(idAnimal)
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            DataInici = dataInici,
            DataFi = dataFi
        };

        var result = await mediator.Send(query);

        LogAnimalVisitsReturned(result.Data.Count, result.Pagination.TotalItems, idAnimal);

        return Ok(result);
    }

    /// <summary>
    /// Obté el detall complet d'una visita específica
    /// </summary>
    /// <param name="id">ID de la visita</param>
    /// <returns>Detall complet de la visita amb textos, proves i vacunes</returns>
    [HttpGet]
    [Route("/api/visits/{id}")]
    [ProducesResponseType(typeof(VisitaDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVisit(Guid id)
    {
        var query = new GetVisitByIdQuery(id);
        var result = await mediator.Send(query);

        if (result == null)
        {
            LogVisitNotFound(id);
            return NotFound(new { message = $"No s'ha trobat cap visita amb l'identificador {id}." });
        }

        LogVisitReturned(id);
        return Ok(result);
    }

    [LoggerMessage(EventId = 1001, Level = LogLevel.Information, Message = "Retornades {Count} visites de {Total} per animal {IdAnimal}")]
    partial void LogAnimalVisitsReturned(int count, int total, Guid idAnimal);
    [LoggerMessage(EventId = 1002, Level = LogLevel.Warning, Message = "Visita {IdVisita} no trobada")]
    partial void LogVisitNotFound(Guid idVisita);
    [LoggerMessage(EventId = 1003, Level = LogLevel.Information, Message = "Retornat detall de visita {IdVisita}")]
    partial void LogVisitReturned(Guid idVisita);
}
