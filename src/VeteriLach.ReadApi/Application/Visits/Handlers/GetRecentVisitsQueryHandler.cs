using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Visits.DTOs;
using VeteriLach.ReadApi.Application.Visits.Queries;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.Visits.Handlers;

public class GetRecentVisitsQueryHandler : IRequestHandler<GetRecentVisitsQuery, List<RecentVisitDto>>
{
    private readonly VeteriLachDbContext _context;
    private readonly ILogger<GetRecentVisitsQueryHandler> _logger;

    public GetRecentVisitsQueryHandler(
        VeteriLachDbContext context,
        ILogger<GetRecentVisitsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<RecentVisitDto>> Handle(GetRecentVisitsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executant GetRecentVisitsQuery: Days={Days}, PageNumber={PageNumber}, PageSize={PageSize}",
            request.Days, request.PageNumber, request.PageSize);

        // Calcular data límit
        var dateLimit = DateTime.Now.AddDays(-request.Days);

        // Query base
        var query = _context.HosVisita
            .Where(v => v.DiaVisita >= dateLimit)
            .OrderByDescending(v => v.DiaVisita)
            .AsQueryable();

        // Si necessitem informació d'animal i propietari, afegim Includes
        if (request.IncludeAnimalInfo)
        {
            query = query
                .Include(v => v.IdPacientNavigation)
                    .ThenInclude(p => p.IdPacient1)
                .Include(v => v.IdPacientNavigation)
                    .ThenInclude(p => p.VetAnimal)
                        .ThenInclude(a => a.IdRasaNavigation)
                            .ThenInclude(r => r.IdEspecieNavigation)
                .Include(v => v.IdPacientNavigation)
                    .ThenInclude(p => p.VetAnimal)
                        .ThenInclude(a => a.IdPropietariNavigation)
                            .ThenInclude(prop => prop.IdPropietari1)
                                .ThenInclude(pers => pers.SlcTelefons)
                .Include(v => v.IdDoctorNavigation)
                    .ThenInclude(d => d.IdDoctor1);
        }

        // Paginació
        var visits = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(v => new RecentVisitDto
            {
                IdVisita = v.IdVisita,
                DiaVisita = v.DiaVisita,
                Resum = v.Resum,
                Pes = v.Pes,
                Alsada = v.Alsada,
                TipusVisita = v.TipusVisita,
                IdDoctor = v.IdDoctor,
                NomDoctor = v.IdDoctorNavigation.IdDoctor1 != null 
                    ? v.IdDoctorNavigation.IdDoctor1.Nom 
                    : null,
                
                // Informació del pacient
                IdPacient = v.IdPacient,
                NomAnimal = v.IdPacientNavigation.IdPacient1 != null 
                    ? v.IdPacientNavigation.IdPacient1.Nom
                    : string.Empty,
                Especie = v.IdPacientNavigation.VetAnimal != null 
                    && v.IdPacientNavigation.VetAnimal.IdRasaNavigation != null
                    && v.IdPacientNavigation.VetAnimal.IdRasaNavigation.IdEspecieNavigation != null
                    ? v.IdPacientNavigation.VetAnimal.IdRasaNavigation.IdEspecieNavigation.Nom
                    : null,
                Rasa = v.IdPacientNavigation.VetAnimal != null 
                    && v.IdPacientNavigation.VetAnimal.IdRasaNavigation != null
                    ? v.IdPacientNavigation.VetAnimal.IdRasaNavigation.Nom
                    : null,
                NumXip = v.IdPacientNavigation.VetAnimal != null
                    ? v.IdPacientNavigation.VetAnimal.NumXip
                    : null,
                
                // Informació del propietari
                IdPropietari = v.IdPacientNavigation.VetAnimal != null
                    ? v.IdPacientNavigation.VetAnimal.IdPropietari
                    : Guid.Empty,
                NomPropietari = v.IdPacientNavigation.VetAnimal != null
                    && v.IdPacientNavigation.VetAnimal.IdPropietariNavigation != null
                    && v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1 != null
                    ? v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1.Nom
                    : string.Empty,
                CognomsPropietari = v.IdPacientNavigation.VetAnimal != null
                    && v.IdPacientNavigation.VetAnimal.IdPropietariNavigation != null
                    && v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1 != null
                    ? (v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1.Cognom1 ?? "") + " " + 
                      (v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1.Cognom2 ?? "")
                    : string.Empty,
                TelefonPropietari = v.IdPacientNavigation.VetAnimal != null
                    && v.IdPacientNavigation.VetAnimal.IdPropietariNavigation != null
                    && v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1 != null
                    && v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1.SlcTelefons.Any()
                    ? v.IdPacientNavigation.VetAnimal.IdPropietariNavigation.IdPropietari1.SlcTelefons
                        .OrderBy(t => t.Ordre)
                        .First().Numero
                    : null
            })
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Trobades {Count} visites recents dels darrers {Days} dies", visits.Count, request.Days);

        return visits;
    }
}
