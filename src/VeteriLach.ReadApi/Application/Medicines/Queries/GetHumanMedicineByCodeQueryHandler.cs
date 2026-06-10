using MediatR;
using VeteriLach.ReadApi.Domain.Medicines;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;


public record GetHumanMedicineByCodeQuery(string CnCode) : IRequest<HumanMedicineDto?>;

/// <summary>
/// Handler per obtenir medicament humà per codi
/// </summary>
public partial class GetHumanMedicineByCodeQueryHandler(ICimaService cimaService, ILogger<GetHumanMedicineByCodeQueryHandler> logger)
    : IRequestHandler<GetHumanMedicineByCodeQuery, HumanMedicineDto?>
{
    public async Task<HumanMedicineDto?> Handle( GetHumanMedicineByCodeQuery request, CancellationToken cancellationToken)
    {
        LogExecutingGetHumanMedicineByCodeQuery(request.CnCode);

        var result = await cimaService.GetMedicineByCodeAsync(
            request.CnCode,
            cancellationToken);

        if (result != null)
        {
            LogFoundHumanMedicine(result.Name);
        }
        else
        {
            LogNotFoundHumanMedicine(request.CnCode);
        }

        return result;
    }

    [LoggerMessage( EventId = 1, Level = LogLevel.Information, Message = "Executant GetHumanMedicineByCodeQuery per codi: {CnCode}")]
    partial void LogExecutingGetHumanMedicineByCodeQuery(string cnCode);
    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Trobat medicament humà: {Name}")]
    partial void LogFoundHumanMedicine(string name);
    [LoggerMessage(EventId = 3, Level = LogLevel.Warning, Message = "No s'ha trobat medicament humà amb codi: {CnCode}")]
    partial void LogNotFoundHumanMedicine(string cnCode);
}
