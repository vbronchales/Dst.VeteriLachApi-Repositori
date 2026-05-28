using System.Text.RegularExpressions;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Services;

/// <summary>
/// Servei per parsejar i estructurar el text de les visites clíniques
/// </summary>
public class TextVisitaParserService
{
    /// <summary>
    /// Parseja un text de visita i intenta extreure'n les seccions estructurades
    /// </summary>
    public SeccioTextVisitaDto ParsejarText(string textPla)
    {
        if (string.IsNullOrWhiteSpace(textPla))
        {
            return new SeccioTextVisitaDto();
        }

        var seccions = new SeccioTextVisitaDto();
        var textMinuscules = textPla.ToLowerInvariant();

        // Paraules clau per cada secció (català i castellà, amb variants)
        var paraulesMotiu = new[] { "motiu", "motivo", "consulta", "acude", "presenta", "ve per", "viene por" };
        var paraulesExploracio = new[] { "exploració", "exploracion", "exploració física", "exploracion fisica", 
            "examen", "examen físic", "palpació", "palpacion", "auscultació", "auscultacion" };
        var paraulesDiagnostic = new[] { "diagnòstic", "diagnostico", "diagnosi", "diagnosis", "impressió", 
            "impresión", "conclusió", "conclusion", "sospita", "sospecha" };
        var paraulesTractament = new[] { "tractament", "tratamiento", "pauta", "prescripció", "prescripcion", 
            "medicació", "medicacion", "recomanacions", "recomendaciones" };

        // Intentar detectar seccions per paraules clau
        var linees = textPla.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var seccioActual = "";
        var contingutActual = new List<string>();

        foreach (var linia in linees)
        {
            var liniaMinuscules = linia.Trim().ToLowerInvariant();
            var liniaOriginal = linia.Trim();

            // Detectar si la línia és un encapçalament de secció
            if (ConteParaulaClau(liniaMinuscules, paraulesMotiu))
            {
                AssignarSeccio(seccions, seccioActual, contingutActual);
                seccioActual = "Motiu";
                contingutActual = new List<string>();
                
                // Si la línia conté més que la paraula clau, afegir-la
                var contingut = ExtreureDespresParaulaClau(liniaOriginal, paraulesMotiu);
                if (!string.IsNullOrWhiteSpace(contingut))
                {
                    contingutActual.Add(contingut);
                }
            }
            else if (ConteParaulaClau(liniaMinuscules, paraulesExploracio))
            {
                AssignarSeccio(seccions, seccioActual, contingutActual);
                seccioActual = "Exploracio";
                contingutActual = new List<string>();
                
                var contingut = ExtreureDespresParaulaClau(liniaOriginal, paraulesExploracio);
                if (!string.IsNullOrWhiteSpace(contingut))
                {
                    contingutActual.Add(contingut);
                }
            }
            else if (ConteParaulaClau(liniaMinuscules, paraulesDiagnostic))
            {
                AssignarSeccio(seccions, seccioActual, contingutActual);
                seccioActual = "Diagnostic";
                contingutActual = new List<string>();
                
                var contingut = ExtreureDespresParaulaClau(liniaOriginal, paraulesDiagnostic);
                if (!string.IsNullOrWhiteSpace(contingut))
                {
                    contingutActual.Add(contingut);
                }
            }
            else if (ConteParaulaClau(liniaMinuscules, paraulesTractament))
            {
                AssignarSeccio(seccions, seccioActual, contingutActual);
                seccioActual = "Tractament";
                contingutActual = new List<string>();
                
                var contingut = ExtreureDespresParaulaClau(liniaOriginal, paraulesTractament);
                if (!string.IsNullOrWhiteSpace(contingut))
                {
                    contingutActual.Add(contingut);
                }
            }
            else if (!string.IsNullOrWhiteSpace(liniaOriginal))
            {
                // Afegir la línia a la secció actual
                contingutActual.Add(liniaOriginal);
            }
        }

        // Assignar l'última secció
        AssignarSeccio(seccions, seccioActual, contingutActual);

        // Si no s'han detectat seccions, intentar una estratègia més simple
        if (string.IsNullOrWhiteSpace(seccions.Motiu) && 
            string.IsNullOrWhiteSpace(seccions.Exploracio) && 
            string.IsNullOrWhiteSpace(seccions.Diagnostic) && 
            string.IsNullOrWhiteSpace(seccions.Tractament))
        {
            // Tot el text es considera observacions
            seccions.Observacions = textPla.Trim();
        }

        return seccions;
    }

