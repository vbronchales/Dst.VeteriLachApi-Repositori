using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.Propietaris.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Propietaris.Queries;

/// <summary>
/// Handler per obtenir llista paginada de propietaris
/// </summary>
public class GetPropietarisListQueryHandler : IRequestHandler<GetPropietarisListQuery, PaginatedResult<PropietariListDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPropietarisListQueryHandler> _logger;

    public GetPropietarisListQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetPropietarisListQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<PropietariListDto>> Handle(GetPropietarisListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtenint llista de propietaris. Pàgina: {PageNumber}, Mida: {PageSize}", 
            request.PageNumber, request.PageSize);

        // Query base
        var query = _context.VetPropietaris
            .Include(p => p.IdPropietari1)
                .ThenInclude(sp => sp.SlcTelefons)
            .Include(p => p.VetAnimals)
            .AsQueryable();

        // Filtres
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchLower = request.SearchTerm.ToLower();
            query = query.Where(p =>
                (p.IdPropietari1.Nom != null && p.IdPropietari1.Nom.ToLower().Contains(searchLower)) ||
                (p.IdPropietari1.Cognom1 != null && p.IdPropietari1.Cognom1.ToLower().Contains(searchLower)) ||
                (p.IdPropietari1.Cognom2 != null && p.IdPropietari1.Cognom2.ToLower().Contains(searchLower)) ||
                (p.IdPropietari1.Email != null && p.IdPropietari1.Email.ToLower().Contains(searchLower)) ||
                p.IdPropietari1.SlcTelefons.Any(t => t.Numero.Contains(request.SearchTerm))
            );
        }

        if (request.NomesActius.HasValue)
        {
            query = query.Where(p => p.IdPropietari1.Actiu == request.NomesActius.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Poblacio))
        {
            var poblacioLower = request.Poblacio.ToLower();
            query = query.Where(p => p.IdPropietari1.Poblacio != null && 
                                    p.IdPropietari1.Poblacio.ToLower().Contains(poblacioLower));
        }

        if (request.AmbAnimals.HasValue)
        {
            if (request.AmbAnimals.Value)
            {
                query = query.Where(p => p.VetAnimals.Any());
            }
            else
            {
                query = query.Where(p => !p.VetAnimals.Any());
            }
        }

        // Ordenar per cognoms i nom
        query = query.OrderBy(p => p.IdPropietari1.Cognom1)
                    .ThenBy(p => p.IdPropietari1.Cognom2)
                    .ThenBy(p => p.IdPropietari1.Nom);

        // Total abans de paginar
        var totalItems = await query.CountAsync(cancellationToken);

        // Paginar i mappejar
        var propietaris = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<PropietariListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Retornant {Count} propietaris de {Total}", propietaris.Count, totalItems);

        return new PaginatedResult<PropietariListDto>
        {
            Data = propietaris,
            Pagination = new PaginationMetadata
            {
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize)
            }
        };
    }
}
