using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Sales.Queries;
using VeteriLach.ReadApi.Domain;

namespace VeteriLach.ReadApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SalesController> _logger;

    public SalesController(IMediator mediator, ILogger<SalesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obté vendes d'un client específic
    /// </summary>
    /// <param name="customerId">ID del client</param>
    /// <param name="startDate">Data inici</param>
    /// <param name="endDate">Data fi</param>
    /// <param name="pageNumber">Número de pàgina (default: 1)</param>
    /// <param name="pageSize">Elements per pàgina (default: 50)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de vendes del client</returns>
    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(typeof(List<SaleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSalesByCustomer(
        Guid customerId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Cercant vendes del client {CustomerId}", customerId);

        if (pageSize > 100) pageSize = 100;

        var query = new GetSalesQuery
        {
            CustomerId = customerId,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var sales = await _mediator.Send(query, cancellationToken);

        return Ok(sales);
    }


}
