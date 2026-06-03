# 📋 PRD & Roadmap - VeteriLach MCP Server

**Document**: Product Requirements Document  
**Projecte**: VeteriLach MCP Server & Read API  
**Versió**: 1.0  
**Data**: 3 Juny 2026  
**Autor**: GitHub Copilot + vbronchales

---

## 🎯 Objectiu General

Crear un servidor MCP (Model Context Protocol) complet que permeti a Claude Desktop i ChatGPT gestionar de manera autònoma totes les necessitats d'una clínica veterinària, des de consultes bàsiques fins a escriptura de dades, anàlisi i automatitzacions.

---

## 📊 Estat Actual (3 Juny 2026)

### API Read (VeteriLach.ReadApi)
- ✅ 8 endpoints funcionals (Animals, Propietaris, Visites individuals, Medicines)
- ⚠️ 3 endpoints amb problemes (Sales - controller no carrega)
- 🔄 Fase 1 implementada però pendent deploy: `/api/visits/recent`, `/api/metadata/especies`, `/api/metadata/rases`

### MCP Server
- ✅ 2 versions: stdio (Claude Desktop) i HTTPS (ChatGPT Desktop)
- ✅ 15 tools implementades: 10 funcionals + 5 Sales desactivades
- ✅ Transport dual operatiu
- ⚠️ Només lectura (cap eina d'escriptura)
- ⚠️ Filtres limitats a les eines existents

---

## 🗺️ Roadmap per Fases

### ✅ Fase 0: Infraestructura Base (COMPLETADA)
**Estat**: Implementada i funcional  
**Durada**: 2 setmanes (Maig 2026)

#### Components
- ✅ API Read amb arquitectura CQRS + MediatR
- ✅ MCP Server stdio per Claude Desktop
- ✅ MCP Server HTTPS per ChatGPT Desktop
- ✅ 15 tools MCP bàsiques
- ✅ Autenticació amb API Key (SHA256)
- ✅ Paginació estàndard
- ✅ Documentació Swagger

---

### 🔄 Fase 1: Consultes Temporals i Metadata (EN CURS)
**Prioritat**: 🔴 Crítica  
**Durada estimada**: 1 setmana  
**Estat**: Backend implementat, pendent deploy IIS

#### Objectius
Desbloquejar consultes temporals com "darrers animals visitats" i "quines espècies teniu".

#### API - Nous Endpoints

| Endpoint | Mètode | Descripció | Estat |
|----------|--------|------------|-------|
| `/api/visits/recent` | GET | Visites recents globals (últims N dies) | ✅ Creat |
| `/api/metadata/especies` | GET | Llistat d'espècies amb comptador d'animals | ✅ Creat |
| `/api/metadata/rases` | GET | Llistat de races amb filtre per espècie | ✅ Creat |

**Paràmetres `/api/visits/recent`**:
- `days` (int, default: 7, max: 90) - Últims N dies
- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 20, max: 100)
- `includeAnimalInfo` (bool, default: true) - Inclou dades d'animal i propietari

**Response Example**:
```json
{
  "data": [
    {
      "idVisita": "guid-1",
      "diaVisita": "2026-06-02T10:30:00",
      "resum": "Vacunació anual",
      "pes": 4.5,
      "nomAnimal": "MOIRA",
      "especie": "FELINA",
      "rasa": "EUROPEA",
      "nomPropietari": "Joan",
      "cognomsPropietari": "García López"
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalItems": 145,
    "totalPages": 8
  }
}
```

#### MCP - Noves Tools

| Tool | Descripció | Endpoint API | Estat |
|------|------------|--------------|-------|
| `get_recent_visits` | Visites recents globals | `/api/visits/recent` | ⏳ Pendent |
| `get_especies` | Llistat d'espècies | `/api/metadata/especies` | ⏳ Pendent |
| `get_rases` | Llistat de races | `/api/metadata/rases` | ⏳ Pendent |

#### Casos d'Ús Desbloquejats
- ✅ "Mostra'm els darrers 10 animals visitats"
- ✅ "Animals visitats avui"
- ✅ "Quines espècies d'animals teniu?"
- ✅ "Quantes races de gos hi ha registrades?"

