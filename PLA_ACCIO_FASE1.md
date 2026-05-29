# 🚀 Pla d'Acció Immediat - Fase 1 Crítica

**Objectiu**: Desbloquejar els 5 casos d'ús més habituals en 1-2 setmanes

---

## ✅ Checklist Fase 1

### Backend API (VeteriLach.ReadApi)

#### 1. Nou Controller: VisitsController.cs
```csharp
// Location: /Controllers/VisitsController.cs

[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentVisits(
        [FromQuery] int? days = 7,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeAnimalInfo = true)
    {
        // SQL: SELECT TOP pageSize * FROM Visites 
        // WHERE DataVisita >= DATEADD(day, -days, GETDATE())
        // ORDER BY DataVisita DESC
        // Include JOINs to Animals and Propietaris if includeAnimalInfo=true
    }

    [HttpGet]
    public async Task<IActionResult> GetVisits(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? animalId,
        [FromQuery] string? propietariId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        // SQL: Filterable global visits search
    }
}
```

#### 2. Nou Controller: MetadataController.cs
```csharp
// Location: /Controllers/MetadataController.cs

[ApiController]
[Route("api/[controller]")]
public class MetadataController : ControllerBase
{
    [HttpGet("especies")]
    public async Task<IActionResult> GetEspecies()
    {
        // SQL: SELECT DISTINCT Especie, COUNT(*) as Count 
        // FROM Animals 
        // GROUP BY Especie 
        // ORDER BY Count DESC
    }

    [HttpGet("rases")]
    public async Task<IActionResult> GetRases([FromQuery] string? especie = null)
    {
        // SQL: SELECT DISTINCT Rasa, COUNT(*) as Count
        // FROM Animals
        // WHERE Especie = @especie OR @especie IS NULL
        // GROUP BY Rasa
        // ORDER BY Rasa
    }

    [HttpGet("colors")]
    public async Task<IActionResult> GetColors()
    {
        // Similar to especies/rases
    }
}
```

#### 3. Modificar AnimalsController.cs
```csharp
// Location: /Controllers/AnimalsController.cs

[HttpGet]
public async Task<IActionResult> GetAnimals(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 20,
    [FromQuery] string? searchTerm = null,
    [FromQuery] string? idPropietari = null,
    [FromQuery] string? idEspecie = null,
    // ⬇️ NOUS PARÀMETRES
    [FromQuery] string? especie = null,        // "CANINA", "FELINA", etc.
    [FromQuery] string? rasa = null,           // "Labrador", "Persa", etc.
    [FromQuery] string? nomPropietari = null,  // Cerca per nom propietari
    [FromQuery] string? sortBy = "nom",        // "nom", "dataAlta", "ultimaVisita"
    [FromQuery] string? sortOrder = "asc")     // "asc" o "desc"
{
    // Implementar filtres adicionals i ordenació
}
```

#### 4. Nou Controller: StatsController.cs (Opcional Fase 1)
```csharp
// Location: /Controllers/StatsController.cs

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    [HttpGet("visits/count")]
    public async Task<IActionResult> GetVisitCount(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        // SQL: SELECT COUNT(*) FROM Visites WHERE ...
    }
}
```

---

### Frontend MCP Server

#### 5. Noves Tools a McpServer.cs

**Localització**: `mcp-server-stdio/Mcp/McpServer.cs` i `mcp-server-https/Mcp/McpServer.cs`

```csharp
// A HandleToolsList, afegir després de les 15 tools existents:

new Tool
{
    Name = "get_recent_visits",
    Description = "Get recent veterinary visits across all animals, ordered by date descending. Use this for queries like 'recent visits', 'animals visited today', 'last week visits'.",
    InputSchema = new InputSchema
    {
        Type = "object",
        Properties = new Dictionary<string, PropertySchema>
        {
            ["days"] = new PropertySchema
            {
                Type = "number",
                Description = "Last N days to search (default: 7, max: 90)"
            },
            ["pageSize"] = new PropertySchema
            {
                Type = "number",
                Description = "Results per page (default: 20, max: 100)"
            },
            ["pageNumber"] = new PropertySchema
            {
                Type = "number",
                Description = "Page number (default: 1)"
            },
            ["includeAnimalInfo"] = new PropertySchema
            {
                Type = "boolean",
                Description = "Include animal and owner information in response (default: true)"
            }
        }
    }
},

new Tool
{
    Name = "get_especies",
    Description = "List all animal species available in the system with counts. Use this to discover valid species names for filtering animals.",
    InputSchema = new InputSchema
    {
        Type = "object",
        Properties = new Dictionary<string, PropertySchema>()
    }
},

new Tool
{
    Name = "get_rases",
    Description = "List all animal breeds, optionally filtered by species.",
    InputSchema = new InputSchema
    {
        Type = "object",
        Properties = new Dictionary<string, PropertySchema>
        {
            ["especie"] = new PropertySchema
            {
                Type = "string",
                Description = "Filter breeds by species name (optional)"
            }
        }
    }
}
```

#### 6. Implementar Tools a HandleToolsCallAsync

