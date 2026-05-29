using System.Text.Json;
using System.Text.Json.Nodes;
using VeteriLach.McpServer.Services;

namespace VeteriLach.McpServer.Mcp;

public class McpServer
{
    private readonly VeteriLachApiClient _apiClient;
    private readonly string _serverName;
    private readonly string _serverVersion;
    private readonly JsonSerializerOptions _jsonOptions;

    public McpServer(VeteriLachApiClient apiClient, string serverName, string serverVersion)
    {
        _apiClient = apiClient;
        _serverName = serverName;
        _serverVersion = serverVersion;
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    public async Task<JsonRpcResponse> HandleRequestAsync(JsonRpcRequest request)
    {
        try
        {
            object? result = request.Method switch
            {
                "initialize" => HandleInitialize(),
                "tools/list" => HandleToolsList(),
                "tools/call" => await HandleToolsCallAsync(request.Params),
                _ => throw new Exception($"Unknown method: {request.Method}")
            };

            return new JsonRpcResponse
            {
                Id = request.Id,
                Result = result
            };
        }
        catch (Exception ex)
        {
            return new JsonRpcResponse
            {
                Id = request.Id,
                Error = new JsonRpcError
                {
                    Code = -32603,
                    Message = ex.Message
                }
            };
        }
    }

    private InitializeResult HandleInitialize()
    {
        return new InitializeResult
        {
            ServerInfo = new ServerInfo
            {
                Name = _serverName,
                Version = _serverVersion
            },
            Capabilities = new ServerCapabilities()
        };
    }

    private ListToolsResult HandleToolsList()
    {
        return new ListToolsResult
        {
            Tools = new List<Tool>
            {
                new Tool
                {
                    Name = "get_sales",
                    Description = "Get a paginated list of sales with optional filters",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["pageNumber"] = new PropertySchema { Type = "number", Description = "Page number (default: 1)" },
                            ["pageSize"] = new PropertySchema { Type = "number", Description = "Page size (default: 20, max: 50)" },
                            ["startDate"] = new PropertySchema { Type = "string", Description = "Filter by start date (YYYY-MM-DD)" },
                            ["endDate"] = new PropertySchema { Type = "string", Description = "Filter by end date (YYYY-MM-DD)" },
                            ["customerId"] = new PropertySchema { Type = "string", Description = "Filter by customer ID (GUID)" },
                            ["sellerId"] = new PropertySchema { Type = "string", Description = "Filter by seller ID (GUID)" },
                            ["animalId"] = new PropertySchema { Type = "string", Description = "Filter by animal ID (GUID)" },
                            ["onlyUnpaid"] = new PropertySchema { Type = "boolean", Description = "Show only unpaid sales" }
                        }
                    }
                },
                new Tool
                {
                    Name = "get_sale_detail",
                    Description = "Get detailed information about a specific sale including all items",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["saleId"] = new PropertySchema { Type = "string", Description = "Sale ID (GUID)" }
                        },
                        Required = new List<string> { "saleId" }
                    }
                },
                new Tool
                {
                    Name = "get_customer_sales",
                    Description = "Get all sales for a specific customer",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["customerId"] = new PropertySchema { Type = "string", Description = "Customer ID (GUID)" },
                            ["pageNumber"] = new PropertySchema { Type = "number", Description = "Page number" },
                            ["pageSize"] = new PropertySchema { Type = "number", Description = "Page size" },
                            ["startDate"] = new PropertySchema { Type = "string", Description = "Start date (YYYY-MM-DD)" },
                            ["endDate"] = new PropertySchema { Type = "string", Description = "End date (YYYY-MM-DD)" }
                        },
                        Required = new List<string> { "customerId" }
                    }
                },
                new Tool
                {
                    Name = "get_debts",
                    Description = "Get list of unpaid or partially paid sales (debts)",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["pageNumber"] = new PropertySchema { Type = "number", Description = "Page number" },
                            ["pageSize"] = new PropertySchema { Type = "number", Description = "Page size" },
                            ["customerId"] = new PropertySchema { Type = "string", Description = "Filter by customer ID" },
                            ["minimumDays"] = new PropertySchema { Type = "number", Description = "Minimum days pending" },
                            ["minimumAmount"] = new PropertySchema { Type = "number", Description = "Minimum pending amount" }
                        }
                    }
                },
                new Tool
                {
                    Name = "get_payment_advances",
                    Description = "Get list of payment advances (acomptes) from customers",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["pageNumber"] = new PropertySchema { Type = "number", Description = "Page number" },
                            ["pageSize"] = new PropertySchema { Type = "number", Description = "Page size" },
                            ["customerId"] = new PropertySchema { Type = "string", Description = "Filter by customer ID" },
                            ["startDate"] = new PropertySchema { Type = "string", Description = "Start date (YYYY-MM-DD)" },
                            ["endDate"] = new PropertySchema { Type = "string", Description = "End date (YYYY-MM-DD)" }
                        }
                    }
                }
            }
        };
    }

    private async Task<ToolResponse> HandleToolsCallAsync(Dictionary<string, object>? params_)
    {
        if (params_ == null || !params_.TryGetValue("name", out var nameObj))
            throw new Exception("Missing tool name");

        var toolName = nameObj?.ToString() ?? throw new Exception("Invalid tool name");
        var arguments = new Dictionary<string, object>();
        
        if (params_.TryGetValue("arguments", out var argsObj) && argsObj is JsonElement argsElement)
        {
            arguments = JsonSerializer.Deserialize<Dictionary<string, object>>(argsElement.GetRawText(), _jsonOptions)
                ?? new Dictionary<string, object>();
        }

        var result = toolName switch
        {
            "get_sales" => await ExecuteGetSalesAsync(arguments),
            "get_sale_detail" => await ExecuteGetSaleDetailAsync(arguments),
            "get_customer_sales" => await ExecuteGetCustomerSalesAsync(arguments),
            "get_debts" => await ExecuteGetDebtsAsync(arguments),
            "get_payment_advances" => await ExecuteGetPaymentAdvancesAsync(arguments),
            _ => throw new Exception($"Unknown tool: {toolName}")
        };

        return new ToolResponse
        {
            Content = new List<ContentItem>
            {
                new ContentItem { Text = result }
            }
        };
    }

    private async Task<string> ExecuteGetSalesAsync(Dictionary<string, object> args)
    {
        var response = await _apiClient.GetSalesAsync(
            pageNumber: GetIntArg(args, "pageNumber"),
            pageSize: GetIntArg(args, "pageSize"),
            startDate: GetStringArg(args, "startDate"),
            endDate: GetStringArg(args, "endDate"),
            customerId: GetStringArg(args, "customerId"),
            sellerId: GetStringArg(args, "sellerId"),
            animalId: GetStringArg(args, "animalId"),
            onlyUnpaid: GetBoolArg(args, "onlyUnpaid")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetSaleDetailAsync(Dictionary<string, object> args)
    {
        var saleId = GetStringArg(args, "saleId") ?? throw new Exception("saleId is required");
        var response = await _apiClient.GetSaleByIdAsync(saleId);
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetCustomerSalesAsync(Dictionary<string, object> args)
    {
        var customerId = GetStringArg(args, "customerId") ?? throw new Exception("customerId is required");
        var response = await _apiClient.GetSalesByCustomerAsync(
            customerId,
            pageNumber: GetIntArg(args, "pageNumber"),
            pageSize: GetIntArg(args, "pageSize"),
            startDate: GetStringArg(args, "startDate"),
            endDate: GetStringArg(args, "endDate")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetDebtsAsync(Dictionary<string, object> args)
    {
        var response = await _apiClient.GetDebtsAsync(
            pageNumber: GetIntArg(args, "pageNumber"),
            pageSize: GetIntArg(args, "pageSize"),
            customerId: GetStringArg(args, "customerId"),
            minimumDays: GetIntArg(args, "minimumDays"),
            minimumAmount: GetDecimalArg(args, "minimumAmount")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetPaymentAdvancesAsync(Dictionary<string, object> args)
    {
        var response = await _apiClient.GetPaymentAdvancesAsync(
            pageNumber: GetIntArg(args, "pageNumber"),
            pageSize: GetIntArg(args, "pageSize"),
            customerId: GetStringArg(args, "customerId"),
            startDate: GetStringArg(args, "startDate"),
            endDate: GetStringArg(args, "endDate")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    // Helper methods para extract arguments
    private static string? GetStringArg(Dictionary<string, object> args, string key)
    {
        if (!args.TryGetValue(key, out var value)) return null;
        
        if (value is JsonElement jsonElement)
            return jsonElement.GetString();
        
        return value?.ToString();
    }

    private static int? GetIntArg(Dictionary<string, object> args, string key)
    {
        if (!args.TryGetValue(key, out var value)) return null;
        
        if (value is JsonElement jsonElement && jsonElement.TryGetInt32(out var intValue))
            return intValue;
        
        if (int.TryParse(value?.ToString(), out var parsed))
            return parsed;
        
        return null;
    }

    private static bool? GetBoolArg(Dictionary<string, object> args, string key)
    {
        if (!args.TryGetValue(key, out var value)) return null;
        
        if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.True)
            return true;
        if (value is JsonElement jsonElement2 && jsonElement2.ValueKind == JsonValueKind.False)
            return false;
        
        if (bool.TryParse(value?.ToString(), out var parsed))
            return parsed;
        
        return null;
    }

    private static decimal? GetDecimalArg(Dictionary<string, object> args, string key)
    {
        if (!args.TryGetValue(key, out var value)) return null;
        
        if (value is JsonElement jsonElement && jsonElement.TryGetDecimal(out var decValue))
            return decValue;
        
        if (decimal.TryParse(value?.ToString(), out var parsed))
            return parsed;
        
        return null;
    }
}
