using MediatR;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.Propietaris.DTOs;

namespace VeteriLach.ReadApi.Application.Propietaris.Queries;

/// <summary>
/// Query per obtenir llista paginada de propietaris amb filtres
/// </summary>
public class GetPropietarisListQuery : IRequest<PaginatedResult<PropietariListDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    
    /// <summary>
    /// Cerca per nom, cognoms, email o telèfon
    /// </summary>
    public string? SearchTerm { get; set; }
    
    /// <summary>
    /// Filtrar només propietaris actius
    /// </summary>
    public bool? NomesActius { get; set; }
    
    /// <summary>
    /// Filtrar per població
    /// </summary>
    public string? Poblacio { get; set; }
    
    /// <summary>
    /// Filtrar propietaris que tinguin animals
    /// </summary>
    public bool? AmbAnimals { get; set; }
}
