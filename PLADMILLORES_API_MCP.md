# Pla de Millores: API REST i Servidor MCP VeteriLach

**Data**: 29 maig 2026  
**Versió**: 1.0  
**Prioritat**: Alta - Funcionalitat Bàsica

---

## 🎯 Objectiu

Millorar la funcionalitat de l'API REST i el servidor MCP per permetre consultes més intel·ligents i reducció de la fricció en casos d'ús habituals.

---

## 🔴 Problemes Identificats (Prioritat Alta)

### 1. No es poden llistar visites globals/recents
**Problema**: `get_animal_visits` requereix `animalId` obligatori  
**Impacte**: Impossible respondre "Quins animals s'han visitat avui?"  
**Cas d'ús**: Llistar darreres visites del dia/setmana/mes

**Solució Proposada API**:
```http
GET /api/visits?startDate=2026-05-29&endDate=2026-05-29&pageSize=20
GET /api/visits/recent?days=7
```

**Solució Proposada MCP**:
```javascript
// Nova tool
{
  "name": "get_recent_visits",
  "description": "Get recent veterinary visits across all animals",
  "inputSchema": {
    "properties": {
      "days": { "type": "number", "description": "Last N days (default: 7)" },
      "pageSize": { "type": "number" },
      "pageNumber": { "type": "number" }
    }
  }
}
```

---

### 2. No es poden filtrar animals per espècie (text)
**Problema**: `idEspecie` requereix GUID, no text com "Conill", "Gat", "Gos"  
**Impacte**: Impossible respondre "Mostra'm tots els conills"  
**Cas d'ús**: Cerca per tipus d'animal

**Solució Proposada API**:
```http
GET /api/animals?especie=CANINA
GET /api/animals?especie=FELINA
GET /api/animals?especie=CONILL
```

**Nou endpoint**:
```http
GET /api/especies
→ [{ "id": "guid-1", "nom": "CANINA" }, { "id": "guid-2", "nom": "FELINA" }, ...]
```

**Solució Proposada MCP**:
```javascript
// Nova tool
{
  "name": "get_especies",
  "description": "List all animal species available in the system",
  "inputSchema": { "type": "object", "properties": {} }
}

// Modificar get_animals per acceptar nom d'espècie
{
  "name": "get_animals",
  "inputSchema": {
    "properties": {
      "especieNom": { "type": "string", "description": "Species name (CANINA, FELINA, CONILL, etc.)" }
    }
  }
}
```

---

### 3. Cerca d'animals massa restrictiva
**Problema**: `searchTerm` només cerca nom o microchip  
**Impacte**: No es pot cercar per raça, color, propietari  
**Cas d'ús**: "Busca gossos de raça Labrador"

**Solució Proposada API**:
```http
GET /api/animals?searchTerm=Labrador&searchFields=rasa
GET /api/animals?rasa=Labrador
GET /api/animals?color=Negre
GET /api/animals?nomPropietari=García
```

---

### 4. No es pot obtenir context complet d'un animal
**Problema**: Cal fer múltiples crides per obtenir:
- Dades animal (`get_animal_detail`)
- Propietari (`get_propietari_detail`)
- Visites (`get_animal_visits`)
- Vendes (`get_customer_sales`)

**Solució Proposada API**:
```http
GET /api/animals/{id}/complete
→ {
    "animal": { ... },
    "propietari": { ... },
    "ultimesVisites": [ ... ],  // últimes 5
    "ultimesVendes": [ ... ]     // últimes 5
  }
```

**Solució Proposada MCP**:
```javascript
{
  "name": "get_animal_complete",
  "description": "Get complete animal information including owner, recent visits and sales",
  "inputSchema": {
    "properties": {
      "animalId": { "type": "string", "required": true }
    }
  }
}
```

---

### 5. No hi ha cerca avançada per dates
**Problema**: Les visites i vendes tenen filtres de data, però els animals no  
**Impacte**: No es pot respondre "Animals visitats al gener 2025"  
**Cas d'ús**: Anàlisi temporal

**Solució Proposada API**:
```http
GET /api/animals/visited?startDate=2025-01-01&endDate=2025-01-31
GET /api/animals/new?startDate=2025-01-01&endDate=2025-01-31  // Nous pacients
```

---

## 🟡 Millores de Qualitat de Vida (Prioritat Mitjana)

### 6. Paginació per defecte massa petita o inexistent
**Problema**: Quan no s'especifica `pageSize`, la resposta pot ser buida o molt petita  
**Impacte**: Claude ha de sempre especificar pageSize

**Solució**:
```csharp
// A l'API: sempre aplicar defaults raonables
pageSize ??= 20;
pageNumber ??= 1;
```

---