```csharp
// A HandleToolsCallAsync, afegir després dels cases existents:

case "get_recent_visits":
    var days = arguments.ContainsKey("days") ? Convert.ToInt32(arguments["days"]) : 7;
    var pageSize = arguments.ContainsKey("pageSize") ? Convert.ToInt32(arguments["pageSize"]) : 20;
    var pageNumber = arguments.ContainsKey("pageNumber") ? Convert.ToInt32(arguments["pageNumber"]) : 1;
    var includeInfo = !arguments.ContainsKey("includeAnimalInfo") || Convert.ToBoolean(arguments["includeAnimalInfo"]);
    
    var recentVisits = await _apiClient.GetRecentVisitsAsync(days, pageNumber, pageSize, includeInfo);
    return JsonSerializer.Serialize(recentVisits, _jsonOptions);

case "get_especies":
    var especies = await _apiClient.GetEspeciesAsync();
    return JsonSerializer.Serialize(especies, _jsonOptions);

case "get_rases":
    var especieFilter = arguments.ContainsKey("especie") ? arguments["especie"]?.ToString() : null;
    var rases = await _apiClient.GetRasesAsync(especieFilter);
    return JsonSerializer.Serialize(rases, _jsonOptions);
```

#### 7. Afegir Mètodes a VeteriLachApiClient.cs

```csharp
// Location: mcp-server-stdio/Services/VeteriLachApiClient.cs

public async Task<RecentVisitsResponse> GetRecentVisitsAsync(
    int days = 7, 
    int pageNumber = 1, 
    int pageSize = 20,
    bool includeAnimalInfo = true)
{
    var queryParams = new List<string>
    {
        $"days={days}",
        $"pageNumber={pageNumber}",
        $"pageSize={pageSize}",
        $"includeAnimalInfo={includeAnimalInfo}"
    };
    var query = "?" + string.Join("&", queryParams);
    var response = await _httpClient.GetFromJsonAsync<RecentVisitsResponse>($"/api/visits/recent{query}", _jsonOptions);
    return response ?? new RecentVisitsResponse();
}

public async Task<EspeciesResponse> GetEspeciesAsync()
{
    var response = await _httpClient.GetFromJsonAsync<EspeciesResponse>("/api/metadata/especies", _jsonOptions);
    return response ?? new EspeciesResponse();
}

public async Task<RasesResponse> GetRasesAsync(string? especie = null)
{
    var query = !string.IsNullOrEmpty(especie) ? $"?especie={especie}" : "";
    var response = await _httpClient.GetFromJsonAsync<RasesResponse>($"/api/metadata/rases{query}", _jsonOptions);
    return response ?? new RasesResponse();
}
```

#### 8. Afegir Models a ApiModels.cs

```csharp
// Location: mcp-server-stdio/Models/ApiModels.cs

public class RecentVisitsResponse
{
    [JsonPropertyName("visits")]
    public List<VisitWithContextDto> Visits { get; set; } = new();
    
    [JsonPropertyName("pagination")]
    public PaginationInfo? Pagination { get; set; }
    
    public int TotalCount => Pagination?.TotalItems ?? 0;
}

public class VisitWithContextDto
{
    [JsonPropertyName("visitId")]
    public required string VisitId { get; set; }
    
    [JsonPropertyName("dataVisita")]
    public DateTime DataVisita { get; set; }
    
    [JsonPropertyName("motiu")]
    public string? Motiu { get; set; }
    
    [JsonPropertyName("animal")]
    public AnimalBasicDto? Animal { get; set; }
    
    [JsonPropertyName("propietari")]
    public PropietariBasicDto? Propietari { get; set; }
}

public class AnimalBasicDto
{
    [JsonPropertyName("idAnimal")]
    public required string IdAnimal { get; set; }
    
    [JsonPropertyName("nom")]
    public string Nom { get; set; } = string.Empty;
    
    [JsonPropertyName("especie")]
    public string? Especie { get; set; }
    
    [JsonPropertyName("rasa")]
    public string? Rasa { get; set; }
}

public class PropietariBasicDto
{
    [JsonPropertyName("idPropietari")]
    public required string IdPropietari { get; set; }
    
    [JsonPropertyName("nom")]
    public string Nom { get; set; } = string.Empty;
    
    [JsonPropertyName("cognoms")]
    public string Cognoms { get; set; } = string.Empty;
    
    [JsonPropertyName("telefon")]
    public string? Telefon { get; set; }
}

public class EspeciesResponse
{
    [JsonPropertyName("especies")]
    public List<EspecieDto> Especies { get; set; } = new();
}

public class EspecieDto
{
    [JsonPropertyName("nom")]
    public required string Nom { get; set; }
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
}

public class RasesResponse
{
    [JsonPropertyName("rases")]
    public List<RasaDto> Rases { get; set; } = new();
}

public class RasaDto
{
    [JsonPropertyName("nom")]
    public required string Nom { get; set; }
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("especie")]
    public string? Especie { get; set; }
}
```

---

### Testing

