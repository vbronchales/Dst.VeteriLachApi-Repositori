# VeteriLach MCP Server

Servidor MCP (Model Context Protocol) per accedir a VeteriLach Read API des de l'aplicació d'escriptori de ChatGPT.

## Característiques

- ✅ Protocol MCP estàndard (JSON-RPC 2.0 sobre stdio)
- ✅ **15 tools exposades** per accedir a tota la funcionalitat de l'API
- ✅ Configuració mínima (URL API + API Key)
- ✅ Compatible amb ChatGPT Desktop

## Índex

- [Checklist d'Instal·lació Ràpida](#checklist-dinstal·lació-ràpida)
- [Guia d'Instal·lació Completa](#guia-dinstal·lació-completa)
- [Tools Disponibles](#tools-disponibles)
- [Test Manual i Depuració](#test-manual-i-depuració)
- [Configuració Avançada](#configuració-avançada)
- [Troubleshooting](#troubleshooting)
- [Seguretat](#seguretat)

## Checklist d'Instal·lació Ràpida

Utilitza aquesta checklist per verificar que tens tot el necessari:

### Requisits Previs
- [ ] Windows 10/11 instal·lat
- [ ] IIS instal·lat i configurat
- [ ] .NET SDK 10.0 o superior (`dotnet --version`)
- [ ] SQL Server amb base de dades VeteriLach
- [ ] ChatGPT Desktop instal·lat
- [ ] PowerShell amb permisos d'administrador

### Instal·lació de l'API
- [ ] Repositori clonat a `C:\Dst2026\Dst.VeteriLachApi-Repositori`
- [ ] API publicada a `C:\inetpub\wwwroot\VeteriLachApi`
- [ ] App Pool "VeteriLAchReadApiAppPool" creat i en marxa
- [ ] Port 41229 configurat a IIS
- [ ] ConnectionString configurat correctament a `appsettings.json`
- [ ] API Key configurada
- [ ] Health check funciona: http://localhost:41229/api/health

### Instal·lació del Servidor MCP
- [ ] Servidor MCP compilat: `dotnet build -c Release`
- [ ] Binary generat a `mcp-server\bin\Release\net10.0\VeteriLach.McpServer.exe`
- [ ] `appsettings.json` del servidor MCP configurat
- [ ] Test manual del servidor funciona

### Configuració de ChatGPT Desktop
- [ ] Fitxer `claude_desktop_config.json` creat/editat
- [ ] Path al `.exe` correcte amb dobles barres invertides
- [ ] ChatGPT Desktop reiniciat
- [ ] Servidor MCP detectat a ChatGPT Desktop
- [ ] Test d'una tool funciona correctament

## Tools Disponibles

### Sales API (Vendes) - 5 tools

#### 1. `get_sales`
Obté una llista paginada de vendes amb filtres opcionals.

**Paràmetres:**
- `pageNumber` (opcional): Número de pàgina (defecte: 1)
- `pageSize` (opcional): Mida de pàgina (defecte: 20, màx: 50)
- `startDate` (opcional): Data inici (YYYY-MM-DD)
- `endDate` (opcional): Data fi (YYYY-MM-DD)
- `customerId` (opcional): ID client (GUID)
- `sellerId` (opcional): ID venedor (GUID)
- `animalId` (opcional): ID animal (GUID)
- `onlyUnpaid` (opcional): Només vendes impagades

#### 2. `get_sale_detail`
Obté informació detallada d'una venda específica amb tots els articles.

**Paràmetres:**
- `saleId` (requerit): ID de la venda (GUID)

#### 3. `get_customer_sales`
Obté totes les vendes d'un client específic.

**Paràmetres:**
- `customerId` (requerit): ID client (GUID)
- `pageNumber`, `pageSize`, `startDate`, `endDate` (opcionals)

#### 4. `get_debts`
Obté la llista de deutes (vendes impagades o parcialment pagades).

**Paràmetres:**
- `pageNumber`, `pageSize` (opcionals)
- `customerId` (opcional): Filtrar per client
- `minimumDays` (opcional): Dies mínims pendents
- `minimumAmount` (opcional): Import mínim pendent

#### 5. `get_payment_advances`
Obté la llista d'acomptes de clients.

**Paràmetres:**
- `pageNumber`, `pageSize` (opcionals)
- `customerId` (opcional): Filtrar per client
- `startDate`, `endDate` (opcionals)

### Propietaris API (Clients) - 2 tools

#### 6. `get_propietaris`
Obté una llista paginada de propietaris/clients amb filtres opcionals.

**Paràmetres:**
- `pageNumber`, `pageSize` (opcionals)
- `searchTerm` (opcional): Cerca per nom, email o telèfon
- `poblacio` (opcional): Filtrar per població
- `codiPostal` (opcional): Filtrar per codi postal
- `nomes_actius` (opcional): Només clients actius

#### 7. `get_propietari_detail`
Obté informació detallada d'un propietari incloent animals i telèfons.

**Paràmetres:**
- `propietariId` (requerit): ID del propietari (GUID)

### Animals API (Mascotes) - 2 tools

#### 8. `get_animals`
Obté una llista paginada d'animals/mascotes amb filtres opcionals.

**Paràmetres:**
- `pageNumber`, `pageSize` (opcionals)
- `searchTerm` (opcional): Cerca per nom o xip
- `idPropietari` (opcional): Filtrar per propietari
- `idEspecie` (opcional): Filtrar per espècie

#### 9. `get_animal_detail`
Obté informació detallada d'un animal incloent dades del propietari.

**Paràmetres:**
- `animalId` (requerit): ID de l'animal (GUID)

### Medical History API (Historial Clínic) - 2 tools

#### 10. `get_animal_visits`
Obté l'historial de visites mèdiques d'un animal.

**Paràmetres:**
- `animalId` (requerit): ID de l'animal (GUID)
- `pageNumber`, `pageSize` (opcionals)
- `dataInici` (opcional): Data inici (YYYY-MM-DD)
- `dataFi` (opcional): Data fi (YYYY-MM-DD)

#### 11. `get_visit_detail`
Obté detalls complets d'una visita veterinària incloent notes clíniques, proves i vacunes.

**Paràmetres:**
- `visitId` (requerit): ID de la visita (GUID)

### Medicines API (Medicaments) - 4 tools

#### 12. `search_veterinary_medicines`
Cerca medicaments veterinaris a la base de dades CimaVet.

**Paràmetres:**
- `query` (requerit): Text de cerca (nom, principi actiu, codi)
- `species` (opcional): Espècie animal

#### 13. `get_veterinary_medicine`
Obté informació detallada d'un medicament veterinari per codi nacional.

**Paràmetres:**
- `cnCode` (requerit): Codi Nacional (CN)

#### 14. `search_human_medicines`
Cerca medicaments humans a la base de dades CIMA.

**Paràmetres:**
- `query` (requerit): Text de cerca (nom, principi actiu, codi)

#### 15. `get_human_medicine`
Obté informació detallada d'un medicament humà per codi nacional.

**Paràmetres:**
- `cnCode` (requerit): Codi Nacional (CN)

## Guia d'Instal·lació Completa

Aquesta guia descriu com instal·lar i configurar el servidor MCP de VeteriLach en una màquina nova.

### Requisits Previs

Abans de començar, assegura't que tens instal·lat:

1. **Windows** (provada en Windows 10/11)
2. **IIS (Internet Information Services)** amb:
   - ASP.NET Core Hosting Bundle 8.0 o superior
   - Application Pools configurables
3. **.NET SDK 10.0** o superior
   ```powershell
   # Verifica la versió
   dotnet --version
   ```
4. **ChatGPT Desktop** (aplicació d'escriptori oficial)
5. **SQL Server** amb la base de dades VeteriLach
6. **PowerShell 5.1** o superior amb permisos d'administrador

### Pas 1: Desplegar VeteriLach Read API a IIS

#### 1.1 Publicar l'API

Clona el repositori i compila l'API:

```powershell
# Clona el repositori
cd C:\Dst2026
git clone https://github.com/vbronchales/Dst.VeteriLachApi-Repositori.git
cd Dst.VeteriLachApi-Repositori

# Publica l'API
dotnet publish src/VeteriLach.ReadApi/VeteriLach.ReadApi.csproj -c Release -o C:\temp\VeteriLachApi_publish
```

#### 1.2 Configurar IIS

Obre **PowerShell com a Administrador** i executa:

```powershell
# Importa el mòdul d'IIS
Import-Module WebAdministration

# Crea l'App Pool
New-WebAppPool -Name "VeteriLAchReadApiAppPool"
Set-ItemProperty IIS:\AppPools\VeteriLAchReadApiAppPool -Name "managedRuntimeVersion" -Value ""

# Crea el directori de l'aplicació
New-Item -ItemType Directory -Force -Path "C:\inetpub\wwwroot\VeteriLachApi"

# Copia els fitxers publicats
Copy-Item -Path "C:\temp\VeteriLachApi_publish\*" -Destination "C:\inetpub\wwwroot\VeteriLachApi" -Recurse -Force

# Crea el lloc web (si no existeix)
New-WebSite -Name "VeteriLachReadApi" -Port 41229 -PhysicalPath "C:\inetpub\wwwroot\VeteriLachApi" -ApplicationPool "VeteriLAchReadApiAppPool" -Force

# Inicia l'App Pool
Start-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

#### 1.3 Configurar appsettings.json de l'API

Edita `C:\inetpub\wwwroot\VeteriLachApi\appsettings.json`:

```json
{
  "ConnectionStrings": {
    "VeteriLachConnection": "Server=localhost;Database=CanMascotaBd;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  },
  "ApiKeys": {
    "MCPServer": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
    "SwaggerUI": "swagger-ui-key-2024",
    "Development": "dev-key-local-2024",
    "Test": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

**IMPORTANT**: Ajusta el `ConnectionString` segons la teva configuració de SQL Server.

#### 1.4 Verificar que l'API funciona

```powershell
# Test de salut
Invoke-WebRequest -Uri "http://localhost:41229/api/health" -UseBasicParsing

# Test amb API Key
Invoke-WebRequest -Uri "http://localhost:41229/api/sales?pageSize=1" -Headers @{"X-Api-Key"="4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4"} -UseBasicParsing
```

Si reps resposta 200 OK, l'API està funcionant correctament. Pots accedir a **Swagger** a:
- http://localhost:41229/swagger

### Pas 2: Compilar el Servidor MCP

#### 2.1 Navega al directori del servidor MCP

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server
```

#### 2.2 Configura appsettings.json del servidor MCP

Edita `appsettings.json` (ja hauria d'estar correcte):

```json
{
  "VeteriLachApi": {
    "BaseUrl": "http://localhost:41229",
    "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
    "TimeoutSeconds": 30
  }
}
```

**IMPORTANT**: 
- `BaseUrl` ha de coincidir amb el port de l'API a IIS (41229)
- `ApiKey` ha de coincidir amb una de les claus definides a l'API

#### 2.3 Compila el servidor en mode Release

```powershell
dotnet build -c Release
```

El binari es generarà a:
```
mcp-server\bin\Release\net10.0\VeteriLach.McpServer.exe
```

#### 2.4 Verifica la compilació

```powershell
cd bin\Release\net10.0

# Test d'inicialització
echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}' | .\VeteriLach.McpServer.exe 2>$null

# Hauries de veure una resposta JSON amb "protocolVersion": "2024-11-05"
```

### Pas 3: Configurar ChatGPT Desktop

#### 3.1 Localitza el fitxer de configuració

El fitxer de configuració de ChatGPT Desktop es troba a:

**Windows:**
```
%APPDATA%\Claude\claude_desktop_config.json
```

Ruta completa (usualment):
```
C:\Users\[TeuUsuari]\AppData\Roaming\Claude\claude_desktop_config.json
```

#### 3.2 Crea o edita el fitxer de configuració

Si el fitxer no existeix, crea'l. Si ja existeix, afegeix la secció `mcpServers`.

**Contingut complet del fitxer:**

```json
{
  "mcpServers": {
    "veterilach": {
      "command": "C:\\Dst2026\\Dst.VeteriLachApi-Repositori\\mcp-server\\bin\\Release\\net10.0\\VeteriLach.McpServer.exe",
      "args": []
    }
  }
}
```

**IMPORTANT**: 
- Assegura't que el path apunta al fitxer .exe compilat
- Usa dobles barres invertides (`\\`) en el path de Windows
- Ajusta el path si has clonat el repositori en una ubicació diferent

**Exemple amb múltiples servidors MCP:**

```json
{
  "mcpServers": {
    "veterilach": {
      "command": "C:\\Dst2026\\Dst.VeteriLachApi-Repositori\\mcp-server\\bin\\Release\\net10.0\\VeteriLach.McpServer.exe",
      "args": []
    },
    "altreServidor": {
      "command": "C:\\path\\to\\other\\server.exe",
      "args": []
    }
  }
}
```

#### 3.3 Reinicia ChatGPT Desktop

1. **Tanca completament** l'aplicació ChatGPT Desktop
2. **Torna a obrir** l'aplicació
3. El servidor MCP es carregarà automàticament

### Pas 4: Verificar la Instal·lació

#### 4.1 Comprova que ChatGPT detecta el servidor

A ChatGPT Desktop, hauries de veure:
- Una icona o indicador que mostra servidors MCP actius
- El servidor "veterilach" llistat com a disponible

#### 4.2 Prova una tool

A ChatGPT Desktop, escriu:

```
Mostra'm les últimes 3 vendes utilitzant el servidor VeteriLach
```

o

```
Cerca animals amb el nom "Max"
```

Si funciona correctament, ChatGPT utilitzarà les tools del servidor MCP per obtenir les dades.

#### 4.3 Logs i Depuració

Si hi ha problemes, pots executar el servidor manualment per veure els logs:

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0

# Executa el servidor (els logs surten per stderr)
echo '{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}' | .\VeteriLach.McpServer.exe

# Hauries de veure 15 tools llistades
```

### Pas 5: Manteniment

#### Actualitzar el servidor MCP

Quan hi hagi noves versions del servidor MCP:

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori

# Actualitza el repositori
git pull

# Recompila el servidor MCP
cd mcp-server
dotnet build -c Release

# Reinicia ChatGPT Desktop
```

**NO cal reconfigurar** `claude_desktop_config.json` si el path no canvia.

#### Actualitzar l'API a IIS

Quan hi hagi noves versions de l'API:

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori

# Actualitza el repositori
git pull

# Executa el script de deploy (com a Administrador)
.\Quick-Deploy.ps1

# O manualment:
dotnet publish src/VeteriLach.ReadApi/VeteriLach.ReadApi.csproj -c Release -o C:\temp\VeteriLachApi_publish

# Para l'App Pool
Import-Module WebAdministration
Stop-WebAppPool -Name "VeteriLAchReadApiAppPool"

# Copia els fitxers
Copy-Item -Path "C:\temp\VeteriLachApi_publish\*" -Destination "C:\inetpub\wwwroot\VeteriLachApi" -Recurse -Force -Exclude @("web.config", "*.log")

# Reinicia l'App Pool
Start-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

## Test Manual i Depuració

### Tests Bàsics

#### Test 1: Verificar que el servidor inicia

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0

echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}' | .\VeteriLach.McpServer.exe 2>$null
```

**Resposta esperada**:
```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "result": {
    "protocolVersion": "2024-11-05",
    "serverInfo": {
      "name": "veterilach-server",
      "version": "1.0.0"
    },
    "capabilities": {
      "tools": {}
    }
  }
}
```

#### Test 2: Llistar totes les tools

```powershell
echo '{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}' | .\VeteriLach.McpServer.exe 2>$null
```

Hauries de veure 15 tools llistades amb els seus esquemes.

#### Test 3: Executar una tool (get_sales)

```powershell
echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"get_sales","arguments":{"pageSize":2}}}' | .\VeteriLach.McpServer.exe 2>$null
```

**Resposta esperada**: JSON amb dades de vendes de l'API.

#### Test 4: Executar tool de propietaris

```powershell
echo '{"jsonrpc":"2.0","id":4,"method":"tools/call","params":{"name":"get_propietaris","arguments":{"pageSize":3,"nomes_actius":true}}}' | .\VeteriLach.McpServer.exe 2>$null
```

#### Test 5: Executar tool de cerca de medicaments

```powershell
echo '{"jsonrpc":"2.0","id":5,"method":"tools/call","params":{"name":"search_veterinary_medicines","arguments":{"query":"amoxicilina","species":"Perro"}}}' | .\VeteriLach.McpServer.exe 2>$null
```

### Veure Logs del Servidor

Per veure els logs mentre proves el servidor:

```powershell
# Redirigeix stderr a un fitxer
echo '{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}' | .\VeteriLach.McpServer.exe 2> logs.txt

# Mostra els logs
Get-Content logs.txt
```

### Test de Connectivitat amb l'API

```powershell
# Health check
Invoke-WebRequest -Uri "http://localhost:41229/api/health" -UseBasicParsing

# Test amb API Key
$headers = @{"X-Api-Key"="4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4"}
Invoke-WebRequest -Uri "http://localhost:41229/api/sales?pageSize=1" -Headers $headers -UseBasicParsing

# Test de propietaris
Invoke-WebRequest -Uri "http://localhost:41229/api/propietaris?pageSize=1" -Headers $headers -UseBasicParsing

# Test d'animals
Invoke-WebRequest -Uri "http://localhost:41229/api/animals?pageSize=1" -Headers $headers -UseBasicParsing
```

## Configuració Avançada

### Executar en una màquina diferent

Si vols executar el servidor MCP en una màquina i accedir a l'API en una altra:

1. **Configura l'API per acceptar connexions remotes** (a la màquina amb IIS):
   ```powershell
   # Afegeix un binding a IIS per l'IP de la màquina
   New-WebBinding -Name "VeteriLachReadApi" -Protocol http -Port 41229 -IPAddress "*"
   
   # Obre el firewall
   New-NetFirewallRule -DisplayName "VeteriLach API" -Direction Inbound -Protocol TCP -LocalPort 41229 -Action Allow
   ```

2. **Actualitza appsettings.json del servidor MCP** (a la màquina client):
   ```json
   {
     "VeteriLachApi": {
       "BaseUrl": "http://192.168.1.100:41229",  // IP de la màquina amb l'API
       "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
       "TimeoutSeconds": 30
     }
   }
   ```

### Múltiples Entorns (Desenvolupament, Producció)

Pots crear múltiples fitxers de configuració:

**appsettings.Development.json**:
```json
{
  "VeteriLachApi": {
    "BaseUrl": "http://localhost:41229",
    "ApiKey": "dev-key-local-2024",
    "TimeoutSeconds": 60
  }
}
```

**appsettings.Production.json**:
```json
{
  "VeteriLachApi": {
    "BaseUrl": "http://production-server:41229",
    "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
    "TimeoutSeconds": 30
  }
}
```

Per utilitzar un entorn específic:
```json
// claude_desktop_config.json
{
  "mcpServers": {
    "veterilach-dev": {
      "command": "C:\\path\\to\\VeteriLach.McpServer.exe",
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "veterilach-prod": {
      "command": "C:\\path\\to\\VeteriLach.McpServer.exe",
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Production"
      }
    }
  }
}
```

### Configurar Logging Avançat

Pots afegir logging més detallat creant un fitxer `appsettings.json` amb:

```json
{
  "VeteriLachApi": {
    "BaseUrl": "http://localhost:41229",
    "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
    "TimeoutSeconds": 30
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

## Arquitectura del Sistema

### Flux de Comunicació

```
┌─────────────────────┐
│  ChatGPT Desktop    │
│                     │
│  "Mostra vendes"    │
└──────────┬──────────┘
           │ stdio (JSON-RPC 2.0)
           ▼
┌─────────────────────┐
│  MCP Server         │
│  VeteriLach.McpServer│
│                     │
│  - Inicialització   │
│  - Tools List       │
│  - Tools Call       │
└──────────┬──────────┘
           │ HTTP + X-Api-Key
           ▼
┌─────────────────────┐
│  VeteriLach API     │
│  (IIS + ASP.NET)    │
│                     │
│  - AnimalsController│
│  - PropietarisCtrl  │
│  - SalesController  │
│  - MedicinesCtrl    │
│  - MedicalHistory   │
└──────────┬──────────┘
           │ Entity Framework
           ▼
┌─────────────────────┐
│  SQL Server         │
│  CanMascotaBd       │
└─────────────────────┘
```

### Components

1. **ChatGPT Desktop**: Aplicació client que invoca les tools MCP
2. **MCP Server**: Servidor que implementa el protocol MCP i tradueix peticions a crides HTTP
3. **VeteriLach API**: API REST que accedeix a la base de dades
4. **SQL Server**: Base de dades amb les dades de VeteriLach

### Protocol MCP

El servidor utilitza **JSON-RPC 2.0** sobre **stdin/stdout**:

- **stdin**: Rep peticions JSON-RPC de ChatGPT Desktop
- **stdout**: Retorna respostes JSON-RPC
- **stderr**: Escriu logs (no es llegeixen per ChatGPT)

### Mètodes Suportats

1. **initialize**: Handshake inicial, retorna capacitats del servidor
2. **tools/list**: Llista totes les tools disponibles amb els seus esquemes
3. **tools/call**: Executa una tool específica amb arguments

## Estructura del Projecte

```
mcp-server/
├── Program.cs                  # Punt d'entrada, stdin/stdout handler
├── appsettings.json           # Configuració
├── Mcp/
│   ├── McpModels.cs          # Models del protocol MCP
│   └── McpServer.cs          # Implementació servidor MCP
├── Models/
│   └── ApiModels.cs          # Models de dades de l'API
└── Services/
    └── VeteriLachApiClient.cs # Client HTTP per l'API
```

## Troubleshooting

### Problemes amb l'API

#### Error 503 Service Unavailable

**Problema**: L'API no respon a http://localhost:41229

**Solucions**:

1. **Comprova que l'App Pool està en marxa**:
   ```powershell
   # Obre PowerShell com a Administrador
   Import-Module WebAdministration
   Get-WebAppPoolState -Name "VeteriLAchReadApiAppPool"
   ```

2. **Inicia l'App Pool si està aturat**:
   ```powershell
   Start-WebAppPool -Name "VeteriLAchReadApiAppPool"
   ```

3. **Reinicia l'App Pool si està engegat**:
   ```powershell
   Restart-WebAppPool -Name "VeteriLAchReadApiAppPool"
   ```

4. **Revisa els logs d'IIS**:
   - Ubica: `C:\inetpub\wwwroot\VeteriLachApi\logs`
   - Cerca errors recents

#### Error 401 Unauthorized

**Problema**: L'API retorna 401 en cridar endpoints

**Solucions**:

1. **Verifica que l'API Key és correcta**:
   - Comprova `mcp-server/appsettings.json` → `VeteriLachApi.ApiKey`
   - Comprova `C:\inetpub\wwwroot\VeteriLachApi\appsettings.json` → `ApiKeys.MCPServer`
   - Han de coincidir exactament

2. **Test manual amb Swagger**:
   - Accedeix a http://localhost:41229/swagger
   - Fes clic a "Authorize"
   - Introdueix l'API Key
   - Prova un endpoint

#### Error de connexió a la base de dades

**Problema**: L'API no pot connectar amb SQL Server

**Solucions**:

1. **Verifica el ConnectionString**:
   ```json
   // C:\inetpub\wwwroot\VeteriLachApi\appsettings.json
   {
     "ConnectionStrings": {
       "VeteriLachConnection": "Server=localhost;Database=CanMascotaBd;Trusted_Connection=True;..."
     }
   }
   ```

2. **Comprova que SQL Server està en marxa**:
   ```powershell
   # Obre Services.msc i cerca "SQL Server"
   Get-Service | Where-Object {$_.Name -like "*SQL*"}
   ```

3. **Verifica que l'App Pool té permisos**:
   - L'identitat de l'App Pool (ApplicationPoolIdentity) necessita accés a la base de dades
   - Afegeix permisos a SQL Server per a `IIS APPPOOL\VeteriLAchReadApiAppPool`

### Problemes amb el Servidor MCP

#### El servidor no es connecta a l'API

**Problema**: El servidor MCP no pot cridar l'API

**Solucions**:

1. **Verifica que l'API funciona**:
   ```powershell
   Invoke-WebRequest -Uri "http://localhost:41229/api/health" -UseBasicParsing
   ```

2. **Comprova la configuració del servidor MCP**:
   ```json
   // mcp-server/appsettings.json
   {
     "VeteriLachApi": {
       "BaseUrl": "http://localhost:41229",  // Ha de coincidir amb el port d'IIS
       "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4"
     }
   }
   ```

3. **Prova el servidor manualment**:
   ```powershell
   cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0
   
   # Hauria de retornar dades de vendes
   echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"get_sales","arguments":{"pageSize":1}}}' | .\VeteriLach.McpServer.exe
   ```

#### ChatGPT Desktop no detecta el servidor

**Problema**: El servidor MCP no apareix a ChatGPT Desktop

**Solucions**:

1. **Verifica el path al fitxer de configuració**:
   ```powershell
   # Mostra el contingut
   Get-Content "$env:APPDATA\Claude\claude_desktop_config.json"
   ```

2. **Comprova que el path al .exe és correcte**:
   ```powershell
   # Verifica que el fitxer existeix
   Test-Path "C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0\VeteriLach.McpServer.exe"
   ```

3. **Valida el JSON de configuració**:
   - Usa un validador online (https://jsonlint.com/)
   - Assegura't que les barres invertides són dobles (`\\`)
   - No hi ha comes extra al final

4. **Reinicia ChatGPT Desktop completament**:
   - Tanca l'aplicació (també des de la safata del sistema)
   - Obre el Task Manager i assegura't que no hi ha processos de Claude
   - Torna a obrir l'aplicació

5. **Revisa els logs de ChatGPT Desktop**:
   - Ubica: `%APPDATA%\Claude\logs`
   - Cerca errors relacionats amb MCP

#### Error "dotnet not found" o problemes de compilació

**Problema**: No es pot compilar el servidor MCP

**Solucions**:

1. **Verifica que .NET SDK està instal·lat**:
   ```powershell
   dotnet --version
   # Ha de mostrar versió 10.0 o superior
   ```

2. **Instal·la .NET SDK 10.0**:
   - Descarrega de: https://dotnet.microsoft.com/download/dotnet/10.0
   - Instal·la el SDK (no només el Runtime)

3. **Neteja i recompila**:
   ```powershell
   cd mcp-server
   dotnet clean
   dotnet restore
   dotnet build -c Release
   ```

### Problemes de Rendiment

#### El servidor MCP respon lentament

**Solucions**:

1. **Augmenta el timeout**:
   ```json
   // mcp-server/appsettings.json
   {
     "VeteriLachApi": {
       "TimeoutSeconds": 60  // Augmenta si cal
     }
   }
   ```

2. **Comprova l'estat de SQL Server**:
   - Queries lentes poden afectar el rendiment
   - Revisa els índexs de la base de dades

3. **Reinicia l'App Pool d'IIS**:
   ```powershell
   Restart-WebAppPool -Name "VeteriLAchReadApiAppPool"
   ```

### Obtenir Ajuda Addicional

Si cap d'aquestes solucions funciona:

1. **Executa el servidor manualment i captura els logs**:
   ```powershell
   cd mcp-server\bin\Release\net10.0
   .\VeteriLach.McpServer.exe 2> errors.log
   # Escriu una petició JSON-RPC i revisa errors.log
   ```

2. **Comprova els logs d'IIS**:
   - Event Viewer → Windows Logs → Application
   - Cerca errors relacionats amb IIS o ASP.NET Core

3. **Consulta la documentació oficial**:
   - MCP Protocol: https://modelcontextprotocol.io/
   - ASP.NET Core: https://docs.microsoft.com/aspnet/core/
   - IIS Hosting: https://docs.microsoft.com/aspnet/core/host-and-deploy/iis/

## Seguretat

### Protecció de l'API Key

**IMPORTANT**: L'API Key és una credencial sensible. Segueix aquestes recomanacions:

1. **NO comparteixis l'API Key** en repositoris públics o xarxes socials
2. **NO la incloguis** en fitxers de configuració que es publiquin a Git
3. **Utilitza API Keys diferents** per a cada entorn (desenvolupament, producció)
4. **Rota les claus periòdicament** per motius de seguretat

### Configuració Segura

#### appsettings.json (API)

El fitxer `C:\inetpub\wwwroot\VeteriLachApi\appsettings.json` conté informació sensible:
- ConnectionString amb credencials de base de dades
- API Keys

**Mesures de seguretat**:
- Assegura't que només els administradors tenen accés al directori
- Configura permisos NTFS adequats:
  ```powershell
  # Només Administrators i SYSTEM poden accedir
  icacls "C:\inetpub\wwwroot\VeteriLachApi\appsettings.json" /inheritance:r /grant:r "Administrators:(F)" "SYSTEM:(F)"
  ```

#### appsettings.json (MCP Server)

El fitxer del servidor MCP també conté l'API Key.

**Opcions més segures**:

1. **Variables d'entorn**:
   ```json
   // claude_desktop_config.json
   {
     "mcpServers": {
       "veterilach": {
         "command": "C:\\path\\to\\VeteriLach.McpServer.exe",
         "env": {
           "VETERILACH_API_KEY": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4"
         }
       }
     }
   }
   ```
   
   Després modifica `Program.cs` per llegir de variables d'entorn.

2. **Azure Key Vault** o **Windows Credential Manager** per a entorns de producció

### Exposició de l'API

Si exposes l'API a Internet:

1. **Utilitza HTTPS** obligatòriament
2. **Configura CORS** correctament
3. **Implementa rate limiting** per prevenir abusos
4. **Monitoritza els logs** per detectar accessos sospitosos
5. **Utilitza un firewall** (Azure Application Gateway, Cloudflare, etc.)

### Recomanacions Generals

- ✅ Mantén .NET SDK i runtime actualitzats
- ✅ Actualitza regularment els paquets NuGet
- ✅ Revisa els logs d'IIS periòdicament
- ✅ Fes backups regulars de la base de dades
- ✅ Implementa autenticació addicional si és necessari (OAuth2, JWT, etc.)

## Desenvolupament

### Executar en Mode Desenvolupament

Per desenvolupar i depurar el servidor MCP:

```powershell
cd mcp-server
dotnet run
```

Després pots escriure peticions JSON-RPC directament:

```
{"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}
```

### Afegir Noves Tools

Per afegir una nova tool:

1. **Afegeix el DTO a `Models/ApiModels.cs`**
2. **Afegeix el mètode al client HTTP a `Services/VeteriLachApiClient.cs`**
3. **Registra la tool a `Mcp/McpServer.cs` en `HandleToolsList()`**
4. **Afegeix el case al switch de `HandleToolsCallAsync()`**
5. **Implementa el mètode `Execute*Async()`**
6. **Actualitza el README.md amb la documentació**
7. **Recompila i prova**

### Estructura del Codi

```
mcp-server/
├── Program.cs                      # Punt d'entrada
│   └── Main loop (stdin → stdout)
├── appsettings.json               # Configuració
├── Mcp/
│   ├── McpModels.cs               # Models MCP (JsonRpc, Tool, etc.)
│   └── McpServer.cs               # Lògica del servidor
│       ├── HandleInitialize()     # Handshake inicial
│       ├── HandleToolsList()      # Llista de tools
│       └── HandleToolsCallAsync() # Execució de tools
├── Models/
│   └── ApiModels.cs               # DTOs de l'API
└── Services/
    └── VeteriLachApiClient.cs     # HTTP client per l'API
        └── Mètodes Get*Async()    # Crides HTTP
```

### Tests Unitaris (TODO)

Encara no implementat. Seria ideal afegir:
- Tests per a cada tool
- Mocks de l'API
- Tests d'integració

## Llicència

MIT
