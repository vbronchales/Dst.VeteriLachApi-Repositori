# ✅ CHECKLIST: Configurar Branch Protection

Aquesta checklist t'ajudarà a completar la configuració del workflow de branching pas a pas.

---

## 📋 Estat Actual

- ✅ **Branca creada**: `docs/branching-workflow-setup`
- ✅ **Documentació creada**:
  - ✅ BRANCHING-STRATEGY.md
  - ✅ .github/pull_request_template.md
  - ✅ .github/BRANCH-PROTECTION-SETUP.md
  - ✅ README.md actualitzat
- ✅ **Commit fet**: "Docs: Afegida estratègia de branching amb PRs i protecció de main"
- ✅ **Branca pujada**: https://github.com/vbronchales/Dst.VeteriLachApi-Repositori/tree/docs/branching-workflow-setup
- ✅ **Pull Request creada**: **PR #1** - https://github.com/vbronchales/Dst.VeteriLachApi-Repositori/pull/1

---

## 🚀 PROPERS PASSOS

### ✅ PAS 1: Revisar i Fer Merge de la PR #1

1. **Obrir la Pull Request**:
   ```
   https://github.com/vbronchales/Dst.VeteriLachApi-Repositori/pull/1
   ```

2. **Revisar els canvis**:
   - Consultar els fitxers modificats (4 fitxers)
   - Verificar que tot estigui correcte

3. **Fer Merge de la PR**:
   - Clica el botó verd **"Merge pull request"**
   - Confirma el merge
   - **Opcional**: Marca "Delete branch" per esborrar `docs/branching-workflow-setup` després del merge

4. **Actualitzar `main` localment**:
   ```bash
   cd c:\Dst2026\Dst.VeteriLachApi-Repositori
   git checkout main
   git pull origin main
   ```

5. **Esborrar branca local** (si no s'ha esborrat automàticament):
   ```bash
   git branch -d docs/branching-workflow-setup
   ```

---

### 🔒 PAS 2: Configurar Protecció de la Branca `main`

**⚠️ IMPORTANT**: Això impedirà pushes directes a `main`. Tots els canvis hauran de passar per PR.

#### A) Accedir a la Configuració

1. Anar a:
   ```
   https://github.com/vbronchales/Dst.VeteriLachApi-Repositori/settings/branches
   ```
   
2. Clica **"Add branch protection rule"**

#### B) Configurar la Regla

**Branch name pattern**:
```
main
```

**Marcar les següents opcions**:

- [x] **Require a pull request before merging**
  - Require approvals: **0** (per ara, un sol desenvolupador)
  - [x] Dismiss stale pull request approvals when new commits are pushed
  - [x] Require approval of the most recent reviewable push

- [ ] **Require status checks to pass before merging** (NO marcar de moment)
  - _Activarem això quan tinguem CI/CD_

- [x] **Do not allow bypassing the above settings**
  - Ni administradors poden saltar-se les regles

- [ ] **Allow force pushes** (NO marcar - prohibir force push)

- [ ] **Allow deletions** (NO marcar - prohibir esborrar main)

#### C) Guardar

- Clica **"Create"** al final de la pàgina
- Veuràs un missatge de confirmació verd ✅

---

### ✅ PAS 3: Verificar que la Protecció Funciona

#### Test 1: Intentar Push Directe (Ha de FALLAR ❌)

```bash
cd c:\Dst2026\Dst.VeteriLachApi-Repositori
git checkout main
git pull origin main

# Intentar fer un canvi directe
echo "# Test" >> test.txt
git add test.txt
git commit -m "Test push directe a main"
git push origin main
```

**Resultat esperat**:
```
remote: error: GH006: Protected branch update failed for refs/heads/main.
remote: error: Changes must be made through a pull request.
```

✅ **Si veus aquest error, la protecció està FUNCIONANT correctament!**

**Desfer el canvi**:
```bash
git reset HEAD~1
git checkout -- test.txt
```

#### Test 2: Via Pull Request (Ha de FUNCIONAR ✅)

```bash
# Crear branca de test
git checkout -b test/branch-protection-verification

# Fer canvi
echo "# Branch Protection Test" > BRANCH-PROTECTION-TEST.md
git add BRANCH-PROTECTION-TEST.md
git commit -m "Test: Verificació de branch protection"

# Pujar branca
git push -u origin test/branch-protection-verification
```

**Crear PR a GitHub**:
1. Anar a https://github.com/vbronchales/Dst.VeteriLachApi-Repositori/pulls
2. Clica **"New pull request"**
3. Base: `main`, Compare: `test/branch-protection-verification`
4. Títol: "Test: Verificació de Branch Protection"
5. Descripció: "Verificant que la protecció de main funciona correctament"
6. **Create Pull Request**
7. **Merge pull request** (ha de funcionar sense problemes)

**Neteja després del test**:
```bash
git checkout main
git pull origin main
git branch -d test/branch-protection-verification
git push origin --delete test/branch-protection-verification
rm BRANCH-PROTECTION-TEST.md  # si cal
```

✅ **Si pots fer merge via PR però NO via push directe, tot està correcte!**

---

## 📊 Verificació Final

Després de completar tots els passos:

- [ ] ✅ PR #1 està merged a `main`
- [ ] ✅ Branca `docs/branching-workflow-setup` esborrada (local i remota)
- [ ] ✅ `main` local actualitzat amb els nous fitxers
- [ ] ✅ Branch protection configurada per a `main`
- [ ] ✅ Test de push directe FALLA (això és bo! ✅)
- [ ] ✅ Test de PR funciona correctament
- [ ] ✅ Llegit BRANCHING-STRATEGY.md completament
- [ ] ✅ Entès el workflow de feature branching

---

## 🎯 A partir d'ara...

### ✅ Tots els desenvolupaments futurs seguiran aquest workflow:

1. **Crear branca** per a cada feature/fase
   ```bash
   git checkout main
   git pull origin main
   git checkout -b feature/fase-8-propietaris-api
   ```

2. **Desenvolupar i fer commits**
   ```bash
   # ... desenvolupar ...
   git add .
   git commit -m "Missatge descriptiu"
   ```

3. **Pujar branca i crear PR**
   ```bash
   git push -u origin feature/fase-8-propietaris-api
   # Crear PR a GitHub
   ```

4. **Merge i neteja**
   ```bash
   # Després del merge a GitHub
   git checkout main
   git pull origin main
   git branch -d feature/fase-8-propietaris-api
   ```

---

## 📚 Recursos

- **Estratègia Completa**: [BRANCHING-STRATEGY.md](../BRANCHING-STRATEGY.md)
- **Guia de Branch Protection**: [BRANCH-PROTECTION-SETUP.md](BRANCH-PROTECTION-SETUP.md)
- **Template PR**: [pull_request_template.md](pull_request_template.md)
- **PR #1 (exemple)**: https://github.com/vbronchales/Dst.VeteriLachApi-Repositori/pull/1

---

## 🆘 Ajuda

Si tens problemes:
1. Consulta [BRANCH-PROTECTION-SETUP.md](BRANCH-PROTECTION-SETUP.md) - Secció Troubleshooting
2. Revisa [BRANCHING-STRATEGY.md](../BRANCHING-STRATEGY.md) - Exemples pràctics

---

**Última actualització**: 2026-05-28  
**Estat**: Workflow implementat i documentat ✅  
**Propera acció**: Configurar branch protection a GitHub 🔒
