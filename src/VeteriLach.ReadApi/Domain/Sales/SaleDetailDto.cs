namespace VeteriLach.ReadApi.Domain;

/// <summary>
/// DTO per detall complet d'una venda amb articles
/// </summary>
public record SaleDetailDto(Guid SaleId, Guid CustomerId, string CustomerName, string? CustomerNif, string? CustomerPhone, string? CustomerEmail, Guid SellerId, string SellerName, DateTime SaleDate, decimal TotalAmount, decimal TotalPaid, decimal TotalChange, Guid PaymentMethodId, string PaymentMethodName, bool IsPaymentCash, string? BankAccount, Guid? AnimalId, string? AnimalName, string? AnimalSpecies, string? Summary, string? Notes, List<SaleItemDto> Items);
