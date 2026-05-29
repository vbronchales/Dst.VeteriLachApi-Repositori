# VeteriLach MCP Server - Guia de configuració dual

Aquest repositori conté **dues versions** del servidor MCP per l'API de VeteriLach:

## 📁 Estructura del projecte

```
mcp-server-stdio/          ← Versió STDIO per Claude Desktop
├── Program.cs
├── VeteriLach.McpServer.Stdio.csproj
├── appsettings.json
├── README.md
├── claude_desktop_config.example.json
└── bin/Release/net10.0/VeteriLach.McpServer.Stdio.exe

mcp-server-https/          ← Versió HTTPS per ChatGPT Desktop
├── Program.cs
├── VeteriLach.McpServer.Https.csproj
├── appsettings.json
├── README.md
├── mcp_config.example.json
└── bin/Release/net10.0/VeteriLach.McpServer.Https.exe

[Carpetes compartides entre ambdues versions]
Mcp/                       ← Models i lògica MCP (compartit)
Models/                    ← DTOs de l'API (compartit)
Services/                  ← Client API HTTP (compartit)
```

## 🔀 Quina versió necessito?

| Client | Versió a utilitzar | Transport |
|--------|-------------------|-----------|
| **Claude Desktop** | `mcp-server-stdio` | stdio (stdin/stdout) |
| **ChatGPT Desktop** | `mcp-server-https` | HTTPS (REST) |

## 🚀 Quick Start

### Per Claude Desktop (stdio)

1. **Compilar**:
   ```powershell
   cd mcp-server-stdio
   dotnet build -c Release
   ```

2. **Configurar Claude**:
   - Fitxer: `C:\Users\[TuUsuari]\AppData\Roaming\Claude\claude_desktop_config.json`
   - Copia el contingut de `claude_desktop_config.example.json`
   - Ajusta el camí al `.exe` si cal

3. **Reiniciar Claude Desktop**

📖 Documentació completa: [mcp-server-stdio/README.md](mcp-server-stdio/README.md)

---

### Per ChatGPT Desktop (https)

1. **Compilar**:
   ```powershell
   cd mcp-server-https
   dotnet build -c Release
   ```

2. **Confiar certificat SSL** (només primera vegada):
   ```powershell
   dotnet dev-certs https --trust
   ```

3. **Executar el servidor** (deixar terminal obert):
   ```powershell
   .\bin\Release\net10.0\VeteriLach.McpServer.Https.exe
   ```

4. **Configurar ChatGPT**:
   - Fitxer: `C:\Users\[TuUsuari]\AppData\Roaming\ChatGPT\mcp_config.json`
   - Copia el contingut de `mcp_config.example.json`

5. **Reiniciar ChatGPT Desktop**

📖 Documentació completa: [mcp-server-https/README.md](mcp-server-https/README.md)

---

## 🛠️ Eines disponibles (15 en ambdues versions)

### 💰 Vendes
- `get_sales` - Llista de vendes amb paginació
- `get_sale_detail` - Detall d'una venda
- `get_customer_sales` - Vendes d'un client
- `get_debts` - Deutes pendents
- `get_payment_advances` - Pagaments anticipats

### 👥 Propietaris
- `get_propietaris` - Llista de propietaris
- `get_propietari_detail` - Detall d'un propietari

### 🐾 Animals
- `get_animals` - Llista d'animals
- `get_animal_detail` - Detall d'un animal
- `get_animal_visits` - Històric de visites

### 🏥 Historials
- `get_visit_detail` - Detall d'una visita veterinària

### 💊 Medicaments
- `search_veterinary_medicines` - Cercar medicaments veterinaris
- `get_veterinary_medicine` - Detall medicament veterinari
- `search_human_medicines` - Cercar medicaments d'ús humà
- `get_human_medicine` - Detall medicament d'ús humà

## ⚙️ Requisits comuns

Ambdues versions necessiten:

### 1. API de VeteriLach en funcionament
- URL: `http://localhost:41229`
- IIS App Pool iniciat
- Verificació: http://localhost:41229/swagger

### 2. .NET 10.0 SDK
```powershell
dotnet --version
# Ha de mostrar 10.x.x
```

### 3. Configuració API correcta
Les dues versions llegeixen `appsettings.json`:
```json
{
  "VeteriLachApi": {
    "BaseUrl": "http://localhost:41229",
    "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
    "TimeoutSeconds": 30
  }
}
```

## 🔍 Comparació tècnica

| Aspecte | stdio | HTTPS |
|---------|-------|-------|
| **SDK** | Microsoft.NET.Sdk | Microsoft.NET.Sdk.Web |
| **OutputType** | Exe (Console) | - (Web) |
| **Comunicació** | stdin/stdout | REST API |
| **Port** | Cap | 5273 |
| **SSL** | No | Sí (dev cert) |
| **Execució** | Automàtica (llançat pel client) | Manual (terminal obert) |
| **Logs** | stderr | Console (stdout) |
| **Dependències** | Microsoft.Extensions.Configuration.* | Microsoft.AspNetCore.OpenApi |
| **CORS** | No aplica | Sí (configurat) |
| **Endpoints** | - | /, /health, /messages |

## 🐛 Troubleshooting

### Problema comú: API no accessible

**Símptoma**: "Failed to connect to API" o errors HTTP al executar eines

**Solució**:
1. Verifica que l'API està en marxa: http://localhost:41229/swagger
2. Si no funciona, inicia l'IIS App Pool:
   ```powershell
   Import-Module WebAdministration
   Start-WebAppPool -Name "VeteriLAchReadApiAppPool"
   ```

### stdio: "No apareixen eines a Claude"
- Reinicia Claude Desktop completament
- Verifica sintaxi JSON de `claude_desktop_config.json`
- Comprova que el camí al `.exe` és correcte

### HTTPS: "ChatGPT no connecta"
- Verifica que el servidor està executant-se (terminal obert)
- Obre https://localhost:5273 al navegador (ha de respondre)
- Comprova que `mcp_config.json` té l'URL correcta
- Reinicia ChatGPT Desktop

### SSL Certificate errors (HTTPS)
```powershell
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

## 📚 Documentació addicional

- **Guia completa stdio**: [mcp-server-stdio/README.md](mcp-server-stdio/README.md)
- **Guia completa HTTPS**: [mcp-server-https/README.md](mcp-server-https/README.md)
- **Instal·lació API**: [INSTALL.md](INSTALL.md)
- **Arquitectura**: [legacy2026/02-VeteriLach-Arquitectura.md](../legacy2026/02-VeteriLach-Arquitectura.md)

## 🔐 Seguretat

- Les claus API estan en text pla a `appsettings.json` - **NO** compartir aquest fitxer
- L'API només escolta a `localhost` (no accessible externament)
- El servidor HTTPS només escolta a `localhost` (no accessible externament)
- El certificat SSL és de desenvolupament (auto-signat)

## 🌐 Repositori

- GitHub: https://github.com/vbronchales/Dst.VeteriLachApi-Repositori
- Path local: `C:\Dst2026\Dst.VeteriLachApi-Repositori`

## 🙋 Suport

En cas de problemes:
1. Consulta el README específic de la teva versió
2. Revisa la secció Troubleshooting
3. Comprova que l'API està funcionant
4. Verifica els logs (stderr per stdio, console per HTTPS)
