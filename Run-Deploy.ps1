# Run-Deploy.ps1
# Wrapper per executar Deploy-IIS.ps1 amb elevacio automatica

$logFile = Join-Path $PSScriptRoot "deploy-output.txt"

Write-Host ""
Write-Host "=== VeteriLach Deployment ===" -ForegroundColor Cyan
Write-Host ""

# Verificar permisos
$currentPrincipal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
$isAdmin = $currentPrincipal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)

if (-not $isAdmin) {
    Write-Host "Elevant a Administrador..." -ForegroundColor Yellow
    
    $scriptPath = Join-Path $PSScriptRoot "Deploy-IIS.ps1"
    
    # Script que captura output
    $captureScript = @"
Set-Location '$PSScriptRoot'
& '$scriptPath' *>&1 | Tee-Object -FilePath '$logFile'
Write-Host ""
Write-Host "Prem qualsevol tecla per tancar..."
`$null = `$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
"@
    
    $tempScript = Join-Path $env:TEMP "VeteriLach-Deploy.ps1"
    Set-Content -Path $tempScript -Value $captureScript -Encoding UTF8
    
    Start-Process powershell.exe -ArgumentList "-NoProfile -ExecutionPolicy Bypass -File `"$tempScript`"" -Verb RunAs -Wait
    
    # Mostrar resultats
    Write-Host ""
    if (Test-Path $logFile) {
        Write-Host "=== RESULTATS ===" -ForegroundColor Cyan
        Write-Host ""
        Get-Content $logFile
        Write-Host ""
        Write-Host "Log complet: $logFile" -ForegroundColor Gray
    } else {
        Write-Host "No s'ha pogut obtenir el log" -ForegroundColor Red
    }
    
    Remove-Item $tempScript -Force -ErrorAction SilentlyContinue
} else {
    # Ja som admin
    & "$PSScriptRoot\Deploy-IIS.ps1" *>&1 | Tee-Object -FilePath $logFile
}

Write-Host ""
