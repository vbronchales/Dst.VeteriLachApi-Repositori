# VeteriLach MCP Server

Servidor MCP (Model Context Protocol) per accedir a VeteriLach Read API des de l'aplicació d'escriptori de ChatGPT.

## Característiques

- ✅ Protocol MCP estàndard (JSON-RPC 2.0 sobre stdio)
- ✅ 5 tools exposades per Sales API
- ✅ Configuració mínima (URL API + API Key)
- ✅ Compatible amb ChatGPT Desktop

## Tools Disponibles

### 1. `get_sales`
Obté una llista paginada de vendes amb filtres opcionals.

**Paràmetres:**
- `pageNumber` (opcional): Número de pàgina (defecte: 1)
- `pageSize` (opcional): Mida de pàgina (defecte: 20, màx: 50)
- `startDate` (opcional): Data inici (YYYY-MM-DD)
- `endDate` (opcional): Data fi (YYYY-MM-DD)
- `customerId` (opcional): ID client (GUID)
- `sellerId` (opcional): ID venedor (GUID)
- `animalId` (opcional): ID animal (GUID)
- `onlyUnpaid` (opcional): Només vendes impagades

### 2. `get_sale_detail`
Obté informació detallada d'una venda específica amb tots els articles.

**Paràmetres:**
- `saleId` (requerit): ID de la venda (GUID)

### 3. `get_customer_sales`
Obté totes les vendes d'un client específic.

**Paràmetres:**
- `customerId` (requerit): ID client (GUID)
- `pageNumber`, `pageSize`, `startDate`, `endDate` (opcionals)

### 4. `get_debts`
Obté la llista de deutes (vendes impagades o parcialment pagades).

**Paràmetres:**
- `pageNumber`, `pageSize` (opcionals)
- `customerId` (opcional): Filtrar per client
- `minimumDays` (opcional): Dies mínims pendents
- `minimumAmount` (opcional): Import mínim pendent

### 5. `get_payment_advances`
Obté la llista d'acomptes de clients.

**Paràmetres:**
- `pageNumber`, `pageSize` (opcionals)
- `customerId` (opcional): Filtrar per client
- `startDate`, `endDate` (opcionals)

## Configuració

### 1. Configurar appsettings.json

Edita `appsettings.json` amb la configuració de la teva API:

```json
{
  "VeteriLachApi": {
    "BaseUrl": "http://localhost:41229",
    "ApiKey": "4c806362b613f7496abf284146efd31da90e4b16169fe001841ca17290f427c4",
    "TimeoutSeconds": 30
  }
}
```

**IMPORTANT**: L'API ha d'estar accessible des de la màquina on s'executa el servidor MCP.

### 2. Compilar el servidor

```powershell
cd mcp-server
dotnet build -c Release
```

### 3. Configurar ChatGPT Desktop

Edita el fitxer de configuració de ChatGPT:

**Windows:**
```
%APPDATA%\Claude\claude_desktop_config.json
```

Afegeix el servidor:

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

**NOTA**: Ajusta el path al binari compilat segons la teva configuració.

### 4. Reiniciar ChatGPT Desktop

Tanca i torna a obrir l'aplicació ChatGPT Desktop perquè carregui el servidor MCP.

## Test Manual

Pots provar el servidor manualment des de PowerShell:

```powershell
cd mcp-server\bin\Release\net10.0

# Test 1: Initialize
echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}' | .\VeteriLach.McpServer.exe

# Test 2: List tools
echo '{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}' | .\VeteriLach.McpServer.exe

# Test 3: Get sales
echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"get_sales","arguments":{"pageSize":2}}}' | .\VeteriLach.McpServer.exe
```

**NOTA**: Els logs del servidor s'escriuen a stderr, no a stdout.

## Estructura del Projecte

```
mcp-server/
├── Program.cs                  # Punt d'entrada, stdin/stdout handler
├── appsettings.json           # Configuració
├── Mcp/
│   ├── McpModels.cs          # Models del protocol MCP
│   └── McpServer.cs          # Implementació servidor MCP
├── Models/
│   └── ApiModels.cs          # Models de dades de l'API
└── Services/
    └── VeteriLachApiClient.cs # Client HTTP per l'API
```

## Troubleshooting

### El servidor no es connecta a l'API

1. Verifica que l'API estigui funcionant:
   ```powershell
   Invoke-WebRequest -Uri "http://localhost:41229/api/health" -UseBasicParsing
   ```

2. Comprova que l'ApiKey sigui correcte a `appsettings.json`

3. Revisa els logs a stderr del servidor MCP

### ChatGPT Desktop no detecta el servidor

1. Verifica el path al binari a `claude_desktop_config.json`
2. Assegura't que el fitxer .exe existeix
3. Reinicia ChatGPT Desktop completament
4. Revisa els logs de ChatGPT Desktop

### Errors de compilació

Assegura't que tens .NET 10 SDK instal·lat:
```powershell
dotnet --version
```

## Desenvolupament

Per executar en mode desenvolupament amb logs visibles:

```powershell
cd mcp-server
dotnet run
```

Després escriu peticions JSON-RPC directament a la consola.

## Llicència

MIT
