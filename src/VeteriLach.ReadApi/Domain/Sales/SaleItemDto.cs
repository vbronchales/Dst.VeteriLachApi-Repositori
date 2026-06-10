namespace VeteriLach.ReadApi.Domain;

/// <summary>
/// DTO per article venut dins d'una venda
/// </summary>
public record SaleItemDto(Guid SaleItemId, Guid ArticleId, string ArticleName, decimal Quantity, decimal UnitPrice, decimal VatAmount, decimal VatRate, string VatName, decimal? Discount, decimal? DiscountPercentage, decimal Subtotal, decimal Total, decimal? AmountPaid, DateTime? PaidDate, decimal? NetCost, decimal? Margin, decimal? MarginPercentage, int Order, string? Notes);
