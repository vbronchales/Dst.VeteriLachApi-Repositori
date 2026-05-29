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
    public List<T> Items { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}
