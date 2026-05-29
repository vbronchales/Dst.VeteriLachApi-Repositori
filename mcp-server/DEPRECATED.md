# ⚠️ CARPETA OBSOLETA

Aquesta carpeta (`mcp-server/`) conté la versió original del servidor MCP i **ja no s'ha d'utilitzar**.

## 🔄 Migració a noves versions

S'han creat **dues versions noves** del servidor MCP, cadascuna optimitzada per un client específic:

### ✅ Versió stdio → Claude Desktop
📁 **Nova ubicació**: `../mcp-server-stdio/`  
📖 **Documentació**: [mcp-server-stdio/README.md](../mcp-server-stdio/README.md)

### ✅ Versió HTTPS → ChatGPT Desktop
📁 **Nova ubicació**: `../mcp-server-https/`  
📖 **Documentació**: [mcp-server-https/README.md](../mcp-server-https/README.md)

## 📚 Documentació

- **Guia principal**: [README_MCP_DUAL.md](../README_MCP_DUAL.md)
- **Configuració dual**: [CONFIGURACIO_MCP_DUAL.md](../CONFIGURACIO_MCP_DUAL.md)

## ❓ Per què dues versions?

- **Claude Desktop**: Només suporta transport **stdio** (stdin/stdout)
- **ChatGPT Desktop**: Només suporta transport **HTTP/HTTPS**

Cada versió està optimitzada per al seu client específic.

---

**NO utilitzis aquesta carpeta**. Utilitza les noves versions a `mcp-server-stdio/` o `mcp-server-https/` segons el client que necessitis.
