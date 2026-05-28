using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public class GetAnimalVisitsListQueryHandler : IRequestHandler<GetAnimalVisitsListQuery, PaginatedResult<VisitaResumatDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAnimalVisitsListQueryHandler> _logger;

    public GetAnimalVisitsListQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetAnimalVisitsListQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<VisitaResumatDto>> Handle(GetAnimalVisitsListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtenint llista de visites per animal {IdAnimal}", request.IdAnimal);

        // Buscar l'animal i les seves visites
        var query = _context.HosVisita
            .Include(v => v.IdDoctorNavigation).ThenInclude(d => d.IdDoctorNavigation)
            .Include(v => v.HosTextVisita)
            .Include(v => v.HosProvas)
            .Include(v => v.HosVacunas)
            .Where(v => v.IdPacient == request.IdAnimal)
            .AsNoTracking();

        // Filtres opcionals per data
        if (request.DataInici.HasValue)
        {
            query = query.Where(v => v.DiaVisita >= request.DataInici.Value);
        }

        if (request.DataFi.HasValue)
        {
            query = query.Where(v => v.DiaVisita <= request.DataFi.Value);
        }

        // Ordenar per data descendent (més recent primer)
        query = query.OrderByDescending(v => v.DiaVisita);

        // Comptar total abans de paginar
        var totalItems = await query.CountAsync(cancellationToken);

        // Paginar
        var visites = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<VisitaResumatDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Trobades {Count} visites de {Total} per animal {IdAnimal}", 
            visites.Count, totalItems, request.IdAnimal);

        return new PaginatedResult<VisitaResumatDto>(
            visites,
            totalItems,
            request.PageNumber,
            request.PageSize
        );
    }
}
