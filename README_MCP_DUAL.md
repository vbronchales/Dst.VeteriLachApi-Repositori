# 🚀 VeteriLach MCP Server - Configuració Dual

Aquest projecte ofereix **dues versions** del servidor MCP per accedir a l'API de VeteriLach des d'aplicacions d'escriptori amb IA:

| Versió | Client | Transport | Ubicació |
|--------|--------|-----------|----------|
| **stdio** | Claude Desktop | stdin/stdout | `mcp-server-stdio/` |
| **HTTPS** | ChatGPT Desktop | REST/HTTPS | `mcp-server-https/` |

## ✅ Estat actual

✅ **Compilació**: Ambdues versions compilades correctament  
✅ **Versió stdio**: Testada i funcionant  
✅ **Versió HTTPS**: Executant-se correctament a https://localhost:5273  
✅ **Configuració Claude**: Actualitzada  
✅ **Configuració ChatGPT**: Actualitzada  

## 📋 Requisits previs

### 1. API de VeteriLach operativa
```powershell
# Verifica que l'API funciona
Start https://localhost:41229/swagger
```

Si no respon, inicia l'App Pool d'IIS:
```powershell
Import-Module WebAdministration
Start-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

### 2. .NET 10.0 SDK
```powershell
dotnet --version
# Ha de mostrar 10.x.x
```

### 3. Certificat SSL (només per HTTPS)
```powershell
dotnet dev-certs https --trust
```

## 🎯 Guia ràpida

### Per utilitzar amb Claude Desktop

**1. Compilar** (només primer cop):
```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-stdio
dotnet build -c Release
```

**2. Configurar Claude Desktop**:
- Fitxer: `C:\Users\vbron\AppData\Roaming\Claude\claude_desktop_config.json`
- Ja està configurat amb:
  ```json
  {
    "mcpServers": {
      "veterilach": {
        "command": "C:\\Dst2026\\Dst.VeteriLachApi-Repositori\\mcp-server-stdio\\bin\\Release\\net10.0\\VeteriLach.McpServer.Stdio.exe",
        "args": [],
        "env": {}
      }
    }
  }
  ```

**3. Reiniciar Claude Desktop**

📚 [Documentació completa stdio →](mcp-server-stdio/README.md)

---

### Per utilitzar amb ChatGPT Desktop

**1. Compilar** (només primer cop):
```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-https
dotnet build -c Release
```

**2. Iniciar el servidor** (cada vegada que vulguis usar ChatGPT):
```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-https
.\bin\Release\net10.0\VeteriLach.McpServer.Https.exe
```

⚠️ **Deixa aquest terminal obert** mentre utilitzis ChatGPT Desktop.

**3. Configurar ChatGPT Desktop**:
- Fitxer: `C:\Users\vbron\AppData\Roaming\ChatGPT\mcp_config.json`
- Ja està configurat amb:
  ```json
  {
    "mcpServers": {
      "veterilach": {
        "url": "https://localhost:5273/messages",
        "transport": "http"
      }
    }
  }
  ```

**4. Reiniciar ChatGPT Desktop**

📚 [Documentació completa HTTPS →](mcp-server-https/README.md)

---

## 🛠️ 15 Eines disponibles

Ambdues versions exposen les mateixes 15 eines:

### 💰 Vendes (5 eines)
- `get_sales` - Llista de vendes amb paginació i filtres
- `get_sale_detail` - Detall complet d'una venda
- `get_customer_sales` - Vendes d'un client específic
- `get_debts` - Deutes pendents de pagament
- `get_payment_advances` - Pagaments anticipats realitzats

### 👥 Propietaris (2 eines)
- `get_propietaris` - Llista de propietaris amb cerca
- `get_propietari_detail` - Detall complet d'un propietari

### 🐾 Animals / Mascotes (3 eines)
- `get_animals` - Llista d'animals amb filtres
- `get_animal_detail` - Detall complet d'un animal
- `get_animal_visits` - Històric de visites veterinàries

### 🏥 Historials mèdics (1 eina)
- `get_visit_detail` - Detall complet d'una visita

### 💊 Medicaments (4 eines)
- `search_veterinary_medicines` - Cercar medicaments veterinaris
- `get_veterinary_medicine` - Detall d'un medicament veterinari
- `search_human_medicines` - Cercar medicaments d'ús humà  
- `get_human_medicine` - Detall d'un medicament d'ús humà

## 📊 Comparació tècnica

| Aspecte | stdio | HTTPS |
|---------|-------|-------|
| **Tipus projecte** | Console App | Web App |
| **SDK** | Microsoft.NET.Sdk | Microsoft.NET.Sdk.Web |
| **Comunicació** | stdin/stdout | REST API |
| **Port** | - | 5273 |
| **SSL** | No | Sí (certificat dev) |
| **Execució** | Automàtica (Claude ho llança) | Manual (deixar terminal obert) |
| **Logs** | stderr | Console |
| **CORS** | - | Configurat |
| **Executable** | VeteriLach.McpServer.Stdio.exe | VeteriLach.McpServer.Https.exe |

## 🔧 Tests realitzats

### ✅ stdio (Claude Desktop)
```powershell
PS> echo '{"jsonrpc":"2.0","id":1,"method":"initialize",...}' | .\VeteriLach.McpServer.Stdio.exe
# Resposta: {"jsonrpc":"2.0","id":1,"result":{"protocolVersion":"2024-11-05",...}}
```

### ✅ HTTPS (ChatGPT Desktop)
```powershell
PS> curl.exe -k https://localhost:5273
# Resposta: {"name":"veterilach-server-https","version":"1.0.0",...}
```

## 🐛 Resolució de problemes

### stdio: Claude no veu les eines
1. Verifica el camí al `.exe` a `claude_desktop_config.json`
2. Reinicia Claude Desktop completament
3. Comprova que has compilat amb `dotnet build -c Release`

### HTTPS: ChatGPT no connecta
1. **Verifica que el servidor està en execució**:
   ```powershell
   curl.exe -k https://localhost:5273
   ```
2. Si no respon, inicia el servidor:
   ```powershell
   .\bin\Release\net10.0\VeteriLach.McpServer.Https.exe
   ```
3. Reinicia ChatGPT Desktop

### HTTPS: "Failed to bind to address... address already in use"
El port 5273 està ocupat per una altra instància:
```powershell
# Troba el procés
netstat -ano | findstr :5273

