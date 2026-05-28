# Estratègia de Branques - VeteriLach API

## 📋 Índex
- [Descripció General](#descripció-general)
- [Branques Principals](#branques-principals)
- [Nomenclatura de Branques](#nomenclatura-de-branques)
- [Workflow de Desenvolupament](#workflow-de-desenvolupament)
- [Pull Requests](#pull-requests)
- [Protecció de Branques](#protecció-de-branques)
- [Exemples Pràctics](#exemples-pràctics)

---

## 📖 Descripció General

Aquest projecte utilitza una estratègia de branques basada en **Feature Branching** amb **Pull Requests obligatòries** per garantir qualitat i traçabilitat del codi.

### Principis:
- ✅ **SEMPRE** treballar en branques de feature
- ✅ **MAI** pujar directament a `main`
- ✅ Crear **Pull Request** per a cada feature/fase
- ✅ Mantenir **commits atòmics** i descriptius
- ✅ **Esborrar branques** després de merge

---

## 🌿 Branques Principals

### `main`
- **Propòsit**: Codi estable i producció
- **Protecció**: ✅ Protegida (no push directe)
- **Només accepta**: Pull Requests aprovades
- **Sempre desplegable**: Sí

### `develop` (opcional per futur)
- **Propòsit**: Integració de features
- **Estat**: No implementada encara
- **Ús futur**: Per quan hi hagi múltiples desenvolupadors

---

## 🏷️ Nomenclatura de Branques

### Format General:
```
<tipus>/<fase>-<descripció-curta>
```

### Tipus de Branques:

#### `feature/` - Noves funcionalitats
```bash
feature/fase-8-propietaris-api
feature/fase-9-historial-clinic
feature/fase-10-cimavet-integration
```

#### `refactor/` - Millores de codi sense canviar funcionalitat
```bash
refactor/fase-6-automapper-optimization
refactor/cleanup-weather-forecast
```

#### `fix/` - Correccions de bugs
```bash
fix/validation-error-handling
fix/api-key-hash-mismatch
```

#### `docs/` - Només documentació
```bash
docs/update-readme-fase-7
docs/postman-collection
```

#### `chore/` - Tasques de manteniment
```bash
chore/update-dependencies
chore/add-gitignore
```

---

## 🔄 Workflow de Desenvolupament

### 1️⃣ Crear Nova Branca

```bash
# Assegurar-se que main està actualitzat
git checkout main
git pull origin main

# Crear i canviar a nova branca
git checkout -b feature/fase-8-propietaris-api
```

### 2️⃣ Desenvolupar la Funcionalitat

```bash
# Fer canvis al codi...

# Afegir fitxers modificats
git add .

# Commit amb missatge descriptiu
git commit -m "Afegit PropietarisController amb endpoint GetAll"

# Més commits segons necessitat...
git commit -m "Afegit DTOs per a Propietaris"
git commit -m "Implementat queries amb MediatR"
```

### 3️⃣ Pujar Branca a GitHub

```bash
# Primera pujada de la branca
git push -u origin feature/fase-8-propietaris-api

# Pujades següents (si hi ha més commits)
git push
```

### 4️⃣ Crear Pull Request

**A GitHub (https://github.com/vbronchales/Dst.VeteriLachApi-Repositori):**

1. Anar a **Pull Requests** → **New Pull Request**
2. Seleccionar:
   - Base: `main`
   - Compare: `feature/fase-8-propietaris-api`
3. Títol: `Fase 8: API de Propietaris`
4. Descripció (template més avall)
5. **Create Pull Request**

### 5️⃣ Review i Merge

```bash
# Si és necessari actualitzar la branca amb canvis de main
git checkout feature/fase-8-propietaris-api
git merge main
git push

# Després d'aprovar la PR a GitHub, fer merge
# (es pot fer des de la interfície de GitHub)
```

### 6️⃣ Neteja Post-Merge

```bash
# Actualitzar main localment
git checkout main
git pull origin main

# Esborrar branca local
git branch -d feature/fase-8-propietaris-api

# Esborrar branca remota (opcional, GitHub ho pot fer automàticament)
git push origin --delete feature/fase-8-propietaris-api
```

---

## 📝 Pull Requests

### Template de Descripció

```markdown
## 📋 Descripció

Implementació de l'API de Propietaris amb endpoints de consulta.

## ✅ Canvis Realitzats

- [ ] PropietarisController amb GET endpoints
- [ ] DTOs: PropietariListDto, PropietariDetailDto
- [ ] Queries MediatR: GetPropietarisListQuery, GetPropietariByIdQuery
- [ ] Validators amb FluentValidation
- [ ] Mapatges AutoMapper
- [ ] Tests unitaris (si aplica)
- [ ] Actualitzat README.md
- [ ] Actualitzada col·lecció Postman

## 🧪 Com Provar

1. Executar API: `dotnet run`
2. Provar endpoint: `GET /api/propietaris?pageNumber=1&pageSize=10`
3. Verificar validacions: `GET /api/propietaris?pageNumber=0` → 400 Bad Request

## 📸 Screenshots (opcional)

(Captures de Swagger, Postman, logs, etc.)

## 🔗 Relacionat

- PRD: PRD-001, Secció 4.2 (API de Propietaris)
- Fase: 8 del pla d'implementació
```

### Títols de PR segons Fase

| Fase | Títol PR |
|------|----------|
| 8 | `Fase 8: API de Propietaris` |
| 9 | `Fase 9: API d'Historial Clínic` |
| 10 | `Fase 10: Integració CimaVet/CIMA` |
| Refactor | `Refactor: [Descripció curta]` |
| Fix | `Fix: [Descripció del bug]` |

---

## 🔒 Protecció de Branques

### Configuració de `main` (GitHub)

**Passos per protegir la branca `main`:**

1. Anar a **Settings** del repositori
2. **Branches** → **Add branch protection rule**
3. Branch name pattern: `main`
4. Activar:
   - ✅ **Require a pull request before merging**
     - Require approvals: 0 (o 1 si hi ha revisor)
     - Dismiss stale pull request approvals when new commits are pushed
   - ✅ **Require status checks to pass before merging** (futur)
   - ✅ **Do not allow bypassing the above settings**
5. **Create** / **Save changes**

### Regles Actuals

- ❌ **NO es pot pujar directament a `main`**
- ✅ **Només via Pull Request**
- ⏳ **Aprovacions requerides**: 0 (un sol desenvolupador)
- ⏳ **Status checks**: No configurats (futur: tests, build)
- ⏳ **Code owners**: No configurat (futur)

---

## 💡 Exemples Pràctics

### Exemple 1: Fase Sencera (Fase 8 - Propietaris)

```bash
# 1. Crear branca
git checkout main
git pull origin main
git checkout -b feature/fase-8-propietaris-api

# 2. Desenvolupar TOTA la fase
# - Controller
# - DTOs
# - Queries/Handlers
# - Validators
# - Mapatges
# - Tests
# - Documentació

# 3. Commits durant el desenvolupament
git add src/VeteriLach.ReadApi/Controllers/PropietarisController.cs
git commit -m "Afegit PropietarisController"

git add src/VeteriLach.ReadApi/Application/Propietaris/
git commit -m "Afegit DTOs, Queries i Handlers per Propietaris"

git add src/VeteriLach.ReadApi/Application/Propietaris/Queries/GetPropietarisListQueryValidator.cs
git commit -m "Afegit validador per GetPropietarisListQuery"

# 4. Pujar i crear PR
git push -u origin feature/fase-8-propietaris-api
# Crear PR a GitHub amb títol: "Fase 8: API de Propietaris"

# 5. Merge PR des de GitHub

# 6. Neteja local
git checkout main
git pull origin main
git branch -d feature/fase-8-propietaris-api
```

### Exemple 2: Fase Gran Dividida (Fase 9 - Historial Clínic)

Si la fase és molt gran, dividir en múltiples PRs:

```bash
# Part 1: Estructura bàsica
git checkout -b feature/fase-9-historial-basic-structure
# Desenvolupar: DTOs bàsics, entitats
git push -u origin feature/fase-9-historial-basic-structure
# PR: "Fase 9 Part 1: Estructura bàsica Historial Clínic"

# Part 2: Queries de consulta
git checkout main
git pull origin main
git checkout -b feature/fase-9-historial-queries
# Desenvolupar: GetHistorialQuery, handlers
git push -u origin feature/fase-9-historial-queries
# PR: "Fase 9 Part 2: Queries d'Historial Clínic"

# Part 3: Relacions complexes
git checkout main
git pull origin main
git checkout -b feature/fase-9-historial-relations
# Desenvolupar: Relacions amb visites, diagnòstics, vacunes
git push -u origin feature/fase-9-historial-relations
# PR: "Fase 9 Part 3: Relacions complexes Historial"
```

### Exemple 3: Hotfix (Bug Urgent)

```bash
# Crear branca de fix directament des de main
git checkout main
git pull origin main
git checkout -b fix/validation-null-reference

# Corregir bug
git add src/VeteriLach.ReadApi/Middleware/ValidationExceptionFilter.cs
git commit -m "Fix: NullReferenceException en ValidationExceptionFilter"

# Pujar i crear PR urgent
git push -u origin fix/validation-null-reference
# PR: "Fix: NullReferenceException en validació"

# Merge ràpid
# Neteja
git checkout main
git pull origin main
git branch -d fix/validation-null-reference
```

### Exemple 4: Refactor (Millora de Codi)

```bash
git checkout -b refactor/optimize-animal-queries

# Millorar queries existents
git add src/VeteriLach.ReadApi/Application/Animals/
git commit -m "Optimitzat queries d'animals amb millor ús d'índexs"

git push -u origin refactor/optimize-animal-queries
# PR: "Refactor: Optimització de queries Animals"
```

---

## 📊 Workflow Visual

```
main (protegida)
  │
  ├─── feature/fase-8-propietaris-api
  │      │
  │      ├─ commit: Afegit Controller
  │      ├─ commit: Afegit DTOs
  │      ├─ commit: Afegit Queries
  │      └─ commit: Afegit Validators
  │      │
  │      └─── PR #1: Fase 8: API de Propietaris
  │             │
  │             └─ MERGE → main
  │
  ├─── feature/fase-9-historial-clinic
  │      │
  │      ├─ commit: Estructura bàsica
  │      ├─ commit: Queries
  │      └─ commit: Relations
  │      │
  │      └─── PR #2: Fase 9: Historial Clínic
  │             │
  │             └─ MERGE → main
  │
  └─── fix/urgent-bug
         │
         ├─ commit: Fix bug
         │
         └─── PR #3: Fix: Bug urgent
                │
                └─ MERGE → main
```

---

## 🚀 Checklist Pre-Push

Abans de crear una PR, verificar:

- [ ] ✅ Codi compila sense errors: `dotnet build`
- [ ] ✅ API s'executa correctament: `dotnet run`
- [ ] ✅ Endpoints funcionen (provar amb Postman)
- [ ] ✅ Validacions retornen errors correctes
- [ ] ✅ Logs es generen correctament
- [ ] ✅ README.md actualitzat si cal
- [ ] ✅ Col·lecció Postman actualitzada si cal
- [ ] ✅ Commits tenen missatges descriptius
- [ ] ✅ No hi ha fitxers temporals (.vs, bin, obj)
- [ ] ✅ appsettings.Local.json NO pujat (secrets)
- [ ] ✅ Branca sincronitzada amb `main` si cal

---

## 📚 Recursos

- **GitHub Flow**: https://guides.github.com/introduction/flow/
- **Conventional Commits**: https://www.conventionalcommits.org/
- **Git Branching Best Practices**: https://git-scm.com/book/en/v2/Git-Branching-Branching-Workflows

---

## 🔄 Actualitzacions d'aquesta Estratègia

| Data | Versió | Canvi |
|------|--------|-------|
| 2026-05-28 | 1.0 | Versió inicial amb protecció de `main` |

---

**Nota**: Aquesta estratègia evolucionarà amb el projecte. Futures millores inclouran:
- CI/CD amb GitHub Actions
- Tests automàtics obligatoris
- Code coverage mínim
- Code review obligatori (quan hi hagi més desenvolupadors)
- Semantic versioning amb tags