#### 9. Tests Unitaris API

```bash
# Tests a crear:
/tests/VeteriLach.ReadApi.Tests/Controllers/VisitsControllerTests.cs
/tests/VeteriLach.ReadApi.Tests/Controllers/MetadataControllerTests.cs
```

#### 10. Tests Integració MCP

```powershell
# Test manual servidor stdio
echo '{"jsonrpc":"2.0","id":1,"method":"tools/list"}' | .\VeteriLach.McpServer.Stdio.exe

# Verificar que les 3 noves tools apareixen: get_recent_visits, get_especies, get_rases

# Test get_recent_visits
echo '{"jsonrpc":"2.0","id":2,"method":"tools/call","params":{"name":"get_recent_visits","arguments":{"days":7}}}' | .\VeteriLach.McpServer.Stdio.exe

# Test get_especies
echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"get_especies","arguments":{}}}' | .\VeteriLach.McpServer.Stdio.exe
```

#### 11. Tests amb Claude Desktop

```
# Casos de test:

1. "Mostra'm els darrers 10 animals visitats"
   → Hauria de cridar get_recent_visits directament

2. "Quines espècies d'animals teniu?"
   → Hauria de cridar get_especies

3. "Mostra'm tots els gats"
   → Hauria de cridar get_animals amb especie=FELINA

4. "Animals de raça Labrador"
   → get_especies per trobar "CANINA", després get_animals amb rasa=Labrador

5. "Quins conills heu visitat aquesta setmana?"
   → get_especies, després get_recent_visits filtrant per espècie
```

---

## 📊 Tracking Progress

### Sprint 1 (Dies 1-3): Backend API
- [ ] Crear VisitsController amb GetRecentVisits
- [ ] Crear VisitsController amb GetVisits (global)
- [ ] Crear MetadataController amb GetEspecies
- [ ] Crear MetadataController amb GetRases
- [ ] Modificar AnimalsController afegint filtres especie, rasa, sortBy
- [ ] Tests unitaris controllers
- [ ] Deploy API a IIS i verificar endpoints

### Sprint 2 (Dies 4-6): MCP Server
- [ ] Afegir models DTO nous a ApiModels.cs (ambdues versions)
- [ ] Afegir mètodes client API a VeteriLachApiClient.cs (ambdues versions)
- [ ] Afegir 3 noves tools a McpServer.cs (ambdues versions)
- [ ] Implementar handlers a HandleToolsCallAsync (ambdues versions)
- [ ] Build stdio i HTTPS
- [ ] Tests manuals amb echo/pipe

### Sprint 3 (Dies 7-10): Testing i Refinament
- [ ] Testing exhaustiu amb Claude Desktop
- [ ] Testing amb ChatGPT Desktop (HTTPS)
- [ ] Optimitzacions SQL (índexs, query performance)
- [ ] Documentació endpoints API (Swagger)
- [ ] Actualitzar README amb noves tools
- [ ] Deploy final i validació

---

## 🎯 Definició de "Done"

Una feature està completa quan:
- ✅ Endpoint API implementat i testejat
- ✅ Swagger documentation actualitzada
- ✅ Tool MCP implementada (stdio + HTTPS)
- ✅ Tests unitaris passen
- ✅ Test manual amb Claude Desktop funciona
- ✅ Documentació actualitzada
- ✅ Deployed a entorn de test

---

## 📝 Notes d'Implementació

### SQL Performance

Afegir índexs a la BD per optimitzar queries:

```sql
-- Índex per visites recents
CREATE NONCLUSTERED INDEX IX_Visites_DataVisita 
ON Visites(DataVisita DESC)
INCLUDE (IdAnimal, Motiu);

-- Índex per cerca per espècie
CREATE NONCLUSTERED INDEX IX_Animals_Especie 
ON Animals(Especie)
INCLUDE (Nom, Rasa, IdPropietari);

-- Índex per cerca per raça
CREATE NONCLUSTERED INDEX IX_Animals_Rasa 
ON Animals(Rasa)
INCLUDE (Nom, Especie);
```

### Cache Strategy

Considerar cache per metadata (espècies, races) que canvia rarament:

```csharp
// A MetadataController
private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

[HttpGet("especies")]
public async Task<IActionResult> GetEspecies()
{
    return await _cache.GetOrCreateAsync("especies", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
        return await _repository.GetEspeciesAsync();
    });
}
```

---

## 🚀 Quick Start

```powershell
# 1. Backend API
cd C:\inetpub\wwwroot\VeteriLachApi
git pull
# Implementar controllers segons checklist
dotnet build
# Restart IIS App Pool

# 2. MCP Servers
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-stdio
# Implementar segons checklist
dotnet build -c Release

cd ..\mcp-server-https
dotnet build -c Release

# 3. Test
# Tancar Claude Desktop
# Obrir Claude Desktop
# Provar: "Mostra'm els darrers 10 animals visitats"
```

---

**Status**: 📋 Ready to implement  
**ETA**: 1-2 setmanes  
**Impacte**: Resol 75% dels casos d'ús més habituals
