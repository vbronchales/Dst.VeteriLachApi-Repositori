using MediatR;
using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Application.Common.Models;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

/// <summary>
/// Query per obtenir una llista paginada d'animals
/// </summary>
public class GetAnimalsListQuery : IRequest<PaginatedResult<AnimalListDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SearchTerm { get; set; }
    public Guid? IdPropietari { get; set; }
    public Guid? IdEspecie { get; set; }
}
