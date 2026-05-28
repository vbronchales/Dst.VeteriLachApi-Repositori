# VeteriLach Read API

API REST de Consulta per a Integració MCP amb IA - VeteriLach (PRD-001)  
API de només lectura en .NET 10 amb Entity Framework Core, MediatR i integració amb CimaVet/CIMA.

## 🚀 Quick Start

### 1. Restaurar packages
```powershell
cd src\VeteriLach.ReadApi
dotnet restore
```

### 2. Executar l'aplicació
```powershell
dotnet run
```

L'API estarà disponible a:
- **HTTPS**: https://localhost:7001
- **HTTP**: http://localhost:5000
- **Swagger UI**: https://localhost:7001/swagger

### 3. Health Check
Prova que l'API funciona (no requereix API Key):
```powershell
curl https://localhost:7001/api/health
```

---

## 🔐 Autenticació amb API Key

Tots els endpoints (excepte `/api/health`) requereixen una API Key vàlida al header `X-API-Key`.

### API Keys de Desenvolupament

Aquestes són les API Keys per defecte (definides a `appsettings.json`):

| Client | API Key | Hash (appsettings.json) |
|--------|---------|-------------------------|
| MCPServer | `mcp-server-key-789` | 66c3bdc6fe32b40629de16acd841325553f0e8eeacf6b63b6620985781f978aa |
| SwaggerUI | `swagger-test-key-123` | 2d7e9dc59130c0af07b11004712a61ecd696f8eadf36eed8003db9c8f58413fd |
| Development | `dev-test-key-456` | 3209a419486ff0ab5d7cc0e9b892a0b9b070c4d8f887367bc41d8ced074b381a |

**⚠️ IMPORTANT**: Les claus MCPServer i SwaggerUI són claus de TEST. Per a producció, genera'n de noves!

### Com generar noves API Keys

Executa aquest script PowerShell:

```powershell
# Generar nova API Key
$key = [System.Convert]::ToBase64String([System.Guid]::NewGuid().ToByteArray())
$hash = [System.BitConverter]::ToString(
    [System.Security.Cryptography.SHA256]::Create().ComputeHash(
        [System.Text.Encoding]::UTF8.GetBytes($key)
    )
).Replace("-", "").ToLower()

Write-Host "`nNova API Key generada:" -ForegroundColor Green
Write-Host "API Key (dona-la al client): $key" -ForegroundColor Yellow
Write-Host "`nHash SHA256 (afegeix-lo a appsettings.json): $hash" -ForegroundColor Cyan
```

### Com usar l'API Key

#### cURL
```bash
curl -X GET "https://localhost:7001/api/animals/1" \
  -H "X-API-Key: swagger-test-key-123" \
  -H "Accept: application/json"
```

#### PowerShell
```powershell
$headers = @{
    "X-API-Key" = "swagger-test-key-123"
    "Accept" = "application/json"
}
Invoke-RestMethod -Uri "https://localhost:7001/api/animals/1" -Headers $headers
```

#### Swagger UI
1. Obre https://localhost:7001/swagger
2. Clica el botó **Authorize** (icona de cadenat)
3. Introdueix l'API Key: `swagger-test-key-123`
4. Clica **Authorize** i després **Close**
5. Ara pots provar els endpoints

---

## 📁 Estructura del Projecte

```
VeteriLach.ReadApi/
├── Controllers/          # Controllers de l'API
│   ├── HealthController.cs
│   └── ...
├── Middleware/           # Middleware personalitzat
│   └── ApiKeyAuthenticationMiddleware.cs
├── Application/          # Lògica de negoci (MediatR)
│   ├── Animals/
│   ├── MedicalHistory/
│   ├── Medicines/
│   └── Common/
├── Infrastructure/       # Accés a dades i serveis externs
│   ├── Data/
│   └── ExternalServices/
├── Program.cs            # Configuració de l'aplicació
└── appsettings.json      # Configuració
```

---

## 🔧 Configuració

### Connection String
Configura la connexió a SQL Server a `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "VeteriLachDb": "Server=localhost;Database=SelachMascotaBd;Integrated Security=true;..."
  }
}
```

### Logging
Els logs es guarden a la carpeta `logs/`:
- Format: `veterilach-api-YYYYMMDD.txt`
- Rotació: diària
- Retenció: 30 dies

---

## 📚 Documentació

### Swagger/OpenAPI
- **URL**: https://localhost:7001/swagger
- **Especificació JSON**: https://localhost:7001/swagger/v1/swagger.json

### PRD
Consulta el PRD complet a la carpeta de documentació del projecte.

---

## 🛠️ Desenvolupament

### Executar en mode desenvolupament
```powershell
dotnet run --environment Development
```

### Compilar
```powershell
dotnet build
```

### Publicar per a IIS
```powershell
dotnet publish -c Release -o ./publish
```

---

## 📝 Notes

- **Només lectura**: Aquesta API només permet operacions GET (consulta)
- **Sense autenticació complexa**: Utilitza API Keys simples (SHA256)
- **Optimitzada per a IA**: Respostes estructurades per a consum per LLMs via MCP
- **Base de dades**: Accés a la BD existent `SelachMascotaBd` (SQL Server)

---

## 🔗 Recursos

- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Serilog](https://serilog.net/)
- [Model Context Protocol](https://modelcontextprotocol.io/)

