# Estratègia de Casos d'Ús per Claude Desktop

**Objectiu**: Permetre a Claude respondre consultes complexes amb menys crides i més context

---

## 📊 Anàlisi del Problema Actual

### Cas d'Ús: "Mostra'm els darrers 10 animals visitats"

**Comportament Actual** (❌ No funciona):
```
1. get_animals → Retorna ordre alfabètic, no per visita
2. get_sales amb dates → Falla (requereix filtre obligatori)
3. get_animal_visits → Falla (requereix animalId obligatori)
4. Intent de crear widget HTML → No pot cridar API MCP des del navegador
```

**Problema Root Cause**:
- ❌ No existeix endpoint `/api/visits` global
- ❌ `get_animals` no té ordenació per última visita
- ❌ Cap tool MCP per obtenir visites recents globals
- ❌ Cal fer N+1 queries (get animals + get visits per cada un)

---

## 🎯 Casos d'Ús Prioritaris (Top 20)

### 🔴 Consultes Temporals (Prioritat Crítica)

#### 1. Visites recents globals
**Query**: "Darrers animals visitats", "Animals visitats avui", "Visites d'aquesta setmana"

**Solució API**:
```http
GET /api/visits/recent?days=7&pageSize=50
GET /api/visits?startDate=2026-05-29&endDate=2026-05-29
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_recent_visits",
  "description": "Get recent veterinary visits across all animals, ordered by date descending",
  "inputSchema": {
    "properties": {
      "days": {
        "type": "number",
        "description": "Last N days (default: 7, max: 90)"
      },
      "pageSize": {
        "type": "number",
        "description": "Results per page (default: 20, max: 100)"
      },
      "includeAnimalInfo": {
        "type": "boolean",
        "description": "Include animal and owner data in response (default: true)"
      }
    }
  }
}
```

**Response Example**:
```json
{
  "visits": [
    {
      "visitId": "guid-1",
      "dataVisita": "2026-05-29T14:30:00",
      "animal": {
        "idAnimal": "guid-x",
        "nom": "MOIRA",
        "especie": "FELINA",
        "rasa": "EUROPEA"
      },
      "propietari": {
        "nom": "García",
        "cognoms": "López"
      },
      "motiu": "Vacunació anual"
    }
  ],
  "totalCount": 145
}
```

---

#### 2. Animals nous/actius per període
**Query**: "Animals nous aquest mes", "Quins animals han vingut per primera vegada?"

**Solució API**:
```http
GET /api/animals/new?startDate=2026-05-01&endDate=2026-05-31
GET /api/animals/first-visit?startDate=2026-05-01
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_new_animals",
  "description": "Get animals registered in a specific time period",
  "inputSchema": {
    "properties": {
      "startDate": { "type": "string", "description": "Start date (YYYY-MM-DD)" },
      "endDate": { "type": "string", "description": "End date (YYYY-MM-DD)" },
      "pageSize": { "type": "number" }
    },
    "required": ["startDate"]
  }
}
```

---

#### 3. Estadístiques de visites
**Query**: "Quantes visites hem tingut aquesta setmana?", "Quin animal ha vingut més vegades?"

**Solució API**:
```http
GET /api/stats/visits/count?startDate=2026-05-22&endDate=2026-05-29
GET /api/stats/visits/by-animal?startDate=2026-01-01&top=10
GET /api/stats/visits/by-day?month=2026-05
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_visit_stats",
  "description": "Get statistics about veterinary visits",
  "inputSchema": {
    "properties": {
      "startDate": { "type": "string", "required": true },
      "endDate": { "type": "string" },
      "groupBy": {
        "type": "string",
        "enum": ["day", "week", "month", "animal", "species"],
        "description": "Group results by time period or entity"
      }
    }
  }
}
```

---

### 🔴 Cerca per Espècie/Raça (Prioritat Crítica)