#### Checklist Fase 1
- [x] Crear DTOs (RecentVisitDto, EspecieDto, RasaDto)
- [x] Crear Queries i Handlers
- [x] Crear Controllers (VisitsController, MetadataController)
- [x] Compilar API correctament
- [ ] Deploy a IIS (**PENDENT - Requereix admin**)
- [ ] Afegir 3 tools al MCP Server
- [ ] Compilar MCP stdio i HTTPS
- [ ] Testejar amb Claude Desktop

---

### 🚀 Fase 2: Filtratge Avançat i Cerca Millorada
**Prioritat**: 🟠 Alta  
**Durada estimada**: 1 setmana  
**Estat**: Pendent

#### Objectius
Millorar les eines existents amb filtres avançats i ordenació flexible.

#### API - Millores a Endpoints Existents

**`GET /api/animals` (millores)**:
- Afegir paràmetres:
  - `sortBy` (string): "name", "birthDate", "lastVisit", "species"
  - `sortOrder` (string): "asc", "desc"
  - `species` (string): Filtre per espècie (nom o GUID)
  - `breed` (string): Filtre per raça
  - `minAge`, `maxAge` (int): Rang d'edat en anys
  - `isCastrated` (bool?): Filtrar per castrat/no castrat
  - `hasChip` (bool?): Té xip o no
  - `lastVisitDays` (int?): Animals amb visita en últims N dies

**`GET /api/animals/{id}/visits` (millores)**:
- Afegir paràmetres:
  - `sortOrder` (string): "asc", "desc" (default: desc)
  - `visitType` (string?): Filtre per tipus de visita
  - `doctorId` (guid?): Filtre per veterinari

**`GET /api/propietaris` (millores)**:
- Afegir paràmetres:
  - `sortBy` (string): "name", "registrationDate", "lastVisit", "totalAnimals"
  - `sortOrder` (string): "asc", "desc"
  - `hasActiveAnimals` (bool?): Només clients amb animals actius
  - `minAnimals`, `maxAnimals` (int): Rang de nombre d'animals
  - `city` (string?): Filtre per població

#### MCP - Millores a Tools Existents

| Tool | Millores | Casos d'Ús |
|------|----------|------------|
| `get_animals` | Afegir tots els paràmetres nous | "Animals grans (>10 anys)", "Gossos no castrats", "Animals sense xip" |
| `get_animal_visits` | Sortir per data, filtrar per doctor | "Totes les visites amb el Dr. Martínez" |
| `get_propietaris` | Ordenar per última visita | "Clients inactius últims 6 mesos" |

#### Casos d'Ús Desbloquejats
- "Animals grans de més de 10 anys"
- "Gossos no castrats"
- "Animals sense microxip"
- "Clients que no han vingut en 6 mesos"
- "Visites del Dr. Martínez aquesta setmana"

#### Checklist Fase 2
- [ ] Modificar GetAnimalsQuery amb nous paràmetres
- [ ] Modificar GetAnimalsQueryHandler amb OrderBy dinàmic
- [ ] Actualitzar AnimalsController
- [ ] Modificar altres Queries (Propietaris, Visits)
- [ ] Actualitzar MCP tools amb nous paràmetres
- [ ] Testing exhaustiu de filtres combinats
- [ ] Deploy

---

### 🏥 Fase 3: Vacunacions i Pla Sanitari
**Prioritat**: 🟠 Alta  
**Durada estimada**: 2 setmanes  
**Estat**: Pendent

#### Objectius
Gestionar el pla sanitari: vacunes, desparasitacions, revisions.

#### API - Nous Endpoints

| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/health/vaccines/{animalId}` | GET | Historial de vacunes d'un animal |
| `/api/health/vaccines/pending` | GET | Vacunes properes o endarrerides |
| `/api/health/dewormings/{animalId}` | GET | Historial de desparasitacions |
| `/api/health/checkups/upcoming` | GET | Revisions properes |

**Paràmetres `/api/health/vaccines/pending`**:
- `daysAhead` (int, default: 30) - Properes en N dies
- `species` (string?) - Filtrar per espècie
- `overdue` (bool, default: false) - Només endarrerides

**Response Example**:
```json
{
  "data": [
    {
      "animalId": "guid-x",
      "nomAnimal": "MAX",
      "propietari": "García",
      "vacuna": "Rabia",
      "dataPrevisita": "2026-06-15",
      "diesPendents": 12,
      "estat": "propera"
    }
  ]
}
```

#### MCP - Noves Tools

| Tool | Descripció | Casos d'Ús |
|------|------------|------------|
| `get_pending_vaccines` | Vacunes properes/endarrerides | "Quins animals necessiten vacuna aquesta setmana?" |
| `get_vaccine_history` | Historial de vacunes d'un animal | "Quan va rebre la darrera vacuna de la rabia?" |
| `get_pending_dewormings` | Desparasitacions properes | "Animals que cal desparasitar aquest mes" |
| `get_health_checkups` | Revisions preventives properes | "Revisions anuals pendents" |

#### Base de Dades
**Taules afectades** (probablement existents):
- `Hos_Vacuna` - Vacunacions
- Possiblement cal crear taula `Hos_Desparasitacio` si no existeix
- Calendar de revisions (pot ser dins de `Hos_Visita` amb tipus específic)

#### Casos d'Ús Desbloquejats
- "Quins gossos necessiten vacuna de la rabia aquest mes?"
- "Animals amb vacunes endarrerides"
- "Quan va rebre la darrera desparasitació MOIRA?"
- "Revisions anuals pendents"

#### Checklist Fase 3
- [ ] Explorar schema BD per vacunes/desparasitacions
- [ ] Crear entities EF Core
- [ ] Crear DTOs per vacunes i pla sanitari
- [ ] Implementar Queries i Handlers
- [ ] Crear HealthController
- [ ] Afegir 4 tools al MCP
- [ ] Testing amb dades reals
- [ ] Deploy

---

### 📊 Fase 4: Estadístiques i Analítica
**Prioritat**: 🟡 Mitjana  
**Durada estimada**: 1-2 setmanes  
**Estat**: Pendent

#### Objectius
Proporcionar informes i estadístiques per gestió del negoci.

#### API - Nous Endpoints

| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/stats/revenue` | GET | Resum d'ingressos per període |
| `/api/stats/visits` | GET | Estadístiques de visites |
| `/api/stats/top-clients` | GET | Clients més actius |
| `/api/stats/species-breakdown` | GET | Distribució d'animals per espècie |
| `/api/stats/vet-workload` | GET | Càrrega de treball per veterinari |

**Paràmetres comuns**:
- `startDate`, `endDate` (DateTime)
- `groupBy` (string): "day", "week", "month", "year"

**Response Example `/api/stats/visits`**:
```json
{
  "period": {
    "startDate": "2026-05-01",
    "endDate": "2026-05-31"
  },
  "totalVisits": 342,
  "bySpecies": {
    "CANINA": 198,
    "FELINA": 132,
    "OTHER": 12
  },
  "byDoctor": {
    "Dr. Martínez": 145,
    "Dr. López": 197
  },
  "averagePerDay": 11.0
}
```

#### MCP - Noves Tools

| Tool | Descripció | Casos d'Ús |
|------|------------|------------|
| `get_revenue_summary` | Resum d'ingressos | "Ingressos del mes passat" |
| `get_visit_statistics` | Estadístiques de visites | "Quantes visites hem tingut aquest mes?" |
| `get_top_clients` | Clients més actius | "Top 10 clients per facturació" |
| `get_species_distribution` | Distribució per espècie | "Quina és la distribució de gats vs gossos?" |
| `get_vet_workload` | Càrrega per veterinari | "Quantes visites ha fet cada veterinari?" |

#### Casos d'Ús Desbloquejats
- "Ingressos d'aquest mes vs mes passat"
- "Quantes visites ha fet cada veterinari aquesta setmana?"
- "Quins són els 10 clients que més gasten?"
- "Distribució d'espècies a la clínica"
- "Dia de la setmana amb més visites"

#### Checklist Fase 4
- [ ] Crear StatsController
- [ ] Implementar queries amb agregacions SQL
- [ ] Optimitzar amb índexs si cal
- [ ] Crear DTOs per estadístiques
- [ ] Afegir 5 tools MCP
- [ ] Testing amb diferents períodes
- [ ] Deploy

