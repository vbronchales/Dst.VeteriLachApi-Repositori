#Requires -RunAsAdministrator

# Quick Deploy - Copia fitxers i reinicia App Pool
# Executar com a administrador

$appPool = "VeteriLAchReadApiAppPool"
$source = "c:\temp\VeteriLachApi_publish\*"
$dest = "c:\inetpub\wwwroot\VeteriLachApi"

Write-Host "Aturant App Pool..." -ForegroundColor Yellow
Import-Module WebAdministration
Stop-WebAppPool -Name $appPool
Start-Sleep -Seconds 2

Write-Host "Copiant fitxers..." -ForegroundColor Yellow
Copy-Item -Path $source -Destination $dest -Recurse -Force -Exclude @('web.config','logs')

Write-Host "Iniciant App Pool..." -ForegroundColor Yellow
Start-WebAppPool -Name $appPool
Start-Sleep -Seconds 3

Write-Host "Fet!" -ForegroundColor Green
Write-Host ""
Write-Host "Test: Invoke-WebRequest -Uri 'http://localhost:41229/api/sales?pageSize=1' -Headers @{'X-Api-Key'='4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4'} -UseBasicParsing" -ForegroundColor Gray

pause