#### 4. Filtrar animals per espècie (text)
**Query**: "Mostra'm tots els conills", "Quants gats tenim?"

**Solució API**:
```http
GET /api/animals?especie=CONILL&pageSize=100
GET /api/animals/count-by-species
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_animals_by_species",
  "description": "Get all animals of a specific species",
  "inputSchema": {
    "properties": {
      "especie": {
        "type": "string",
        "description": "Species name: CANINA, FELINA, CONILL, etc. Use get_especies to see all available."
      },
      "pageSize": { "type": "number" },
      "onlyActive": {
        "type": "boolean",
        "description": "Only animals with recent visits (default: false)"
      }
    },
    "required": ["especie"]
  }
}
```

**Modificació Tool Existent**:
```javascript
{
  "name": "get_animals",
  // Afegir nous paràmetres:
  "inputSchema": {
    "properties": {
      // ... existing properties
      "especie": {
        "type": "string",
        "description": "Filter by species name (CANINA, FELINA, CONILL, etc.)"
      },
      "rasa": {
        "type": "string",
        "description": "Filter by breed name"
      },
      "sortBy": {
        "type": "string",
        "enum": ["nom", "dataAlta", "ultimaVisita"],
        "description": "Sort results by field"
      },
      "sortOrder": {
        "type": "string",
        "enum": ["asc", "desc"],
        "description": "Sort order (default: asc)"
      }
    }
  }
}
```

---

#### 5. Llistar espècies i races disponibles
**Query**: "Quines espècies teniu?", "Quines races de gos hi ha?"

**Solució API**:
```http
GET /api/metadata/especies
GET /api/metadata/rases?especie=CANINA
GET /api/metadata/colors
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_especies",
  "description": "List all animal species available in the system with counts",
  "inputSchema": {
    "type": "object",
    "properties": {}
  }
}
```

**Response**:
```json
{
  "especies": [
    { "nom": "CANINA", "count": 3245 },
    { "nom": "FELINA", "count": 7644 },
    { "nom": "CONILL", "count": 156 },
    { "nom": "EXOTICA", "count": 89 }
  ]
}
```

---

### 🟡 Context Complet (Prioritat Alta)

#### 6. Informació completa d'un animal
**Query**: "Explica'm tot sobre l'animal X", "Historial complet de MOIRA"

**Solució API**:
```http
GET /api/animals/{id}/complete
```

**Response**:
```json
{
  "animal": { "nom": "MOIRA", "especie": "FELINA", ... },
  "propietari": { "nom": "García López", "telefon": "...", ... },
  "ultimesVisites": [
    { "data": "2026-05-15", "motiu": "Revisió", ... }
  ],
  "ultimesVendes": [
    { "data": "2026-05-15", "import": 45.50, ... }
  ],
  "totalVisites": 12,
  "totalVendes": 8,
  "importTotal": 456.75,
  "primeraVisita": "2024-03-10",
  "ultimaVisita": "2026-05-15"
}
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_animal_complete",
  "description": "Get complete information about an animal including owner, visit history, and sales",
  "inputSchema": {
    "properties": {
      "animalId": { "type": "string", "required": true },
      "includeFullHistory": {
        "type": "boolean",
        "description": "Include all visits and sales (default: only last 10)"
      }
    }
  }
}
```

---

#### 7. Informació completa d'un propietari
**Query**: "Explica'm tot sobre el client García", "Quins animals té aquest propietari?"

**Solució API**:
```http
GET /api/propietaris/{id}/complete
```

**Response**:
```json
{
  "propietari": { "nom": "García", "cognoms": "López", ... },
  "animals": [
    { "nom": "MOIRA", "especie": "FELINA", "ultimaVisita": "2026-05-15" }
  ],
  "totalAnimals": 2,
  "totalVisites": 24,
  "totalVendes": 1245.80,
  "dataAlta": "2020-06-15",
  "ultimaVisita": "2026-05-15"
}
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_propietari_complete",
  "description": "Get complete information about a client including all animals and activity summary"
}
```

