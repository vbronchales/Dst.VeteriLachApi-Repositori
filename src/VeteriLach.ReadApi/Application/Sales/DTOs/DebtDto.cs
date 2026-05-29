namespace VeteriLach.ReadApi.Application.Sales.DTOs;

/// <summary>
/// DTO per deutes pendents de clients
/// </summary>
public class DebtDto
{
    public Guid SaleId { get; set; }
    
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string? CustomerNif { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerEmail { get; set; }
    
    public DateTime SaleDate { get; set; }
    public int DaysPending => (DateTime.Now - SaleDate).Days;
    
    public decimal TotalAmount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal PendingAmount => TotalAmount - TotalPaid;
    
    public Guid? AnimalId { get; set; }
    public string? AnimalName { get; set; }
    
    public string? Summary { get; set; }
    public string? Notes { get; set; }
}
