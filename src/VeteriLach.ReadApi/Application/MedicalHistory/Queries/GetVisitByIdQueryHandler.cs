using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;
using VeteriLach.ReadApi.Application.MedicalHistory.Services;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public class GetVisitByIdQueryHandler : IRequestHandler<GetVisitByIdQuery, VisitaDetailDto?>
{
    private readonly VeteriLachDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetVisitByIdQueryHandler> _logger;
    private readonly TextVisitaParserService _textParser;

    public GetVisitByIdQueryHandler(
        VeteriLachDbContext context,
        IMapper mapper,
        ILogger<GetVisitByIdQueryHandler> logger,
        TextVisitaParserService textParser)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _textParser = textParser;
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

        // Parsejar els textos clínics per extreure seccions estructurades
        foreach (var textDto in result.TextosClínics)
        {
            textDto.Seccions = _textParser.ParsejarText(textDto.TextPla);
        }

        // Generar un resum estructurat a partir de tots els textos
        var seccionsTotal = result.TextosClínics
            .Select(t => t.Seccions!)
            .Where(s => s != null)
            .ToList();

        if (seccionsTotal.Any())
        {
            result.Resum = _textParser.GenerarResum(seccionsTotal);
        }
        else if (!string.IsNullOrWhiteSpace(visita.Resum))
        {
            // Si no hi ha textos parseables, utilitzar el resum original
            result.Resum = visita.Resum;
        }

        _logger.LogInformation("Visita {IdVisita} trobada: {TextosCount} textos, {ProvesCount} proves, {VacunesCount} vacunes",
            request.IdVisita, result.TextosClínics.Count, result.Proves.Count, result.Vacunes.Count);

        return result;
    }
}