    /// <summary>
    /// Combina múltiples textos i genera un resum estructurat
    /// </summary>
    public string GenerarResum(IEnumerable<SeccioTextVisitaDto> seccions)
    {
        var resumParts = new List<string>();

        // Combinar totes les seccions de tipus Motiu
        var motius = seccions
            .Where(s => !string.IsNullOrWhiteSpace(s.Motiu))
            .Select(s => s.Motiu)
            .ToList();
        
        if (motius.Any())
        {
            resumParts.Add($"Motiu: {string.Join("; ", motius)}");
        }

        // Combinar exploracions
        var exploracions = seccions
            .Where(s => !string.IsNullOrWhiteSpace(s.Exploracio))
            .Select(s => s.Exploracio)
            .ToList();
        
        if (exploracions.Any())
        {
            resumParts.Add($"Exploració: {string.Join("; ", exploracions)}");
        }

        // Combinar diagnòstics
        var diagnostics = seccions
            .Where(s => !string.IsNullOrWhiteSpace(s.Diagnostic))
            .Select(s => s.Diagnostic)
            .ToList();
        
        if (diagnostics.Any())
        {
            resumParts.Add($"Diagnòstic: {string.Join("; ", diagnostics)}");
        }

        // Combinar tractaments
        var tractaments = seccions
            .Where(s => !string.IsNullOrWhiteSpace(s.Tractament))
            .Select(s => s.Tractament)
            .ToList();
        
        if (tractaments.Any())
        {
            resumParts.Add($"Tractament: {string.Join("; ", tractaments)}");
        }

        // Si hi ha observacions generals
        var observacions = seccions
            .Where(s => !string.IsNullOrWhiteSpace(s.Observacions))
            .Select(s => s.Observacions)
            .ToList();
        
        if (observacions.Any() && !resumParts.Any())
        {
            // Si només hi ha observacions, retornar-les sense etiqueta
            return string.Join("; ", observacions);
        }
        else if (observacions.Any())
        {
            resumParts.Add($"Observacions: {string.Join("; ", observacions)}");
        }

        return resumParts.Any() 
            ? string.Join(" | ", resumParts) 
            : "Sense informació";
    }

    private bool ConteParaulaClau(string text, string[] paraulesClau)
    {
        foreach (var paraula in paraulesClau)
        {
            if (text.Contains(paraula))
            {
                return true;
            }
        }
        return false;
    }

    private string ExtreureDespresParaulaClau(string text, string[] paraulesClau)
    {
        foreach (var paraula in paraulesClau)
        {
            var index = text.IndexOf(paraula, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                // Extreure tot el text després de la paraula clau, saltant ':' si n'hi ha
                var despres = text.Substring(index + paraula.Length).TrimStart(':', ' ');
                return despres;
            }
        }
        return text;
    }

    private void AssignarSeccio(SeccioTextVisitaDto seccions, string nomSeccio, List<string> contingut)
    {
        if (contingut.Count == 0) return;

        var text = string.Join(" ", contingut).Trim();
        if (string.IsNullOrWhiteSpace(text)) return;

        switch (nomSeccio)
        {
            case "Motiu":
                seccions.Motiu = CombinarTexts(seccions.Motiu, text);
                break;
            case "Exploracio":
                seccions.Exploracio = CombinarTexts(seccions.Exploracio, text);
                break;
            case "Diagnostic":
                seccions.Diagnostic = CombinarTexts(seccions.Diagnostic, text);
                break;
            case "Tractament":
                seccions.Tractament = CombinarTexts(seccions.Tractament, text);
                break;
            default:
                seccions.Observacions = CombinarTexts(seccions.Observacions, text);
                break;
        }
    }

    private string CombinarTexts(string? textExistent, string textNou)
    {
        if (string.IsNullOrWhiteSpace(textExistent))
        {
            return textNou;
        }
        return textExistent + "; " + textNou;
    }
}