---

### 📅 Fase 5: Agenda i Cites
**Prioritat**: 🟠 Alta  
**Durada estimada**: 2 setmanes  
**Estat**: Pendent

#### Objectius
Gestionar l'agenda de la clínica: cites, disponibilitat, recordatoris.

#### API - Nous Endpoints

| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/appointments` | GET | Llistat de cites |
| `/api/appointments/{id}` | GET | Detall d'una cita |
| `/api/appointments` | POST | Crear nova cita |
| `/api/appointments/{id}` | PUT | Modificar cita |
| `/api/appointments/{id}` | DELETE | Cancel·lar cita |
| `/api/appointments/availability` | GET | Franges horàries disponibles |

**Paràmetres `GET /api/appointments`**:
- `date` (DateTime?) - Dia concret
- `startDate`, `endDate` (DateTime?) - Rang de dates
- `vetId` (Guid?) - Veterinari concret
- `status` (string?) - "pending", "confirmed", "completed", "cancelled"
- `animalId` (Guid?) - Cites d'un animal

**Request Body `POST /api/appointments`**:
```json
{
  "animalId": "guid-x",
  "vetId": "guid-y",
  "dateTime": "2026-06-10T10:00:00",
  "duration": 30,
  "reason": "Vacunació anual",
  "notes": "Primera vegada"
}
```

#### MCP - Noves Tools

| Tool | Descripció | Tipus | Casos d'Ús |
|------|------------|-------|------------|
| `get_appointments` | Consultar agenda | Lectura | "Cites d'avui" |
| `get_appointment_detail` | Detall d'una cita | Lectura | "Info de la cita de les 10h" |
| `check_availability` | Disponibilitat horària | Lectura | "Té forat el Dr. Martínez demà?" |
| `create_appointment` | Crear cita | **Escriptura** | "Programa cita per MAX dimecres 10h" |
| `update_appointment` | Modificar cita | **Escriptura** | "Mou la cita de les 10 a les 11" |
| `cancel_appointment` | Cancel·lar cita | **Escriptura** | "Cancel·la la cita de demà" |

#### Base de Dades
**Taula necessària**: `Agn_Cita` o similar (probablement ja existeix al mòdul AgendaLach)

**Camps**:
- IdCita (Guid, PK)
- IdAnimal (Guid, FK)
- IdVeterinari (Guid, FK)
- DataHora (DateTime)
- Durada (int, minuts)
- Estat (string)
- Motiu (string)
- Notes (string)

#### Casos d'Ús Desbloquejats
- "Quines cites tinc avui?"
- "Programa una cita per MAX dimecres a les 10h"
- "El Dr. Martínez té forat demà a la tarda?"
- "Cancel·la la cita de les 3"
- "Mou la cita de les 10 a les 11"

#### Checklist Fase 5
- [ ] Verificar existència de taula Agn_Cita
- [ ] Crear entities EF Core
- [ ] Crear DTOs (AppointmentDto, CreateAppointmentDto, etc.)
- [ ] Implementar Commands (CreateAppointment, UpdateAppointment, CancelAppointment)
- [ ] Implementar Queries (GetAppointments, CheckAvailability)
- [ ] Crear AppointmentsController
- [ ] Afegir 6 tools MCP (3 read + **3 write**)
- [ ] Testing exhaustiu (concurrència, validacions)
- [ ] Deploy

⚠️ **IMPORTANT**: Aquesta és la primera fase amb **escriptura de dades**. Cal:
- Validacions estrictes
- Control de concurrència
- Logging exhaustiu
- Rollback en cas d'error

---

### 📦 Fase 6: Estoc i Inventari
**Prioritat**: 🟠 Alta  
**Durada estimada**: 2-3 setmanes  
**Estat**: Pendent

#### Objectius
Gestionar estoc de productes i medicaments: consultes, alertes, moviments.

#### API - Nous Endpoints

| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/inventory/products` | GET | Llistat de productes |
| `/api/inventory/stock/{productId}` | GET | Estoc actual d'un producte |
| `/api/inventory/low-stock` | GET | Productes per sota del mínim |
| `/api/inventory/movements` | GET | Moviments d'estoc (entrades/sortides) |
| `/api/inventory/categories` | GET | Categories de productes |

