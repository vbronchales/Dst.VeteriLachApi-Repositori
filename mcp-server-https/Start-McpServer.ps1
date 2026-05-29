# VeteriLach MCP Server HTTPS - Script d'inici ràpid
# Per ChatGPT Desktop

Write-Host "🚀 Iniciant VeteriLach MCP Server (HTTPS)..." -ForegroundColor Cyan
Write-Host ""

# Comprova que l'API està funcionant
Write-Host "📡 Comprovant API de VeteriLach..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "http://localhost:41229/swagger/index.html" -Method Head -TimeoutSec 3 -ErrorAction Stop
    Write-Host "✅ API de VeteriLach operativa" -ForegroundColor Green
} catch {
    Write-Host "❌ ERROR: L'API de VeteriLach no està accessible" -ForegroundColor Red
    Write-Host "   URL esperada: http://localhost:41229" -ForegroundColor Red
    Write-Host ""
    Write-Host "   Solució:" -ForegroundColor Yellow
    Write-Host "   Import-Module WebAdministration" -ForegroundColor White
    Write-Host "   Start-WebAppPool -Name 'VeteriLAchReadApiAppPool'" -ForegroundColor White
    Write-Host ""
    Read-Host "Prem Enter per sortir"
    exit 1
}

Write-Host ""

# Comprova que el port 5273 està lliure
Write-Host "🔍 Comprovant port 5273..." -ForegroundColor Yellow
$portInUse = Get-NetTCPConnection -LocalPort 5273 -State Listen -ErrorAction SilentlyContinue
if ($portInUse) {
    Write-Host "⚠️  AVÍS: El port 5273 ja està en ús (PID: $($portInUse.OwningProcess))" -ForegroundColor Yellow
    Write-Host "   Probablement ja tens el servidor executant-se." -ForegroundColor Yellow
    Write-Host ""
    $resposta = Read-Host "Vols aturar el procés existent i tornar a iniciar? (S/N)"
    if ($resposta -eq "S" -or $resposta -eq "s") {
        Stop-Process -Id $portInUse.OwningProcess -Force
        Write-Host "✅ Procés aturat" -ForegroundColor Green
        Start-Sleep -Seconds 1
    } else {
        Write-Host "❌ Sortint..." -ForegroundColor Red
        exit 0
    }
}

Write-Host ""

# Inicia el servidor
Write-Host "🌐 Iniciant servidor MCP HTTPS..." -ForegroundColor Cyan
Write-Host "   URL: https://localhost:5273" -ForegroundColor White
Write-Host "   Endpoints:" -ForegroundColor White
Write-Host "     - GET  /" -ForegroundColor Gray
Write-Host "     - GET  /health" -ForegroundColor Gray
Write-Host "     - POST /messages" -ForegroundColor Gray
Write-Host ""
Write-Host "⚠️  IMPORTANT: Deixa aquesta finestra oberta mentre utilitzis ChatGPT Desktop" -ForegroundColor Yellow
Write-Host "   Prem Ctrl+C per aturar el servidor" -ForegroundColor Yellow
Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════════" -ForegroundColor DarkGray
Write-Host ""

# Executa el servidor
$exePath = Join-Path $PSScriptRoot "bin\Release\net10.0\VeteriLach.McpServer.Https.exe"

if (-not (Test-Path $exePath)) {
    Write-Host "❌ ERROR: No s'ha trobat l'executable compilat" -ForegroundColor Red
    Write-Host "   Path esperat: $exePath" -ForegroundColor Red
    Write-Host ""
    Write-Host "   Solució: Compila el projecte primer" -ForegroundColor Yellow
    Write-Host "   dotnet build -c Release" -ForegroundColor White
    Write-Host ""
    Read-Host "Prem Enter per sortir"
    exit 1
}

& $exePath
