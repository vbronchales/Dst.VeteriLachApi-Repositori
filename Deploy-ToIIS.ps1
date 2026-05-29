# Deploy-ToIIS.ps1
# Script per publicar VeteriLach.ReadApi al IIS local
# REQUEREIX EXECUTAR COM A ADMINISTRADOR

param(
    [string]$AppPoolName = "VeteriLachApi",
    [string]$IISPath = "c:\inetpub\wwwroot\VeteriLachApi",
    [string]$TempPath = "c:\temp\VeteriLachApi_publish"
)

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Publicant VeteriLach.ReadApi al IIS Local" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Verificar que s'executa com a administrador
$isAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) {
    Write-Host "ERROR: Aquest script requereix privilegis d'administrador!" -ForegroundColor Red
    Write-Host "Executeu PowerShell com a administrador i torneu a provar." -ForegroundColor Yellow
    exit 1
}

# Pas 1: Compilar i publicar a directori temporal
Write-Host "[1/5] Compilant versio Release..." -ForegroundColor Yellow
cd "$PSScriptRoot\src\VeteriLach.ReadApi"
$publishResult = dotnet publish -c Release -o $TempPath 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR en la compilacio!" -ForegroundColor Red
    Write-Host $publishResult
    exit 1
}
Write-Host "Compilacio correcta" -ForegroundColor Green
Write-Host ""

# Pas 2: Parar App Pool
Write-Host "[2/5] Parant App Pool '$AppPoolName'..." -ForegroundColor Yellow
Import-Module WebAdministration
$appPoolState = (Get-WebAppPoolState -Name $AppPoolName).Value
if ($appPoolState -eq "Started") {
    Stop-WebAppPool -Name $AppPoolName
    Start-Sleep -Seconds 2
    Write-Host "App Pool parat" -ForegroundColor Green
}
else {
    Write-Host "App Pool ja estava parat" -ForegroundColor Green
}
Write-Host ""

# Pas 3: Esborrar fitxers antics (excepte web.config i logs)
Write-Host "[3/5] Netejant fitxers antics..." -ForegroundColor Yellow
if (Test-Path $IISPath) {
    Get-ChildItem -Path $IISPath -Exclude web.config,logs | Remove-Item -Recurse -Force
    Write-Host "Fitxers antics esborrats" -ForegroundColor Green
}
else {
    New-Item -Path $IISPath -ItemType Directory -Force | Out-Null
    Write-Host "Directori creat" -ForegroundColor Green
}
Write-Host ""

# Pas 4: Copiar nous fitxers
Write-Host "[4/5] Copiant nous fitxers..." -ForegroundColor Yellow
Copy-Item -Path "$TempPath\*" -Destination $IISPath -Recurse -Force
Write-Host "Fitxers copiats" -ForegroundColor Green
Write-Host ""

# Pas 5: Reiniciar App Pool
Write-Host "[5/5] Reiniciant App Pool '$AppPoolName'..." -ForegroundColor Yellow
Start-WebAppPool -Name $AppPoolName
Start-Sleep -Seconds 2
Write-Host "App Pool reiniciat" -ForegroundColor Green
Write-Host ""

# Verificar estat
$newState = (Get-WebAppPoolState -Name $AppPoolName).Value
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Publicacio Completada!" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "App Pool State: $newState" -ForegroundColor $(if($newState -eq "Started"){"Green"}else{"Yellow"})
Write-Host "URL API: http://localhost:5072" -ForegroundColor Cyan
Write-Host ""
Write-Host "Prova API amb:" -ForegroundColor Yellow
Write-Host "  Invoke-WebRequest -Uri `"http://localhost:5072/api/health`"" -ForegroundColor White
Write-Host ""
