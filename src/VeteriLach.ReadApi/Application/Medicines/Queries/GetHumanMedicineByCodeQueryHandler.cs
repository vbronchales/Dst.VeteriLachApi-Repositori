using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

/// <summary>
/// Handler per obtenir medicament humà per codi
/// </summary>
public class GetHumanMedicineByCodeQueryHandler
    : IRequestHandler<GetHumanMedicineByCodeQuery, HumanMedicineDto?>
{
    private readonly ICimaService _cimaService;
    private readonly ILogger<GetHumanMedicineByCodeQueryHandler> _logger;

    public GetHumanMedicineByCodeQueryHandler(
        ICimaService cimaService,
        ILogger<GetHumanMedicineByCodeQueryHandler> logger)
    {
        _cimaService = cimaService;
        _logger = logger;
    }

    public async Task<HumanMedicineDto?> Handle(
        GetHumanMedicineByCodeQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Executant GetHumanMedicineByCodeQuery per codi: {CnCode}",
            request.CnCode);

        var result = await _cimaService.GetMedicineByCodeAsync(
            request.CnCode,
            cancellationToken);

        if (result != null)
        {
            _logger.LogInformation(
                "Trobat medicament humà: {Name}",
                result.Name);
        }
        else
        {
            _logger.LogWarning(
                "No s'ha trobat medicament humà amb codi: {CnCode}",
                request.CnCode);
        }

        return result;
    }
}
