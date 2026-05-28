# Scripts de Deployment

Aquest repositori conté scripts PowerShell per instal·lar IIS i desplegar l'API.

## ⚠️ Prerequisites

### 1. IIS (Internet Information Services)

**Si IIS no està instal·lat**, executa primer:

```powershell
.\Install-IIS-Auto.ps1
```

Aquest script:
- S'auto-eleva a Administrador
- Instal·la IIS amb tots els components necessaris
- Et demanarà reiniciar el sistema

**Després del reinici:**

1. Instal·la **ASP.NET Core Hosting Bundle 10.0**:
   - Descarrega: https://dotnet.microsoft.com/download/dotnet/10.0
   - Cerca "Hosting Bundle" a la pàgina
   - Instal·la l'executable
   - Reinicia de nou (pot ser necessari)

2. Ara ja pots usar els scripts de deployment

### 2. .NET SDK

Per compilar el projecte necessites .NET SDK 10.0 (ja el tens instal·lat si `dotnet --version` funciona).

---

## 📜 Scripts de Deployment

### 1. `Deploy-Local-Auto.ps1` ⭐ **RECOMANAT**

**Ús més fàcil** - S'auto-eleva a Administrador automàticament.

```powershell
.\Deploy-Local-Auto.ps1
```

- ✅ Demana permisos d'Administrador automàticament
- ✅ Executa tot el deployment complet
- ✅ No cal obrir PowerShell com a Administrador manualment

**Això és el que hauries d'utilitzar normalment!**

---

### 2. `Deploy-Local.ps1`

Script principal de deployment complet.

```powershell
# REQUEREIX executar PowerShell com a Administrador PRIMER
.\Deploy-Local.ps1
```

**Què fa:**
- Build del projecte en mode Release
- Publicació a `publish\local`
- Creació/actualització d'Application Pool a IIS
- Creació/actualització de Site a IIS (port 41228 per defecte, autodetecta si ocupat)
- Configuració de permisos
- Test del Health endpoint

**Paràmetres:**
```powershell
# Força eliminació del deployment anterior
.\Deploy-Local.ps1 -Force
```

**Requereix:** PowerShell com a Administrador

---

### 3. `Build-And-Publish.ps1`

Només build i publicació, **sense configurar IIS**.

```powershell
# NO requereix Administrador
.\Build-And-Publish.ps1
```

**Què fa:**
- Build del projecte en mode Release
- Publicació a `publish\local`

**NO fa:**
- No configura IIS
- No requereix permisos d'Administrador

**Quan utilitzar-lo:**
- Només vols generar els fitxers
- Configuraràs IIS manualment
- Deployment a servidor remot (després copiaràs els fitxers)

---

## 🚀 Workflow Recomanat

### Després de fer merge d'una fase:

```powershell
# Opció A: Deployment automàtic complet (RECOMANAT)
.\Deploy-Local-Auto.ps1

# Opció B: Només generar fitxers (si no tens IIS o vols configurar manualment)
.\Build-And-Publish.ps1
```

### Verificar que funciona:

```powershell
# Provar Health endpoint
curl http://localhost:8080/api/health

# Obrir Swagger
start http://localhost:8080/swagger
```

---

## ⚙️ Configuració

Pots personalitzar la configuració editant les variables a `Deploy-Local.ps1`:

```powershell
# Configuració del Site IIS
$siteName = "VeteriLachReadAPI"
$appPoolName = "VeteriLachReadAPI-AppPool"
$sitePath = "c:\inetpub\wwwroot\VeteriLachReadAPI"
$port = 8080
$protocol = "http"
```

---

## ❌ Solució de Problemes

### Error: "no se puede ejecutar porque contiene una instrucción #requires"

**Problema:** Intentes executar `Deploy-Local.ps1` sense permisos d'Administrador.

**Solució:**
```powershell
# En lloc de Deploy-Local.ps1, usa:
.\Deploy-Local-Auto.ps1
```

### Error: IIS no instal·lat

**Solució:** Instal·la IIS primer (consulta [DEPLOYMENT.md](DEPLOYMENT.md))

```powershell
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole
```

### Error: .NET SDK no trobat

**Solució:** Instal·la .NET SDK 10.0 o superior:
https://dotnet.microsoft.com/download/dotnet/10.0

---

## 📚 Més Informació

Per deployment remot, configuració de producció, HTTPS, i troubleshooting detallat:

👉 **[DEPLOYMENT.md](DEPLOYMENT.md)**
