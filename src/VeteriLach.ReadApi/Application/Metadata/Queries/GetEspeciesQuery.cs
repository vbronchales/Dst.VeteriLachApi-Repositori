using MediatR;
using VeteriLach.ReadApi.Application.Metadata.DTOs;

namespace VeteriLach.ReadApi.Application.Metadata.Queries;

/// <summary>
/// Query per obtenir totes les espècies amb comptador d'animals
/// </summary>
public class GetEspeciesQuery : IRequest<List<EspecieDto>>
{
}
