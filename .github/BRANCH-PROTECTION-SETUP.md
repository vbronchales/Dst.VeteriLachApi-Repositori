# Configurar Protecció de Branques a GitHub

Aquesta guia explica pas a pas com configurar la protecció de la branca `main` al repositori **Dst.VeteriLachApi-Repositori**.

---

## 📋 Objectiu

Protegir la branca `main` perquè:
- ❌ NO es pugui pujar codi directament (`git push origin main` fallarà)
- ✅ NOMÉS s'acceptin canvis via Pull Request
- ✅ Mantenir la qualitat i traçabilitat del codi

---

## 🔧 Passos de Configuració

### 1️⃣ Accedir a la Configuració del Repositori

1. Obre el repositori a GitHub:
   ```
   https://github.com/vbronchales/Dst.VeteriLachApi-Repositori
   ```

2. Clica a **Settings** (⚙️ a la barra superior dreta)
   - Si no veus Settings, necessites permisos d'administrador

### 2️⃣ Anar a Branch Protection Rules

1. Al menú lateral esquerre, clica **Branches**
2. A la secció "Branch protection rules", clica **Add branch protection rule**

### 3️⃣ Configurar la Regla per `main`

#### A) Branch name pattern
```
main
```
> Això aplicarà la regla només a la branca `main`

#### B) Proteccions a Activar

Marca les següents opcions:

##### ✅ Require a pull request before merging
- **Descripció**: Obliga a crear una PR abans de fer merge a `main`
- **Config**:
  - [ ] Require approvals: **0** (per ara, un sol desenvolupador)
    - _Nota: Posa 1 si hi ha algú més que pugui revisar_
  - [x] Dismiss stale pull request approvals when new commits are pushed
    - _Si es fa push després d'aprovar, cal tornar a aprovar_
  - [ ] Require review from Code Owners (deixar desmarcada)
  - [x] Require approval of the most recent reviewable push
  - [ ] Require conversation resolution before merging (opcional)

##### ✅ Require status checks to pass before merging
- **Descripció**: Requereix que passin tests/builds abans de merge
- **Config** (DESACTIVAR de moment):
  - [ ] No marcar aquesta opció ara
  - _Activarem això més endavant quan afegim CI/CD_

##### ⚠️ Require conversation resolution before merging
- **Opcional**: Si vols que es resolguin tots els comentaris abans de merge
- **Recomanat**: Deixar desmarcada de moment

##### ❌ Require signed commits
- **Descripció**: Tots els commits han d'estar signats amb GPG
- **Config**: Deixar desmarcada (no obligatori de moment)

##### ✅ Require linear history
- **Descripció**: No permet merge commits, només fast-forward o squash
- **Config**: **DESACTIVADA** (permet merge commits normals)

##### ❌ Require deployments to succeed before merging
- **Descripció**: Per CI/CD avançat
- **Config**: Deixar desmarcada

##### ⚠️ Lock branch
- **Descripció**: Fa la branca read-only
- **Config**: **NO marcar** (volem fer merges via PR)

##### ❌ Do not allow bypassing the above settings
- **Descripció**: Ni administradors poden saltar-se les regles
- **Config**:
  - [x] **MARCAR** per màxima protecció
  - [ ] Deixar desmarcada si vols poder fer bypass en emergències

#### C) Regles Addicionals (Opcional)

##### Restrict who can dismiss pull request reviews
- Deixar desmarcada (no aplica amb 0 reviewers)

