using System.Security.Cryptography;
using System.Text;

namespace VeteriLach.ReadApi.Middleware;

/// <summary>
/// Middleware per validar API Key en totes les peticions (excepte /api/health)
/// </summary>
public class ApiKeyAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiKeyAuthenticationMiddleware> _logger;
    private const string ApiKeyHeaderName = "X-API-Key";

    public ApiKeyAuthenticationMiddleware(
        RequestDelegate next,
        IConfiguration configuration,
        ILogger<ApiKeyAuthenticationMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip health check endpoint
        if (context.Request.Path.StartsWithSegments("/api/health"))
        {
            await _next(context);
            return;
        }

        // Skip Swagger endpoints en desenvolupament
        if (context.Request.Path.StartsWithSegments("/swagger") || 
            context.Request.Path.StartsWithSegments("/api-docs"))
        {
            await _next(context);
            return;
        }

        // Verificar presència de l'API Key
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            _logger.LogWarning("API Key mancant. IP: {IP}, Path: {Path}", 
                context.Connection.RemoteIpAddress, 
                context.Request.Path);
            
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                status = 401,
                error = "Unauthorized",
                message = "API Key mancant. Proporcioni una API Key vàlida al header X-API-Key.",
                timestamp = DateTime.UtcNow
            });
            return;
        }

        // Validar API Key
        if (!IsValidApiKey(extractedApiKey!))
        {
            _logger.LogWarning("API Key invàlida. IP: {IP}, Path: {Path}, Key: {Key}", 
                context.Connection.RemoteIpAddress, 
                context.Request.Path,
                MaskApiKey(extractedApiKey!));
            
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                status = 401,
                error = "Unauthorized",
                message = "API Key invàlida. La clau proporcionada no és vàlida.",
                timestamp = DateTime.UtcNow
            });
            return;
        }

        _logger.LogInformation("API Key vàlida. IP: {IP}, Path: {Path}", 
            context.Connection.RemoteIpAddress, 
            context.Request.Path);

        await _next(context);
    }

    /// <summary>
    /// Valida l'API Key comparant el seu hash SHA256 amb els hash configurats
    /// </summary>
    private bool IsValidApiKey(string apiKey)
    {
        var apiKeysSection = _configuration.GetSection("ApiKeys");
        var validApiKeyHashes = apiKeysSection.GetChildren()
            .Select(x => x.Value)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        if (!validApiKeyHashes.Any())
        {
            _logger.LogError("No hi ha API Keys configurades al fitxer de configuració");
            return false;
        }

        // Calcular hash SHA256 de l'API Key proporcionada
        var apiKeyHash = ComputeSha256Hash(apiKey);

        // Comparar amb els hash configurats
        return validApiKeyHashes.Any(hash => 
            string.Equals(hash, apiKeyHash, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Calcula el hash SHA256 d'una cadena
    /// </summary>
    private static string ComputeSha256Hash(string rawData)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

    /// <summary>
    /// Emmascarar API Key per logging (mostra només primers 4 caràcters)
    /// </summary>
    private static string MaskApiKey(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey) || apiKey.Length <= 4)
            return "****";
        
        return $"{apiKey[..4]}****";
    }
}
