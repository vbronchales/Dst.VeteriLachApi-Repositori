#Requires -RunAsAdministrator

# Deploy-IIS.ps1
# Script simplificat per deployment a IIS local

param(
    [string]$SiteName = "VeteriLachReadAPI",
    [int]$Port = 41228
)

$ErrorActionPreference = "Stop"

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  VeteriLach Read API - Deploy a IIS" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Directoris
$projectPath = $PSScriptRoot
$publishPath = Join-Path $projectPath "publish\local"
$srcPath = Join-Path $projectPath "src\VeteriLach.ReadApi"

# 1. BUILD
Write-Host "[1/5] Netejant build anterior..." -ForegroundColor Yellow
Set-Location $srcPath
dotnet clean --configuration Release | Out-Null

Write-Host "[2/5] Compilant..." -ForegroundColor Yellow
dotnet build --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build ha fallat" -ForegroundColor Red
    exit 1
}

# Aturar IIS abans de publicar per evitar bloquejos de fitxers
Write-Host "[3/5] Aturant site IIS (si existeix)..." -ForegroundColor Yellow
Import-Module WebAdministration -ErrorAction SilentlyContinue
$appPoolName = $SiteName + "AppPool"
$existingSite = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
$script:existingSitePort = $null

if ($existingSite) {
    # Guardar el port actual per reutilitzar-lo despres
    $existingBinding = Get-WebBinding -Name $SiteName
    if ($existingBinding.bindingInformation -match ':(\d+):') {
        $script:existingSitePort = [int]$matches[1]
        Write-Host "  - Site trobat al port $script:existingSitePort" -ForegroundColor Gray
    }
    
    Write-Host "  - Aturant website $SiteName..." -ForegroundColor Gray
    Stop-Website -Name $SiteName -ErrorAction SilentlyContinue
    
    if (Test-Path "IIS:\AppPools\$appPoolName") {
        Write-Host "  - Aturant app pool $appPoolName..." -ForegroundColor Gray
        Stop-WebAppPool -Name $appPoolName -ErrorAction SilentlyContinue
    }
    
    Write-Host "  - Esperant que IIS alliberi fitxers..." -ForegroundColor Gray
    Start-Sleep -Seconds 5
}

Write-Host "[4/5] Publicant..." -ForegroundColor Yellow
if (Test-Path $publishPath) {
    Remove-Item $publishPath -Recurse -Force
}
dotnet publish --configuration Release --output $publishPath
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Publish ha fallat" -ForegroundColor Red
    exit 1
}

# 2. IIS
Write-Host "[5/6] Configurant IIS..." -ForegroundColor Yellow

Import-Module WebAdministration -ErrorAction Stop

# Verificar si el ASP.NET Core Hosting Bundle esta instal·lat
$aspNetCoreModulePath = "C:\Program Files\IIS\Asp.Net Core Module\V2\aspnetcorev2.dll"
if (-not (Test-Path $aspNetCoreModulePath)) {
    Write-Host ""
    Write-Host "================================================================" -ForegroundColor Red
    Write-Host "  AVIS: .NET Hosting Bundle NO esta instal·lat!" -ForegroundColor Red
    Write-Host "================================================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "L'aplicacio s'ha publicat correctament, pero IIS no podra" -ForegroundColor Yellow
    Write-Host "executar-la sense el ASP.NET Core Hosting Bundle." -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Consulta:" -ForegroundColor Cyan
    Write-Host "  INSTALAR-HOSTING-BUNDLE.md" -ForegroundColor White
    Write-Host ""
    Write-Host "Continuant el deployment (es creara el site pero no funcionara)..." -ForegroundColor Yellow
    Write-Host ""
    Start-Sleep -Seconds 3
}

# Verificar si tenim un port del site existent
$finalPort = $Port

