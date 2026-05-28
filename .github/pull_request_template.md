## 📋 Descripció

<!-- Descriu breument què fa aquesta PR -->

## ✅ Canvis Realitzats

<!-- Marca amb [x] el que s'ha fet -->

- [ ] Controller/Endpoints nous
- [ ] DTOs nous o modificats
- [ ] Queries/Handlers MediatR
- [ ] Validators FluentValidation
- [ ] Mapatges AutoMapper
- [ ] Middleware/Filters
- [ ] Tests (si aplica)
- [ ] README.md actualitzat
- [ ] Col·lecció Postman actualitzada
- [ ] Documentació (comentaris, XMLDoc)

## 🔧 Tipus de Canvi

<!-- Marca amb [x] el tipus de canvi -->

- [ ] 🎯 Feature (nova funcionalitat)
- [ ] 🐛 Fix (correcció de bug)
- [ ] ♻️ Refactor (millora de codi sense canviar funcionalitat)
- [ ] 📝 Docs (només documentació)
- [ ] 🔧 Chore (manteniment, dependencies, etc.)

## 🧪 Com Provar

<!-- Instruccions per provar els canvis -->

```bash
# 1. Executar API
cd src/VeteriLach.ReadApi
dotnet run

# 2. Provar endpoint amb Postman o curl
# Exemple:
curl -X GET "http://localhost:5072/api/..." \
  -H "X-API-Key: swagger-test-key-123"
```

## 📸 Evidències (opcional)

<!-- Screenshots, logs, resultats de tests, etc. -->

## 🔗 Relacionat

<!-- Referències a issues, PRD, fases, etc. -->

- Fase: 
- PRD: PRD-001, Secció 
- Issue: #

## ✔️ Checklist Pre-Merge

<!-- Verificar abans de fer merge -->

- [ ] ✅ Codi compila: `dotnet build`
- [ ] ✅ API s'executa: `dotnet run`
- [ ] ✅ Endpoints funcionen correctament
- [ ] ✅ Validacions retornen errors adequats
- [ ] ✅ No hi ha secrets al codi (appsettings.Local.json NO pujat)
- [ ] ✅ Commits tenen missatges descriptius
- [ ] ✅ Branca actualitzada amb `main` si cal

## 💬 Comentaris Addicionals

<!-- Qualsevol informació rellevant addicional -->
