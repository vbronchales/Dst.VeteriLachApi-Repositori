using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure.Data;
using VeteriLach.ReadApi.Mapper;

namespace VeteriLach.ReadApi.Infrastructure
{
    public partial class PropietariRepository(VeteriLachDbContext context, ILogger<PropietariRepository> logger) : IPropietariRepository
    {
        public async Task<PropietariDetailDto?> GetPropietariByIdAsync(Guid idPropietari, CancellationToken cancellationToken)
        {
            LogObtenintDetallPropietari(idPropietari);

            var propietari = await context.VetPropietaris
                .Include(p => p.IdPropietari1)
                    .ThenInclude(sp => sp.SlcTelefons)
                .Include(p => p.VetAnimals)
                    .ThenInclude(a => a.IdAnimalNavigation)
                        .ThenInclude(pac => pac.IdPacient1)
                .Include(p => p.VetAnimals)
                    .ThenInclude(a => a.IdRasaNavigation)
                        .ThenInclude(r => r.IdEspecieNavigation)
                .FirstOrDefaultAsync(p => p.IdPropietari == idPropietari, cancellationToken);

            if (propietari == null)
            {
                LogPropietariNoTrobat(idPropietari);
                return null;
            }

            var result = propietari.ToPropietariDetailDto();
            if (result == null)
            {
                LogPropietariNoTrobat(idPropietari);
                return null;
            }

            LogPropietariTrobat(idPropietari);
            return result;
        }

        [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Obtenint detall del propietari {IdPropietari}")]
        partial void LogObtenintDetallPropietari(Guid idPropietari);
        [LoggerMessage(EventId = 2, Level = LogLevel.Warning, Message = "Propietari {IdPropietari} no trobat")]
        partial void LogPropietariNoTrobat(Guid idPropietari);
        [LoggerMessage(EventId = 3, Level = LogLevel.Information, Message = "Propietari {IdPropietari} trobat")]
        partial void LogPropietariTrobat(Guid idPropietari);

        public async Task<PaginatedResult<PropietariListDto>> GetPropietarisListAsync(string? searchTerm, string? poblacio, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            LogObtenintPropietaris(pageNumber, pageSize);

            // Query base
            var query = context.VetPropietaris
                .Include(p => p.IdPropietari1)
                    .ThenInclude(sp => sp.SlcTelefons)
                .Include(p => p.VetAnimals)
                .AsQueryable();

            // Filtres
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchLower = searchTerm.ToLower();
                query = query.Where(p =>
                    (p.IdPropietari1.Nom != null && p.IdPropietari1.Nom.ToLower().Contains(searchLower)) ||
                    (p.IdPropietari1.Cognom1 != null && p.IdPropietari1.Cognom1.ToLower().Contains(searchLower)) ||
                    (p.IdPropietari1.Cognom2 != null && p.IdPropietari1.Cognom2.ToLower().Contains(searchLower)) ||
                    (p.IdPropietari1.Email != null && p.IdPropietari1.Email.ToLower().Contains(searchLower)) ||
                    p.IdPropietari1.SlcTelefons.Any(t => t.Numero.Contains(searchTerm))
                );
            }

            if (!string.IsNullOrWhiteSpace(poblacio))
            {
                var poblacioLower = poblacio.ToLower();
                query = query.Where(p => p.IdPropietari1.Poblacio != null &&
                                        p.IdPropietari1.Poblacio.ToLower().Contains(poblacioLower));
            }

            // Ordenar per cognoms i nom
            query = query.OrderBy(p => p.IdPropietari1.Cognom1)
                        .ThenBy(p => p.IdPropietari1.Cognom2)
                        .ThenBy(p => p.IdPropietari1.Nom);

            // Total abans de paginar
            var totalItems = await query.CountAsync(cancellationToken);

            // Paginar i mappejar
            var propietaris = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => p.ToPropietariListDto())
                .ToListAsync(cancellationToken);

            LogRetornantPropietaris(propietaris.Count, totalItems);

            return new PaginatedResult<PropietariListDto>
            {
                Data = propietaris,
                Pagination = new PaginationMetadata
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                }
            };
        }

        [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "Retornant {Count} propietaris de {Total}")]
        partial void LogRetornantPropietaris(int count, int total);
        [LoggerMessage(EventId = 5, Level = LogLevel.Information, Message = "Obtenint llista de propietaris. Pàgina: {PageNumber}, Mida: {PageSize}")]
        partial void LogObtenintPropietaris(int pageNumber, int pageSize);
    }
}