---

### 🟡 Cerca Avançada (Prioritat Alta)

#### 8. Cerca multi-criteri
**Query**: "Gats de raça Persa amb visites el 2025", "Gossos castrats amb deute"

**Solució API**:
```http
GET /api/animals/search?especie=FELINA&rasa=Persa&visitedAfter=2025-01-01
GET /api/animals/search?especie=CANINA&castrat=true&hasDebt=true
```

**Nova Tool MCP**:
```javascript
{
  "name": "search_animals_advanced",
  "description": "Advanced multi-criteria search for animals",
  "inputSchema": {
    "properties": {
      "especie": { "type": "string" },
      "rasa": { "type": "string" },
      "color": { "type": "string" },
      "castrat": { "type": "boolean" },
      "sexe": { "type": "number", "description": "0=Unknown, 1=Male, 2=Female" },
      "visitedAfter": { "type": "string", "description": "Only animals with visits after this date" },
      "visitedBefore": { "type": "string" },
      "hasDebt": { "type": "boolean", "description": "Has pending payments" },
      "ageMin": { "type": "number", "description": "Minimum age in years" },
      "ageMax": { "type": "number", "description": "Maximum age in years" }
    }
  }
}
```

---

#### 9. Cerca per propietari (multi-endpoint)
**Query**: "Animals del senyor García", "Visites del client amb telèfon 93..."

**Modificació Tool Existent**:
```javascript
{
  "name": "get_animals",
  "inputSchema": {
    "properties": {
      // Afegir:
      "nomPropietari": {
        "type": "string",
        "description": "Filter by owner name (searches in nom and cognoms)"
      }
    }
  }
}
```

---

### 🟡 Operacions Financeres (Prioritat Alta)

#### 10. Deutes per client amb context
**Query**: "Qui em deu diners?", "Deutes pendents de més de 30 dies"

**Modificació Tool Existent**:
```javascript
{
  "name": "get_debts",
  "inputSchema": {
    "properties": {
      // Afegir:
      "includeClientInfo": {
        "type": "boolean",
        "description": "Include client contact information (default: true)"
      },
      "sortBy": {
        "type": "string",
        "enum": ["amount", "days", "client"],
        "description": "Sort debts by field"
      }
    }
  }
}
```

---

#### 11. Resum financer per període
**Query**: "Quant he facturat aquest mes?", "Vendes per espècie d'animal"

**Solució API**:
```http
GET /api/stats/vendes/summary?startDate=2026-05-01&endDate=2026-05-31
GET /api/stats/vendes/by-species?year=2026
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_sales_summary",
  "description": "Get sales summary and statistics for a time period",
  "inputSchema": {
    "properties": {
      "startDate": { "type": "string", "required": true },
      "endDate": { "type": "string" },
      "groupBy": {
        "type": "string",
        "enum": ["day", "week", "month", "species", "client"],
        "description": "Group sales by dimension"
      }
    }
  }
}
```

---

### 🟢 Anàlisi i Insights (Prioritat Mitjana)

#### 12. Top clients/animals
**Query**: "Clients que més gasten", "Animals amb més visites"

**Solució API**:
```http
GET /api/stats/top-clients?startDate=2026-01-01&limit=10
GET /api/stats/top-animals?metric=visits&limit=10
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_top_rankings",
  "description": "Get top rankings by different metrics",
  "inputSchema": {
    "properties": {
      "rankBy": {
        "type": "string",
        "enum": ["sales", "visits", "spending", "debt"],
        "required": true
      },
      "entity": {
        "type": "string",
        "enum": ["clients", "animals", "species"],
        "required": true
      },
      "limit": { "type": "number", "description": "Top N results (default: 10)" },
      "startDate": { "type": "string" },
      "endDate": { "type": "string" }
    }
  }
}
```

---