# Atura'l (substitueix XXXXX pel PID)
Stop-Process -Id XXXXX -Force
```

### API no accessible
```powershell
# Verifica l'API
Start https://localhost:41229/swagger

# Si no funciona, inicia l'App Pool
Import-Module WebAdministration
Start-WebAppPool -Name "VeteriLAchReadApiAppPool"
```

### Errors de certificat SSL (HTTPS)
```powershell
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

## 📂 Estructura del projecte

```
Dst.VeteriLachApi-Repositori/
│
├── mcp-server-stdio/              ← Versió stdio per Claude
│   ├── Program.cs                 ← App console, stdin/stdout
│   ├── VeteriLach.McpServer.Stdio.csproj
│   ├── appsettings.json
│   ├── README.md
│   ├── claude_desktop_config.example.json
│   ├── Mcp/                       ← Models MCP (compartit)
│   ├── Models/                    ← DTOs API (compartit)
│   └── Services/                  ← Client API (compartit)
│
├── mcp-server-https/              ← Versió HTTPS per ChatGPT
│   ├── Program.cs                 ← App web ASP.NET Core
│   ├── VeteriLach.McpServer.Https.csproj
│   ├── appsettings.json
│   ├── README.md
│   ├── mcp_config.example.json
│   ├── Mcp/                       ← Models MCP (compartit)
│   ├── Models/                    ← DTOs API (compartit)
│   └── Services/                  ← Client API (compartit)
│
├── README_MCP_DUAL.md             ← Aquest fitxer
├── CONFIGURACIO_MCP_DUAL.md       ← Documentació extensa
└── INSTALL.md                     ← Instal·lació de l'API
```

## 🔐 Seguretat

⚠️ **Important**:
- Les claus API estan en `appsettings.json` - **NO** compartir
- L'API només escolta a `localhost` (no accessible externament)
- El servidor MCP només escolta a `localhost`
- El certificat SSL és de desenvolupament (auto-signat)

## 🌐 Enllaços

- **Repositori GitHub**: https://github.com/vbronchales/Dst.VeteriLachApi-Repositori
- **Documentació stdio**: [mcp-server-stdio/README.md](mcp-server-stdio/README.md)
- **Documentació HTTPS**: [mcp-server-https/README.md](mcp-server-https/README.md)
- **Guia configuració dual**: [CONFIGURACIO_MCP_DUAL.md](CONFIGURACIO_MCP_DUAL.md)
- **Instal·lació API**: [INSTALL.md](INSTALL.md)

## 📝 Notes finals

- **Claude Desktop**: Llança automàticament el servidor stdio quan l'utilitzes
- **ChatGPT Desktop**: Has d'iniciar manualment el servidor HTTPS abans d'usar-lo
- **Ambdues versions**: Utilitzen la mateixa API i ofereixen les mateixes funcionalitats
- **Codi compartit**: Les carpetes Mcp/, Models/ i Services/ són idèntiques en ambdues versions

---

✨ **Les dues versions ja estan compilades, configurades i testades!**

Per començar:
- **Claude Desktop**: Només reinicia l'aplicació
- **ChatGPT Desktop**: Executa el servidor HTTPS i reinicia l'aplicació
