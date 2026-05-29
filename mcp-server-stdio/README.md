# VeteriLach MCP Server - Stdio Version

Aquesta és la versió **stdio** del servidor MCP per l'API de VeteriLach. Aquesta versió està dissenyada per utilitzar-se amb **Claude Desktop** i altres clients MCP que suporten el protocol stdio (stdin/stdout).

## Què és aquesta versió?

- **Transport**: stdio (stdin/stdout)
- **Client compatible**: Claude Desktop
- **Port**: Cap (no usa xarxa)
- **Comunicació**: Llegeix peticions JSON-RPC de stdin i escriu respostes a stdout

## Instal·lació i configuració per Claude Desktop

### 1. Compilar el projecte

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-stdio
dotnet build -c Release
```

### 2. Configurar Claude Desktop

Edita el fitxer de configuració de Claude Desktop:

**Ubicació**: `C:\Users\[TuUsuari]\AppData\Roaming\Claude\claude_desktop_config.json`

**Contingut** (afegeix o modifica la secció `mcpServers`):

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

⚠️ **Important**: Assegura't que el camí al fitxer `.exe` és correcte segons on tinguis el projecte.

### 3. Reiniciar Claude Desktop

Després de guardar la configuració, **tanca i torna a obrir Claude Desktop** perquè carregui el servidor MCP.

## Verificar que funciona

Un cop Claude Desktop estigui reiniciat:

1. Obre una nova conversa
2. Hauràs de veure les eines MCP disponibles (icona 🔧 o similar)
3. Prova a executar una eina com `get_propietaris` per veure si funciona

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

## Logs i depuració

Els logs del servidor s'escriuen a **stderr** (no stdout, que està reservat pel protocol MCP). Per veure els logs:

- Claude Desktop normalment els captura i els mostra en cas d'error
- També pots executar manualment el servidor per veure els logs:

```powershell
.\bin\Release\net10.0\VeteriLach.McpServer.Stdio.exe
# Escriu peticions JSON-RPC manualment per provar
```

## Requisits

### API de VeteriLach en funcionament

El servidor MCP necessita que l'API de VeteriLach estigui operativa:

- **URL**: `http://localhost:41229`
- **IIS**: L'App Pool ha d'estar iniciat
- **Verificar**: Obre http://localhost:41229/swagger en el navegador

Si l'API no funciona, consulta la documentació principal a `../INSTALL.md`

## Diferències amb la versió HTTPS

| Característica | Stdio | HTTPS |
|----------------|-------|-------|
| Transport | stdin/stdout | REST/HTTPS |
| Client | Claude Desktop | ChatGPT Desktop |
| Port | Cap | 5273 |
| Xarxa | No | Sí (localhost) |
| SSL | No | Sí (certificat dev) |

## Problemes comuns

### "No se encuentra el comando"
- Verifica que el camí al `.exe` a `claude_desktop_config.json` és correcte
- Comprova que has compilat el projecte (`dotnet build -c Release`)

### "No apareixen les eines a Claude"
- Reinicia Claude Desktop completament
- Comprova que la sintaxi JSON de `claude_desktop_config.json` és correcta
- Revisa els logs de Claude Desktop

### "Error al executar una eina"
- Assegura't que l'API de VeteriLach està en funcionament (http://localhost:41229)
- Verifica que l'IIS App Pool està iniciat
- Comprova que la clau API és correcta a `appsettings.json`

## Suport

Per més informació sobre el projecte VeteriLach i l'API:
- Repositori: https://github.com/vbronchales/Dst.VeteriLachApi-Repositori
- Documentació API: `../INSTALL.md`
- Versió HTTPS: `../mcp-server-https/README.md`