#### 13. Comparatives temporals
**Query**: "Com va anar aquest mes comparat amb el mateix mes l'any passat?"

**Solució API**:
```http
GET /api/stats/compare?period1=2026-05&period2=2025-05
```

**Nova Tool MCP**:
```javascript
{
  "name": "compare_periods",
  "description": "Compare activity between two time periods",
  "inputSchema": {
    "properties": {
      "period1Start": { "type": "string", "required": true },
      "period1End": { "type": "string", "required": true },
      "period2Start": { "type": "string", "required": true },
      "period2End": { "type": "string", "required": true },
      "metrics": {
        "type": "array",
        "items": { "enum": ["visits", "sales", "newAnimals", "newClients"] },
        "description": "Metrics to compare"
      }
    }
  }
}
```

---

#### 14. Animals inactius
**Query**: "Animals que fa temps que no vénen", "Clients perduts"

**Solució API**:
```http
GET /api/animals/inactive?monthsSince=6
GET /api/propietaris/inactive?monthsSince=12
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_inactive_entities",
  "description": "Find animals or clients that haven't had recent activity",
  "inputSchema": {
    "properties": {
      "entity": {
        "type": "string",
        "enum": ["animals", "clients"],
        "required": true
      },
      "monthsSince": {
        "type": "number",
        "description": "Months without activity (default: 6)"
      },
      "pageSize": { "type": "number" }
    }
  }
}
```

---

### 🟢 Cerca de Medicaments Millorada (Prioritat Baixa)

#### 15. Cerca amb context d'ús
**Query**: "Medicaments usats en gats", "Què li hem donat a aquest animal?"

**Modificació Tools Existents**:
```javascript
{
  "name": "search_veterinary_medicines",
  "inputSchema": {
    "properties": {
      // Afegir:
      "usedInSpecies": {
        "type": "string",
        "description": "Filter medicines used in specific species"
      },
      "usedInAnimal": {
        "type": "string",
        "description": "Animal ID to see medicines used in its visits"
      }
    }
  }
}
```

---

### 🟢 Exportació i Reports (Prioritat Baixa)

#### 16. Generar informes
**Query**: "Fes-me un informe de visites d'aquest mes"

**Solució API**:
```http
GET /api/reports/visits?startDate=2026-05-01&format=summary
GET /api/reports/sales?startDate=2026-05-01&format=detailed
```

**Nova Tool MCP**:
```javascript
{
  "name": "generate_report",
  "description": "Generate a formatted report for a specific time period",
  "inputSchema": {
    "properties": {
      "reportType": {
        "type": "string",
        "enum": ["visits", "sales", "debts", "activity"],
        "required": true
      },
      "startDate": { "type": "string", "required": true },
      "endDate": { "type": "string" },
      "format": {
        "type": "string",
        "enum": ["summary", "detailed"],
        "description": "Level of detail (default: summary)"
      }
    }
  }
}
```

---

### 🟢 Operacions Batch (Prioritat Baixa)

#### 17. Obtenir múltiples animals/clients
**Query**: "Informació dels animals A, B i C"

**Solució API**:
```http
POST /api/animals/batch
Body: { "ids": ["guid-1", "guid-2", "guid-3"] }
```

**Nova Tool MCP**:
```javascript
{
  "name": "get_animals_batch",
  "description": "Get information for multiple animals in a single call",
  "inputSchema": {
    "properties": {
      "animalIds": {
        "type": "array",
        "items": { "type": "string" },
        "description": "Array of animal GUIDs",
        "required": true
      },
      "includeVisits": { "type": "boolean" }
    }
  }
}
```

---

### 🟢 Alertes i Notificacions (Prioritat Baixa)

#### 18. Vacunacions pendents
**Query**: "Animals que necessiten vacuna", "Recordatoris de visites"

**Solució API**:
```http
GET /api/alerts/vaccinations-due?days=30
GET /api/alerts/checkups-due
```

---

