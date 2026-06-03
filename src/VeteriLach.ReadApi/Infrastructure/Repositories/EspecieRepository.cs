using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Metadata.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Infrastructure
{
    public partial class EspecieRepository(VeteriLachDbContext context, ILogger<EspecieRepository> logger) : IEspecieRepository
    {
        public async Task<IEnumerable<EspecieDto>> GetEspecies(CancellationToken cancellationToken)
        {
            LogExecutingGetEspeciesQuery();

            var especies = await context.VetEspecies
                .Select(e => new EspecieDto(
                    e.IdEspecie,
                    e.Nom,
                    e.TipusEspecie,
                    e.VetRasas.SelectMany(r => r.VetAnimals).Count()
                    ))
                .OrderByDescending(e => e.TotalAnimals)
                .ThenBy(e => e.Nom)
                .ToListAsync(cancellationToken);

            LogFoundEspecies(especies.Count);

            return especies;
        }

        [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Executant GetEspeciesQuery")]
        partial void LogExecutingGetEspeciesQuery();
        [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Trobades {Count} espècies")]
        partial void LogFoundEspecies(int count);
    }
}
