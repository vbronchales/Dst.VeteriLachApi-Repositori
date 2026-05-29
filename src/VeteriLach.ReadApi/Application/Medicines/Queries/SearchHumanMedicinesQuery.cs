using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

/// <summary>
/// Query per cercar medicaments humans
/// </summary>
public class SearchHumanMedicinesQuery : IRequest<List<HumanMedicineDto>>
{
    /// <summary>
    /// Text de cerca (nom, principi actiu, codi)
    /// </summary>
    public string Query { get; set; } = string.Empty;
}
