using System.Text.Json.Serialization;

namespace VeteriLach.McpServer.Mcp;

// JSON-RPC 2.0 Models
public class JsonRpcRequest
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc { get; set; } = "2.0";
    
    [JsonPropertyName("id")]
    public object? Id { get; set; }
    
    [JsonPropertyName("method")]
    public required string Method { get; set; }
    
    [JsonPropertyName("params")]
    public Dictionary<string, object>? Params { get; set; }
}

public class JsonRpcResponse
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc { get; set; } = "2.0";
    
    [JsonPropertyName("id")]
    public object? Id { get; set; }
    
    [JsonPropertyName("result")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Result { get; set; }
    
    [JsonPropertyName("error")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonRpcError? Error { get; set; }
}

public class JsonRpcError
{
    [JsonPropertyName("code")]
    public int Code { get; set; }
    
    [JsonPropertyName("message")]
    public required string Message { get; set; }
    
    [JsonPropertyName("data")]
    public object? Data { get; set; }
}

// MCP Protocol Models
public class InitializeResult
{
    [JsonPropertyName("protocolVersion")]
    public string ProtocolVersion { get; set; } = "2024-11-05";
    
    [JsonPropertyName("serverInfo")]
    public required ServerInfo ServerInfo { get; set; }
    
    [JsonPropertyName("capabilities")]
    public required ServerCapabilities Capabilities { get; set; }
}

public class ServerInfo
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("version")]
    public required string Version { get; set; }
}

public class ServerCapabilities
{
    [JsonPropertyName("tools")]
    public object Tools { get; set; } = new { };
}

public class Tool
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    
    [JsonPropertyName("inputSchema")]
    public required InputSchema InputSchema { get; set; }
}

public class InputSchema
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "object";
    
    [JsonPropertyName("properties")]
    public required Dictionary<string, PropertySchema> Properties { get; set; }
    
    [JsonPropertyName("required")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Required { get; set; }
}

public class PropertySchema
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }
    
    [JsonPropertyName("enum")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Enum { get; set; }
}

public class ToolResponse
{
    [JsonPropertyName("content")]
    public required List<ContentItem> Content { get; set; }
}

public class ContentItem
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "text";
    
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ListToolsResult
{
    [JsonPropertyName("tools")]
    public required List<Tool> Tools { get; set; }
}
