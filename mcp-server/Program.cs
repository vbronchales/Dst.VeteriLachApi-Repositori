using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using VeteriLach.McpServer.Mcp;
using VeteriLach.McpServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Read configuration
var apiBaseUrl = builder.Configuration["VeteriLachApi:BaseUrl"] 
    ?? throw new Exception("VeteriLachApi:BaseUrl not configured");
var apiKey = builder.Configuration["VeteriLachApi:ApiKey"] 
    ?? throw new Exception("VeteriLachApi:ApiKey not configured");
var timeout = int.Parse(builder.Configuration["VeteriLachApi:TimeoutSeconds"] ?? "30");

var serverName = builder.Configuration["McpServer:Name"] ?? "veterilach-server";
var serverVersion = builder.Configuration["McpServer:Version"] ?? "1.0.0";

// Register MCP services as singletons
builder.Services.AddSingleton(new VeteriLachApiClient(apiBaseUrl, apiKey, timeout));
builder.Services.AddSingleton(sp => new McpServer(
    sp.GetRequiredService<VeteriLachApiClient>(), 
    serverName, 
    serverVersion
));

var app = builder.Build();

app.UseCors();

var logger = app.Logger;
logger.LogInformation("VeteriLach MCP Server v{Version} starting...", serverVersion);
logger.LogInformation("API URL: {BaseUrl}", apiBaseUrl);

// MCP endpoint - POST /messages
app.MapPost("/messages", async ([FromBody] JsonRpcRequest request, [FromServices] McpServer mcpServer) =>
{
    logger.LogInformation("Processing method: {Method}", request.Method);
    
    var response = await mcpServer.HandleRequestAsync(request);
    
    logger.LogInformation("Response sent for method: {Method}", request.Method);
    return Results.Json(response, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    });
});

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    server = serverName,
    version = serverVersion,
    timestamp = DateTime.UtcNow
}));

// Root endpoint with info
app.MapGet("/", () => Results.Ok(new
{
    name = serverName,
    version = serverVersion,
    protocol = "MCP",
    transport = "HTTP",
    endpoints = new
    {
        messages = "/messages (POST)",
        health = "/health (GET)"
    }
}));

logger.LogInformation("Server ready. Listening for HTTP requests...");

app.Run();
