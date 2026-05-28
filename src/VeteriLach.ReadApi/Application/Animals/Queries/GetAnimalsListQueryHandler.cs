using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

public class GetAnimalsListQueryHandler : IRequestHandler<GetAnimalsListQuery, PaginatedResult<AnimalListDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAnimalsListQueryHandler> _logger;

    public GetAnimalsListQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetAnimalsListQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<AnimalListDto>> Handle(GetAnimalsListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtenint llista d'animals. Page: {PageNumber}, Size: {PageSize}, Search: {SearchTerm}",
            request.PageNumber, request.PageSize, request.SearchTerm);

        // Query base amb eager loading
        var query = _context.VetAnimals
            .Include(a => a.IdAnimalNavigation)
                .ThenInclude(p => p.IdPacient1)
            .Include(a => a.IdRasaNavigation)
                .ThenInclude(r => r.IdEspecieNavigation)
            .AsQueryable();

        // Filtres
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchLower = request.SearchTerm.ToLower();
            query = query.Where(a =>
                (a.IdAnimalNavigation.IdPacient1.Nom != null && a.IdAnimalNavigation.IdPacient1.Nom.ToLower().Contains(searchLower)) ||
                (a.NumXip != null && a.NumXip.ToLower().Contains(searchLower)));
        }

        if (request.IdPropietari.HasValue)
        {
            query = query.Where(a => a.IdPropietari == request.IdPropietari.Value);
        }

        if (request.IdEspecie.HasValue)
        {
            query = query.Where(a => a.IdRasaNavigation.IdEspecie == request.IdEspecie.Value);
        }

        // Total items abans de paginar
        var totalItems = await query.CountAsync(cancellationToken);

        // Paginació amb ProjectTo d'AutoMapper (més eficient)
        var animals = await query
            .OrderBy(a => a.IdAnimalNavigation.IdPacient1.Nom)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<AnimalListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("S'han recuperat {Count} animals de {TotalItems} totals", animals.Count, totalItems);

        return new PaginatedResult<AnimalListDto>
        {
            Data = animals,
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
