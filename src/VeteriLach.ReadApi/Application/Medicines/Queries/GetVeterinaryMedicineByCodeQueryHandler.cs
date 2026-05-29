using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

/// <summary>
/// Handler per obtenir medicament veterinari per codi
/// </summary>
public class GetVeterinaryMedicineByCodeQueryHandler
    : IRequestHandler<GetVeterinaryMedicineByCodeQuery, VeterinaryMedicineDto?>
{
    private readonly ICimaVetService _cimaVetService;
    private readonly ILogger<GetVeterinaryMedicineByCodeQueryHandler> _logger;

    public GetVeterinaryMedicineByCodeQueryHandler(
        ICimaVetService cimaVetService,
        ILogger<GetVeterinaryMedicineByCodeQueryHandler> logger)
    {
        _cimaVetService = cimaVetService;
        _logger = logger;
    }

    public async Task<VeterinaryMedicineDto?> Handle(
        GetVeterinaryMedicineByCodeQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Executant GetVeterinaryMedicineByCodeQuery per codi: {CnCode}",
            request.CnCode);

        var result = await _cimaVetService.GetMedicineByCodeAsync(
            request.CnCode,
            cancellationToken);

        if (result != null)
        {
            _logger.LogInformation(
                "Trobat medicament veterinari: {Name}",
                result.CommercialName);
        }
        else
        {
            _logger.LogWarning(
                "No s'ha trobat medicament veterinari amb codi: {CnCode}",
                request.CnCode);
        }

        return result;
    }
}
