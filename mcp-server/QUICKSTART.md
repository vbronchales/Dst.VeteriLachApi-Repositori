# 🚀 QUICKSTART - Servidor MCP HTTP per ChatGPT Desktop

## 📋 Resum de Canvis

El servidor MCP s'ha convertit de **stdio** (stdin/stdout) a **HTTP** perquè ChatGPT Desktop només suporta servidors MCP amb transport HTTP.

## ⚡ Passos Ràpids

### 1. Compilar el servidor

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server
dotnet build --configuration Release
```

### 2. Executar el servidor

```powershell
cd bin\Release\net10.0
.\VeteriLach.McpServer.exe
```

**Sortida esperada:**
```
info: VeteriLach.McpServer[0]
      VeteriLach MCP Server v1.0.0 starting...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5273
```

⚠️ **IMPORTANT**: Deixa aquest terminal obert mentre uses ChatGPT Desktop.

### 3. Configurar ChatGPT Desktop

Edita el fitxer:
```
%APPDATA%\ChatGPT\mcp_config.json
```

Ruta completa (ajusta el teu usuari):
```
C:\Users\vbron\AppData\Roaming\ChatGPT\mcp_config.json
```

**Contingut** (copia exactament):
```json
{
  "mcpServers": {
    "veterilach": {
      "url": "http://localhost:5273/messages",
      "transport": "http"
    }
  }
}
```

### 4. Reiniciar ChatGPT Desktop

1. Tanca **completament** ChatGPT Desktop
2. Assegura't que el servidor MCP està executant-se
3. Obre ChatGPT Desktop
4. El servidor hauria d'aparèixer disponible

### 5. Verificar que funciona

Des de ChatGPT Desktop, prova:
```
Quines tools tens disponibles del servidor VeteriLach?
```

O:
```
Llista'm les últimes 5 vendes
```

## 🔍 Verificació Manual

### Test 1: Health check

```powershell
Invoke-WebRequest -Uri "http://localhost:5273/health" -UseBasicParsing
```

**Resposta esperada:**
```json
{"status":"healthy","server":"veterilach-server","version":"1.0.0","timestamp":"..."}
```

### Test 2: Llistar tools

```powershell
$body = @{
    jsonrpc = "2.0"
    id = 1
    method = "tools/list"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5273/messages" -Method Post -Body $body -ContentType "application/json"
```

**Hauria de retornar 15 tools.**

## 📚 Diferències amb la versió stdio

| Aspecte | Versió stdio (antiga) | Versió HTTP (nova) |
|---------|----------------------|-------------------|
| Transport | stdin/stdout | HTTP REST |
| Port | - | 5273 |
| Format config | `command`, `args`, `env` | `url`, `transport` |
| Compatibilitat | Claude Desktop | ChatGPT Desktop |
| Endpoints | - | `GET /`, `GET /health`, `POST /messages` |

## ❌ Errors Comuns

### Error: "No se puede conectar"

**Solució**: Assegura't que el servidor està executant-se:
```powershell
# Verifica
Invoke-WebRequest -Uri "http://localhost:5273/health" -UseBasicParsing

# Si falla, executa el servidor
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0
.\VeteriLach.McpServer.exe
```

### Error: "Port 5273 already in use"

**Solució**: Canvia el port a `appsettings.json`:
```json
{
  "Urls": "http://localhost:5280"
}
```

I actualitza `mcp_config.json`:
```json
{
  "mcpServers": {
    "veterilach": {
      "url": "http://localhost:5280/messages",
      "transport": "http"
    }
  }
}
```

### Error: ChatGPT Desktop no detecta el servidor

**Causes**:
1. Format incorrecte de `mcp_config.json` (usa l'exemple de més amunt)
2. Servidor no executant-se
3. ChatGPT Desktop no reiniciat

## 🔧 Instal·lació com a servei (opcional)

Per no haver d'executar manualment cada vegada:

```powershell
# Descarrega NSSM des de https://nssm.cc/download

# Crea el servei
nssm install VeteriLachMcpServer "C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0\VeteriLach.McpServer.exe"

# Configura el directori de treball
nssm set VeteriLachMcpServer AppDirectory "C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0"

# Inicia el servei
nssm start VeteriLachMcpServer
```

Ara el servidor s'iniciarà automàticament amb Windows.

## 📖 Documentació Completa

Per més detalls, consulta:
- [README.md](README.md) - Documentació completa
- [INSTALL.md](INSTALL.md) - Guia d'instal·lació de l'API
- [mcp_config.example.json](mcp_config.example.json) - Exemple de configuració

## 🎯 15 Tools Disponibles

El servidor exposa:
- 📊 **5 tools de Vendes** (get_sales, get_sale_detail, get_customer_sales, get_debts, get_payment_advances)
- 👥 **2 tools de Propietaris** (get_propietaris, get_propietari_detail)
- 🐾 **2 tools d'Animals** (get_animals, get_animal_detail)
- 🏥 **2 tools d'Historial Clínic** (get_animal_visits, get_visit_detail)
- 💊 **4 tools de Medicaments** (search_veterinary_medicines, get_veterinary_medicine, search_human_medicines, get_human_medicine)

## ✅ Checklist

- [ ] Servidor compilat (`dotnet build --configuration Release`)
- [ ] Servidor executant-se (`.\VeteriLach.McpServer.exe`)
- [ ] Health check funciona (`http://localhost:5273/health`)
- [ ] `mcp_config.json` editat amb format HTTP
- [ ] ChatGPT Desktop reiniciat
- [ ] Servidor detectat a ChatGPT Desktop
- [ ] Test d'una tool funciona

## 🆘 Suport

Si tens problemes, consulta la secció [Troubleshooting del README.md](README.md#troubleshooting).
