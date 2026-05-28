#Requires -RunAsAdministrator

# Remove-VeteriLachSite.ps1
# Script per eliminar el site de VeteriLach d'IIS

Import-Module WebAdministration -ErrorAction Stop

$siteName = "VeteriLachReadAPI"
$appPoolName = $siteName + "AppPool"

Write-Host "Eliminant site $siteName..." -ForegroundColor Yellow

# Aturar i eliminar site
if (Get-Website -Name $siteName -ErrorAction SilentlyContinue) {
    Stop-Website -Name $siteName -ErrorAction SilentlyContinue
    Remove-Website -Name $siteName -ErrorAction SilentlyContinue
    Write-Host "Site eliminat" -ForegroundColor Green
} else {
    Write-Host "Site no trobat" -ForegroundColor Gray
}

# Aturar i eliminar app pool
if (Test-Path "IIS:\AppPools\$appPoolName") {
    Stop-WebAppPool -Name $appPoolName -ErrorAction SilentlyContinue
    Remove-WebAppPool -Name $appPoolName -ErrorAction SilentlyContinue
    Write-Host "App Pool eliminat" -ForegroundColor Green
} else {
    Write-Host "App Pool no trobat" -ForegroundColor Gray
}

Write-Host "Fet!" -ForegroundColor Green