### 🟢 Cerca Geogràfica (Prioritat Baixa)

#### 19. Clients per zona
**Query**: "Clients de Barcelona", "Animals del codi postal 08001"

**Modificació Tool**:
```javascript
{
  "name": "get_propietaris",
  "inputSchema": {
    "properties": {
      // Already exists:
      "poblacio": { "type": "string" },
      "codiPostal": { "type": "string" },
      // Could add:
      "provincia": { "type": "string" }
    }
  }
}
```

---

### 🟢 Cerca Temporal Avançada (Prioritat Baixa)

#### 20. Aniversaris i dates
**Query**: "Animals que fan anys aquest mes", "Clients que fan X anys amb nosaltres"

**Solució API**:
```http
GET /api/animals/birthdays?month=5
GET /api/propietaris/anniversaries?years=5
```

---

## 📈 Resum Estratègia

### Noves Tools MCP Necessàries (Total: 15)

| # | Nom Tool | Prioritat | Resol Casos |
|---|----------|-----------|-------------|
| 1 | `get_recent_visits` | 🔴 Crítica | 1, 2, 3 |
| 2 | `get_new_animals` | 🔴 Crítica | 2 |
| 3 | `get_visit_stats` | 🔴 Crítica | 3, 12 |
| 4 | `get_animals_by_species` | 🔴 Crítica | 4 |
| 5 | `get_especies` | 🔴 Crítica | 4, 5 |
| 6 | `get_animal_complete` | 🟡 Alta | 6 |
| 7 | `get_propietari_complete` | 🟡 Alta | 7 |
| 8 | `search_animals_advanced` | 🟡 Alta | 8 |
| 9 | `get_sales_summary` | 🟡 Alta | 11 |
| 10 | `get_top_rankings` | 🟢 Mitjana | 12 |
| 11 | `compare_periods` | 🟢 Mitjana | 13 |
| 12 | `get_inactive_entities` | 🟢 Mitjana | 14 |
| 13 | `generate_report` | 🟢 Baixa | 16 |
| 14 | `get_animals_batch` | 🟢 Baixa | 17 |
| 15 | `get_metadata` | 🔴 Crítica | 5, 8 |

### Modificacions a Tools Existents (Total: 4)

| Tool | Modificació | Prioritat |
|------|-------------|-----------|
| `get_animals` | Afegir: especie, rasa, sortBy, sortOrder, nomPropietari | 🔴 Crítica |
| `get_debts` | Afegir: includeClientInfo, sortBy | 🟡 Alta |
| `search_veterinary_medicines` | Afegir: usedInSpecies, usedInAnimal | 🟢 Baixa |
| `search_human_medicines` | Afegir: category, activeIngredient filters | 🟢 Baixa |

### Nous Endpoints API Necessaris (Total: ~25)

**Prioritat Crítica (5)**:
- `GET /api/visits/recent`
- `GET /api/visits` (global search)
- `GET /api/metadata/especies`
- `GET /api/metadata/rases`
- `GET /api/animals` (amb filtres especie, rasa, sortBy)

**Prioritat Alta (8)**:
- `GET /api/animals/{id}/complete`
- `GET /api/propietaris/{id}/complete`
- `GET /api/animals/new`
- `GET /api/animals/search` (advanced)
- `GET /api/stats/visits/count`
- `GET /api/stats/visits/by-animal`
- `GET /api/stats/vendes/summary`
- `GET /api/stats/vendes/by-species`

**Prioritat Mitjana (7)**:
- `GET /api/stats/top-clients`
- `GET /api/stats/top-animals`
- `GET /api/stats/compare`
- `GET /api/animals/inactive`
- `GET /api/propietaris/inactive`
- `GET /api/reports/visits`
- `GET /api/reports/sales`

**Prioritat Baixa (5)**:
- `POST /api/animals/batch`
- `GET /api/alerts/vaccinations-due`
- `GET /api/animals/birthdays`
- `GET /api/propietaris/anniversaries`
- `GET /api/metadata/all`

