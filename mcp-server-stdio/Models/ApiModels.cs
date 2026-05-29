using System.Text.Json.Serialization;

namespace VeteriLach.McpServer.Models;

public class SaleDto
{
    public required string SaleId { get; set; }
    public required string CustomerId { get; set; }
    public required string CustomerName { get; set; }
    public string? SellerId { get; set; }
    public string? SellerName { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal PendingAmount { get; set; }
    public bool IsFullyPaid { get; set; }
    public string? AnimalId { get; set; }
    public string? AnimalName { get; set; }
    public int ItemCount { get; set; }
}

public class SaleDetailDto : SaleDto
{
    public string? CustomerNif { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerEmail { get; set; }
    public string? BankAccount { get; set; }
    public string? AnimalSpecies { get; set; }
    public List<SaleItemDto> Items { get; set; } = new();
}

public class SaleItemDto
{
    public required string ArticleId { get; set; }
    public required string ArticleName { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VatAmount { get; set; }
    public decimal VatRate { get; set; }
    public decimal Discount { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public decimal NetCost { get; set; }
    public decimal Margin { get; set; }
    public decimal MarginPercentage { get; set; }
}

public class DebtDto : SaleDto
{
    public int DaysPending { get; set; }
}

public class PaymentAdvanceDto
{
    public required string PaymentAdvanceId { get; set; }
    public required string CustomerId { get; set; }
    public required string CustomerName { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Reference { get; set; }
}

public class PaginatedResponse<T>
{
    [JsonPropertyName("data")]
    public List<T> Items { get; set; } = new();
    
    [JsonPropertyName("pagination")]
    public PaginationInfo? Pagination { get; set; }
    
    // Properties for backward compatibility and easy access
    public int PageNumber => Pagination?.CurrentPage ?? 0;
    public int PageSize => Pagination?.PageSize ?? 0;
    public int TotalCount => Pagination?.TotalItems ?? 0;
    public int TotalPages => Pagination?.TotalPages ?? 0;
    public bool HasPreviousPage => Pagination?.HasPreviousPage ?? false;
    public bool HasNextPage => Pagination?.HasNextPage ?? false;
}

public class PaginationInfo
{
    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }
    
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
    
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }
    
    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
    
    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage { get; set; }
    
    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage { get; set; }
}

// ===== Propietaris (Clients) =====

public class PropietariListDto
{
    public Guid IdPropietari { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Cognoms { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefon { get; set; }
    public string? Poblacio { get; set; }
    public string? CodiPostal { get; set; }
    public int TotalAnimals { get; set; }
    public bool Actiu { get; set; }
}

public class PropietariDetailDto
{
    public Guid IdPropietari { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Cognom1 { get; set; }
    public string? Cognom2 { get; set; }
    public string Cognoms { get; set; } = string.Empty;
    public string? Nif { get; set; }
    public DateTime? DataNaixement { get; set; }
    public string? Email { get; set; }
    public bool AmbWhatsApp { get; set; }
    public string? Adresa { get; set; }
    public string? CodiPostal { get; set; }
    public string? Poblacio { get; set; }
    public string? Provincia { get; set; }
    public string? Pais { get; set; }
    public List<TelefonDto> Telefons { get; set; } = new();
    public List<AnimalResumatDto> Animals { get; set; } = new();
    public bool Actiu { get; set; }
    public string? Observacions { get; set; }
}

public class TelefonDto
{
    public string Numero { get; set; } = string.Empty;
    public int TipusTelefon { get; set; }
    public string TipusTelefonDescripcio { get; set; } = string.Empty;
    public int Ordre { get; set; }
    public string? Observacions { get; set; }
}

public class AnimalResumatDto
{
    public Guid IdAnimal { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string? Rasa { get; set; }
    public string? Sexe { get; set; }
    public DateTime? DataNaixement { get; set; }
    public string? NumXip { get; set; }
    public bool Castrat { get; set; }
}

// ===== Animals (Mascotes) =====

public class AnimalListDto
{
    public Guid IdAnimal { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int? Sexe { get; set; }
    public DateTime? DataNaixement { get; set; }
    public string Especie { get; set; } = string.Empty;
    public string Rasa { get; set; } = string.Empty;
    public string? Color { get; set; }
    public string? NumXip { get; set; }
    public bool Castrat { get; set; }
}

public class AnimalDetailDto
{
    public Guid IdAnimal { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int? Sexe { get; set; }
    public DateTime? DataNaixement { get; set; }
    public string Especie { get; set; } = string.Empty;
    public string Rasa { get; set; } = string.Empty;
    public string? Color { get; set; }
    public string? NumXip { get; set; }
    public bool Castrat { get; set; }
    public string? Capa { get; set; }
    public string? Tatuatge { get; set; }
    public string? Caracter { get; set; }
    public PropietariDto? Propietari { get; set; }
}

public class PropietariDto
{
    public Guid IdPropietari { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Cognoms { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefon { get; set; }
    public string? Adresa { get; set; }
    public string? CodiPostal { get; set; }
    public string? Poblacio { get; set; }
}

// ===== Medical History (Visites) =====

public class VisitaResumatDto
{
    public Guid IdVisita { get; set; }
    public DateTime DiaVisita { get; set; }
    public string Veterinari { get; set; } = string.Empty;
    public string? Resum { get; set; }
    public decimal? Pes { get; set; }
    public int TotalTextos { get; set; }
    public int TotalProves { get; set; }
    public int TotalVacunes { get; set; }
}

public class VisitaDetailDto
{
    public Guid IdVisita { get; set; }
    public Guid IdPacient { get; set; }
    public DateTime DiaVisita { get; set; }
    public VeterinariDto Veterinari { get; set; } = null!;
    public string? Resum { get; set; }
    public decimal? Pes { get; set; }
    public decimal? Alsada { get; set; }
    public int TipusVisita { get; set; }
    public List<TextVisitaDto> TextosClínics { get; set; } = new();
    public List<ProvaDto> Proves { get; set; } = new();
    public List<VacunaDto> Vacunes { get; set; } = new();
}

public class VeterinariDto
{
    public Guid IdDoctor { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Especialitat { get; set; }
    public string? NumColegiat { get; set; }
}

public class TextVisitaDto
{
    public int IndexText { get; set; }
    public string TextPla { get; set; } = string.Empty;
    public SeccioTextVisitaDto? Seccions { get; set; }
}

public class SeccioTextVisitaDto
{
    public string? Motiu { get; set; }
    public string? Exploracio { get; set; }
    public string? Diagnostic { get; set; }
    public string? Tractament { get; set; }
    public string? Observacions { get; set; }
}

public class ProvaDto
{
    public Guid IdProva { get; set; }
    public string TipusProva { get; set; } = string.Empty;
    public int Ordre { get; set; }
    public string? CodiMostra { get; set; }
    public string? Observacions { get; set; }
    public List<DetallProvaDto> Resultats { get; set; } = new();
}

public class DetallProvaDto
{
    public string Parametre { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
    public string? Observacions { get; set; }
}

public class VacunaDto
{
    public Guid IdVacuna { get; set; }
    public string TipusVacuna { get; set; } = string.Empty;
    public DateTime DiaVacuna { get; set; }
    public string? Observacions { get; set; }
    public bool NoRevacunar { get; set; }
    public int FrequenciaDies { get; set; }
    public DateTime? ProximaVacuna { get; set; }
}

// ===== Medicines (Medicaments) =====

public class VeterinaryMedicineDto
{
    public string CnCode { get; set; } = string.Empty;
    public string CommercialName { get; set; } = string.Empty;
    public string ActiveIngredient { get; set; } = string.Empty;
    public string? Concentration { get; set; }
    public string? PharmaceuticalForm { get; set; }
    public List<string> TargetSpecies { get; set; } = new();
    public string? TherapeuticIndications { get; set; }
    public string? Dosage { get; set; }
    public string? Contraindications { get; set; }
    public string? Laboratory { get; set; }
    public bool PrescriptionRequired { get; set; }
    public WithdrawalPeriodDto? WithdrawalPeriod { get; set; }
    public List<string> PackageSizes { get; set; } = new();
    public DateTime? LastUpdated { get; set; }
}

public class WithdrawalPeriodDto
{
    public int? MeatDays { get; set; }
    public int? MilkDays { get; set; }
    public int? EggsDays { get; set; }
}

public class HumanMedicineDto
{
    public string CnCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ActiveIngredient { get; set; } = string.Empty;
    public string? PharmaceuticalForm { get; set; }
    public string? Dose { get; set; }
    public string? AdministrationRoute { get; set; }
    public string? Laboratory { get; set; }
    public string? AuthorizationStatus { get; set; }
    public DateTime? AuthorizationDate { get; set; }
    public string? Indications { get; set; }
    public bool PrescriptionRequired { get; set; }
    public bool IsGeneric { get; set; }
    public decimal? PricePvp { get; set; }
    public bool? AffectedByReducedContribution { get; set; }
    public string? TechnicalDataSheetUrl { get; set; }
    public string? PatientLeafletUrl { get; set; }
    public DateTime? LastUpdated { get; set; }
}
