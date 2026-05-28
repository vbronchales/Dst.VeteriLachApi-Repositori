# Test-Port.ps1
# Script per comprovar quins ports estan en us

Write-Host ""
Write-Host "=== Comprovant ports ===" -ForegroundColor Cyan
Write-Host ""

$portsToCheck = @(8080, 8081, 41228, 5000)

foreach ($port in $portsToCheck) {
    $listener = Get-NetTCPConnection -LocalPort $port -State Listen -ErrorAction SilentlyContinue
    
    if ($listener) {
        Write-Host "[OCUPAT]  Port $port" -ForegroundColor Red
        
        $process = Get-Process -Id $listener.OwningProcess -ErrorAction SilentlyContinue
        if ($process) {
            Write-Host "          Proces: $($process.Name) (PID: $($process.Id))" -ForegroundColor Gray
        }
    } else {
        Write-Host "[LLIURE]  Port $port" -ForegroundColor Green
    }
}

Write-Host ""
Write-Host "Port per defecte del deployment: 41228" -ForegroundColor Cyan
Write-Host ""
