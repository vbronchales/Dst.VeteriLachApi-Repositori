using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Application.MedicalHistory.Services;
using VeteriLach.ReadApi.Domain.MedicalHistory;
using VeteriLach.ReadApi.Infrastructure.Data;
using VeteriLach.ReadApi.Mapper;

namespace VeteriLach.ReadApi.Infrastructure
{
    public partial class VisitesRepository(VeteriLachDbContext context, ILogger<VisitesRepository> logger) : IVisitesRepository
    {
        public async Task<PaginatedResult<VisitaResumDto>> GetVisitsByIdAnimalAsync(Guid idAnimal, int pageNumber, int pageSize, DateTime? dataInici, DateTime? dataFi, CancellationToken cancellationToken)
        {
            // Buscar l'animal i les seves visites
            var query = context.HosVisita
                .Include(v => v.IdDoctorNavigation).ThenInclude(d => d.IdDoctorNavigation)
                .Include(v => v.HosTextVisita)
                .Include(v => v.HosProvas)
                .Include(v => v.HosVacunas)
                .Where(v => v.IdPacient == idAnimal)
                .AsNoTracking();

            // Filtres opcionals per data
            if (dataInici.HasValue)
            {
                query = query.Where(v => v.DiaVisita >= dataInici.Value);
            }

            if (dataFi.HasValue)
            {
                query = query.Where(v => v.DiaVisita <= dataFi.Value);
            }

            // Ordenar per data descendent (més recent primer)
            query = query.OrderByDescending(v => v.DiaVisita);

            // Comptar total abans de paginar
            var totalItems = await query.CountAsync(cancellationToken);

            // Paginar
            var visites = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(v => v.ToVisitaResumatDto()) // Mapear a DTO directament a la consulta per optimitzar
                .ToListAsync(cancellationToken);

            LogResultatsObtenintVisites(visites.Count, totalItems, idAnimal);

            return new PaginatedResult<VisitaResumDto>(
                visites,
                totalItems,
                pageNumber,
                pageSize
            );
        }

        public async Task<VisitaDetailDto?> GetVisitByIdAsync(Guid idVisita, CancellationToken cancellationToken)
        {
            var visita = await context.HosVisita
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
                .FirstOrDefaultAsync(v => v.IdVisita == idVisita, cancellationToken);
            var result = visita.ToVisitaDetailDto();
            if (result == null)
            {
                LogVisitaNoTrobada(idVisita);
                return null;
            }

            // Generar un resum estructurat a partir de tots els textos
            var seccionsTotal = result.TextosClínics
                .Where(t => t.Seccions != null)
                .Select(t => t.Seccions!);

            if (seccionsTotal.Any())
            {
                var resumGenerat = TextVisitaParserService.GenerarResum(seccionsTotal);
                if (!string.IsNullOrEmpty(resumGenerat))
                {
                    result.Resum = resumGenerat;
                }
            }
            return result;
        }

        [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Trobades {Count} visites de {Total} per animal {IdAnimal}")]
        partial void LogResultatsObtenintVisites(int count, int total, Guid idAnimal);

        [LoggerMessage(EventId = 3, Level = LogLevel.Warning, Message = "Visita {IdVisita} no trobada")]
        partial void LogVisitaNoTrobada(Guid idVisita);


        public async Task<PaginatedResult<VisitaResumDto>> GetRecentVisitsAsync(int days, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            LogExecutantGetRecentVisitsQuery(days, pageNumber, pageSize);
            // Calcular data límit
            var dateLimit = DateTime.Now.AddDays(-days);
            // Query base
            var query = context.HosVisita
                .Where(v => v.DiaVisita >= dateLimit)
                .OrderByDescending(v => v.DiaVisita)
                .AsNoTracking();
            // Comptar total abans de paginar
            var totalItems = await query.CountAsync(cancellationToken);
            // Paginació i mapatge a DTO
            var visits = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(v => v.ToVisitaResumatDto())
                .ToListAsync(cancellationToken);
            
            LogResultatsObtenintVisites(visits.Count, totalItems, Guid.Empty); // Guid.Empty indica que no és per un animal específic
            return new PaginatedResult<VisitaResumDto>(
                visits,
                totalItems,
                pageNumber,
                pageSize
            );
        }

        [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "Executant GetRecentVisitsQuery: Days={Days}, PageNumber={PageNumber}, PageSize={PageSize}")]
        partial void LogExecutantGetRecentVisitsQuery(int days, int pageNumber, int pageSize);
    }
}
