# 🐛 Problema: SalesController No Càrregat

**Data**: 2026-05-29  
**Estat**: ⚠️ En investigació

---

## 🔍 Resum del Problema

El `SalesController.cs` existeix al codi font de l'API però **no s'està carregant/registrant** quan l'aplicació s'inicia.

### Evidència

**✅ Codi Font Complet i Correcte:**
- `src/VeteriLach.ReadApi/Controllers/SalesController.cs` - Existeix
- `Application/Sales/Handlers/GetSalesQueryHandler.cs` - Existeix
- `Application/Sales/Queries/GetSalesQuery.cs` - Existeix
- `Application/Sales/Mappings/SalesMappingProfile.cs` - Existeix
- `Application/Sales/DTOs/SaleDto.cs` - Existeix

**✅ Compilació Exitosa:**
- Build Release: ✅ Sense errors
- AutoMapper Profile: ✅ Correcte
- MediatR Handlers: ✅ Registrats

**❌ Endpoint No Disponible:**
```powershell
# Test manual
Invoke-WebRequest -Uri "http://localhost:41229/api/sales?pageSize=2" `
  -Headers @{"X-Api-Key"="mcp-server-key-789"}

# Resultat: 404 Not Found
```

**❌ No Apareix al Swagger:**
```
/api/health ✅
/api/Animals ✅
/api/Animals/{id} ✅
/api/animals/{idAnimal}/visits ✅
/api/visits/{id} ✅
/api/medicines/veterinary/search ✅
/api/medicines/human/search ✅
/api/Propietaris ✅
/api/Propietaris/{id} ✅
/api/sales ❌ ← NO EXISTEIX
```

---

## 🕵️ Possible Causes

### 1. Error en Temps d'Execució (No Visible)
- Pot ser que el controller llanci una excepció durant la inicialització que és silenciada
- Els handlers poden tenir un problema de dependències no resolt
- AutoMapper pot fallar en temps de càrrega (encara que compila)

### 2. Taula de BD No Existeix
- El handler fa servir `_context.FacVenda`
- Si aquesta taula no existeix a la BD, el DbContext pot fallar silenciosament
- **Acció**: Verificar si `FacVenda` existeix a `SelachMascotaBd`

```sql
SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME = 'FacVenda'
```

### 3. Controller No Inclòs al Build
- Pot estar exclòs del `.csproj`
- **Acció**: Revisar `VeteriLach.ReadApi.csproj`

### 4. Problema de Routing
- El controller usa `[Route("api/[controller]")]`
- Per algun motiu, ASP.NET Core no el detecta
- **Acció**: Provar amb ruta explícita `[Route("api/sales")]`

### 5. Dependència Circular o Faltant
- Els handlers poden tenir dependències no registrades
- **Acció**: Revisar logs d'inici de l'aplicació

---

## 🔧 Accions Immediates Preses

### ✅ Solució Temporal: Deshabilitar Tools MCP

**Tools Eliminades del MCP Server:**
- `get_sales` ❌
- `get_sale_detail` ❌
- `get_customer_sales` ❌
- `get_debts` ❌
- `get_payment_advances` ❌

**Motiu:** Aquestes tools fan referència a endpoints que no existeixen (`/api/sales`, `/api/sales/{id}`, etc.)

**Tools Funcionals (Mantenudes):**
- ✅ `get_animals`
- ✅ `get_animal_detail`
- ✅ `get_animal_visits`
- ✅ `get_visit_detail`
- ✅ `get_propietaris`
- ✅ `get_propietari_detail`
- ✅ `search_veterinary_medicines`
- ✅ `get_veterinary_medicine`
- ✅ `search_human_medicines`
- ✅ `get_human_medicine`

---

## 📋 Pla d'Investigació

### Fase 1: Verificar Base de Dades (15 min)

```sql
-- Comprovar si les taules de facturació existeixen
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE 'Fac%'

-- Comprovar dades a FacVenda (si existeix)
SELECT COUNT(*) FROM FacVenda

-- Comprovar estructura
EXEC sp_columns 'FacVenda'
```

### Fase 2: Revisar Logs d'Inici (10 min)

```powershell
# Logs de l'API
Get-Content "C:\inetpub\wwwroot\VeteriLachApi\logs\veterilach-api-*.txt" `
  | Select-String "SalesController|FacVenda|Error|Exception" `
  | Select-Object -Last 50
```

### Fase 3: Debugging Controlat (30 min)

1. Afegir logging explícit al `SalesController` constructor:
```csharp
public SalesController(IMediator mediator, ILogger<SalesController> logger)
{
    _logger = logger;
    _logger.LogCritical("🔥 SalesController CONSTRUCTOR CALLED!");
    _mediator = mediator;
}
```

2. Rebuild i restart IIS

3. Verificar si apareix el log

### Fase 4: Test Directe del Handler (20 min)

```csharp
// Crear test unitari per al handler
[Fact]
public async Task GetSalesQueryHandler_ShouldReturnSales()
{
    // Arrange
    var context = CreateTestDbContext();
    var mapper = CreateTestMapper();
    var logger = CreateTestLogger();
    
    var handler = new GetSalesQueryHandler(context, mapper, logger);
    var query = new GetSalesQuery { PageSize = 10, PageNumber = 1 };
    
    // Act
    var result = await handler.Handle(query, CancellationToken.None);
    
    // Assert
    Assert.NotNull(result);
}
```

---

## 🎯 Objectius

### Curt Termini (Avui)
- ✅ Desactivar tools MCP que no funcionen
- ✅ Documentar problema
- ⏳ Verificar si taula `FacVenda` existeix

### Mitjà Termini (Aquesta Setmana)
- ⏳ Investigar per què SalesController no es carrega
- ⏳ Activar SalesController
- ⏳ Reactivar tools MCP de Sales

### Llarg Termini (Fase 2)
- Implementar endpoints addicionals segons `ESTRATEGIA_CASOS_US_CLAUDE.md`
- Afegir `get_recent_visits`, `get_especies`, etc.

---

## 📞 Contacte

Si necessites assistència:
1. Revisar logs a: `C:\inetpub\wwwroot\VeteriLachApi\logs\`
2. Comprovar Swagger: `http://localhost:41229/swagger`
3. Executar tests manuals amb PowerShell (veure secció "Test Endpoints" a STATUS_FINAL.md)

---

**Actualitzat**: 2026-05-29 20:45  
**Per**: GitHub Copilot
