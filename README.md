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

### 4. Publicar al IIS Local

Per publicar al servidor IIS local (port 5072):

```powershell
# Executar PowerShell com a Administrador i executar:
.\Deploy-ToIIS.ps1
```

📖 **Documentació completa**: Consulta [DEPLOY-IIS.md](./DEPLOY-IIS.md) per:
- Instruccions detallades de publicació
- Configuració de GitHub Actions amb runner local
- Automatització amb Git hooks
- Troubleshooting i resolució d'errors

**URL IIS Local**: http://localhost:5072

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

### Propietaris

#### Llista paginada amb filtres
```powershell
# Llista bàsica (pàgina 1, 20 items)
GET /api/propietaris

# Amb paginació personalitzada
GET /api/propietaris?pageNumber=2&pageSize=10

# Cerca per nom, cognoms, email o telèfon
GET /api/propietaris?searchTerm=barcelona

# Filtrar només actius
GET /api/propietaris?nomesActius=true

# Filtrar per població
GET /api/propietaris?poblacio=Barcelona

# Filtrar propietaris amb animals
GET /api/propietaris?ambAnimals=true

# Combinació de filtres
GET /api/propietaris?nomesActius=true&ambAnimals=true&poblacio=Barcelona
```

**Resposta paginada:**
```json
{
  "data": [
    {
      "idPropietari": "cfe68687-4c04-4795-90d5-07f40219cb23",
      "nom": "CAN MASCOTA",
      "cognoms": "",
      "email": "",
      "telefon": "932964165",
      "poblacio": "BARCELONA",
      "codiPostal": "08014",
      "totalAnimals": 1,
      "actiu": true
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 10,
    "totalItems": 5916,
    "totalPages": 592,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### Detall d'un propietari
```powershell
GET /api/propietaris/{id}
```

**Resposta amb telèfons i animals:**
```json
{
  "idPropietari": "cfe68687-4c04-4795-90d5-07f40219cb23",
  "nom": "CAN MASCOTA",
  "cognoms": "",
  "email": "",
  "ambWhatsApp": false,
  "adresa": "C/ CONSTITUCION, 79",
  "codiPostal": "08014",
  "poblacio": "BARCELONA",
  "provincia": "BARCELONA",
  "telefons": [
    {
      "numero": "932964165",
      "tipusTelefon": 1,
      "tipusTelefonDescripcio": "Fix",
      "ordre": 1
    }
  ],
  "animals": [
    {
      "idAnimal": "86a052f4-a1bc-4c5c-bc9e-0fe633cbc5e0",
      "nom": "PALOMA ARCOIRIS",
      "especie": "OCELL",
      "rasa": "PALOMA",
      "sexe": "0",
      "dataNaixement": "2013-03-15T13:14:35.297",
      "numXip": "",
      "castrat": false
    }
  ],
  "actiu": true
}
```

---

### Historial Clínic

#### Llista de visites d'un animal
```powershell
# Llista bàsica (pàgina 1, 20 items)
GET /api/animals/{idAnimal}/visits

# Amb paginació personalitzada
GET /api/animals/{idAnimal}/visits?pageNumber=2&pageSize=10

