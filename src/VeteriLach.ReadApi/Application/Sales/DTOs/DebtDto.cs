namespace VeteriLach.ReadApi.Application.Sales.DTOs;

/// <summary>
/// DTO per deutes pendents de clients
/// </summary>
public record DebtDto(Guid SaleId, Guid CustomerId, string CustomerName, string? CustomerNif, string? CustomerPhone, string? CustomerEmail, DateTime SaleDate, decimal TotalAmount, decimal TotalPaid, Guid? AnimalId, string? AnimalName, string? Summary, string? Notes);
