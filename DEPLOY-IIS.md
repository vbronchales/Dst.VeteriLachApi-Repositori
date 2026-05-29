# Guia de Publicació al IIS Local

## 📋 Procés Manual de Publicació

### Opció 1: Script Automatitzat (RECOMANAT)

Executa el script `Deploy-ToIIS.ps1` com a **Administrador**:

```powershell
# 1. Obre PowerShell com a Administrador (Botó dret → "Executar com a administrador")

# 2. Navega al directori del projecte
cd c:\Dst2026\Dst.VeteriLachApi-Repositori

# 3. Executa el script de publicació
.\Deploy-ToIIS.ps1
```

El script fa automàticament:
1. ✅ Compila versió Release
2. ✅ Para l'App Pool
3. ✅ Neteja fitxers antics
4. ✅ Copia nous fitxers
5. ✅ Reinicia l'App Pool

### Opció 2: Publicació Manual

Si prefereixes fer-ho manualment:

```powershell
# 1. Compilar versió Release
cd c:\Dst2026\Dst.VeteriLachApi-Repositori\src\VeteriLach.ReadApi
dotnet publish -c Release -o c:\temp\VeteriLachApi_publish

# 2. Obrir PowerShell com a Administrador i executar:
Import-Module WebAdministration
Stop-WebAppPool -Name "VeteriLachApi"
Start-Sleep -Seconds 2

# 3. Copiar fitxers
Remove-Item "c:\inetpub\wwwroot\VeteriLachApi\*" -Exclude web.config,logs -Recurse -Force
Copy-Item "c:\temp\VeteriLachApi_publish\*" -Destination "c:\inetpub\wwwroot\VeteriLachApi" -Recurse -Force

# 4. Reiniciar App Pool
Start-WebAppPool -Name "VeteriLachApi"
```

## 🔄 Automatització amb GitHub Actions (Local)

⚠️ **NOTA**: GitHub Actions requereix un runner local per executar al IIS de desenvolupament.

### Configurar GitHub Actions Runner Local

1. **Descarregar i configurar runner:**
```powershell
# Crear directori per al runner
mkdir c:\actions-runner; cd c:\actions-runner

# Descarregar (verifica la versió més recent a https://github.com/actions/runner/releases)
Invoke-WebRequest -Uri https://github.com/actions/runner/releases/download/v2.311.0/actions-runner-win-x64-2.311.0.zip -OutFile actions-runner-win-x64-2.311.0.zip

# Descomprimir
Expand-Archive -Path actions-runner-win-x64-2.311.0.zip -DestinationPath .

# Configurar (necessitaràs un token del repositori GitHub)
.\config.cmd --url https://github.com/vbronchales/Dst.VeteriLachApi-Repositori --token <TOKEN>

# Instal·lar com a servei Windows
.\svc.cmd install
.\svc.cmd start
```

2. **Crear workflow `.github/workflows/deploy-iis-local.yml`:**
```yaml
name: Deploy to IIS Local

on:
  pull_request:
    types: [closed]
    branches:
      - main

jobs:
  deploy:
    if: github.event.pull_request.merged == true
    runs-on: self-hosted  # Usa el runner local
    
    steps:
      - name: Checkout code
        uses: actions/checkout@v3
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '10.0.x'
      
      - name: Deploy to IIS
        shell: powershell
        run: |
          .\Deploy-ToIIS.ps1
```

### Alternativa: Git Hook Local

Si no vols configurar GitHub Actions, pots usar un **Git hook local**:

1. **Crear hook post-merge** (`.git/hooks/post-merge`):
```bash
#!/bin/sh
# Hook que es dispara després de cada merge a main

echo "Detectat merge a main. Publicant al IIS local..."
powershell.exe -ExecutionPolicy Bypass -File "Deploy-ToIIS.ps1"
```

2. **Fer executable:**
```powershell
# Nota: Els hooks de Git no es sincronitzen al repositori, són locals
```

## 🧪 Verificar la Publicació

Després de publicar, prova que l'API funciona:

```powershell
# Health check
Invoke-WebRequest -Uri "http://localhost:5072/api/health"

# Provar endpoint amb API Key
$headers = @{"X-API-Key" = "test-api-key"}
Invoke-WebRequest -Uri "http://localhost:5072/api/animals?pageSize=5" -Headers $headers
```

## 📝 Checklist de Publicació

Abans de cada publicació:

- [ ] ✅ Codi compilat sense errors (`dotnet build`)
- [ ] ✅ Tests passats (quan s'implementin)
- [ ] ✅ Canvis commitejats i pushejats
- [ ] ✅ PR aprovada i merged
- [ ] ✅ Executar `Deploy-ToIIS.ps1` com a administrador
- [ ] ✅ Verificar health check
- [ ] ✅ Provar algun endpoint amb dades reals

## 🚨 Troubleshooting

### Error: "Access Denied" al copiar fitxers
**Solució**: Executa PowerShell com a **Administrador**

### Error: "App Pool no respon"
**Solució**: Verifica l'estat manualment i reinicia
```powershell
Import-Module WebAdministration
Get-WebAppPoolState -Name "VeteriLachApi"
Restart-WebAppPool -Name "VeteriLachApi"
```

### Error: "Port 5072 ja està en ús"
**Solució**: Atura el procés que l'està utilitzant
```powershell
Get-Process -Id (Get-NetTCPConnection -LocalPort 5072).OwningProcess | Stop-Process -Force
```

### Logs d'errors
**Ubicació**: `c:\inetpub\wwwroot\VeteriLachApi\logs\`
```powershell
Get-Content "c:\inetpub\wwwroot\VeteriLachApi\logs\log-$(Get-Date -Format 'yyyyMMdd').txt" -Tail 50
```
