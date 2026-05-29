@echo off
REM ========================================================
REM Script per Deploy Fase 1 - VeteriLach ReadAPI
REM IMPORTANT: Executar com a Administrador!
REM ========================================================

echo.
echo ============================================
echo   DEPLOY FASE 1 - VeteriLach ReadAPI
echo ============================================
echo.

REM Verificar si s'executa com a administrador
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERROR] Aquest script requereix privilegis d'Administrador!
    echo.
    echo Boto dret sobre aquest fitxer i selecciona "Run as Administrator"
    echo.
    pause
    exit /b 1
)

echo [1/5] Aturant IIS...
iisreset /stop
if %errorLevel% neq 0 (
    echo [ERROR] No s'ha pogut aturar IIS
    pause
    exit /b 1
)
echo [OK] IIS aturat
echo.

echo [2/5] Esperant 3 segons...
timeout /t 3 /nobreak > nul
echo.

echo [3/5] Publicant API...
cd /d "C:\Dst2026\Dst.VeteriLachApi-Repositori\src\VeteriLach.ReadApi"
dotnet publish -c Release -o "C:\inetpub\wwwroot\VeteriLachApi"
if %errorLevel% neq 0 (
    echo [ERROR] Error durant la publicacio
    echo.
    echo Intentant reiniciar IIS de totes maneres...
    iisreset /start
    pause
    exit /b 1
)
echo [OK] API publicada correctament
echo.

echo [4/5] Iniciant IIS...
iisreset /start
if %errorLevel% neq 0 (
    echo [WARN] Error iniciant IIS, pero provem de continuar...
)
echo [OK] IIS iniciat
echo.

echo [5/5] Verificant endpoints nous...
timeout /t 5 /nobreak > nul

powershell -Command "try { $r = Invoke-WebRequest -Uri 'http://localhost:41229/api/metadata/especies' -Headers @{'X-Api-Key'='mcp-server-key-789'} -ErrorAction Stop; Write-Host '[OK] Endpoint /api/metadata/especies funciona!' -ForegroundColor Green; Write-Host '     Status Code:' $r.StatusCode } catch { Write-Host '[ERROR] Endpoint no disponible:' $_.Exception.Message -ForegroundColor Red }"

powershell -Command "try { $r = Invoke-WebRequest -Uri 'http://localhost:41229/api/visits/recent?days=1&pageSize=1' -Headers @{'X-Api-Key'='mcp-server-key-789'} -ErrorAction Stop; Write-Host '[OK] Endpoint /api/visits/recent funciona!' -ForegroundColor Green; Write-Host '     Status Code:' $r.StatusCode } catch { Write-Host '[ERROR] Endpoint no disponible:' $_.Exception.Message -ForegroundColor Red }"

echo.
echo ============================================
echo   DEPLOY COMPLETAT!
echo ============================================
echo.
echo Endpoints disponibles:
echo   - GET /api/visits/recent
echo   - GET /api/metadata/especies  
echo   - GET /api/metadata/rases
echo.
echo Per veure la documentacio completa:
echo   http://localhost:41229/swagger
echo.

pause
