using MediatR;
using VeteriLach.ReadApi.Application.Metadata.DTOs;

namespace VeteriLach.ReadApi.Application.Metadata.Queries;

/// <summary>
/// Query per obtenir races amb filtre opcional per espècie
/// </summary>
public class GetRasesQuery : IRequest<List<RasaDto>>
{
    /// <summary>
    /// Filtre per espècie (opcional, GUID o nom)
    /// </summary>
    public string? Especie { get; set; }
}
