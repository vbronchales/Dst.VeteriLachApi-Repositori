# VeteriLach MCP Server - HTTPS Version

Aquesta és la versió **HTTPS** del servidor MCP per l'API de VeteriLach. Aquesta versió està dissenyada per utilitzar-se amb **ChatGPT Desktop** i altres clients MCP que requereixen transport HTTP/HTTPS.

## Què és aquesta versió?

- **Transport**: HTTPS (REST API)
- **Client compatible**: ChatGPT Desktop
- **Port**: 5273
- **Comunicació**: Servidor web ASP.NET Core que escolta peticions JSON-RPC a `/messages`

## Instal·lació i configuració per ChatGPT Desktop

### 1. Compilar el projecte

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-https
dotnet build -c Release
```

### 2. Confiar el certificat SSL (només primera vegada)

```powershell
dotnet dev-certs https --trust
```

Confirma quan Windows demani permís per instal·lar el certificat.

### 3. Executar el servidor MCP

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-https
.\bin\Release\net10.0\VeteriLach.McpServer.Https.exe
```

Hauràs de veure:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5273
```

**Deixa aquest terminal obert** mentre utilitzis ChatGPT Desktop.

### 4. Configurar ChatGPT Desktop

Edita el fitxer de configuració de ChatGPT Desktop:

**Ubicació**: `C:\Users\[TuUsuari]\AppData\Roaming\ChatGPT\mcp_config.json`

**Contingut**:

```json
{
  "veterilach": {
    "url": "https://localhost:5273/messages",
    "transport": "http"
  }
}
```

### 5. Reiniciar ChatGPT Desktop

Després de guardar la configuració, **tanca i torna a obrir ChatGPT Desktop** perquè carregui el servidor MCP.

## Verificar que funciona

### Test del servidor (sense ChatGPT)

1. Executa el servidor (pas 3)
2. Obre el navegador i ves a: `https://localhost:5273`
3. Hauràs de veure un JSON amb informació del servidor

### Test amb ChatGPT Desktop

1. Assegura't que el servidor està en execució
2. Obre ChatGPT Desktop
3. Hauràs de veure les eines MCP disponibles
4. Prova a executar una eina com `get_propietaris`

## Tools disponibles (15)

Aquest servidor MCP exposa 15 eines per interactuar amb l'API de VeteriLach:

### Vendes
- `get_sales` - Obtenir llista de vendes amb paginació
- `get_sale_detail` - Obtenir detall d'una venda concreta
- `get_customer_sales` - Obtenir vendes d'un client específic
- `get_debts` - Obtenir llista de deutes pendents
- `get_payment_advances` - Obtenir pagaments anticipats

### Propietaris
- `get_propietaris` - Obtenir llista de propietaris
- `get_propietari_detail` - Obtenir detall d'un propietari

### Animals (Mascotes)
- `get_animals` - Obtenir llista d'animals amb paginació
- `get_animal_detail` - Obtenir detall d'un animal
- `get_animal_visits` - Obtenir històric de visites d'un animal

### Historials Mèdics
- `get_visit_detail` - Obtenir detall d'una visita veterinària

### Medicaments
- `search_veterinary_medicines` - Cercar medicaments veterinaris
- `get_veterinary_medicine` - Obtenir detall d'un medicament veterinari
- `search_human_medicines` - Cercar medicaments d'ús humà
- `get_human_medicine` - Obtenir detall d'un medicament d'ús humà

## Endpoints del servidor

- `GET /` - Informació del servidor
- `GET /health` - Health check
- `POST /messages` - Endpoint principal MCP (JSON-RPC 2.0)

## Logs i depuració

El servidor web escriu logs a la consola:

```
info: VeteriLach MCP Server v1.0.0 starting (HTTPS mode)...
info: API URL: http://localhost:41229
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5273
```

En cas d'error, els veuràs també a la consola amb detalls.

## Requisits

### API de VeteriLach en funcionament

El servidor MCP necessita que l'API de VeteriLach estigui operativa:

- **URL**: `http://localhost:41229`
- **IIS**: L'App Pool ha d'estar iniciat
- **Verificar**: Obre http://localhost:41229/swagger en el navegador

Si l'API no funciona, consulta la documentació principal a `../INSTALL.md`

### Certificat SSL

El servidor HTTPS necessita un certificat SSL de desenvolupament:

```powershell
dotnet dev-certs https --trust
```

Això s'ha de fer **només una vegada** per ordinador.

## Diferències amb la versió stdio

| Característica | HTTPS | Stdio |
|----------------|-------|-------|
| Transport | REST/HTTPS | stdin/stdout |
| Client | ChatGPT Desktop | Claude Desktop |
| Port | 5273 | Cap |
| Xarxa | Sí (localhost) | No |
| SSL | Sí (certificat dev) | No |
| Execució | Manual (deixar terminal obert) | Automàtica (llançat per Claude) |

## Problemes comuns

### "Unable to configure HTTPS endpoint"
- Executa: `dotnet dev-certs https --clean`
- Després: `dotnet dev-certs https --trust`
- Reinicia el servidor

### "ChatGPT no veu les eines"
- Verifica que el servidor està en execució (`https://localhost:5273`)
- Comprova que `mcp_config.json` té el format correcte
- Reinicia ChatGPT Desktop
- Comprova que no hi ha errors al terminal del servidor

### "Error al executar una eina"
- Assegura't que l'API de VeteriLach està en funcionament (http://localhost:41229)
- Verifica que l'IIS App Pool està iniciat
- Comprova que la clau API és correcta a `appsettings.json`
- Revisa els logs del servidor MCP al terminal

### "Connection refused" o "Cannot connect"
- Verifica que el servidor està executant-se (`.\bin\Release\net10.0\VeteriLach.McpServer.Https.exe`)
- Comprova que no hi ha cap altre servei utilitzant el port 5273
- Assegura't que l'URL a `mcp_config.json` és exactament `https://localhost:5273/messages`

## Executar automàticament

Si vols que el servidor s'executi automàticament a l'inici de Windows:

1. Crea un accés directe al `.exe`
2. Posa'l a `shell:startup` (Escriu-ho a l'explorador de Windows)
3. El servidor s'iniciarà automàticament amb Windows

**Nota**: Això consumirà recursos contínuament. Només si l'utilitzes freqüentment.

## Suport

Per més informació sobre el projecte VeteriLach i l'API:
- Repositori: https://github.com/vbronchales/Dst.VeteriLachApi-Repositori
- Documentació API: `../INSTALL.md`
- Versió stdio: `../mcp-server-stdio/README.md`
