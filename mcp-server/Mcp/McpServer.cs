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
                },
                // Propietaris (Clients)
                new Tool
                {
                    Name = "get_propietaris",
                    Description = "Get a paginated list of clients/owners with optional filters",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["pageNumber"] = new PropertySchema { Type = "number", Description = "Page number" },
                            ["pageSize"] = new PropertySchema { Type = "number", Description = "Page size" },
                            ["searchTerm"] = new PropertySchema { Type = "string", Description = "Search by name, email, or phone" },
                            ["poblacio"] = new PropertySchema { Type = "string", Description = "Filter by city" },
                            ["codiPostal"] = new PropertySchema { Type = "string", Description = "Filter by postal code" },
                            ["nomes_actius"] = new PropertySchema { Type = "boolean", Description = "Show only active clients" }
                        }
                    }
                },
                new Tool
                {
                    Name = "get_propietari_detail",
                    Description = "Get detailed information about a specific client/owner including animals and phones",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["propietariId"] = new PropertySchema { Type = "string", Description = "Client ID (GUID)" }
                        },
                        Required = new List<string> { "propietariId" }
                    }
                },
                // Animals (Mascotes)
                new Tool
                {
                    Name = "get_animals",
                    Description = "Get a paginated list of animals/pets with optional filters",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["pageNumber"] = new PropertySchema { Type = "number", Description = "Page number" },
                            ["pageSize"] = new PropertySchema { Type = "number", Description = "Page size" },
                            ["searchTerm"] = new PropertySchema { Type = "string", Description = "Search by name or microchip" },
                            ["idPropietari"] = new PropertySchema { Type = "string", Description = "Filter by owner ID" },
                            ["idEspecie"] = new PropertySchema { Type = "string", Description = "Filter by species ID" }
                        }
                    }
                },
                new Tool
                {
                    Name = "get_animal_detail",
                    Description = "Get detailed information about a specific animal/pet including owner data",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["animalId"] = new PropertySchema { Type = "string", Description = "Animal ID (GUID)" }
                        },
                        Required = new List<string> { "animalId" }
                    }
                },
                // Medical History (Visites)
                new Tool
                {
                    Name = "get_animal_visits",
                    Description = "Get medical visit history for a specific animal",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["animalId"] = new PropertySchema { Type = "string", Description = "Animal ID (GUID)" },
                            ["pageNumber"] = new PropertySchema { Type = "number", Description = "Page number" },
                            ["pageSize"] = new PropertySchema { Type = "number", Description = "Page size" },
                            ["dataInici"] = new PropertySchema { Type = "string", Description = "Start date (YYYY-MM-DD)" },
                            ["dataFi"] = new PropertySchema { Type = "string", Description = "End date (YYYY-MM-DD)" }
                        },
                        Required = new List<string> { "animalId" }
                    }
                },
                new Tool
                {
                    Name = "get_visit_detail",
                    Description = "Get complete details of a veterinary visit including clinical notes, tests, and vaccines",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["visitId"] = new PropertySchema { Type = "string", Description = "Visit ID (GUID)" }
                        },
                        Required = new List<string> { "visitId" }
                    }
                },
                // Medicines (Medicaments)
                new Tool
                {
                    Name = "search_veterinary_medicines",
                    Description = "Search veterinary medicines in CimaVet database",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["query"] = new PropertySchema { Type = "string", Description = "Search query (name, active ingredient, code)" },
                            ["species"] = new PropertySchema { Type = "string", Description = "Target species (optional)" }
                        },
                        Required = new List<string> { "query" }
                    }
                },
                new Tool
                {
                    Name = "get_veterinary_medicine",
                    Description = "Get detailed information about a veterinary medicine by national code",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["cnCode"] = new PropertySchema { Type = "string", Description = "National Code (CN)" }
                        },
                        Required = new List<string> { "cnCode" }
                    }
                },
                new Tool
                {
                    Name = "search_human_medicines",
                    Description = "Search human medicines in CIMA database",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["query"] = new PropertySchema { Type = "string", Description = "Search query (name, active ingredient, code)" }
                        },
                        Required = new List<string> { "query" }
                    }
                },
                new Tool
                {
                    Name = "get_human_medicine",
                    Description = "Get detailed information about a human medicine by national code",
                    InputSchema = new InputSchema
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            ["cnCode"] = new PropertySchema { Type = "string", Description = "National Code (CN)" }
                        },
                        Required = new List<string> { "cnCode" }
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
            "get_propietaris" => await ExecuteGetPropietarisAsync(arguments),
            "get_propietari_detail" => await ExecuteGetPropietariDetailAsync(arguments),
            "get_animals" => await ExecuteGetAnimalsAsync(arguments),
            "get_animal_detail" => await ExecuteGetAnimalDetailAsync(arguments),
            "get_animal_visits" => await ExecuteGetAnimalVisitsAsync(arguments),
            "get_visit_detail" => await ExecuteGetVisitDetailAsync(arguments),
            "search_veterinary_medicines" => await ExecuteSearchVeterinaryMedicinesAsync(arguments),
            "get_veterinary_medicine" => await ExecuteGetVeterinaryMedicineAsync(arguments),
            "search_human_medicines" => await ExecuteSearchHumanMedicinesAsync(arguments),
            "get_human_medicine" => await ExecuteGetHumanMedicineAsync(arguments),
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

    // ===== Propietaris (Clients) =====

    private async Task<string> ExecuteGetPropietarisAsync(Dictionary<string, object> args)
    {
        var response = await _apiClient.GetPropietarisAsync(
            pageNumber: GetIntArg(args, "pageNumber"),
            pageSize: GetIntArg(args, "pageSize"),
            searchTerm: GetStringArg(args, "searchTerm"),
            poblacio: GetStringArg(args, "poblacio"),
            codiPostal: GetStringArg(args, "codiPostal"),
            nomes_actius: GetBoolArg(args, "nomes_actius")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetPropietariDetailAsync(Dictionary<string, object> args)
    {
        var propietariId = GetStringArg(args, "propietariId") ?? throw new Exception("propietariId is required");
        var response = await _apiClient.GetPropietariByIdAsync(propietariId);
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    // ===== Animals (Mascotes) =====

    private async Task<string> ExecuteGetAnimalsAsync(Dictionary<string, object> args)
    {
        var response = await _apiClient.GetAnimalsAsync(
            pageNumber: GetIntArg(args, "pageNumber"),
            pageSize: GetIntArg(args, "pageSize"),
            searchTerm: GetStringArg(args, "searchTerm"),
            idPropietari: GetStringArg(args, "idPropietari"),
            idEspecie: GetStringArg(args, "idEspecie")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetAnimalDetailAsync(Dictionary<string, object> args)
    {
        var animalId = GetStringArg(args, "animalId") ?? throw new Exception("animalId is required");
        var response = await _apiClient.GetAnimalByIdAsync(animalId);
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    // ===== Medical History (Visites) =====

    private async Task<string> ExecuteGetAnimalVisitsAsync(Dictionary<string, object> args)
    {
        var animalId = GetStringArg(args, "animalId") ?? throw new Exception("animalId is required");
        var response = await _apiClient.GetAnimalVisitsAsync(
            animalId,
            pageNumber: GetIntArg(args, "pageNumber"),
            pageSize: GetIntArg(args, "pageSize"),
            dataInici: GetStringArg(args, "dataInici"),
            dataFi: GetStringArg(args, "dataFi")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetVisitDetailAsync(Dictionary<string, object> args)
    {
        var visitId = GetStringArg(args, "visitId") ?? throw new Exception("visitId is required");
        var response = await _apiClient.GetVisitByIdAsync(visitId);
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    // ===== Medicines (Medicaments) =====

    private async Task<string> ExecuteSearchVeterinaryMedicinesAsync(Dictionary<string, object> args)
    {
        var query = GetStringArg(args, "query") ?? throw new Exception("query is required");
        var response = await _apiClient.SearchVeterinaryMedicinesAsync(
            query,
            species: GetStringArg(args, "species")
        );
        
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetVeterinaryMedicineAsync(Dictionary<string, object> args)
    {
        var cnCode = GetStringArg(args, "cnCode") ?? throw new Exception("cnCode is required");
        var response = await _apiClient.GetVeterinaryMedicineByCodeAsync(cnCode);
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteSearchHumanMedicinesAsync(Dictionary<string, object> args)
    {
        var query = GetStringArg(args, "query") ?? throw new Exception("query is required");
        var response = await _apiClient.SearchHumanMedicinesAsync(query);
        return JsonSerializer.Serialize(response, _jsonOptions);
    }

    private async Task<string> ExecuteGetHumanMedicineAsync(Dictionary<string, object> args)
    {
        var cnCode = GetStringArg(args, "cnCode") ?? throw new Exception("cnCode is required");
        var response = await _apiClient.GetHumanMedicineByCodeAsync(cnCode);
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
