namespace VeteriLach.ReadApi.Application.Sales.DTOs;

/// <summary>
/// DTO per detall complet d'una venda amb articles
/// </summary>
public class SaleDetailDto
{
    public Guid SaleId { get; set; }
    
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string? CustomerNif { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerEmail { get; set; }
    
    public Guid SellerId { get; set; }
    public string SellerName { get; set; } = null!;
    
    public DateTime SaleDate { get; set; }
    
    public decimal TotalAmount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal TotalChange { get; set; }
    public decimal PendingAmount => TotalAmount - TotalPaid;
    
    public bool IsFullyPaid => TotalPaid >= TotalAmount;
    public DateTime? PaidDate { get; set; }
    
    public Guid PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; } = null!;
    public bool IsPaymentCash { get; set; }
    public string? BankAccount { get; set; }
    
    public Guid? AnimalId { get; set; }
    public string? AnimalName { get; set; }
    public string? AnimalSpecies { get; set; }
    
    public string? Summary { get; set; }
    public string? Notes { get; set; }
    
    public List<SaleItemDto> Items { get; set; } = new();
}
