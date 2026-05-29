# VeteriLach MCP Server (HTTP)

Servidor MCP (Model Context Protocol) per accedir a VeteriLach Read API des de l'aplicació d'escriptori de ChatGPT.

## ⚠️ IMPORTANT - Transport HTTP

Aquest servidor MCP utilitza **transport HTTPS** (HTTP segur). ChatGPT Desktop requereix servidors MCP amb comunicació HTTP/REST, i aquesta implementació utilitza HTTPS per màxima seguretat.

## Característiques

- ✅ Protocol MCP estàndard (JSON-RPC 2.0 sobre HTTPS)
- ✅ **15 tools exposades** per accedir a tota la funcionalitat de l'API
- ✅ Servidor web autònom amb certificat SSL
- ✅ Compatible amb ChatGPT Desktop
- ✅ Endpoints HTTPS RESTful segurs

## Arquitectura

```
ChatGPT Desktop
      ↓
 HTTPS (JSON-RPC)
      ↓
MCP Server HTTPS (port 5273)
      ↓
 HTTP REST API
      ↓
VeteriLach API (port 41229)
      ↓
 SQL Server
```

## Índex

- [Instal·lació Ràpida](#instal·lació-ràpida)
- [Configuració de ChatGPT Desktop](#configuració-de-chatgpt-desktop)
- [Endpoints HTTP](#endpoints-http)
- [Tools Disponibles](#tools-disponibles)
- [Test Manual](#test-manual)
- [Troubleshooting](#troubleshooting)

## Instal·lació Ràpida

### Requisits Previs

1. **Windows 10/11**
2. **.NET SDK 10.0** o superior
3. **VeteriLach Read API** funcionant a http://localhost:41229 (veure INSTALL.md)
4. **ChatGPT Desktop** instal·lat

### Pas 1: Compilar el Servidor MCP

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server
dotnet build --configuration Release
```

### Pas 2: Configurar appsettings.json

Edita `appsettings.json` si cal canviar la configuració:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Urls": "https://localhost:5273",
  "VeteriLachApi": {
    "BaseUrl": "http://localhost:41229",
    "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
    "TimeoutSeconds": 30
  },
  "McpServer": {
    "Name": "veterilach-server",
    "Version": "1.0.0"
  },
  "Kestrel": {
    "Endpoints": {
      "Https": {
        "Url": "https://localhost:5273",
        "Protocols": "Http1AndHttp2"
      }
    }
  }
}
```

### Pas 2b: Configurar certificat SSL

Per a desenvolupament local, utilitza el certificat de .NET:

```powershell
# Instal·la i confia en el certificat de desenvolupament
dotnet dev-certs https --trust
```

### Pas 3: Executar el Servidor MCP

#### Opció A: Executar manualment (desenvolupament)

```powershell
cd bin\Release\net10.0
.\VeteriLach.McpServer.exe
```

#### Opció B: Com a servei de Windows (producció)

Crea un servei de Windows perquè s'executi automàticament:

```powershell
# Instal·la NSSM (Non-Sucking Service Manager) primer
# Des de https://nssm.cc/download

# Crea el servei
nssm install VeteriLachMcpServer "C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0\VeteriLach.McpServer.exe"

# Configura el directori de treball
nssm set VeteriLachMcpServer AppDirectory "C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0"

# Inicia el servei
nssm start VeteriLachMcpServer
```

### Pas 4: Verificar que funciona

Obre el navegador o fes:

```powershell
# Test de salut
curl https://localhost:5273/health

# Resposta esperada:
# {"status":"healthy","server":"veterilach-server","version":"1.0.0","timestamp":"..."}
```

## Configuració de ChatGPT Desktop

### Localització del fitxer de configuració

El fitxer de configuració de ChatGPT Desktop es troba a:

```
%APPDATA%\ChatGPT\mcp_config.json
```

Ruta completa (ajusta segons el teu usuari):
```
C:\Users\<TeuUsuari>\AppData\Roaming\ChatGPT\mcp_config.json
```

### Format de configuració per servidor HTTP

Edita o crea el fitxer `mcp_config.json`:

```json
{
  "mcpServers": {
    "veterilach": {
      "url": "https://localhost:5273/messages",
      "transport": "http"
    }
  }
}
```

**NOTES IMPORTANTS:**
- La URL ha de ser exactament `https://localhost:5273/messages` (endpoint POST per MCP)
- Utilitza **HTTPS** (no HTTP) per comunicació segura
- El camp `transport` ha de ser `"http"` (no `"stdio"`)
- **NO** utilitzis `command`, `args` o `env` (això és per servidors stdio)

### Reiniciar ChatGPT Desktop

Després de modificar `mcp_config.json`:

1. Tanca **completament** ChatGPT Desktop
2. Assegura't que el servidor MCP està executant-se (veure Pas 3)
3. Obre ChatGPT Desktop
4. El servidor MCP hauria d'aparèixer disponible

## Endpoints HTTP

El servidor MCP exposa els següents endpoints:

### GET /

Informació bàsica del servidor:

```bash
curl https://localhost:5273/
```

Resposta:
```json
{
  "name": "veterilach-server",
  "version": "1.0.0",
  "protocol": "MCP",
  "transport": "HTTP",
  "endpoints": {
    "messages": "/messages (POST)",
    "health": "/health (GET)"
  }
}
```

### GET /health

Health check del servidor:

```bash
curl https://localhost:5273/health
```

Resposta:
```json
{
  "status": "healthy",
  "server": "veterilach-server",
  "version": "1.0.0",
  "timestamp": "2026-05-29T15:12:51.7884945Z"
}
```

### POST /messages

**Endpoint principal MCP** - Accepta peticions JSON-RPC 2.0:

```bash
curl -X POST https://localhost:5273/messages \
  -H "Content-Type: application/json" \
  -d '{
    "jsonrpc": "2.0",
    "id": 1,
    "method": "initialize",
    "params": {
      "protocolVersion": "2024-11-05",
      "capabilities": {},
      "clientInfo": {
        "name": "chatgpt-desktop",
        "version": "1.0.0"
      }
    }
  }'
```

#### Mètodes disponibles:

1. **initialize** - Inicialitza la sessió MCP
2. **tools/list** - Llista totes les tools disponibles
3. **tools/call** - Executa una tool específica

## Tools Disponibles

El servidor exposa **15 tools** agrupades en 5 categories:

### 📊 Sales API (Vendes) - 5 tools

| Tool | Descripció | Paràmetres principals |
|------|------------|----------------------|
| `get_sales` | Llista de vendes paginada | pageNumber, pageSize, startDate, endDate, customerId |
| `get_sale_detail` | Detall d'una venda | saleId (GUID) |
| `get_customer_sales` | Vendes d'un client | customerId (GUID) |
| `get_debts` | Llistat de deutes | minimumDays, minimumAmount |
| `get_payment_advances` | Llistat d'acomptes | customerId, startDate, endDate |

### 👥 Propietaris API (Clients) - 2 tools

| Tool | Descripció | Paràmetres principals |
|------|------------|----------------------|
| `get_propietaris` | Llista de propietaris | searchTerm, poblacio, codiPostal, nomes_actius |
| `get_propietari_detail` | Detall d'un propietari | propietariId (GUID) |

### 🐾 Animals API (Mascotes) - 2 tools

| Tool | Descripció | Paràmetres principals |
|------|------------|----------------------|
| `get_animals` | Llista d'animals | searchTerm, idPropietari, idEspecie |
| `get_animal_detail` | Detall d'un animal | animalId (GUID) |

### 🏥 Medical History API (Historial Clínic) - 2 tools

| Tool | Descripció | Paràmetres principals |
|------|------------|----------------------|
| `get_animal_visits` | Historial de visites | animalId (GUID), dataInici, dataFi |
| `get_visit_detail` | Detall d'una visita | visitId (GUID) |

### 💊 Medicines API (Medicaments) - 4 tools

| Tool | Descripció | Paràmetres principals |
|------|------------|----------------------|
| `search_veterinary_medicines` | Cerca medicaments veterinaris | query, species |
| `get_veterinary_medicine` | Detall medicament veterinari | cnCode |
| `search_human_medicines` | Cerca medicaments humans | query |
| `get_human_medicine` | Detall medicament humà | cnCode |

### Exemples d'ús

#### 1. Inicialitzar sessió

```powershell
$body = @{
    jsonrpc = "2.0"
    id = 1
    method = "initialize"
    params = @{
        protocolVersion = "2024-11-05"
        capabilities = @{}
        clientInfo = @{
            name = "test-client"
            version = "1.0.0"
        }
    }
} | ConvertTo-Json -Depth 5

Invoke-RestMethod -Uri "http://localhost:5273/messages" -Method Post -Body $body -ContentType "application/json"
```

#### 2. Llistar tools

```powershell
$body = @{
    jsonrpc = "2.0"
    id = 2
    method = "tools/list"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5273/messages" -Method Post -Body $body -ContentType "application/json"
```

#### 3. Executar una tool (exemple: get_sales)

```powershell
$body = @{
    jsonrpc = "2.0"
    id = 3
    method = "tools/call"
    params = @{
        name = "get_sales"
        arguments = @{
            pageNumber = 1
            pageSize = 10
            onlyUnpaid = $false
        }
    }
} | ConvertTo-Json -Depth 5

Invoke-RestMethod -Uri "http://localhost:5273/messages" -Method Post -Body $body -ContentType "application/json"
```

## Test Manual

### 1. Verificar que el servidor MCP està executant-se

```powershell
# Health check
Invoke-WebRequest -Uri "https://localhost:5273/health" -UseBasicParsing

# Hauria de retornar 200 OK amb:
# {"status":"healthy",...}
```

### 2. Verificar que l'API funciona

```powershell
# Test API directament
$headers = @{ "X-Api-Key" = "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4" }
Invoke-WebRequest -Uri "http://localhost:41229/api/health" -Headers $headers -UseBasicParsing
```

### 3. Test complet: Llistar vendes

```powershell
$body = @{
    jsonrpc = "2.0"
    id = 1
    method = "tools/call"
    params = @{
        name = "get_sales"
        arguments = @{
            pageSize = 5
        }
    }
} | ConvertTo-Json -Depth 5

Invoke-RestMethod -Uri "https://localhost:5273/messages" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
```

## Troubleshooting

### ❌ Error: "No se puede conectar al servidor MCP"

**Causa**: El servidor MCP no està executant-se.

**Solució**:
```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0
.\VeteriLach.McpServer.exe

# O verifica que el servei està en marxa:
nssm status VeteriLachMcpServer
```

### ❌ Error: HTTP 500 al cridar una tool

**Causa**: L'API de VeteriLach no està funcionant o retorna errors.

**Solució**:
```powershell
# Verifica que l'API està en marxa
Invoke-WebRequest -Uri "http://localhost:41229/api/health" -UseBasicParsing

# Si retorna 503, reinicia l'App Pool d'IIS:
Restart-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

### ❌ Error: "API Key no válida" (401)

**Causa**: L'API Key configurada no coincideix.

**Solució**:
1. Verifica l'API Key a `C:\inetpub\wwwroot\VeteriLachApi\appsettings.json`
2. Verifica l'API Key a `mcp-server\appsettings.json`
3. Han de ser idèntiques

### ❌ ChatGPT Desktop no detecta el servidor MCP

**Causes possibles**:

1. **Format incorrecte del mcp_config.json**:
   ```json
   // ❌ INCORRECTE (stdio format)
   {
     "mcpServers": {
       "veterilach": {
         "command": "...",  // ❌ NO per HTTP
         "args": []
       }
     }
   }

   // ✅ CORRECTE (HTTP format)
   {
     "mcpServers": {
       "veterilach": {
         "url": "http://localhost:5273/messages",
         "transport": "http"
       }
     }
   }
   ```

2. **Servidor no executant-se**: Verifica amb `curl https://localhost:5273/health`

3. **ChatGPT Desktop no reiniciat**: Tanca completament i reobre l'aplicació

### ❌ Error de connexió a la base de dades

**Causa**: SQL Server no està en marxa o ConnectionString incorrecte.

**Solució**:
```powershell
# Verifica SQL Server
Get-Service MSSQLSERVER

# Si està aturat, inicia'l:
Start-Service MSSQLSERVER

# Verifica ConnectionString a:
# C:\inetpub\wwwroot\VeteriLachApi\appsettings.json
```

### ❌ Port 5273 ja està en ús

**Causa**: Un altre procés està usant el port 5273.

**Solució**:

**Opció 1**: Canvia el port a `appsettings.json`:
```json
{
  "Urls": "https://localhost:5280"
}
```

I actualitza `mcp_config.json`:
```json
{
  "mcpServers": {
    "veterilach": {
      "url": "https://localhost:5280/messages",
      "transport": "http"
    }
  }
}
```

**Opció 2**: Identifica i atura el procés:
```powershell
# Troba el procés que usa el port
netstat -ano | findstr :5273

# Atura'l (canvia PID pel número que surt)
Stop-Process -Id <PID> -Force
```

## Seguretat

### ⚠️ IMPORTANT - Només per ús local

Aquest servidor MCP està dissenyat per **ús local** (localhost) solament:

- ✅ Exposició a `https://localhost:5273` (només accessible des de la mateixa màquina)
- ✅ Utilitza **HTTPS** per comunicació xifrada
- ✅ Certificat SSL de desenvolupament (auto-signat, confiat localment)
- ❌ **NO exposar públicament a Internet sense certificat vàlid**
- ❌ **NO usar en producció sense autenticació addicional**

### API Key

L'API Key està hardcodejada per simplicitat. En un entorn de producció:

1. Utilitza **variables d'entorn** per l'API Key
2. Implementa **rotació periòdica** de claus
3. Per producció, usa **certificat SSL vàlid** (no auto-signat)
4. Implementa **autenticació adicional** (OAuth2, JWT, etc.)

## Arquitectura Tècnica

### Components

```
┌─────────────────────────────────────┐
│   ChatGPT Desktop (Client)          │
└──────────────┬──────────────────────┘
               │ HTTPS POST /messages
               │ (JSON-RPC 2.0)
               ▼
┌─────────────────────────────────────┐
│   MCP Server (ASP.NET Core)         │
│   - Port: 5273 (HTTPS)              │
│   - Endpoints: /, /health, /messages│
│   - Protocol: MCP v2024-11-05       │
│   - SSL: Certificat desenvolupament │
└──────────────┬──────────────────────┘
               │ HTTP REST calls
               │ (X-Api-Key header)
               ▼
┌─────────────────────────────────────┐
│   VeteriLach Read API               │
│   - Port: 41229                     │
│   - IIS + ASP.NET Core 8.0          │
└──────────────┬──────────────────────┘
               │ SQL Server queries
               ▼
┌─────────────────────────────────────┐
│   SQL Server (CanMascotaBd)         │
└─────────────────────────────────────┘
```

### Flux d'una petició

1. **ChatGPT Desktop** envia una petició JSON-RPC a `POST https://localhost:5273/messages`
2. **MCP Server** rep la petició, deserialitza i identifica el mètode
3. Si és `tools/call`, extreu el nom de la tool i arguments
4. **MCP Server** crida l'API de VeteriLach corresponent amb l'API Key
5. **VeteriLach API** consulta la base de dades SQL Server
6. **VeteriLach API** retorna JSON a MCP Server
7. **MCP Server** serialitza la resposta en format MCP i la retorna
8. **ChatGPT Desktop** processa la resposta i la mostra a l'usuari

## Desenvolupament

### Estructura del projecte

```
mcp-server/
├── Program.cs                  # Entrada del servidor web ASP.NET Core (HTTPS)
├── appsettings.json           # Configuració del servidor (incl. SSL)
├── Mcp/
│   ├── McpModels.cs           # Models del protocol MCP (JSON-RPC)
│   └── McpServer.cs           # Lògica del servidor MCP (15 tools)
├── Models/
│   └── ApiModels.cs           # DTOs de l'API de VeteriLach
├── Services/
│   └── VeteriLachApiClient.cs # Client HTTP per l'API
└── VeteriLach.McpServer.csproj
```

### Afegir noves tools

Per afegir una nova tool al servidor:

1. **Afegir el mètode Execute a `McpServer.cs`**:
   ```csharp
   private async Task<ToolResponse> ExecuteMyNewToolAsync(Dictionary<string, object> arguments)
   {
       var param1 = GetStringArg(arguments, "param1", required: true);
       var result = await _apiClient.GetMyNewDataAsync(param1);
       return new ToolResponse
       {
           Content = new List<ContentItem>
           {
               new() { Type = "text", Text = JsonSerializer.Serialize(result) }
           }
       };
   }
   ```

2. **Afegir la definició de la tool a `HandleToolsList()`**:
   ```csharp
   new Tool
   {
       Name = "my_new_tool",
       Description = "Descripció de la nova tool",
       InputSchema = new InputSchema
       {
           Type = "object",
           Properties = new Dictionary<string, PropertySchema>
           {
               ["param1"] = new() { Type = "string", Description = "Paràmetre 1" }
           },
           Required = new List<string> { "param1" }
       }
   }
   ```

3. **Afegir el case a `HandleToolsCallAsync()`**:
   ```csharp
   "my_new_tool" => await ExecuteMyNewToolAsync(arguments),
   ```

4. **Afegir el mètode a `VeteriLachApiClient.cs`**:
   ```csharp
   public async Task<MyNewDto> GetMyNewDataAsync(string param1)
   {
       var response = await _httpClient.GetAsync($"/api/mynew/{param1}");
       response.EnsureSuccessStatusCode();
       return await response.Content.ReadFromJsonAsync<MyNewDto>();
   }
   ```

### Compilar i executar en mode desenvolupament

```powershell
# Compilar
dotnet build

# Executar en mode desenvolupament (amb hot reload)
dotnet run

# Executar en mode Release
dotnet build -c Release
cd bin\Release\net10.0
.\VeteriLach.McpServer.exe
```

### Logs

El servidor registra tota l'activitat a la consola:

```
info: VeteriLach.McpServer[0]
      VeteriLach MCP Server v1.0.0 starting...
info: VeteriLach.McpServer[0]
      API URL: http://localhost:41229
info: VeteriLach.McpServer[0]
      Server ready. Listening for HTTP requests...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5273
info: VeteriLach.McpServer[0]
      Processing method: tools/call
info: VeteriLach.McpServer[0]
      Response sent for method: tools/call
```

## Changelog

### v1.0.0 (2026-05-29) - HTTP Transport

- 🚀 **BREAKING CHANGE**: Migració de transport stdio a HTTPS
- ✅ Servidor web ASP.NET Core amb endpoints RESTful segurs (HTTPS)
- ✅ Certificat SSL de desenvolupament per comunicació xifrada
- ✅ 15 tools implementades:
  - 5 Sales API (get_sales, get_sale_detail, get_customer_sales, get_debts, get_payment_advances)
  - 2 Propietaris API (get_propietaris, get_propietari_detail)
  - 2 Animals API (get_animals, get_animal_detail)
  - 2 Medical History API (get_animal_visits, get_visit_detail)
  - 4 Medicines API (search_veterinary_medicines, get_veterinary_medicine, search_human_medicines, get_human_medicine)
- ✅ Endpoints: `GET /`, `GET /health`, `POST /messages`
- ✅ Compatible amb ChatGPT Desktop
- ✅ Documentació completa d'instal·lació i configuració

### v0.1.0 (stdio - deprecated)

- Implementació inicial amb transport stdio
- **NOTA**: Aquesta versió no és compatible amb ChatGPT Desktop

## Llicència

Aquest projecte és propietat de **DaSeTeo** i està destinat a ús intern exclusivament.

## Contacte

Per problemes o preguntes, contacta amb l'equip de desenvolupament de DaSeTeo.
