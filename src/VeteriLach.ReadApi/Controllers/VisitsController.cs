using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Visits.DTOs;
using VeteriLach.ReadApi.Application.Visits.Queries;

namespace VeteriLach.ReadApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class VisitsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VisitsController> _logger;

    public VisitsController(IMediator mediator, ILogger<VisitsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obté les visites més recents ordenades per data descendent
    /// </summary>
    /// <param name="days">Nombre de dies enrere (default: 7, max: 90)</param>
    /// <param name="pageNumber">Número de pàgina (default: 1)</param>
    /// <param name="pageSize">Elements per pàgina (default: 20, max: 100)</param>
    /// <param name="includeAnimalInfo">Incloure informació d'animal i propietari (default: true)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de visites recents</returns>
    [HttpGet("recent")]
    [ProducesResponseType(typeof(List<RecentVisitDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecentVisits(
        [FromQuery] int days = 7,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeAnimalInfo = true,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obtenint visites recents dels darrers {Days} dies", days);

        // Validar límits
        if (days > 90) days = 90;
        if (days < 1) days = 1;
        if (pageSize > 100) pageSize = 100;
        if (pageSize < 1) pageSize = 1;
        if (pageNumber < 1) pageNumber = 1;

        var query = new GetRecentVisitsQuery
        {
            Days = days,
            PageNumber = pageNumber,
            PageSize = pageSize,
            IncludeAnimalInfo = includeAnimalInfo
        };

        var visits = await _mediator.Send(query, cancellationToken);

        return Ok(visits);
    }
}
