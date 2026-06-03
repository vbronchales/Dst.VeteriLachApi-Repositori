using MediatR;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;
using VeteriLach.ReadApi.Application.MedicalHistory.Services;
using VeteriLach.ReadApi.Infrastructure;
using VeteriLach.ReadApi.Infrastructure.Data;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public record GetVisitByIdQuery(Guid IdVisita) : IRequest<VisitaDetailDto?>;

public class GetVisitByIdQueryHandler(IVisitesRepository repository, ILogger<GetVisitByIdQueryHandler> logger) : IRequestHandler<GetVisitByIdQuery, VisitaDetailDto?>
{
    public async Task<VisitaDetailDto?> Handle(GetVisitByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetVisitByIdAsync(request.IdVisita, cancellationToken);
        return result;
    }
}
