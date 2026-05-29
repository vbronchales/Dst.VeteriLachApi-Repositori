using System.Text.Json.Serialization;

namespace VeteriLach.ReadApi.Infrastructure.ExternalServices.Models;

/// <summary>
/// Models per parsejar les respostes de l'API REST de CIMA
/// Aquests models són interns i s'utilitzen només per deserialització
/// </summary>

/// <summary>
/// Resposta de cerca de medicaments
/// </summary>
public class CimaSearchResponse
{
    [JsonPropertyName("totalFilas")]
    public int TotalRows { get; set; }

    [JsonPropertyName("pagina")]
    public int Page { get; set; }

    [JsonPropertyName("tamanioPagina")]
    public int PageSize { get; set; }

    [JsonPropertyName("resultados")]
    public List<CimaMedicineSearchResult> Results { get; set; } = new();
}

/// <summary>
/// Resultat de cerca resumit
/// </summary>
public class CimaMedicineSearchResult
{
    [JsonPropertyName("nregistro")]
    public string RegistrationNumber { get; set; } = string.Empty;

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("labtitular")]
    public string Laboratory { get; set; } = string.Empty;

    [JsonPropertyName("cpresc")]
    public string PrescriptionType { get; set; } = string.Empty;

    [JsonPropertyName("comerc")]
    public bool IsCommercialized { get; set; }

    [JsonPropertyName("receta")]
    public bool RequiresPrescription { get; set; }

    [JsonPropertyName("generico")]
    public bool IsGeneric { get; set; }

    [JsonPropertyName("vtm")]
    public VirtualTherapeuticMoiety? Vtm { get; set; }

    [JsonPropertyName("dosis")]
    public string? Dose { get; set; }

    [JsonPropertyName("formaFarmaceutica")]
    public PharmaceuticalFormInfo? PharmaceuticalForm { get; set; }

    [JsonPropertyName("viasAdministracion")]
    public List<AdministrationRouteInfo> AdministrationRoutes { get; set; } = new();

    [JsonPropertyName("docs")]
    public List<DocumentInfo> Documents { get; set; } = new();
}

/// <summary>
/// Detall complet de medicament
/// </summary>
public class CimaMedicineDetail
{
    [JsonPropertyName("nregistro")]
    public string RegistrationNumber { get; set; } = string.Empty;

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("pactivos")]
    public string ActiveIngredients { get; set; } = string.Empty;

    [JsonPropertyName("labtitular")]
    public string Laboratory { get; set; } = string.Empty;

    [JsonPropertyName("labcomercializador")]
    public string? MarketingLaboratory { get; set; }

    [JsonPropertyName("cpresc")]
    public string PrescriptionType { get; set; } = string.Empty;

    [JsonPropertyName("estado")]
    public AuthorizationState? State { get; set; }

    [JsonPropertyName("comerc")]
    public bool IsCommercialized { get; set; }

    [JsonPropertyName("receta")]
    public bool RequiresPrescription { get; set; }

    [JsonPropertyName("generico")]
    public bool IsGeneric { get; set; }

    [JsonPropertyName("triangulo")]
    public bool HasTriangle { get; set; }

    [JsonPropertyName("huerfano")]
    public bool IsOrphan { get; set; }

    [JsonPropertyName("biosimilar")]
    public bool IsBiosimilar { get; set; }

    [JsonPropertyName("conduc")]
    public bool AffectsDriving { get; set; }

    [JsonPropertyName("vtm")]
    public VirtualTherapeuticMoiety? Vtm { get; set; }

    [JsonPropertyName("dosis")]
    public string? Dose { get; set; }

    [JsonPropertyName("formaFarmaceutica")]
    public PharmaceuticalFormInfo? PharmaceuticalForm { get; set; }

    [JsonPropertyName("viasAdministracion")]
    public List<AdministrationRouteInfo> AdministrationRoutes { get; set; } = new();

    [JsonPropertyName("atcs")]
    public List<AtcInfo> Atcs { get; set; } = new();

    [JsonPropertyName("principiosActivos")]
    public List<ActivePrincipleInfo> ActivePrinciples { get; set; } = new();

    [JsonPropertyName("excipientes")]
    public List<ExcipientInfo> Excipients { get; set; } = new();

    [JsonPropertyName("presentaciones")]
    public List<PresentationInfo> Presentations { get; set; } = new();

    [JsonPropertyName("docs")]
    public List<DocumentInfo> Documents { get; set; } = new();
}

public class VirtualTherapeuticMoiety
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;
}

public class PharmaceuticalFormInfo
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;
}

public class AdministrationRouteInfo
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;
}

public class DocumentInfo
{
    [JsonPropertyName("tipo")]
    public int Type { get; set; } // 1: Ficha Técnica, 2: Prospecto

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("urlHtml")]
    public string? UrlHtml { get; set; }

    [JsonPropertyName("fecha")]
    public long? DateTimestamp { get; set; }
}

public class AuthorizationState
{
    [JsonPropertyName("aut")]
    public long? AuthorizationTimestamp { get; set; }

    [JsonPropertyName("rev")]
    public long? RevisionTimestamp { get; set; }
}

public class AtcInfo
{
    [JsonPropertyName("codigo")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("nivel")]
    public int Level { get; set; }
}

public class ActivePrincipleInfo
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("codigo")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("cantidad")]
    public string Quantity { get; set; } = string.Empty;

    [JsonPropertyName("unidad")]
    public string Unit { get; set; } = string.Empty;

    [JsonPropertyName("orden")]
    public int Order { get; set; }
}

public class ExcipientInfo
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("cantidad")]
    public string? Quantity { get; set; }

    [JsonPropertyName("unidad")]
    public string? Unit { get; set; }

    [JsonPropertyName("orden")]
    public int Order { get; set; }
}

public class PresentationInfo
{
    [JsonPropertyName("cn")]
    public string NationalCode { get; set; } = string.Empty;

    [JsonPropertyName("nombre")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("estado")]
    public AuthorizationState? State { get; set; }

    [JsonPropertyName("comerc")]
    public bool IsCommercialized { get; set; }

    [JsonPropertyName("psum")]
    public bool? Psum { get; set; }
}
