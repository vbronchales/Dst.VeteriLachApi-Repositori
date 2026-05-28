# 🚀 Guia de Deployment - VeteriLach Read API

Documentació completa per desplegar l'API a servidors Windows amb IIS.

---

## 📋 Taula de Continguts

1. [Prerequisites](#prerequisites)
2. [Deployment Local (IIS al mateix equip)](#deployment-local-iis-al-mateix-equip)
3. [Deployment Remot (IIS a un altre servidor)](#deployment-remot-iis-a-un-altre-servidor)
4. [Configuració de Producció](#configuració-de-producció)
5. [Troubleshooting](#troubleshooting)
6. [Script d'Automatització](#script-dautomatització)

---

## Prerequisites

### Al Servidor de Destinació (Local o Remot)

#### 1. IIS (Internet Information Services)
```powershell
# Instal·lar IIS amb PowerShell (executa com a Administrador)
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServer
Enable-WindowsOptionalFeature -Online -FeatureName IIS-CommonHttpFeatures
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpErrors
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ApplicationDevelopment
Enable-WindowsOptionalFeature -Online -FeatureName IIS-NetFxExtensibility45
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HealthAndDiagnostics
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpLogging
Enable-WindowsOptionalFeature -Online -FeatureName IIS-Security
Enable-WindowsOptionalFeature -Online -FeatureName IIS-RequestFiltering
Enable-WindowsOptionalFeature -Online -FeatureName IIS-Performance
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerManagementTools
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ManagementConsole
```

**O via GUI:**
1. Control Panel → Programs → Turn Windows features on or off
2. Marca **Internet Information Services**
3. Expandeix i marca:
   - Web Management Tools → IIS Management Console
   - World Wide Web Services → Application Development Features → todos los componentes
   - World Wide Web Services → Common HTTP Features → todos
   - World Wide Web Services → Security → Basic Authentication

#### 2. .NET Runtime (Servidor)

**⚠️ IMPORTANT**: Al servidor només necessites el **Runtime**, NO el SDK complet.

Descarrega i instal·la:
- **.NET 10.0 Runtime** (o superior)
- **ASP.NET Core Runtime 10.0** (o superior)
- **ASP.NET Core Hosting Bundle** (inclou Runtime + IIS Module)

```powershell
# Verifica la versió instal·lada
dotnet --list-runtimes
```

**Descàrrega oficial:** https://dotnet.microsoft.com/download/dotnet/10.0

**Hosting Bundle** (recomanat): Instal·la tot el necessari amb un sol executable.

#### 3. SQL Server Client (Servidor)

- Accés al servidor SQL Server (BRUVICTOR7 o el servidor de producció)
- Connection string vàlida amb usuari SQL o Windows Authentication
- Firewall configurat per permetre connexió al SQL Server

---

## Deployment Local (IIS al mateix equip)

### Opció A: Deployment Automàtic amb Script (Recomanat) ⭐

**Mètode 1: Auto-elevació (MÉS FÀCIL)**

```powershell
# Des de la carpeta arrel del repositori (NO cal ser Administrador)
.\Deploy-Local-Auto.ps1
```

Aquest script **automàticament demanarà permisos d'Administrador** i farà tot el deployment.

**Mètode 2: Executar com a Administrador**

Si prefereixes control manual:

1. Obre **PowerShell com a Administrador** (clic dret → "Executar com a administrador")
2. Navega a la carpeta del projecte
3. Executa:

```powershell
.\Deploy-Local.ps1
```

**Què fan aquests scripts:**
1. Build del projecte en mode Release
2. Publicació a `publish/local`
3. Creació/actualització de l'Application Pool
4. Creació/actualització del Site a IIS
5. Configuració de permisos
6. Reinici de l'Application Pool
7. Test del Health endpoint

**Configuració del deployment:**
- **Site Name**: VeteriLachReadAPI
- **Port**: 41228 (autodetecta si està ocupat)
- **Application Pool**: VeteriLachReadAPI-AppPool
- **Ruta física**: `c:\inetpub\wwwroot\VeteriLachReadAPI`

**Script alternatiu (només build, sense IIS):**

Si només vols generar els fitxers sense configurar IIS:

```powershell
# NO requereix Administrador
.\Build-And-Publish.ps1
```

📖 **Més info sobre els scripts:** [SCRIPTS-README.md](SCRIPTS-README.md)

### Opció B: Deployment Manual Pas a Pas

#### Pas 1: Publicar l'aplicació (Mode Release)

```powershell
# Des de la carpeta arrel del repositori
cd src\VeteriLach.ReadApi

# Publicar en mode Release
dotnet publish -c Release -o ..\..\publish\local
```

Això genera tots els fitxers necessaris a `publish\local`.

#### Pas 2: Crear Application Pool a IIS

```powershell
# PowerShell com a Administrador
Import-Module WebAdministration

# Crear Application Pool
New-WebAppPool -Name "VeteriLachReadAPI-AppPool"

# Configurar .NET Runtime
Set-ItemProperty IIS:\AppPools\VeteriLachReadAPI-AppPool -Name managedRuntimeVersion -Value ""

# No Managed Code per ASP.NET Core
Set-ItemProperty IIS:\AppPools\VeteriLachReadAPI-AppPool -Name startMode -Value "AlwaysRunning"
Set-ItemProperty IIS:\AppPools\VeteriLachReadAPI-AppPool -Name processModel.idleTimeout -Value ([TimeSpan]::FromMinutes(0))
```

**O via GUI (IIS Manager):**
1. Obre **IIS Manager** (inetmgr)
2. Expandeix el servidor → **Application Pools**
3. Clic dret → **Add Application Pool**
   - Name: `VeteriLachReadAPI-AppPool`
   - .NET CLR version: **No Managed Code**
   - Managed pipeline mode: **Integrated**
4. Clic dret al pool creat → **Advanced Settings**
   - Start Mode: **AlwaysRunning**
   - Idle Time-out (minutes): **0**

#### Pas 3: Copiar fitxers publicats

```powershell
# Crear carpeta de destinació
New-Item -ItemType Directory -Path "c:\inetpub\wwwroot\VeteriLachReadAPI" -Force

# Copiar fitxers publicats
Copy-Item -Path "publish\local\*" -Destination "c:\inetpub\wwwroot\VeteriLachReadAPI" -Recurse -Force
```

#### Pas 4: Crear Site a IIS

```powershell
# PowerShell com a Administrador
Import-Module WebAdministration

# Crear Site
New-Website -Name "VeteriLachReadAPI" `
    -PhysicalPath "c:\inetpub\wwwroot\VeteriLachReadAPI" `
    -ApplicationPool "VeteriLachReadAPI-AppPool" `
    -Port 41228
```

**O via GUI (IIS Manager):**
1. Obre **IIS Manager**
2. Clic dret a **Sites** → **Add Website**
   - Site name: `VeteriLachReadAPI`
   - Application pool: `VeteriLachReadAPI-AppPool`
   - Physical path: `c:\inetpub\wwwroot\VeteriLachReadAPI`
   - Binding:
     - Type: **http**
     - IP address: **All Unassigned**
     - Port: **41228** (per defecte, autodetecta si ocupat)
     - Host name: (deixa buit per ara)

#### Pas 5: Configurar Permisos

```powershell
# Donar permisos a IIS_IUSRS
$path = "c:\inetpub\wwwroot\VeteriLachReadAPI"
$acl = Get-Acl $path
$permission = "IIS_IUSRS", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow"
$rule = New-Object System.Security.AccessControl.FileSystemAccessRule $permission
$acl.SetAccessRule($rule)
Set-Acl $path $acl
```

**O via GUI:**
1. Botó dret a `c:\inetpub\wwwroot\VeteriLachReadAPI` → Properties
2. Security → Edit → Add
3. Afegeix **IIS_IUSRS**
4. Marca **Full control**

#### Pas 6: Verificar Deployment

```powershell
# Reiniciar Application Pool
Restart-WebAppPool -Name "VeteriLachReadAPI-AppPool"

# Provar Health endpoint
curl http://localhost:8080/api/health
```

Hauries de veure: `{"status":"Healthy","message":"VeteriLach Read API is running"}`

**URL Swagger UI:** http://localhost:8080/swagger

---

## Deployment Remot (IIS a un altre servidor)

### Escenari: Publicar des del teu equip de desenvolupament a un servidor de producció

#### Pas 1: Publicar l'aplicació (Mode Release)

**Al teu equip de desenvolupament:**

```powershell
# Des de la carpeta arrel del repositori
cd src\VeteriLach.ReadApi

# Publicar en mode Release amb configuració de producció
dotnet publish -c Release -o ..\..\publish\production
```

#### Pas 2: Configurar appsettings.Production.json

**ABANS de copiar els fitxers**, crea/modifica `publish\production\appsettings.Production.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "VeteriLachDb": "Server=TU_SERVIDOR_SQL_PRODUCCIÓ;Database=CanMascotaBd;User Id=USUARI_PRODUCCIÓ;Password=PASSWORD_PRODUCCIÓ;TrustServerCertificate=True;MultipleActiveResultSets=True;"
  },
  "ApiKeys": {
    "MCPServer": "HASH_SHA256_DE_LA_CLAU_PRODUCCIÓ_1",
    "SwaggerUI": "HASH_SHA256_DE_LA_CLAU_PRODUCCIÓ_2",
    "Development": "HASH_SHA256_DE_LA_CLAU_PRODUCCIÓ_3"
  }
}
```

**⚠️ IMPORTANT:**
- Canvia el connection string pel servidor de producció
- Genera noves API Keys (veure secció [Generar API Keys](#generar-api-keys-de-producció))
- NO facis commit d'aquest fitxer al repositori

#### Pas 3: Comprimir fitxers publicats

```powershell
# Comprimir la carpeta publish\production
Compress-Archive -Path "publish\production\*" -DestinationPath "VeteriLachAPI-Release.zip" -Force
```

#### Pas 4: Transferir al servidor remot

**Opcions:**
- **RDP (Remote Desktop):** Copia el fitxer .zip via Remote Desktop
- **Shared folder:** Copia a una carpeta compartida de xarxa
- **USB:** Si tens accés físic
- **SCP/SFTP:** Si tens SSH habilitat

```powershell
# Exemple: Copiar a carpeta compartida
Copy-Item "VeteriLachAPI-Release.zip" "\\SERVIDOR_REMOT\C$\Temp\VeteriLachAPI-Release.zip"
```

#### Pas 5: Al Servidor Remot - Descomprimir

**Connecta al servidor via RDP i executa:**

```powershell
# Crear carpeta de destinació
New-Item -ItemType Directory -Path "c:\inetpub\wwwroot\VeteriLachReadAPI" -Force

# Descomprimir
Expand-Archive -Path "C:\Temp\VeteriLachAPI-Release.zip" -DestinationPath "c:\inetpub\wwwroot\VeteriLachReadAPI" -Force
```

#### Pas 6: Al Servidor Remot - Configurar IIS

**Executa com a Administrador:**

```powershell
Import-Module WebAdministration

# Crear Application Pool
New-WebAppPool -Name "VeteriLachReadAPI-AppPool"
Set-ItemProperty IIS:\AppPools\VeteriLachReadAPI-AppPool -Name managedRuntimeVersion -Value ""
Set-ItemProperty IIS:\AppPools\VeteriLachReadAPI-AppPool -Name startMode -Value "AlwaysRunning"
Set-ItemProperty IIS:\AppPools\VeteriLachReadAPI-AppPool -Name processModel.idleTimeout -Value ([TimeSpan]::FromMinutes(0))

# Crear Site
New-Website -Name "VeteriLachReadAPI" `
    -PhysicalPath "c:\inetpub\wwwroot\VeteriLachReadAPI" `
    -ApplicationPool "VeteriLachReadAPI-AppPool" `
    -Port 80

# Configurar permisos
$path = "c:\inetpub\wwwroot\VeteriLachReadAPI"
$acl = Get-Acl $path
$permission = "IIS_IUSRS", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow"
$rule = New-Object System.Security.AccessControl.FileSystemAccessRule $permission
$acl.SetAccessRule($rule)
Set-Acl $path $acl

# Reiniciar Application Pool
Restart-WebAppPool -Name "VeteriLachReadAPI-AppPool"
```

#### Pas 7: Verificar Deployment

```powershell
# Al servidor remot
curl http://localhost/api/health

# Des del teu equip (canvia IP_SERVIDOR per la IP del servidor)
curl http://IP_SERVIDOR/api/health
```

#### Pas 8: Configurar Firewall (si cal)

Si vols accedir des de fora del servidor:

```powershell
# Al servidor remot
New-NetFirewallRule -DisplayName "VeteriLach API HTTP" -Direction Inbound -Protocol TCP -LocalPort 80 -Action Allow
```

---

## Configuració de Producció

### Generar API Keys de Producció

**NO usis les API Keys de desenvolupament a producció!**

```powershell
# Executar aquest script 3 vegades per generar 3 claus diferents

function New-ApiKey {
    param([string]$ClientName)
    
    # Generar clau aleatòria segura
    $key = [System.Convert]::ToBase64String([System.Guid]::NewGuid().ToByteArray()) + 
           [System.Convert]::ToBase64String([System.Guid]::NewGuid().ToByteArray())
    
    # Calcular hash SHA256
    $hash = [System.BitConverter]::ToString(
        [System.Security.Cryptography.SHA256]::Create().ComputeHash(
            [System.Text.Encoding]::UTF8.GetBytes($key)
        )
    ).Replace("-", "").ToLower()
    
    Write-Host "`n=== API Key per a: $ClientName ===" -ForegroundColor Green
    Write-Host "API Key (dona al client):" -ForegroundColor Yellow
    Write-Host $key -ForegroundColor Cyan
    Write-Host "`nHash SHA256 (afegeix a appsettings.Production.json):" -ForegroundColor Yellow
    Write-Host $hash -ForegroundColor Cyan
}

# Generar 3 claus
New-ApiKey -ClientName "MCPServer"
New-ApiKey -ClientName "SwaggerUI"
New-ApiKey -ClientName "OtherClient"
```

**Afegeix els HASH a `appsettings.Production.json`:**

```json
{
  "ApiKeys": {
    "MCPServer": "hash_generat_1",
    "SwaggerUI": "hash_generat_2",
    "OtherClient": "hash_generat_3"
  }
}
```

### Configurar Connection String de Producció

**Opcions:**

#### Opció 1: SQL Authentication (recomanat per APIs)

```json
{
  "ConnectionStrings": {
    "VeteriLachDb": "Server=SERVIDOR_PRODUCCIÓ;Database=CanMascotaBd;User Id=api_veterilach;Password=PASSWORD_SEGUR;TrustServerCertificate=True;MultipleActiveResultSets=True;"
  }
}
```

**Al SQL Server, crea un usuari amb permisos només de lectura:**

```sql
-- Al servidor de producció
USE CanMascotaBd;
GO

CREATE LOGIN api_veterilach WITH PASSWORD = 'PASSWORD_SEGUR';
CREATE USER api_veterilach FOR LOGIN api_veterilach;

-- Només permisos de lectura
EXEC sp_addrolemember 'db_datareader', 'api_veterilach';

-- Permetre executar stored procedures si cal (opcional)
-- GRANT EXECUTE TO api_veterilach;
```

#### Opció 2: Windows Authentication

```json
{
  "ConnectionStrings": {
    "VeteriLachDb": "Server=SERVIDOR_PRODUCCIÓ;Database=CanMascotaBd;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;"
  }
}
```

**Configura l'Application Pool per usar una identitat específica:**

1. IIS Manager → Application Pools → VeteriLachReadAPI-AppPool
2. Advanced Settings → Process Model → Identity → Custom account
3. Introdueix les credencials del compte de domini amb accés al SQL Server

### Deshabilitar Swagger a Producció (Opcional)

Si no vols exposar Swagger a producció, modifica `Program.cs`:

```csharp
// Només habilitar Swagger en Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

O configura autenticació bàsica per Swagger a producció.

### Configurar HTTPS (Recomanat)

#### Opció 1: Certificat autosignat (només per testing intern)

```powershell
# Crear certificat autosignat
$cert = New-SelfSignedCertificate -DnsName "veterilach-api.local" -CertStoreLocation "cert:\LocalMachine\My"

# Lligar el certificat al site
New-WebBinding -Name "VeteriLachReadAPI" -Protocol "https" -Port 443 -SslFlags 0
```

#### Opció 2: Certificat d'una CA corporativa o Let's Encrypt

1. Obtén el certificat (format .pfx)
2. Importa'l al Certificate Store del servidor
3. A IIS Manager → Site → Bindings → Add:
   - Type: **https**
   - Port: **443**
   - SSL certificate: **Selecciona el certificat importat**

---

## Troubleshooting

### Error: 500.19 - Cannot read configuration file

**Causa:** `web.config` no es va generar correctament.

**Solució:**
```powershell
# Torna a publicar amb flag --self-contained false
dotnet publish -c Release --self-contained false -o publish\local
```

### Error: 500.30 - ASP.NET Core app failed to start

**Causa:** .NET Runtime no instal·lat o versió incorrecta.

**Solució:**
1. Verifica runtime: `dotnet --list-runtimes`
2. Instal·la **ASP.NET Core Hosting Bundle 10.0**: https://dotnet.microsoft.com/download/dotnet/10.0
3. Reinicia el servidor després d'instal·lar

### Error: 500.21 - Handler "aspNetCore" has a bad module

**Causa:** ASP.NET Core Module no instal·lat.

**Solució:**
```powershell
# Instal·la Hosting Bundle i executa
& "$env:ProgramFiles\IIS\Shared Configuration\aspnetcore_repair.exe"

# O descarrega i instal·la el Hosting Bundle de nou
```

### Error: Cannot connect to SQL Server

**Verifica:**
1. Connection string correcte a `appsettings.Production.json`
2. SQL Server accessible des del servidor IIS (ping, telnet port 1433)
3. Firewall permet connexió (port 1433)
4. Usuari SQL té permisos correctes
5. Si uses Windows Auth, l'identitat de l'Application Pool té accés

**Test de connexió:**
```powershell
# Des del servidor IIS
Test-NetConnection -ComputerName SERVIDOR_SQL -Port 1433
```

### L'API retorna 503 Service Unavailable

**Causes comunes:**
1. Application Pool aturat
2. Error a l'aplicació (revisa logs)

**Solució:**
```powershell
# Reiniciar Application Pool
Restart-WebAppPool -Name "VeteriLachReadAPI-AppPool"

# Revisar Event Viewer
Get-EventLog -LogName Application -Source "IIS AspNetCore Module V2" -Newest 10
```

### Com veure logs detallats

**1. Habilitar stdout logging (temporalment):**

Edita `web.config` a `c:\inetpub\wwwroot\VeteriLachReadAPI\web.config`:

```xml
<aspNetCore processPath="dotnet" arguments=".\VeteriLach.ReadApi.dll" stdoutLogEnabled="true" stdoutLogFile=".\logs\stdout" />
```

**2. Revisa els logs:**
```powershell
Get-Content "c:\inetpub\wwwroot\VeteriLachReadAPI\logs\stdout_*.log" -Tail 50
```

**3. Revisa logs de Serilog:**
```powershell
Get-Content "c:\inetpub\wwwroot\VeteriLachReadAPI\logs\veterilach-api-*.txt" -Tail 50
```

---

## Script d'Automatització

### Deploy-Local.ps1

Aquest script automatitza tot el procés de deployment local.

**Ubicació:** Carpeta arrel del repositori (`Deploy-Local.ps1`)

**Ús:**
```powershell
# Executa com a Administrador
.\Deploy-Local.ps1
```

**Característiques:**
- ✅ Build en mode Release
- ✅ Publicació a carpeta temporal
- ✅ Creació/actualització d'Application Pool
- ✅ Creació/actualització de Site a IIS
- ✅ Configuració automàtica de permisos
- ✅ Reinici automàtic de l'Application Pool
- ✅ Test del Health endpoint

**Configuració personalitzable** (edita les variables al principi del script):
```powershell
$siteName = "VeteriLachReadAPI"
$appPoolName = "VeteriLachReadAPI-AppPool"
$sitePath = "c:\inetpub\wwwroot\VeteriLachReadAPI"
$port = 41228
```

**Workflow post-merge:**

Després de fer merge d'una fase, executa:
```powershell
# 1. Actualitza main
git checkout main
git pull origin main

# 2. Desplega a IIS local
.\Deploy-Local.ps1

# 3. Verifica
curl http://localhost:41228/api/health
curl http://localhost:41228/swagger
```

---

## Checklist de Deployment

### Pre-Deployment

- [ ] Codi compilat sense errors
- [ ] Tests passats (si n'hi ha)
- [ ] Connection string de producció configurat
- [ ] Noves API Keys generades per producció
- [ ] `appsettings.Production.json` creat i configurat
- [ ] Swagger deshabilitat per producció (opcional)
- [ ] Logs configurats correctament

### Al Servidor

- [ ] .NET 10 Runtime instal·lat
- [ ] ASP.NET Core Hosting Bundle instal·lat
- [ ] IIS instal·lat i configurat
- [ ] SQL Server accessible
- [ ] Firewall configurat (si cal)
- [ ] Permisos IIS_IUSRS configurats

### Post-Deployment

- [ ] Health endpoint funciona: `http://servidor/api/health`
- [ ] Swagger funciona (si està habilitat): `http://servidor/swagger`
- [ ] Endpoints d'API funcionen amb API Key
- [ ] Connexió a base de dades correcta
- [ ] Logs es generen correctament
- [ ] Application Pool està Running
- [ ] Monitorització configurada (opcional)

---

## Recursos Addicionals

- **Documentació oficial ASP.NET Core + IIS:** https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/
- **Hosting Bundle:** https://dotnet.microsoft.com/download/dotnet/10.0
- **IIS Manager:** Executa `inetmgr` al servidor
- **Event Viewer:** Logs de IIS i ASP.NET Core Module

---

## Notes Finals

1. **Seguretat:** SEMPRE genera noves API Keys per producció
2. **Connection Strings:** NO facis commit de credencials reals
3. **Logs:** Monitoritza els logs regularment
4. **Updates:** Mantén el .NET Runtime actualitzat al servidor
5. **Backups:** Fes backup de la base de dades abans de deploys importants
6. **Testing:** Prova sempre en un entorn de staging abans de producció (si és possible)

Per qualsevol dubte o problema, revisa la secció [Troubleshooting](#troubleshooting).
