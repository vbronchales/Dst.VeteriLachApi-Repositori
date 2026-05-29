using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.ReadApi.Application.Sales.DTOs;
using VeteriLach.ReadApi.Application.Sales.Queries;

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
    /// Obté llista de vendes amb filtres i paginació
    /// </summary>
    /// <param name="startDate">Data inici</param>
    /// <param name="endDate">Data fi</param>
    /// <param name="customerId">Filtre per client</param>
    /// <param name="sellerId">Filtre per venedor</param>
    /// <param name="animalId">Filtre per animal</param>
    /// <param name="onlyPending">Només vendes pendents de pagament</param>
    /// <param name="onlyPaid">Només vendes pagades</param>
    /// <param name="pageNumber">Número de pàgina (default: 1)</param>
    /// <param name="pageSize">Elements per pàgina (default: 50, max: 100)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de vendes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<SaleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSales(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] Guid? customerId,
        [FromQuery] Guid? sellerId,
        [FromQuery] Guid? animalId,
        [FromQuery] bool? onlyPending,
        [FromQuery] bool? onlyPaid,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Cercant vendes amb filtres");

        if (pageSize > 100) pageSize = 100;

        var query = new GetSalesQuery
        {
            StartDate = startDate,
            EndDate = endDate,
            CustomerId = customerId,
            SellerId = sellerId,
            AnimalId = animalId,
            OnlyPending = onlyPending,
            OnlyPaid = onlyPaid,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var sales = await _mediator.Send(query, cancellationToken);

        return Ok(sales);
    }

    /// <summary>
    /// Obté detall complet d'una venda amb articles
    /// </summary>
    /// <param name="id">ID de la venda</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Detall de la venda</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SaleDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleDetail(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obtenint detall de venda {SaleId}", id);

        var query = new GetSaleDetailQuery { SaleId = id };
        var sale = await _mediator.Send(query, cancellationToken);

        if (sale == null)
        {
            _logger.LogWarning("Venda {SaleId} no trobada", id);
            return NotFound(new { message = $"Venda {id} no trobada" });
        }

        return Ok(sale);
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

    /// <summary>
    /// Obté deutes pendents de clients
    /// </summary>
    /// <param name="customerId">Filtre per client específic</param>
    /// <param name="minimumDays">Mínim de dies pendents</param>
    /// <param name="minimumAmount">Import mínim pendent</param>
    /// <param name="pageNumber">Número de pàgina (default: 1)</param>
    /// <param name="pageSize">Elements per pàgina (default: 50)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de deutes</returns>
    [HttpGet("debts")]
    [ProducesResponseType(typeof(List<DebtDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDebts(
        [FromQuery] Guid? customerId,
        [FromQuery] int? minimumDays,
        [FromQuery] decimal? minimumAmount,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Cercant deutes pendents");

        if (pageSize > 100) pageSize = 100;

        var query = new GetDebtsQuery
        {
            CustomerId = customerId,
            MinimumDays = minimumDays,
            MinimumAmount = minimumAmount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var debts = await _mediator.Send(query, cancellationToken);

        return Ok(debts);
    }

    /// <summary>
    /// Obté pagaments a compte
    /// </summary>
    /// <param name="startDate">Data inici</param>
    /// <param name="endDate">Data fi</param>
    /// <param name="customerId">Filtre per client</param>
    /// <param name="animalId">Filtre per animal</param>
    /// <param name="pageNumber">Número de pàgina (default: 1)</param>
    /// <param name="pageSize">Elements per pàgina (default: 50)</param>
    /// <param name="cancellationToken">Token de cancel·lació</param>
    /// <returns>Llista de pagaments a compte</returns>
    [HttpGet("advances")]
    [ProducesResponseType(typeof(List<PaymentAdvanceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentAdvances(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] Guid? customerId,
        [FromQuery] Guid? animalId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Cercant pagaments a compte");

        if (pageSize > 100) pageSize = 100;

        var query = new GetPaymentAdvancesQuery
        {
            StartDate = startDate,
            EndDate = endDate,
            CustomerId = customerId,
            AnimalId = animalId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var advances = await _mediator.Send(query, cancellationToken);

        return Ok(advances);
    }
}
