# ⚠️ REINICIAR IIS REQUERIT - Fase 1 Implementada

## ✅ Què s'ha fet

He creat i compilat correctament els nous endpoints de la Fase 1:

### Nous Controllers
1. **VisitsController** - `/api/visits/recent`
2. **MetadataController** - `/api/metadata/especies` i `/api/metadata/rases`

### DTOs Creats
- `RecentVisitDto` - Visites amb info d'animal i propietari
- `EspecieDto` - Espècies amb comptador
- `RasaDto` - Races amb info d'espècie

### Queries i Handlers
- `GetRecentVisitsQuery` + Handler
- `GetEspeciesQuery` + Handler  
- `GetRasesQuery` + Handler

**Compilació**: ✅ Correcta  
**Deploy**: ⚠️ Pendent (fitxers bloquejats per IIS)

---

## 🔄 PAS OBLIGATORI: Reiniciar IIS

Els fitxers estan bloquejats per IIS. Necessites executar com a **Administrador**:

### Opció 1: PowerShell com a Administrador

```powershell
# 1. Obrir PowerShell com a Administrador (botó dret → Run as Administrator)

# 2. Aturar IIS
iisreset /stop

# 3. Publicar l'API
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\src\VeteriLach.ReadApi
dotnet publish -c Release -o C:\inetpub\wwwroot\VeteriLachApi

# 4. Iniciar IIS
iisreset /start

# 5. Verificar que funciona
Invoke-WebRequest -Uri "http://localhost:41229/api/health" `
  -Headers @{"X-Api-Key"="mcp-server-key-789"}
```

### Opció 2: Gestionar IIS (GUI)

```
1. Obrir "IIS Manager" (inetmgr)
2. Navegar a "Application Pools"
3. Botó dret a "VeteriLAchReadApiAppPool" → Stop
4. Esperar 5 segons
5. A PowerShell normal: 
   cd C:\Dst2026\Dst.VeteriLachApi-Repositori\src\VeteriLach.ReadApi
   dotnet publish -c Release -o C:\inetpub\wwwroot\VeteriLachApi
6. Tornar a IIS Manager → Botó dret a l'App Pool → Start
```

---

## 🧪 Verificar que Funciona

Un cop reiniciat IIS:

```powershell
# Test 1: Visites recents (darrer dia)
Invoke-WebRequest -Uri "http://localhost:41229/api/visits/recent?days=1&pageSize=5" `
  -Headers @{"X-Api-Key"="mcp-server-key-789"} | 
  Select-Object -ExpandProperty Content | ConvertFrom-Json | 
  Select-Object -First 1

# Resultat esperat:
# {
#   "idVisita": "...",
#   "diaVisita": "2026-05-29...",
#   "nomAnimal": "Max",
#   "especie": "Gos",
#   "nomPropietari": "Joan",
#   ...
# }

# Test 2: Espècies
Invoke-WebRequest -Uri "http://localhost:41229/api/metadata/especies" `
  -Headers @{"X-Api-Key"="mcp-server-key-789"} | 
  Select-Object -ExpandProperty Content | ConvertFrom-Json

# Resultat esperat:
# [
#   {"idEspecie": "...", "nom": "Gos", "totalAnimals": 5234},
#   {"idEspecie": "...", "nom": "Gat", "totalAnimals": 2410},
#   ...
# ]

# Test 3: Races (filtre per gos)
Invoke-WebRequest -Uri "http://localhost:41229/api/metadata/rases?especie=Gos" `
  -Headers @{"X-Api-Key"="mcp-server-key-789"} | 
  Select-Object -ExpandProperty Content | ConvertFrom-Json | 
  Select-Object -First 3

# Resultat esperat:
# [
#   {"idRasa": "...", "nom": "Labrador", "nomEspecie": "Gos", "totalAnimals": 423},
#   {"idRasa": "...", "nom": "Pastor Alemán", "nomEspecie": "Gos", "totalAnimals": 312},
#   ...
# ]
```

---

## 📊 Endpoints Disponibles Després del Reinici

| Endpoint | Descripció | Paràmetres |
|----------|------------|------------|
| `GET /api/visits/recent` | Visites recents ordenades per data | days (1-90), pageSize, includeAnimalInfo |
| `GET /api/metadata/especies` | Llistat d'espècies amb comptador | - |
| `GET /api/metadata/rases` | Llistat de races | especie (opcional, GUID o nom) |

---

## ⏭️ Pròxims Passos (després del reinici)

1. ✅ Verificar que els 3 endpoints funcionen
2. 🔄 Modificar AnimalsController amb filtres avançats
3. 🔄 Afegir les 3 noves tools MCP
4. 🔄 Compilar servidors MCP
5. 🔄 Testejar amb Claude Desktop

---

## 🆘 Solució de Problemes

### Error 404 en endpoints nous

**Problema**: Els endpoints retornen 404  
**Causa**: IIS no s'ha reiniciat correctament  
**Solució**:
```powershell
# Com a Administrador
iisreset /restart
```

### Error 500 en endpoints nous

**Problema**: Error intern del servidor  
**Causa**: Problema amb les queries o BD  
**Solució**: Revisar logs a `C:\inetpub\wwwroot\VeteriLachApi\logs\veterilach-api-*.txt`

### Compilació fallida

**Problema**: Build errors  
**Solució**: Els fitxers ja estan creats i compilats correctament. Si vols recompilar:
```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\src\VeteriLach.ReadApi
dotnet clean
dotnet build -c Release
```

---

**Estat**: ⏸️ Pendent reinici IIS per completar deploy  
**Creat**: 2026-05-29 21:00  
**Autor**: GitHub Copilot