##### Restrict who can push to matching branches
- **Opcional**: Limitar qui pot crear PRs
- **Recomanat**: Deixar desmarcada (ets l'únic desenvolupador)

##### Allow force pushes
- **Config**: **NO marcar** (prohibir force push a `main`)
- ❌ Force push pot destruir historial

##### Allow deletions
- **Config**: **NO marcar** (prohibir esborrar `main`)
- ❌ No volem que es pugui esborrar accidentalment

### 4️⃣ Guardar la Configuració

1. Revisa totes les opcions
2. Clica **Create** (o **Save changes** si ja existeix)
3. Veuràs un missatge de confirmació verd ✅

---

## ✅ Verificar que Funciona

### Test 1: Intentar Push Directe (Ha de FALLAR)

```bash
# Això FALLARÀ amb la protecció activada
git checkout main
echo "test" >> README.md
git add README.md
git commit -m "Test push directe"
git push origin main

# ERROR esperat:
# remote: error: GH006: Protected branch update failed for refs/heads/main.
# remote: error: Changes must be made through a pull request.
```

✅ **Si veig aquest error, la protecció està ACTIVA**

### Test 2: Via Pull Request (Ha de FUNCIONAR)

```bash
# Crear branca
git checkout -b feature/test-branch-protection

# Fer canvi
echo "# Test" >> test.md
git add test.md
git commit -m "Test PR workflow"

# Pujar branca
git push -u origin feature/test-branch-protection

# Crear PR a GitHub i fer merge
# ✅ Això ha de funcionar sense problemes
```

---

## 📊 Configuració Recomanada Inicial

| Opció | Estat | Raó |
|-------|-------|-----|
| Require pull request | ✅ ON | Obliga a usar PRs |
| Require approvals | 0 | Un sol desenvolupador |
| Dismiss stale approvals | ✅ ON | Seguretat |
| Require status checks | ❌ OFF | No tenim CI/CD encara |
| Require signed commits | ❌ OFF | No obligatori |
| Require linear history | ❌ OFF | Permetem merge commits |
| Lock branch | ❌ OFF | Volem fer merges |
| Do not allow bypass | ✅ ON | Protecció màxima |
| Allow force pushes | ❌ OFF | Prohibit |
| Allow deletions | ❌ OFF | Prohibit |

---

## 🔄 Evolució Futura

Quan afegim CI/CD, actualitzar:

### Fase 2: CI/CD Bàsic
```
✅ Require status checks to pass before merging
  → Marcar: "Build and Test" workflow
  → Require branches to be up to date before merging
```

### Fase 3: Múltiples Desenvolupadors
```
✅ Require approvals: 1
  → Al menys una persona ha de revisar
✅ Require code owners review
  → CODEOWNERS file defineix qui ha de revisar cada part
```

### Fase 4: Qualitat de Codi
```
✅ Require status checks:
  → Code coverage > 80%
  → Linter passed
  → Security scan passed
```

---

## 🆘 Troubleshooting

### Problema: No veig l'opció "Settings"
**Solució**: Necessites permisos d'administrador al repositori.

### Problema: Vaig fer push directe abans de configurar protecció
**Solució**: No passa res, la protecció només aplica a partir d'ara.

### Problema: Necessito fer un canvi urgent a `main` directament
**Solució**:
1. Desmarca temporalment "Do not allow bypass"
2. Fes el canvi
3. Torna a marcar-la immediatament

### Problema: La PR es pot fer merge sense aprovació
**Solució**: Revisa que "Require approvals" està a 0 (correcte per un sol desenvolupador). Si vols auto-aprovar, està bé així.

---

## 📸 Screenshots de Referència

### Vista de Branch Protection Rules
```
Branch protection rules
────────────────────────────────────────
main                                [Edit] [Delete]
  ✓ Require pull request before merging
  ✗ Require status checks to pass
  ✓ Do not allow bypassing
```

### Missatge d'Error en Push Directe
```
$ git push origin main

remote: error: GH006: Protected branch update failed for refs/heads/main.
remote: error: Changes must be made through a pull request.
To https://github.com/vbronchales/Dst.VeteriLachApi-Repositori.git
 ! [remote rejected] main -> main (protected branch hook declined)
error: failed to push some refs to 'https://github.com/vbronchales/...'
```

---

## 📚 Recursos

- [GitHub Docs: About branch protection](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/about-protected-branches)
- [GitHub Docs: Managing branch protection rules](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/managing-a-branch-protection-rule)

---

## ✅ Checklist de Configuració

- [ ] Accedir a Settings → Branches
- [ ] Crear nova regla per `main`
- [ ] ✅ Marcar "Require pull request"
- [ ] Configurar approvals a 0
- [ ] ✅ Marcar "Dismiss stale approvals"
- [ ] ✅ Marcar "Do not allow bypass"
- [ ] ❌ Deixar "Allow force pushes" desmarcada
- [ ] ❌ Deixar "Allow deletions" desmarcada
- [ ] Guardar configuració
- [ ] Verificar amb test de push directe
- [ ] Verificar amb test de PR
- [ ] ✅ Documentar en BRANCHING-STRATEGY.md