if ($null -ne $script:existingSitePort) {
    # Si el site existeix, reutilitzar el seu port actual
    $finalPort = $script:existingSitePort
    Write-Host "Reutilitzant port del site existent: $finalPort" -ForegroundColor Green
} else {
    # Si no existeix, buscar un port disponible
    Write-Host "Site nou, buscant port disponible..." -ForegroundColor Yellow
    
    # Trobar port disponible (comprovar TOTS els ports en us, no nomes IIS)
    $usedPorts = @()

    # Ports d'IIS
    $usedPorts += Get-WebBinding | ForEach-Object { 
        if ($_.bindingInformation -match ':(\d+):') { 
            [int]$matches[1] 
        } 
    }

    # Ports de TOTS els processos
    $netstatPorts = netstat -ano | Select-String "LISTENING" | ForEach-Object {
        if ($_ -match ':(\d+)\s') {
            [int]$matches[1]
        }
    }
    $usedPorts += $netstatPorts

    $usedPorts = $usedPorts | Select-Object -Unique

    while ($usedPorts -contains $finalPort) {
        Write-Host "Port $finalPort ocupat, provant el seguent..." -ForegroundColor Yellow
        $finalPort++
        if ($finalPort -gt 45000) {
            Write-Host "ERROR: No s'ha trobat cap port disponible" -ForegroundColor Red
            exit 1
        }
    }

    Write-Host "Port disponible trobat: $finalPort" -ForegroundColor Green
}

# Crear o actualitzar site
$site = Get-Website -Name $SiteName -ErrorAction SilentlyContinue

if ($site) {
    Write-Host "Site existent, actualitzant..." -ForegroundColor Yellow
    
    Set-ItemProperty "IIS:\Sites\$SiteName" -Name physicalPath -Value $publishPath
    
    $binding = Get-WebBinding -Name $SiteName
    if ($binding.bindingInformation -notlike "*$finalPort*") {
        Remove-WebBinding -Name $SiteName -BindingInformation "*:*:*" -ErrorAction SilentlyContinue
        New-WebBinding -Name $SiteName -Protocol http -Port $finalPort -IPAddress "*"
    }
} else {
    Write-Host "Creant nou site..." -ForegroundColor Yellow
    New-Website -Name $SiteName -PhysicalPath $publishPath -Port $finalPort -Force
}

# Configurar Application Pool
$appPoolName = $SiteName + "AppPool"
if (-not (Test-Path "IIS:\AppPools\$appPoolName")) {
    New-WebAppPool -Name $appPoolName
}

Set-ItemProperty "IIS:\Sites\$SiteName" -Name applicationPool -Value $appPoolName
Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name managedRuntimeVersion -Value ""

# Iniciar
Start-WebAppPool -Name $appPoolName -ErrorAction SilentlyContinue
Start-Website -Name $SiteName

# 3. VERIFICAR
Write-Host "[6/6] Verificant..." -ForegroundColor Yellow
Start-Sleep -Seconds 3

$healthUrl = "http://localhost:$finalPort/health"
try {
    $response = Invoke-WebRequest -Uri $healthUrl -UseBasicParsing -TimeoutSec 10
    if ($response.StatusCode -eq 200) {
        Write-Host ""
        Write-Host "========================================" -ForegroundColor Green
        Write-Host "  DEPLOYMENT COMPLETAT AMB EXIT!" -ForegroundColor Green
        Write-Host "========================================" -ForegroundColor Green
        Write-Host ""
        Write-Host "URL: http://localhost:$finalPort" -ForegroundColor Cyan
        Write-Host "Health: $healthUrl" -ForegroundColor Cyan
        Write-Host ""
    } else {
        Write-Host "AVIS: Health retorna status $($response.StatusCode)" -ForegroundColor Yellow
    }
} catch {
    Write-Host ""
    Write-Host "AVIS: No s'ha pogut verificar health endpoint" -ForegroundColor Yellow
    Write-Host "URL: http://localhost:$finalPort" -ForegroundColor Cyan
    Write-Host "Comprova manualment: $healthUrl" -ForegroundColor Yellow
    Write-Host ""
}

Set-Location $projectPath
Write-Host "Fet!" -ForegroundColor Green
Write-Host ""
