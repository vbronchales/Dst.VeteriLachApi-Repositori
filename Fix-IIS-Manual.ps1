# Script manual per corregir la configuració del IIS
# Executar com administrador: .\Fix-IIS-Manual.ps1

#Requires -RunAsAdministrator

Import-Module WebAdministration

Write-Host "=========================================" -ForegroundColor Cyan
Write-Host "  FIX IIS - VeteriLachApi" -ForegroundColor Cyan
Write-Host "=========================================" -ForegroundColor Cyan
Write-Host ""

$siteName = "VeteriLachApi"
$appPoolName = "VeteriLachApi"
$correctPath = "c:\inetpub\wwwroot\VeteriLachApi"

# 1. Verificar configuració actual
Write-Host "[1] Configuració actual:" -ForegroundColor Yellow
$site = Get-Website -Name $siteName
if ($site) {
    Write-Host "  Site: $($site.name)" -ForegroundColor Gray
    Write-Host "  State: $($site.state)" -ForegroundColor Gray
    Write-Host "  Physical Path: $($site.physicalPath)" -ForegroundColor Gray
    Write-Host "  Bindings:" -ForegroundColor Gray
    $site.bindings.Collection | ForEach-Object {
        Write-Host "    $($_.protocol) - $($_.bindingInformation)" -ForegroundColor Gray
    }
} else {
    Write-Host "  ERROR: El site $siteName no existeix!" -ForegroundColor Red
    exit 1
}

$appPoolState = Get-WebAppPoolState -Name $appPoolName
Write-Host "  App Pool State: $($appPoolState.Value)" -ForegroundColor Gray
Write-Host ""

# 2. Corregir path si cal
if ($site.physicalPath -ne $correctPath) {
    Write-Host "[2] Corregint path del site..." -ForegroundColor Yellow
    Write-Host "  De: $($site.physicalPath)" -ForegroundColor Red
    Write-Host "  A:  $correctPath" -ForegroundColor Green
    
    Stop-WebAppPool -Name $appPoolName
    Start-Sleep -Seconds 2
    
    Set-ItemProperty "IIS:\Sites\$siteName" -Name physicalPath -Value $correctPath
    
    Start-WebAppPool -Name $appPoolName
    Start-Sleep -Seconds 3
    
    Write-Host "  Path actualitzat!" -ForegroundColor Green
} else {
    Write-Host "[2] Path correcte! No cal canviar" -ForegroundColor Green
}

Write-Host ""

# 3. Reiniciar App Pool
Write-Host "[3] Reiniciant App Pool..." -ForegroundColor Yellow
Restart-WebAppPool -Name $appPoolName
Start-Sleep -Seconds 5

# 4. Verificar estat final
Write-Host "[4] Estat final:" -ForegroundColor Yellow
$site = Get-Website -Name $siteName
$appPoolState = Get-WebAppPoolState -Name $appPoolName
Write-Host "  Site State: $($site.state)" -ForegroundColor Green
Write-Host "  App Pool State: $($appPoolState.Value)" -ForegroundColor Green
Write-Host "  Physical Path: $($site.physicalPath)" -ForegroundColor Green

Write-Host ""

# 5. Test port
Write-Host "[5] Verificant port 5072..." -ForegroundColor Yellow
$portTest = Test-NetConnection -ComputerName localhost -Port 5072 -WarningAction SilentlyContinue
if ($portTest.TcpTestSucceeded) {
    Write-Host "  Port 5072 ESCOLTANT!" -ForegroundColor Green
} else {
    Write-Host "  Port 5072 NO respon" -ForegroundColor Red
    Write-Host "  Esperant 10 segons més..." -ForegroundColor Yellow
    Start-Sleep -Seconds 10
    $portTest = Test-NetConnection -ComputerName localhost -Port 5072 -WarningAction SilentlyContinue
    if ($portTest.TcpTestSucceeded) {
        Write-Host "  Port 5072 ESCOLTANT!" -ForegroundColor Green
    } else {
        Write-Host "  Port 5072 encara NO respon" -ForegroundColor Red
        Write-Host ""
        Write-Host "  Comprovant logs d'esdeveniments..." -ForegroundColor Yellow
        $events = Get-EventLog -LogName Application -Newest 10 -After (Get-Date).AddMinutes(-5) | 
            Where-Object {$_.Source -match "IIS|AspNetCore|W3SVC"}
        if ($events) {
            $events | ForEach-Object {
                Write-Host "    [$($_.EntryType)] $($_.Source): $($_.Message.Substring(0,[Math]::Min(100,$_.Message.Length)))..." -ForegroundColor Gray
            }
        } else {
            Write-Host "    No hi ha events recents" -ForegroundColor Gray
        }
    }
}

Write-Host ""
Write-Host "=========================================" -ForegroundColor Cyan
Write-Host "Prova l'API amb:" -ForegroundColor White
Write-Host "  Invoke-WebRequest -Uri 'http://localhost:5072/api/health'" -ForegroundColor Gray
Write-Host "=========================================" -ForegroundColor Cyan
Write-Host ""

Read-Host "Prem Enter per tancar"
