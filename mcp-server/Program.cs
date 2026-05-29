using System.Text.Json;
using Microsoft.Extensions.Configuration;
using VeteriLach.McpServer.Mcp;
using VeteriLach.McpServer.Services;

// Load configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var apiBaseUrl = configuration["VeteriLachApi:BaseUrl"] 
    ?? throw new Exception("VeteriLachApi:BaseUrl not configured");
var apiKey = configuration["VeteriLachApi:ApiKey"] 
    ?? throw new Exception("VeteriLachApi:ApiKey not configured");
var timeout = int.Parse(configuration["VeteriLachApi:TimeoutSeconds"] ?? "30");

var serverName = configuration["McpServer:Name"] ?? "veterilach-server";
var serverVersion = configuration["McpServer:Version"] ?? "1.0.0";

// Create API client and MCP server
var apiClient = new VeteriLachApiClient(apiBaseUrl, apiKey, timeout);
var mcpServer = new McpServer(apiClient, serverName, serverVersion);

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false
};

// Log to stderr (stdout is reserved for MCP protocol)
var logToStderr = (string message) => Console.Error.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");

logToStderr($"VeteriLach MCP Server v{serverVersion} starting...");
logToStderr($"API URL: {apiBaseUrl}");

try
{
    // Read from stdin and write to stdout (MCP protocol)
    using var reader = new StreamReader(Console.OpenStandardInput());
    using var writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };

    logToStderr("Server ready. Waiting for requests...");

    while (true)
    {
        var line = await reader.ReadLineAsync();
        if (line == null)
        {
            logToStderr("EOF received. Shutting down...");
            break;
        }

        if (string.IsNullOrWhiteSpace(line))
            continue;

        try
        {
            logToStderr($"Received: {line.Substring(0, Math.Min(100, line.Length))}...");

            var request = JsonSerializer.Deserialize<JsonRpcRequest>(line, jsonOptions);
            if (request == null)
            {
                logToStderr("ERROR: Failed to deserialize request");
                continue;
            }

            logToStderr($"Processing method: {request.Method}");
            var response = await mcpServer.HandleRequestAsync(request);
            
            var responseJson = JsonSerializer.Serialize(response, jsonOptions);
            await writer.WriteLineAsync(responseJson);
            
            logToStderr($"Response sent for method: {request.Method}");
        }
        catch (Exception ex)
        {
            logToStderr($"ERROR processing request: {ex.Message}");
            
            var errorResponse = new JsonRpcResponse
            {
                Error = new JsonRpcError
                {
                    Code = -32700,
                    Message = $"Parse error: {ex.Message}"
                }
            };
            
            var errorJson = JsonSerializer.Serialize(errorResponse, jsonOptions);
            await writer.WriteLineAsync(errorJson);
        }
    }
}
catch (Exception ex)
{
    logToStderr($"FATAL ERROR: {ex.Message}");
    logToStderr(ex.StackTrace ?? "");
    return 1;
}

logToStderr("Server shut down gracefully.");
return 0;
