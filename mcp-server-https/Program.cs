using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using VeteriLach.McpServer.Mcp;
using VeteriLach.McpServer.Services;

// Set content root to executable directory
var exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
    ?? Directory.GetCurrentDirectory();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = exeDirectory
});

// Configure services
var apiBaseUrl = builder.Configuration["VeteriLachApi:BaseUrl"] 
    ?? throw new Exception("VeteriLachApi:BaseUrl not configured");
var apiKey = builder.Configuration["VeteriLachApi:ApiKey"] 
    ?? throw new Exception("VeteriLachApi:ApiKey not configured");
var timeout = int.Parse(builder.Configuration["VeteriLachApi:TimeoutSeconds"] ?? "30");

var serverName = builder.Configuration["McpServer:Name"] ?? "veterilach-server-https";
var serverVersion = builder.Configuration["McpServer:Version"] ?? "1.0.0";

builder.Services.AddSingleton(new VeteriLachApiClient(apiBaseUrl, apiKey, timeout));
builder.Services.AddSingleton(sp => 
{
    var apiClient = sp.GetRequiredService<VeteriLachApiClient>();
    return new McpServer(apiClient, serverName, serverVersion);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

// Root endpoint
app.MapGet("/", () => Results.Ok(new
{
    Name = serverName,
    Version = serverVersion,
    Transport = "HTTPS",
    Endpoints = new
    {
        Messages = "/messages",
        Health = "/health"
    },
    Documentation = "https://github.com/vbronchales/Dst.VeteriLachApi-Repositori"
}));

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new
{
    Status = "healthy",
    Timestamp = DateTime.UtcNow,
    Server = serverName,
    Version = serverVersion
}));

// MCP messages endpoint
app.MapPost("/messages", async (HttpContext context, McpServer mcpServer) =>
{
    try
    {
        var request = await JsonSerializer.DeserializeAsync<JsonRpcRequest>(
            context.Request.Body, jsonOptions);

        if (request == null)
        {
            return Results.BadRequest(new { error = "Invalid request format" });
        }

        var response = await mcpServer.HandleRequestAsync(request);
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Error processing MCP request");
        return Results.Json(new JsonRpcResponse
        {
            Error = new JsonRpcError
            {
                Code = -32700,
                Message = $"Parse error: {ex.Message}"
            }
        }, jsonOptions);
    }
});

app.Logger.LogInformation("VeteriLach MCP Server v{Version} starting (HTTPS mode)...", serverVersion);
app.Logger.LogInformation("API URL: {ApiUrl}", apiBaseUrl);

app.Run();