**Response Example `/api/inventory/low-stock`**:
```json
{
  "data": [
    {
      "productId": "guid-1",
      "nom": "Vacuna Rabia Nobivac",
      "categoria": "Medicaments",
      "stockActual": 3,
      "stockMinim": 10,
      "unitats": "dosis",
      "diesDisponibles": 5
    }
  ]
}
```

#### MCP - Noves Tools

| Tool | Descripció | Casos d'Ús |
|------|------------|------------|
| `search_products` | Cerca de productes del catàleg | "Busca vacunes de rabia disponibles" |
| `get_product_stock` | Estoc actual d'un producte | "Quantes dosis de Nobivac queden?" |
| `get_low_stock_alerts` | Productes a punt d'esgotar-se | "Quins medicaments cal demanar?" |
| `get_stock_movements` | Historial de moviments | "Moviments de pipetes antiparàsits" |

#### Base de Dades
**Taules afectades** (probablement del mòdul ArtiLach):
- `Art_Article` - Productes/serveis
- `Art_Magatzem` - Magatzems
- `Art_EstocMagatzem` - Estoc per magatzem
- `Fac_LínieVenda` - Per analitzar consum

#### Casos d'Ús Desbloquejats
- "Quants vials de vacuna Nobivac queden?"
- "Productes a punt d'esgotar-se"
- "Pipetes antiparàsits venudes aquest mes"
- "On està l'estoc de cada producte?" (per magatzem)

#### Checklist Fase 6
- [ ] Explorar schema BD del mòdul ArtiLach
- [ ] Crear entities EF Core per inventari
- [ ] Crear DTOs (ProductDto, StockDto, etc.)
- [ ] Implementar Queries
- [ ] Crear InventoryController
- [ ] Afegir 4 tools MCP
- [ ] Testing amb dades reals
- [ ] Deploy

---

### ✍️ Fase 7: API Write - Escriptura de Dades
**Prioritat**: 🔴 Crítica (llarg termini)  
**Durada estimada**: 3-4 setmanes  
**Estat**: Pendent

#### Objectius
Permetre a Claude **crear i modificar dades** a la base de dades: animals, visites, propietaris, vendes.

⚠️ **AQUESTA ÉS LA FASE MÉS COMPLEXA**: Requereix:
- Projecte nou: `VeteriLach.WriteApi` o afegir Commands a la Read API
- Validacions exhaustives
- Transaccionalitat
- Auditoria de canvis
- Control de permisos
- Testing intensiu

#### Arquitectura
**Opció 1**: Separar Read i Write (CQRS pur)
- `VeteriLach.ReadApi` (actual) - Només GET
- `VeteriLach.WriteApi` (nou) - POST/PUT/DELETE

**Opció 2**: Ampliar Read API amb Commands
- Mantenir un sol projecte
- Afegir `Application/Commands` folder
- Usar MediatR per Commands (ja usat per Queries)

**Recomanació**: Opció 2 (més simple, menys overhead)

#### API - Nous Endpoints (Escriptura)

**Animals**:
| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/animals` | POST | Crear nou animal |
| `/api/animals/{id}` | PUT | Actualitzar dades d'un animal |
| `/api/animals/{id}` | DELETE | Donar de baixa un animal |

**Propietaris**:
| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/propietaris` | POST | Crear nou client |
| `/api/propietaris/{id}` | PUT | Actualitzar dades del client |

**Visites**:
| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/visits` | POST | Crear nova visita |
| `/api/visits/{id}` | PUT | Actualitzar notes/diagnòstic |
| `/api/visits/{id}/close` | POST | Tancar visita |

**Vendes**:
| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/sales` | POST | Crear nova venda |
| `/api/sales/{id}/payment` | POST | Registrar pagament |

**Request Example `POST /api/animals`**:
```json
{
  "nom": "Luna",
  "idPropietari": "guid-x",
  "idRasa": "guid-y",
  "dataNaixement": "2025-03-15",
  "sexe": 2,
  "color": "Blanca",
  "numXip": "981234567890123",
  "castrat": false
}
```

