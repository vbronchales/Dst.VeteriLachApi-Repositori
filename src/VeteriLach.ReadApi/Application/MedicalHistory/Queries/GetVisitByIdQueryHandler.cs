using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public class GetVisitByIdQueryHandler : IRequestHandler<GetVisitByIdQuery, VisitaDetailDto?>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetVisitByIdQueryHandler> _logger;

    public GetVisitByIdQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetVisitByIdQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<VisitaDetailDto?> Handle(GetVisitByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtenint detall de visita {IdVisita}", request.IdVisita);

        var visita = await _context.HosVisita
            .Include(v => v.IdDoctorNavigation).ThenInclude(d => d.IdDoctorNavigation)
            .Include(v => v.HosTextVisita.OrderBy(t => t.IndexText))
            .Include(v => v.HosProvas.OrderBy(p => p.Ordre))
                .ThenInclude(p => p.IdTipusProvaNavigation)
            .Include(v => v.HosProvas)
                .ThenInclude(p => p.HosDetallProvas)
                    .ThenInclude(d => d.IdDetallTipusProvaNavigation)
            .Include(v => v.HosVacunas)
                .ThenInclude(vac => vac.IdTipusVacunaNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.IdVisita == request.IdVisita, cancellationToken);

        if (visita == null)
        {
            _logger.LogWarning("Visita {IdVisita} no trobada", request.IdVisita);
            return null;
        }

        var result = _mapper.Map<VisitaDetailDto>(visita);

        _logger.LogInformation("Visita {IdVisita} trobada: {TextosCount} textos, {ProvesCount} proves, {VacunesCount} vacunes",
            request.IdVisita, result.TextosClínics.Count, result.Proves.Count, result.Vacunes.Count);

        return result;
    }
}
