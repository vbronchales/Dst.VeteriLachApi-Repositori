using MediatR;
using VeteriLach.ReadApi.Application.Visits.DTOs;

namespace VeteriLach.ReadApi.Application.Visits.Queries;

/// <summary>
/// Query per obtenir visites recents ordenades per data
/// </summary>
public class GetRecentVisitsQuery : IRequest<List<RecentVisitDto>>
{
    /// <summary>
    /// Nombre de dies enrere (default: 7)
    /// </summary>
    public int Days { get; set; } = 7;
    
    /// <summary>
    /// Nombre de pàgina (default: 1)
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Elements per pàgina (default: 20, max: 100)
    /// </summary>
    public int PageSize { get; set; } = 20;
    
    /// <summary>
    /// Incloure informació d'animal i propietari (default: true)
    /// </summary>
    public bool IncludeAnimalInfo { get; set; } = true;
}
