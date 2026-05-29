namespace VeteriLach.ReadApi.Application.Sales.DTOs;

/// <summary>
/// DTO per article venut dins d'una venda
/// </summary>
public class SaleItemDto
{
    public Guid SaleItemId { get; set; }
    
    public Guid ArticleId { get; set; }
    public string ArticleName { get; set; } = null!;
    
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VatAmount { get; set; }
    public decimal VatRate { get; set; }
    public string VatName { get; set; } = null!;
    
    public decimal? Discount { get; set; }
    public decimal? DiscountPercentage => Discount.HasValue && UnitPrice > 0 
        ? (Discount.Value / UnitPrice) * 100 
        : null;
    
    public decimal Subtotal => UnitPrice * Quantity;
    public decimal Total => (UnitPrice - (Discount ?? 0)) * Quantity + VatAmount;
    
    public decimal? AmountPaid { get; set; }
    public DateTime? PaidDate { get; set; }
    
    public decimal? NetCost { get; set; }
    public decimal? Margin => NetCost.HasValue ? UnitPrice - NetCost.Value : null;
    public decimal? MarginPercentage => NetCost.HasValue && NetCost.Value > 0
        ? ((UnitPrice - NetCost.Value) / NetCost.Value) * 100
        : null;
    
    public int Order { get; set; }
    public string? Notes { get; set; }
}