### 7. Falta informació d'ajuda sobre paràmetres vàlids
**Problema**: No es sap quins valors accepta `idEspecie`, `idRasa`, etc.  
**Impacte**: Impossible descobrir opcions vàlides

**Solució Proposada**:
```http
GET /api/metadata/especies → Llista espècies
GET /api/metadata/rases → Llista races
GET /api/metadata/colors → Llista colors
```

---

### 8. Respostes massa verboses per MCP
**Problema**: Les respostes inclouen tots els camps, incloent IDs interns  
**Impacte**: Token usage alt, respostes llargues

**Solució**:
```http
GET /api/animals?fields=nom,especie,rasa,dataNaixement
```

---

### 9. No hi ha endpoints d'agregació/estadístiques
**Problema**: Cal processar totes les dades per obtenir estadístiques  
**Cas d'ús**: "Quants gats hi ha al sistema?"

**Solució Proposada**:
```http
GET /api/stats/animals/count
GET /api/stats/animals/by-species
GET /api/stats/visits/by-month?year=2025
```

---

### 10. Falta cerca per propietari a múltiples endpoints
**Problema**: Només `get_customer_sales` accepta `customerId`  
**Impacte**: No es pot veure "Totes les visites del client X"

**Solució**:
```http
GET /api/visits?customerId={guid}
GET /api/animals/by-owner/{customerId}  // Alies de get_animals?idPropietari
```

---

## 🟢 Millores Avançades (Prioritat Baixa)

### 11. Cerca semàntica / fuzzy search
Implementar cerca amb tolerància a errors tipogràfics.

### 12. Cache de resultats freqüents
Afegir cache a endpoints consultats sovint (especies, rases, etc.)

### 13. Webhooks per notificacions en temps real
Notificar canvis en animals, visites, vendes.

### 14. Suport per batch operations
Obtenir múltiples animals en una sola crida.

### 15. GraphQL endpoint com alternativa
Permetre queries personalitzades.

---

## 📊 Resum Prioritat

| Prioritat | Millores | Impacte |
|-----------|----------|---------|
| 🔴 Alta | 5 millores | Funcionalitat bàsica bloquejada |
| 🟡 Mitjana | 5 millores | Experiència d'usuari millorada |
| 🟢 Baixa | 5 millores | Optimitzacions avançades |

---

## 🚀 Fases d'Implementació Recomanades

### Fase 1: Fixes Crítics (1-2 setmanes)
- ✅ Problema 1: Endpoint visites globals
- ✅ Problema 2: Filtre espècie per text + endpoint especies
- ✅ Problema 6: Defaults de paginació

### Fase 2: Millores Funcionals (2-3 setmanes)
- ✅ Problema 3: Cerca avançada animals
- ✅ Problema 4: Endpoint animal complet
- ✅ Problema 7: Metadata endpoints

### Fase 3: Optimitzacions (1-2 setmanes)
- ✅ Problema 5: Cerca temporal
- ✅ Problema 8: Response filtering
- ✅ Problema 9: Estadístiques

### Fase 4: Features Avançats (Opcional)
- Problemes 11-15 segons necessitat

---

## 📝 Canvis Necessaris al Codi

### API (VeteriLach.ReadApi)

**Nous Controllers**:
```
/Controllers/VisitsController.cs  → GetRecentVisits()
/Controllers/MetadataController.cs → GetEspecies(), GetRases()
/Controllers/StatsController.cs → GetAnimalCount(), GetBySpecies()
```

**Modificacions**:
```
/Controllers/AnimalsController.cs
  - Afegir paràmetre especieNom
  - Afegir cerca per raça, color
  - Afegir GetComplete(id)
  - Afegir GetVisited(startDate, endDate)
```

### MCP Server

**Noves Tools**:
```javascript
get_recent_visits     // Visites globals recents
get_especies          // Llista espècies
get_animal_complete   // Animal amb context complet
get_stats_animals     // Estadístiques
```

**Modificacions**:
```javascript
get_animals
  - Afegir paràmetre especieNom (string)
  - Documentar millor els paràmetres
```

---

## ✅ Checklist per Implementar

- [ ] Crear issues a GitHub per cada millora
- [ ] Definir swagger/OpenAPI per nous endpoints
- [ ] Implementar endpoints API
- [ ] Afegir tests unitaris
- [ ] Actualitzar documentació API
- [ ] Actualitzar MCP tools
- [ ] Rebuild i test servidors stdio/HTTPS
- [ ] Actualitzar configuració Claude/ChatGPT
- [ ] Provar casos d'ús reals
- [ ] Documentar canvis a README

---

## 📞 Contacte

Per dubtes o suggerències sobre aquest pla:
- Revisar amb l'equip de desenvolupament
- Validar amb usuaris finals (veterinaris)
- Prioritzar segons feedback real

---

**Última Actualització**: 29 maig 2026
