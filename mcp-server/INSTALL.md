# Guia d'Instal·lació Ràpida - VeteriLach MCP Server

Aquesta és una guia condensada per instal·lar el servidor MCP. Per més detalls, consulta [README.md](README.md).

## Prerequisites

- Windows 10/11
- IIS amb ASP.NET Core Hosting Bundle
- .NET SDK 10.0+
- SQL Server amb base de dades VeteriLach
- ChatGPT Desktop

## Instal·lació en 5 Passos

### 1. Desplega l'API a IIS

```powershell
# Clona el repositori
cd C:\Dst2026
git clone https://github.com/vbronchales/Dst.VeteriLachApi-Repositori.git
cd Dst.VeteriLachApi-Repositori

# Publica l'API
dotnet publish src/VeteriLach.ReadApi/VeteriLach.ReadApi.csproj -c Release -o C:\temp\VeteriLachApi_publish

# Configura IIS (com a Administrador)
Import-Module WebAdministration
New-WebAppPool -Name "VeteriLAchReadApiAppPool"
Set-ItemProperty IIS:\AppPools\VeteriLAchReadApiAppPool -Name "managedRuntimeVersion" -Value ""
New-Item -ItemType Directory -Force -Path "C:\inetpub\wwwroot\VeteriLachApi"
Copy-Item -Path "C:\temp\VeteriLachApi_publish\*" -Destination "C:\inetpub\wwwroot\VeteriLachApi" -Recurse -Force
New-WebSite -Name "VeteriLachReadApi" -Port 41229 -PhysicalPath "C:\inetpub\wwwroot\VeteriLachApi" -ApplicationPool "VeteriLAchReadApiAppPool" -Force
Start-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

### 2. Configura appsettings.json de l'API

Edita `C:\inetpub\wwwroot\VeteriLachApi\appsettings.json`:

```json
{
  "ConnectionStrings": {
    "VeteriLachConnection": "Server=localhost;Database=CanMascotaBd;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  },
  "ApiKeys": {
    "MCPServer": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4"
  }
}
```

### 3. Verifica l'API

```powershell
Invoke-WebRequest -Uri "http://localhost:41229/api/health" -UseBasicParsing
```

### 4. Compila el Servidor MCP

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server
dotnet build -c Release
```

### 5. Configura ChatGPT Desktop

Crea/edita `%APPDATA%\Claude\claude_desktop_config.json`:

```json
{
  "mcpServers": {
    "veterilach": {
      "command": "C:\\Dst2026\\Dst.VeteriLachApi-Repositori\\mcp-server\\bin\\Release\\net10.0\\VeteriLach.McpServer.exe",
      "args": []
    }
  }
}
```

**Reinicia ChatGPT Desktop** completament.

## Verificació

Prova a ChatGPT Desktop:

```
Mostra'm les últimes 5 vendes utilitzant VeteriLach
```

o

```
Cerca animals amb el nom "Luna"
```

## Troubleshooting Ràpid

### Error 503 (API no respon)
```powershell
# Reinicia l'App Pool
Import-Module WebAdministration
Restart-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

### ChatGPT no detecta el servidor
1. Verifica que el path al `.exe` és correcte
2. Usa dobles barres invertides (`\\`) al JSON
3. Reinicia ChatGPT completament

### Error de connexió a la base de dades
- Verifica el ConnectionString
- Comprova que SQL Server està en marxa
- Afegeix permisos a `IIS APPPOOL\VeteriLAchReadApiAppPool`

## Més Ajuda

Consulta [README.md](README.md) per:
- Guia d'instal·lació detallada
- Configuració avançada
- Seguretat
- Test manual
- Troubleshooting complet