#### MCP - Noves Tools (Escriptura)

| Tool | Descripció | Validacions Crítiques |
|------|------------|----------------------|
| `create_animal` | Crear nou animal | Propietari existeix, raça vàlida, xip únic |
| `update_animal` | Modificar animal | Animal existeix, no modificar IDs |
| `create_propietari` | Crear client | NIF únic, email vàlid |
| `update_propietari` | Modificar client | Client existeix |
| `create_visit` | Crear visita | Animal i veterinari existeixen |
| `update_visit` | Modificar visita | Visita no tancada |
| `create_sale` | Crear venda | Client existeix, productes vàlids |
| `add_payment` | Registrar pagament | Import <= deute pendent |

#### Seguretat i Validacions

**Nivell 1 - Validacions de negoci**:
- Animal: Xip únic, raça pertany a espècie correcta, data naixement <= avui
- Propietari: NIF vàlid i únic, email format correcte
- Visita: Animal actiu, veterinari vàlid, data <= avui
- Venda: Estoc disponible, preus positius

**Nivell 2 - Transaccionalitat**:
- Usar `[TransactionScope]` per operacions multi-taula
- Rollback automàtic en cas d'error

**Nivell 3 - Auditoria**:
- Taula `Slc_AuditLog` per registrar tots els canvis
- Camps: Usuari, Data, Acció, TaulaAfectada, IdRegistre, ValorsAntics, ValorsNous

**Nivell 4 - Permisos** (opcional):
- Diferents API Keys per read vs write
- O autenticació JWT amb rols (més complex)

#### Casos d'Ús Desbloquejats
- "Crea un nou animal anomenat Luna, propietari Joan García, gat europeu blanc"
- "Actualitza el pes de MOIRA a 4.2 kg"
- "Registra una visita d'avui per vacunació de MAX"
- "Crea una venda de pipetes antiparàsits per al client García"
- "Registra un pagament de 50€ del client Martínez"

#### Checklist Fase 7
- [ ] Decidir arquitectura (CQRS pur vs Commands en Read API)
- [ ] Crear Commands (CreateAnimal, UpdateAnimal, etc.)
- [ ] Crear CommandHandlers amb validacions
- [ ] Implementar sistema d'auditoria
- [ ] Afegir transaccionalitat
- [ ] Crear endpoints POST/PUT/DELETE
- [ ] Afegir 8 tools MCP d'escriptura
- [ ] Testing exhaustiu (happy path + edge cases)
- [ ] Testing de rollback i concurrència
- [ ] Deploy amb API Key separada per write
- [ ] Documentació de seguretat

⚠️ **RISC ALT**: Aquesta fase modifica dades reals. Requereix:
- Testing amb BD de test
- Backup de BD abans de deploy
- Monitorització intensiva post-deploy
- Possibilitat de rollback ràpid

---

### 📄 Fase 8: Documents i Comunicacions
**Prioritat**: 🟢 Baixa  
**Durada estimada**: 1-2 setmanes  
**Estat**: Futur

#### Objectius
Gestionar documents adjunts (radiografies, analítiques) i comunicacions (SMS, emails).

#### API - Nous Endpoints

