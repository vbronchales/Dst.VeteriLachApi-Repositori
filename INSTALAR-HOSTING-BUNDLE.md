# Instal·lació del .NET Hosting Bundle per a IIS

## Problema
L'API de VeteriLach funciona correctament quan s'executa directament, però IIS retorna error 500.19 perquè falta el **ASP.NET Core Hosting Bundle**.

## Solució

### 1. Descarregar el .NET 10 Hosting Bundle

Obre el navegador i ves a:
```
https://dotnet.microsoft.com/download/dotnet/10.0
```

A la secció **Run apps - Runtime**, descarrega:
- **Hosting Bundle** (és l'opció que diu "Hosting Bundle" o "ASP.NET Core Runtime - Windows Hosting Bundle")

### 2. Instal·lar

1. Executa el fitxer descarregat (dotnet-hosting-10.x.x-win.exe)
2. Segueix l'assistent d'instal·lació
3. **IMPORTANT:** Després de la instal·lació, has de **reiniciar IIS**:

```powershell
# Executar com a Administrador
iisreset
```

O bé, si iisreset no funciona:
```powershell
# Executar com a Administrador
net stop was /y
net start w3svc
```

### 3. Verificar la instal·lació

Després de reiniciar IIS, comprova que el mòdul està instal·lat:

```powershell
Get-ChildItem "C:\Program Files\IIS\Asp.Net Core Module"
```

Hauries de veure fitxers com `aspnetcorev2.dll`.

### 4. Provar l'API

Després de la instal·lació i reinici d'IIS, l'API hauria de funcionar.

**Port actual del site:** http://localhost:8081/api/health (site creat avui)

**Port per defecte futurs deployments:** http://localhost:41228/api/health

## Alternativa: Executar sense IIS (temporal)

Si vols provar l'API ara mateix sense instal·lar el Hosting Bundle, pots executar-la directament:

```powershell
cd C:\Dst2026\Dst.VeteriLachApi-Repositori\publish\local
$env:ASPNETCORE_URLS="http://localhost:5000"
dotnet VeteriLach.ReadApi.dll
```

Això executarà l'API al port 5000 directament amb Kestrel (sense IIS).

## Per a futures fases

Després d'instal·lar el Hosting Bundle UNA VEGADA, ja no caldrà fer-ho més. Tots els futurs deployments amb `.\Run-Deploy.ps1` funcionaran correctament.

---

**Nota**: El Hosting Bundle és un component del servidor, no de l'aplicació. S'instal·la una sola vegada al servidor i serveix per a totes les aplicacions ASP.NET Core.
