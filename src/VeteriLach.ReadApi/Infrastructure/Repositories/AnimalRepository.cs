using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Infrastructure.Data;
using VeteriLach.ReadApi.Mapper;

namespace VeteriLach.ReadApi.Infrastructure;

public class AnimalRepository(VeteriLachDbContext context) : IAnimalRepository
{
    public async Task<AnimalDetailDto?> GetAnimalById(Guid idAnimal, CancellationToken cancellationToken)
    {
        var animal = await context.VetAnimals
            .Include(a => a.IdAnimalNavigation)
            .ThenInclude(p => p.IdPacient1)
            .Include(a => a.IdRasaNavigation)
            .ThenInclude(r => r.IdEspecieNavigation)
            .Include(a => a.IdPropietariNavigation)
            .ThenInclude(p => p.IdPropietari1)
            .ThenInclude(s => s.SlcTelefons)
            .Where(a => a.IdAnimal == idAnimal)
            .FirstOrDefaultAsync(cancellationToken);

        if (animal == null)
        {
            return null;
        }

        // Utilitzar AutoMapper per mapejar l'entitat al DTO
        return animal.ToAnimalDetailDto();
    }

    public async Task<PaginatedResult<AnimalListDto>> GetAnimalsList(int pageNumber, int pageSize, string? searchTerm, Guid? idPropietari, Guid? idEspecie, CancellationToken cancellationToken)
    {
        // Query base amb eager loading
        var query = context.VetAnimals
            .Include(a => a.IdAnimalNavigation)
                .ThenInclude(p => p.IdPacient1)
            .Include(a => a.IdRasaNavigation)
                .ThenInclude(r => r.IdEspecieNavigation)
            .AsQueryable();

        // Filtres
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var searchLower = searchTerm.ToLower();
            query = query.Where(a =>
                (a.IdAnimalNavigation.IdPacient1.Nom != null && a.IdAnimalNavigation.IdPacient1.Nom.ToLower().Contains(searchLower)) ||
                (a.NumXip != null && a.NumXip.ToLower().Contains(searchLower)));
        }

        if (idPropietari.HasValue)
        {
            query = query.Where(a => a.IdPropietari == idPropietari.Value);
        }

        if (idEspecie.HasValue)
        {
            query = query.Where(a => a.IdRasaNavigation.IdEspecie == idEspecie.Value);
        }

        // Total items abans de paginar
        var totalItems = await query.CountAsync(cancellationToken);

        // Paginació amb ProjectTo d'AutoMapper (més eficient)
        var animals = await query
            .OrderBy(a => a.IdAnimalNavigation.IdPacient1.Nom)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(a => a.ToAnimalListDto())
            .ToListAsync(cancellationToken);

        return new PaginatedResult<AnimalListDto>
        {
            Data = animals,
            Pagination = new PaginationMetadata
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            }
        };
    }
}