**Documents**:
| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/documents/{animalId}` | GET | Documents d'un animal |
| `/api/documents/visit/{visitId}` | GET | Documents d'una visita |
| `/api/documents/{id}/download` | GET | Descarregar document |

**Comunicacions**:
| Endpoint | Mètode | Descripció |
|----------|--------|------------|
| `/api/communications/log` | GET | Historial de SMS/emails enviats |
| `/api/communications/pending` | GET | Recordatoris pendents d'enviar |

#### MCP - Noves Tools

| Tool | Descripció |
|------|------------|
| `get_animal_documents` | Llistat de documents d'un animal |
| `get_communication_log` | Historial de comunicacions enviades |

#### Casos d'Ús
- "Quins documents tens de l'animal MAX?"
- "Historial de recordatoris enviats a García"

---

## 📅 Timeline Recomanat

| Fase | Prioritat | Durada | Inici Estimat | Fi Estimat |
|------|-----------|--------|---------------|------------|
| Fase 0: Infraestructura | 🔴 Crítica | 2 setmanes | Maig 2026 | ✅ Completada |
| Fase 1: Consultes Temporals | 🔴 Crítica | 1 setmana | 3 Juny 2026 | 10 Juny 2026 |
| Fase 2: Filtratge Avançat | 🟠 Alta | 1 setmana | 10 Juny 2026 | 17 Juny 2026 |
| Fase 3: Vacunacions | 🟠 Alta | 2 setmanes | 17 Juny 2026 | 1 Juliol 2026 |
| Fase 4: Estadístiques | 🟡 Mitjana | 1-2 setmanes | 1 Juliol 2026 | 15 Juliol 2026 |
| Fase 5: Agenda | 🟠 Alta | 2 setmanes | 15 Juliol 2026 | 29 Juliol 2026 |
| Fase 6: Inventari | 🟠 Alta | 2-3 setmanes | 29 Juliol 2026 | 19 Agost 2026 |
| Fase 7: API Write | 🔴 Crítica | 3-4 setmanes | 19 Agost 2026 | 16 Setembre 2026 |
| Fase 8: Documents | 🟢 Baixa | 1-2 setmanes | Futur | Futur |

**Total estimat**: ~3 mesos (fins Fase 7 completada)

---

## 🎯 KPIs i Mètriques d'Èxit

### Per Fase
- **Cobertura de casos d'ús**: % de queries de Claude que retornen resposta vàlida
- **Temps de resposta**: < 500ms per tool call
- **Error rate**: < 1% en tools MCP
- **Adopció**: Nombre de queries diàries a través de Claude

### Globals
- **Tools MCP totals**: De 15 (actual) → 50+ (Fase 7 completada)
- **Endpoints API**: De 11 (actual) → 60+ (Fase 7 completada)
- **Cobertura funcional**: De 30% → 95% (funcions d'una clínica veterinària)

---

## 🔧 Decisions Tècniques Pendents

### Per discutir abans de cada fase:

**Fase 1**: ✅ Resolt (en deploy)

**Fase 2**:
- Límit màxim de filtres simultanis per performance?
- Cache per queries freqüents?

**Fase 3**:
- Esquema de vacunes: taula única o per tipus?
- Calendari automàtic vs manual?

**Fase 5**:
- Sistema de recordatoris automàtic?
- Integració amb Google Calendar / Outlook?

**Fase 7** (CRÍTICA):
- ⚠️ Arquitectura: CQRS pur (2 APIs) vs Commands en Read API?
- ⚠️ Seguretat: API Keys separades vs JWT amb rols?
- ⚠️ Auditoria: Taula única vs per entitat?
- ⚠️ Rollback: Manual vs automàtic amb snapshots?

---

## 📚 Recursos i Documentació

### Documents Existents
- ✅ `ESTRATEGIA_CASOS_US_CLAUDE.md` - Casos d'ús i anàlisi
- ✅ `PLA_ACCIO_FASE1.md` - Checklist detallat Fase 1
- ✅ `AnalisisDelQueFaltaAlMcp_20260603.md` - Gap analysis complet
- ✅ `REINICIAR_IIS_FASE1.md` - Instruccions deploy
- ✅ `deploy-fase1.bat` - Script automatitzat

### Per crear per cada fase
- [ ] `FASE_X_TECHNICAL_SPEC.md` - Especificació tècnica detallada
- [ ] `FASE_X_TESTING_PLAN.md` - Pla de testing
- [ ] `FASE_X_DEPLOYMENT_GUIDE.md` - Guia de deploy
- [ ] `FASE_X_ROLLBACK_PLAN.md` - Pla de rollback (Fases 5+)

---

## ✅ Aprovació i Signoff

**Propietari del producte**: vbronchales  
**Data proposta**: 3 Juny 2026  
**Versió**: 1.0  

**Aprovacions necessàries**:
- [ ] Revisar prioritats de fases
- [ ] Validar timeline
- [ ] Aprovar decisions tècniques per Fase 7 (API Write)
- [ ] Confirmar recursos disponibles

---

**Pròxim pas immediat**: Executar `deploy-fase1.bat` com a administrador per completar Fase 1.
