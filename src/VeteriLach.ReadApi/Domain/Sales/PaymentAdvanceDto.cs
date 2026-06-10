namespace VeteriLach.ReadApi.Domain;

/// <summary>
/// DTO per pagaments a compte (acomptes)
/// </summary>
public record PaymentAdvanceDto(Guid PaymentAdvanceId, Guid CustomerId, string CustomerName, Guid SellerId, string SellerName, DateTime PaymentDate, decimal Amount, Guid? PaymentMethodId, string? PaymentMethodName, Guid? AnimalId, string? AnimalName, string? Reference, string? Notes);