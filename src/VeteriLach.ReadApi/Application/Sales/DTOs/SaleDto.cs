namespace VeteriLach.ReadApi.Application.Sales.DTOs;

/// <summary>
/// DTO per informació bàsica d'una venda
/// </summary>
public record SaleDto(Guid SaleId, Guid CustomerId, string CustomerName, Guid SellerId, string SellerName, DateTime SaleDate, decimal TotalAmount, decimal TotalPaid, decimal TotalChange, Guid PaymentMethodId, string PaymentMethodName, bool IsPaymentCash, Guid? AnimalId, string? AnimalName, string? Summary, string? Notes, int ItemCount);
