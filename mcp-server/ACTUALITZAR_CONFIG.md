# ⚠️ IMPORTANT - Actualitza la configuració de ChatGPT Desktop

El servidor MCP ara utilitza **HTTPS** en lloc de HTTP.

## 📝 Què has de fer ara

### 1. Actualitza el fitxer mcp_config.json

Edita el fitxer:
```
C:\Users\vbron\AppData\Roaming\ChatGPT\mcp_config.json
```

**Canvia HTTP per HTTPS:**

❌ **ANTIC (HTTP)**:
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

✅ **NOU (HTTPS)**:
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

### 2. Reinicia el servidor MCP

Si tens el servidor executant-se, atura'l (Ctrl+C) i torna'l a executar:

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\mcp-server\bin\Release\net10.0
.\VeteriLach.McpServer.exe
```

Hauries de veure:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5273
```

### 3. Reinicia ChatGPT Desktop

1. Tanca completament ChatGPT Desktop
2. Obre ChatGPT Desktop
3. El servidor hauria d'estar disponible amb HTTPS

## ✅ Verificació

Testa que funciona:

```powershell
Invoke-WebRequest -Uri "https://localhost:5273/health" -UseBasicParsing
```

Hauria de retornar `200 OK`.

## 🔒 Seguretat

Ara la comunicació entre ChatGPT Desktop i el servidor MCP està **xifrada amb SSL/TLS**:

- ✅ Comunicació xifrada (HTTPS)
- ✅ Certificat de desenvolupament auto-signat i confiat
- ✅ Protocols HTTP/1.1 i HTTP/2 suportats

---

**Data de canvi**: 29 maig 2026  
**Commit**: 3ca85e1  
**Versió servidor**: 1.0.0