# Filtrar per període de dates
GET /api/animals/{idAnimal}/visits?dataInici=2020-01-01&dataFi=2020-12-31
```

**Resposta paginada:**
```json
{
  "data": [
    {
      "idVisita": "260d97cf-d952-43a2-859d-debfc01ba908",
      "diaVisita": "2010-01-26T18:04:15",
      "veterinari": "PAOLA PINNA",
      "resum": null,
      "pes": 5500,
      "totalTextos": 3,
      "totalProves": 0,
      "totalVacunes": 1
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalItems": 18,
    "totalPages": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

#### Detall complet d'una visita
```powershell
GET /api/visits/{idVisita}
```

**Resposta amb textos clínics, proves i vacunes:**
```json
{
  "idVisita": "260d97cf-d952-43a2-859d-debfc01ba908",
  "idPacient": "ba96fa79-710c-4974-9770-24de069bf5fc",
  "diaVisita": "2010-01-26T18:04:15",
  "veterinari": {
    "idDoctor": "d2fc2df5-9f40-4fb4-9c27-de898a1fa375",
    "nom": "PAOLA PINNA",
    "especialitat": "VETERINARI",
    "numColegiat": ""
  },
  "resum": null,
  "pes": 5500,
  "alsada": null,
  "tipusVisita": 1,
  "textosClínics": [
    {
      "indexText": 1,
      "textPla": "vacuna\ncome bien mini junior\ncaca bien \nse rasca los ojo y llora"
    },
    {
      "indexText": 2,
      "textPla": "todo ok\ntiene la congiuntiva mas roja..."
    },
    {
      "indexText": 4,
      "textPla": "milbemax\ntobradex colirio 2v/d por 7 dia..."
    }
  ],
  "proves": [],
  "vacunes": [
    {
      "idVacuna": "f9a8a9e5-6cab-4fef-afd3-09e044e89ce0",
      "tipusVacuna": "TETRAVALENTE",
      "diaVacuna": "2010-01-26T18:04:15",
      "observacions": "",
      "noRevacunar": false,
      "frequenciaDies": 365,
      "proximaVacuna": "2011-01-26T18:04:15"
    }
  ]
}
```

---

### Vendes i Facturació

#### Llista de vendes amb filtres
```powershell
# Llista bàsica (pàgina 1, 50 items)
GET /api/sales

# Amb paginació personalitzada
GET /api/sales?pageNumber=2&pageSize=10

# Filtrar per període de dates
GET /api/sales?startDate=2026-01-01&endDate=2026-12-31

# Filtrar per client
GET /api/sales?customerId={guid}

# Filtrar per venedor
GET /api/sales?sellerId={guid}

# Filtrar per animal
GET /api/sales?animalId={guid}

# Només vendes pendents de pagament
GET /api/sales?onlyPending=true

# Només vendes pagades
GET /api/sales?onlyPaid=true

# Combinació de filtres
GET /api/sales?startDate=2026-05-01&customerId={guid}&onlyPending=true
```

**Resposta paginada:**
```json
[
  {
    "saleId": "dfde8545-c88c-4475-80ee-ef8ade462711",
    "customerId": "e4cb0b07-5d54-47d1-a853-ec98ccd86536",
    "customerName": "CRISTINA",
    "sellerId": "e60b6d88-89ce-4e07-a1d3-1475db930250",
    "sellerName": "LAURA",
    "saleDate": "2026-12-22T15:36:34",
    "totalAmount": 11.63,
    "totalPaid": 0.00,
    "totalChange": 0.00,
    "pendingAmount": 11.63,
    "isFullyPaid": false,
    "paymentMethodId": "b73f84db-ef4b-4e22-b80b-2404a0abc41f",
    "paymentMethodName": "01 EFECTIU",
    "isPaymentCash": true,
    "animalId": "48b9655e-086b-4e15-b0c1-48f2c491035c",
    "animalName": "PALOMAS",
    "summary": "1xINYECTABLE EXOTICO ( <1 KG ) PROPIETARIO, 1xVERSELE PRESTIGE TORTOLAS 1kg",
    "itemCount": 2
  }
]
```

#### Detall d'una venda amb articles
```powershell
GET /api/sales/{id}
```

**Resposta amb dades completes:**
```json
{
  "saleId": "dfde8545-c88c-4475-80ee-ef8ade462711",
  "customerId": "e4cb0b07-5d54-47d1-a853-ec98ccd86536",
  "customerName": "CRISTINA",
  "customerNif": "37314505L",
  "customerPhone": "617300906",
  "customerEmail": "",
  "sellerId": "e60b6d88-89ce-4e07-a1d3-1475db930250",
  "sellerName": "LAURA",
  "saleDate": "2026-12-22T15:36:34",
  "totalAmount": 11.63,
  "totalPaid": 0.00,
  "totalChange": 0.00,
  "pendingAmount": 11.63,
  "isFullyPaid": false,
  "paymentMethodId": "b73f84db-ef4b-4e22-b80b-2404a0abc41f",
  "paymentMethodName": "01 EFECTIU",
  "isPaymentCash": true,
  "bankAccount": null,
  "animalId": "48b9655e-086b-4e15-b0c1-48f2c491035c",
  "animalName": "PALOMAS",
  "animalSpecies": "OCELL",
  "summary": "1xINYECTABLE EXOTICO ( <1 KG ) PROPIETARIO, 1xVERSELE PRESTIGE TORTOLAS 1kg",
  "items": [
    {
      "saleItemId": "a540ef68-78e5-4e01-bc06-993e796a3c1d",
      "articleId": "ab9837c5-6d7c-4ae4-a10d-4b77f2ed5774",
      "articleName": "INYECTABLE EXOTICO ( <1 KG ) PROPIETARIO",
      "quantity": 1.00,
      "unitPrice": 5.066115,
      "vatAmount": 1.063884,
      "vatRate": 21.00,
      "vatName": "IVA ESTANDAR",
      "discount": 0.000000,
      "discountPercentage": 0,
      "subtotal": 5.06611500,
      "total": 6.12999900,
      "netCost": 0.000000,
      "margin": 5.066115,
      "order": 0
    },
    {
      "saleItemId": "fb72650a-52fb-4ad2-998c-6b152109753e",
      "articleId": "61743689-c862-44fd-8526-15668ba17b35",
      "articleName": "VERSELE PRESTIGE TORTOLAS 1kg",
      "quantity": 1.00,
      "unitPrice": 5.000000,
      "vatAmount": 0.500000,
      "vatRate": 10.00,
      "vatName": "IVA REDUCIDO",
      "discount": 0.000000,
      "discountPercentage": 0,
      "subtotal": 5.00000000,
      "total": 5.50000000,
      "netCost": 1.360000,
      "margin": 3.640000,
      "marginPercentage": 267.65,
      "order": 1
    }
  ]
}
```

#### Vendes d'un client específic
```powershell
# Totes les vendes d'un client
GET /api/sales/customer/{customerId}

# Amb filtres de dates
GET /api/sales/customer/{customerId}?startDate=2026-01-01&endDate=2026-12-31

# Amb paginació
GET /api/sales/customer/{customerId}?pageNumber=1&pageSize=20
```

#### Deutes pendents
```powershell
# Tots els deutes
GET /api/sales/debts

# Filtrar per client específic
GET /api/sales/debts?customerId={guid}

# Mínim de dies pendents
GET /api/sales/debts?minimumDays=30

# Import mínim pendent
GET /api/sales/debts?minimumAmount=50.00

# Combinació de filtres
GET /api/sales/debts?minimumDays=60&minimumAmount=100
```

**Resposta de deutes:**
```json
[
  {
    "saleId": "a34a28bd-9a85-47c6-a1c5-0927399229eb",
    "customerId": "f47dec63-28e8-4296-bed5-709bb6d6f9a8",
    "customerName": "Mª CRISTINA",
    "customerNif": "37370553S",
    "customerPhone": "934210729",
    "customerEmail": "bcnmariacristina@hotmail.com",
    "saleDate": "2001-10-01T14:15:38",
    "daysPending": 9005,
    "totalAmount": 3.00,
    "totalPaid": 0.00,
    "pendingAmount": 3.00,
    "animalId": "3cd74d48-71de-4aab-9bef-6d0bca7080e6",
    "animalName": "ALEA",
    "summary": "1xCONTROL"
  }
]
```

#### Pagaments a compte (acomptes)
```powershell
# Tots els pagaments a compte
GET /api/sales/advances

# Filtrar per període de dates
GET /api/sales/advances?startDate=2026-01-01&endDate=2026-12-31

# Filtrar per client
GET /api/sales/advances?customerId={guid}

# Filtrar per animal
GET /api/sales/advances?animalId={guid}

# Amb paginació
GET /api/sales/advances?pageNumber=1&pageSize=10
```

**Resposta de pagaments a compte:**
```json
[
  {
    "paymentAdvanceId": "8dfe8cca-b7f2-4625-a54d-81713f375f21",
    "customerId": "737dfaf6-2ed8-48b5-8dde-21753ef6a9ca",
    "customerName": "YOLANDA",
    "sellerId": "93ae6d80-a18b-49a3-ba51-3eb57803fadb",
    "sellerName": "GERALDINE",
    "paymentDate": "2026-05-06T20:50:02",
    "amount": 52.00,
    "paymentMethodId": "5d6b7243-9faf-400a-b52e-fbb7e877ec48",
    "paymentMethodName": "04 TPV LA CAIXA",
    "animalId": "2068e887-3cd5-419f-b9a5-1be8b14419db",
    "animalName": "MUNAY",
    "reference": "MUNAY"
  }
]
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
- **Fase 8**: API de Propietaris (GET llista paginada amb filtres, GET detall amb animals i telèfons)
- **Fase 9**: API d'Historial Clínic (GET visites per animal amb paginació i filtres de data, GET detall de visita amb textos, proves i vacunes)

### 🔄 En Desenvolupament

_Properament: Integració CimaVet/CIMA per medicaments._

### 📅 Planificat
API d'Historial Clínic
- Integració CimaVet i CIMA (medicaments veterinaris/humans)
- API de Vendes i Estoc (Fase 2)

---

## 🌿 Git & Branching Strategy

Aquest projecte utilitza **Feature Branching** amb **Pull Requests obligatòries**.

### 🔒 Regles de Protecció

- ❌ **NO es pot pujar directament a `main`**
- ✅ **Totes les features via Pull Request**
- ✅ **Branca `main` està protegida**
- ✅ **Commits atòmics i descriptius**

### 📋 Workflow Ràpid

```bash
# 1. Crear branca de feature
git checkout main
git pull origin main
git checkout -b feature/fase-8-propietaris-api

# 2. Desenvolupar i fer commits
git add .
git commit -m "Descripció del canvi"

# 3. Pujar i crear PR
git push -u origin feature/fase-8-propietaris-api
# Crear Pull Request a GitHub

# 4. Després del merge, netejar
git checkout main
git pull origin main
git branch -d feature/fase-8-propietaris-api
```

### 📖 Documentació Completa

Consulta **[BRANCHING-STRATEGY.md](BRANCHING-STRATEGY.md)** per:
- Nomenclatura de branques (`feature/`, `fix/`, `refactor/`, etc.)
- Templates de Pull Requests
- Workflow detallat per fase
- Exemples pràctics
- Checklist pre-merge

### 🔗 Links Ràpids

- [Estratègia Completa](BRANCHING-STRATEGY.md)
- [Template PR](.github/pull_request_template.md)
- [Configurar Branch Protection](https://github.com/vbronchales/Dst.VeteriLachApi-Repositori/settings/branches)

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

### Deployment i Producció
- **[DEPLOYMENT.md](DEPLOYMENT.md)**: Guia completa de deployment a IIS (local i remot)
- **[SCRIPTS-README.md](SCRIPTS-README.md)**: Documentació dels scripts de deployment
- **Deploy-Local-Auto.ps1**: Script d'automatització (s'auto-eleva a Administrador) ⭐ Recomanat
- **Deploy-Local.ps1**: Script de deployment (requereix executar PowerShell com a Admin)
- **Build-And-Publish.ps1**: Només build i publish (no configura IIS)

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

### Deployment a IIS

#### Deployment Local Automàtic (Recomanat) ⭐
Després de fer merge d'una fase, executa el script d'automatització:

```powershell
# Des de la carpeta arrel (s'auto-eleva a Administrador)
.\Deploy-Local-Auto.ps1
```

Aquest script fa tot el procés automàticament:
- Demana permisos d'Administrador automàticament
- Build i publicació en mode Release
- Creació/actualització de l'Application Pool i Site a IIS
- Configuració de permisos
- Test del Health endpoint

**Configuració per defecte:**
- Site: VeteriLachReadAPI
- Port: 41228 (detecta automàticament si està ocupat)
- URL: http://localhost:41228

**Alternativa (només build sense IIS):**
```powershell
# Si només vols generar fitxers (no requereix Administrador)
.\Build-And-Publish.ps1
```

#### Deployment Manual i Remot

Consulta la **[Guia de Deployment Completa (DEPLOYMENT.md)](DEPLOYMENT.md)** per:
- Instruccions pas a pas per deployment manual a IIS local
- Deployment a servidors remots (sense pipelines)
- Configuració de producció (API Keys, connection strings)
- Troubleshooting i resolució de problemes
- Configuració HTTPS amb certificats

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

