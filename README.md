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

## 🐾 Endpoints Disponibles

### Animals

#### Llista paginada amb filtres
```powershell
# Llista bàsica (pàgina 1, 20 items)
GET /api/animals

# Amb paginació personalitzada
GET /api/animals?pageNumber=2&pageSize=10

# Cerca per nom o microxip
GET /api/animals?searchTerm=RICHI

# Filtrar per propietari
GET /api/animals?idPropietari={guid}

# Filtrar per espècie
GET /api/animals?idEspecie={guid}

# Combinació de filtres
GET /api/animals?pageNumber=1&pageSize=5&searchTerm=max&idEspecie={guid}
```

**Resposta paginada:**
```json
{
  "data": [
    {
      "idAnimal": "f0484004-ba6f-4722-b5be-000b84e22835",
      "nom": "RICHI",
      "sexe": 1,
      "dataNaixement": "2024-01-04T15:37:37",
      "especie": "ROSSEGADOR",
      "rasa": "HAMSTER",
      "color": null,
      "numXip": null,
      "castrat": false
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 5,
    "totalItems": 7644,
    "totalPages": 1529,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### Detall d'un animal
```powershell
GET /api/animals/{id}
```

**Resposta amb propietari:**
```json
{
  "success": true,
  "data": {
    "idAnimal": "f0484004-ba6f-4722-b5be-000b84e22835",
    "nom": "RICHI",
    "especie": "ROSSEGADOR",
    "rasa": "HAMSTER",
    "propietari": {
      "idPropietari": "3ca37a46-6c43-40e0-badf-759fa663b7ed",
      "nom": "CHIARA",
      "cognoms": "SEMINATI ",
      "email": "",
      "telefon": "655342732",
      "adresa": "CARRER ENT CORTINES 12",
      "codiPostal": "08003",
      "poblacio": "BARCELONA"
    }
  }
}
```

---

## 📁 Estructura del Projecte

```
VeteriLach.ReadApi/
├── Controllers/          # Controllers de l'API
│   ├── HealthController.cs
│   ├── AnimalsController.cs
│   └── ...
├── Middleware/           # Middleware personalitzat
│   └── ApiKeyAuthenticationMiddleware.cs
├── Application/          # Lògica de negoci (MediatR/CQRS)
│   ├── Animals/
│   │   ├── DTOs/        # Data Transfer Objects
│   │   └── Queries/     # MediatR Queries i Handlers
│   ├── Common/
│   │   ├── Behaviors/   # MediatR Pipeline Behaviors
│   │   ├── Mappings/    # AutoMapper Profiles
│   │   └── Models/      # Models compartits (PaginatedResult, etc.)
│   ├── MedicalHistory/  # (Futur)
│   └── Medicines/       # (Futur)
├── Infrastructure/       # Accés a dades i serveis externs
│   ├── Data/
│   │   ├── VeteriLachDbContext.cs
│   │   └── Entities/    # EF Core Entities (151 taules)
│   └── ExternalServices/ # (CimaVet/CIMA - Futur)
├── Program.cs            # Configuració de l'aplicació
└── appsettings.json      # Configuració
```

### Arquitectura Implementada

**Pattern CQRS amb MediatR:**
- ✅ Queries i Handlers per a operacions de lectura
- ✅ DTOs separats per a llistes i detalls
- ✅ LoggingBehavior pipeline per registrar temps d'execució

**Mapatge amb AutoMapper:**
- ✅ Perfils de mapatge centralitzats (Entity → DTO)
- ✅ `ProjectTo<>()` per a optimització de queries SQL
- ✅ `Map<>()` per a mapatges en memòria

**Entity Framework Core:**
- ✅ 151 entitats generades per scaffold de BD existent (CanMascotaBd)
- ✅ QueryTrackingBehavior.NoTracking (API read-only)
- ✅ Eager loading optimitzat per evitar N+1 queries

---

## 🏗️ Estat de Desenvolupament

### ✅ Fases Completades

- **Fase 0**: Setup i preparació (.NET 10, EF Core, Git)
- **Fase 1**: Repositori GitHub creat i configurat
- **Fase 2**: Solució i projecte creat amb tots els packages
- **Fase 3**: Configuració completa (API Keys SHA256, Serilog, Swagger)
- **Fase 4**: Entity Framework Core scaffold (151 entitats)
- **Fase 5**: MediatR i CQRS implementat
- **Fase 6**: AutoMapper i perfils de mapatge
- **Fase 7**: FluentValidation amb pipeline behaviors i filtre global

### 🔄 En Desenvolupament

_Fase base completada. Properament: API de Propietaris i Historial Clínic._

### 📅 Planificat

- API de Propietaris
- API d'Historial Clínic
- Integració CimaVet i CIMA (medicaments veterinaris/humans)
- API de Vendes i Estoc (Fase 2)

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

