# Configuració IIS - VeteriLach Read API

## Dades de Configuració

### IIS
- **Site Name**: VeteriLachApi
- **App Pool Name**: VeteriLAchReadApiAppPool
- **Port**: 41229
- **Physical Path**: c:\inetpub\wwwroot\VeteriLachApi

### Endpoints
- **Base URL**: http://localhost:41229
- **Health Check**: http://localhost:41229/api/health
- **Swagger**: http://localhost:41229/swagger

### API Keys (des de appsettings.json)
- **Test Key**: `4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4`
- **Development Key**: `3209a419486ff0ab5d7cc0e9b892a0b9b070c4d8f887367bc41d8ced074b381a`
- **Swagger UI**: `2d7e9dc59130c0af07b11004712a61ecd696f8eadf36eed8003db9c8f58413fd`

### Paths de Compilació
- **Temp Publish**: c:\temp\VeteriLachApi_publish
- **Source Path**: c:\Dst2026\Dst.VeteriLachApi-Repositori\src\VeteriLach.ReadApi

## Comandes de Test Ràpides

### PowerShell
```powershell
# Test Health
Invoke-WebRequest -Uri "http://localhost:41229/api/health" -UseBasicParsing

# Test Sales API
Invoke-WebRequest -Uri "http://localhost:41229/api/sales?pageSize=2" `
    -Headers @{"X-Api-Key"="4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4"} `
    -UseBasicParsing

# Reiniciar App Pool
Import-Module WebAdministration
Restart-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

## Notes
- L'App Pool s'ha de reiniciar després de copiar fitxers nous
- El directori `logs` i `web.config` NO es sobreescriuen durant el deploy