---

## 🚀 Pla d'Implementació per Fases

### Fase 1: Crítica (Setmana 1-2)
**Objectiu**: Desbloquejar consultes temporals i cerca per espècie

1. Implementar `/api/visits/recent` i `/api/visits`
2. Implementar `/api/metadata/especies` i `/api/metadata/rases`
3. Afegir filtres `especie`, `rasa`, `sortBy` a `/api/animals`
4. Crear tools MCP: `get_recent_visits`, `get_especies`, `get_animals_by_species`
5. Modificar tool `get_animals` amb nous paràmetres
6. Testing intensiu amb Claude Desktop

**Casos Resolts**: 1, 2, 3, 4, 5 (75% dels casos més comuns)

---

### Fase 2: Alta (Setmana 3-4)
**Objectiu**: Context complet i cerca avançada

1. Implementar `/api/animals/{id}/complete`
2. Implementar `/api/propietaris/{id}/complete`
3. Implementar `/api/animals/search` (advanced)
4. Implementar endpoints estadístiques bàsiques
5. Crear tools MCP corresponents
6. Testing amb casos d'ús complexos

**Casos Resolts**: 6, 7, 8, 9, 11 (95% cobertura casos habituals)

---

### Fase 3: Mitjana (Setmana 5-6)
**Objectiu**: Anàlisi i insights

1. Implementar endpoints de rankings i comparatives
2. Implementar detecció d'inactivitat
3. Crear tools MCP corresponents
4. Optimitzacions de rendiment (cache, índexs DB)

**Casos Resolts**: 12, 13, 14 (98% cobertura)

---

### Fase 4: Baixa (Setmana 7+)
**Objectiu**: Features avançats opcionals

1. Reports i exportació
2. Batch operations
3. Alertes i notificacions
4. Optimitzacions finals

**Casos Resolts**: 15, 16, 17, 18, 19, 20 (100% cobertura)

---

## 📊 Mètriques d'Èxit

### Abans (Estat Actual)
- ❌ "Darrers animals visitats" → No es pot respondre
- ❌ "Animals de raça X" → No es pot filtrar
- ⚠️ "Historial animal" → 3+ crides necessàries
- ⚠️ "Estadístiques" → Processament manual

### Després Fase 1 (Crítica)
- ✅ "Darrers animals visitats" → 1 crida (`get_recent_visits`)
- ✅ "Animals de raça X" → 1 crida (`get_animals especie=X`)
- ⚠️ "Historial animal" → 3+ crides (encara)
- ⚠️ "Estadístiques" → Parcial

### Després Fase 2 (Alta)
- ✅ "Darrers animals visitats" → 1 crida
- ✅ "Animals de raça X" → 1 crida
- ✅ "Historial animal" → 1 crida (`get_animal_complete`)
- ✅ "Estadístiques" → 1-2 crides

### Objectiu Final (Totes Fases)
- ✅ 100% casos d'ús coberts
- ✅ Màxim 1-2 crides per consulta complexa
- ✅ Respostes en <2 segons
- ✅ Context complet en cada resposta

---

## 🎯 KPIs de Rendiment

| Mètrica | Actual | Fase 1 | Fase 2 | Objectiu Final |
|---------|--------|--------|--------|----------------|
| % Consultes respondibles | 30% | 75% | 95% | 100% |
| Crides promig per consulta | 5+ | 2-3 | 1-2 | 1 |
| Temps resposta promig | N/A | <3s | <2s | <1s |
| Token usage promig | N/A | -30% | -50% | -60% |
| Satisfacció Claude | ❌ | ✅ | ✅✅ | ✅✅✅ |

---

**Conclusió**: Amb aquestes millores, Claude Desktop podrà respondre pràcticament qualsevol consulta sobre la clínica veterinària de manera eficient, precisa i amb context complet.
