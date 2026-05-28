using Microsoft.AspNetCore.Mvc;

namespace VeteriLach.ReadApi.Controllers;

/// <summary>
/// Health check endpoint per monitoritzar l'estat de l'API
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Health check bàsic (no requereix API Key)
    /// </summary>
    /// <returns>Estat de l'API</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check executat");

        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            service = "VeteriLach.ReadApi",
            version = "1.0.0",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
        });
    }
}
