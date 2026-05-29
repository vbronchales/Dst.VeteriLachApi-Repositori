# ✅ Servidor MCP stdio Preparat per Claude Desktop

## Canvis Realitzats

S'ha corregit el problema de validació JSON-RPC que causava l'error "could not attach MCP server veterilach".

### Problema Original
Claude Desktop rebutjava l'schema perquè serialitzàvem camps opcionals amb valor `null`:
```json
{
  "enum": null,
  "required": null,
  "description": null
}
```

### Solució Aplicada
1. **Afegit `JsonIgnore` als models** (`McpModels.cs`):
   ```csharp
   [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
   ```

2. **Configurat opcions JSON** (`Program.cs`):
   ```csharp
   DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
   ```

Ara els camps opcionals simplement **no apareixen** en el JSON quan són null.

### JSON Validat ✅
```powershell
echo '{"jsonrpc":"2.0","id":2,"method":"tools/list"}' | C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-stdio\bin\Release\net10.0\VeteriLach.McpServer.Stdio.exe 2>$null | Select-String 'enum'
```
**Resultat**: Cap aparició de `"enum"` en el JSON (correcte!)

## Com Provar amb Claude Desktop

### 1. Verifica la Configuració
Fitxer: `C:\Users\vbron\AppData\Roaming\Claude\claude_desktop_config.json`
```json
{
  "mcpServers": {
    "veterilach": {
      "command": "C:\\Dst2026\\Dst.VeteriLachApi-Repositori\\mcp-server-stdio\\bin\\Release\\net10.0\\VeteriLach.McpServer.Stdio.exe"
    }
  }
}
```

### 2. Reinicia Claude Desktop
1. Tanca completament Claude Desktop (sortir de la bandeja del sistema)
2. Obre Claude Desktop de nou
3. El servidor MCP s'hauria de connectar automàticament

### 3. Verifica la Connexió
A Claude Desktop, pregunta:
- "Quines eines tens disponibles?"
- Hauries de veure 15 eines del servidor VeteriLach:
  - get_sales
  - get_sale_detail
  - get_customer_sales
  - get_debts
  - get_payment_advances
  - get_propietaris
  - get_propietari_detail
  - get_animals
  - get_animal_detail
  - get_animal_visits
  - get_visit_detail
  - search_veterinary_medicines
  - get_veterinary_medicine
  - search_human_medicines
  - get_human_medicine

### 4. Prova una Eina
```
"Busca les vendes dels darrers 7 dies"
```

Claude hauria d'utilitzar l'eina `get_sales` amb els paràmetres adequats.

## Logs de Debugging

Si hi ha problemes, consulta el log:
```
C:\Users\vbron\AppData\Roaming\Claude\logs\mcp-server-veterilach.log
```

## Executables Actualitzats

- ✅ **stdio**: `C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-stdio\bin\Release\net10.0\VeteriLach.McpServer.Stdio.exe`
- ✅ **HTTPS**: `C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server-https\bin\Release\net10.0\` (necessita rebuild de l'executable)

## Pròxim Pas: HTTPS per ChatGPT

Un cop Claude funcioni, reconstruirem correctament l'executable HTTPS per ChatGPT Desktop.
