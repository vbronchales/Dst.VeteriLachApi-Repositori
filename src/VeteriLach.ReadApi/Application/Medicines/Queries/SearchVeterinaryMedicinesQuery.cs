using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

/// <summary>
/// Query per cercar medicaments veterinaris
/// </summary>
public class SearchVeterinaryMedicinesQuery : IRequest<List<VeterinaryMedicineDto>>
{
    /// <summary>
    /// Text de cerca (nom, principi actiu, codi)
    /// </summary>
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// Espècie animal (opcional)
    /// </summary>
    public string? Species { get; set; }
}
