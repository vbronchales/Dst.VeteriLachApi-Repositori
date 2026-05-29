namespace VeteriLach.ReadApi.Application.Sales.DTOs;

/// <summary>
/// DTO per pagaments a compte (acomptes)
/// </summary>
public class PaymentAdvanceDto
{
    public Guid PaymentAdvanceId { get; set; }
    
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    
    public Guid SellerId { get; set; }
    public string SellerName { get; set; } = null!;
    
    public DateTime PaymentDate { get; set; }
    
    public decimal Amount { get; set; }
    
    public Guid? PaymentMethodId { get; set; }
    public string? PaymentMethodName { get; set; }
    
    public Guid? AnimalId { get; set; }
    public string? AnimalName { get; set; }
    
    public string? Reference { get; set; }
    public string? Notes { get; set; }
}
