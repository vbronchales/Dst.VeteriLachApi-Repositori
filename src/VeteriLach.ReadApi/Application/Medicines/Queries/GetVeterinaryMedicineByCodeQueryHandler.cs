using MediatR;
using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Interfaces;

namespace VeteriLach.ReadApi.Application.Medicines.Queries;

public record GetVeterinaryMedicineByCodeQuery(string CnCode) : IRequest<VeterinaryMedicineDto?>;

/// <summary>
/// Handler per obtenir medicament veterinari per codi
/// </summary>
public partial class GetVeterinaryMedicineByCodeQueryHandler(ICimaVetService cimaVetService, ILogger<GetVeterinaryMedicineByCodeQueryHandler> logger)
    : IRequestHandler<GetVeterinaryMedicineByCodeQuery, VeterinaryMedicineDto?>
{
    public async Task<VeterinaryMedicineDto?> Handle(
        GetVeterinaryMedicineByCodeQuery request,
        CancellationToken cancellationToken)
    {
        LogExecutingGetVeterinaryMedicineByCodeQuery(request.CnCode);

        var result = await cimaVetService.GetMedicineByCodeAsync(
            request.CnCode,
            cancellationToken);

        if (result != null)
        {
            LogFoundVeterinaryMedicine(result.CommercialName);
        }
        else
        {
            LogNotFoundVeterinaryMedicine(request.CnCode);
        }

        return result;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Executant GetVeterinaryMedicineByCodeQuery per codi: {CnCode}")]
    public partial void LogExecutingGetVeterinaryMedicineByCodeQuery(string cnCode);
    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Trobat medicament veterinari: {Name}")]
    public partial void LogFoundVeterinaryMedicine(string name);
    [LoggerMessage(EventId = 3, Level = LogLevel.Warning, Message = "No s'ha trobat medicament veterinari amb codi: {CnCode}")]
    public partial void LogNotFoundVeterinaryMedicine(string cnCode);
}
