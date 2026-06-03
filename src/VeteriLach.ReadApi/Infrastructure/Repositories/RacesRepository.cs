using Azure.Core;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Metadata.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Infrastructure
{
    public partial class RacesRepository(VeteriLachDbContext context, ILogger<RacesRepository> logger) : IRacesRepository
    {
        public async Task<IEnumerable<RasaDto>> GetRaces(Guid IdEspecie, CancellationToken cancellationToken)
        {
            LogGetRasesQuery(IdEspecie);

            var query = context.VetRasas
                .Include(r => r.IdEspecieNavigation)
                .AsQueryable();

            // Aplicar filtre per espècie si s'especifica
            if (IdEspecie != Guid.Empty)
            {
                query = query.Where(r => r.IdEspecie == IdEspecie);
            }

            var rases = await query
                .Select(r => new RasaDto(
                    r.IdRasa,
                    r.Nom,
                    r.IdEspecie,
                    r.IdEspecieNavigation.Nom,
                    r.VetAnimals.Count,
                    r.TamanyRelatiu
                    ))
                .OrderBy(r => r.NomEspecie)
                .ThenByDescending(r => r.TotalAnimals)
                .ThenBy(r => r.Nom)
                .ToListAsync(cancellationToken);

            LogRacesFound(rases.Count);

            return rases;

        }

        [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Executant GetRasesQuery amb filtre Especie={IdEspecie}")]
        partial void LogGetRasesQuery(Guid IdEspecie);
        [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Trobades {Count} races")]
        partial void LogRacesFound(int Count);
    }
}
