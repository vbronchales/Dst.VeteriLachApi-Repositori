using VeteriLach.ReadApi.Application.MedicalHistory.Services;
using VeteriLach.ReadApi.Domain.MedicalHistory;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper
{
    public static class VisitesMapper
    {
        public static VisitaResumDto ToVisitaResumatDto(this HosVisitum visita)
        {
            var visitaResumida = new VisitaResumDto(
                visita.IdVisita, 
                visita.DiaVisita, 
                visita.IdDoctorNavigation?.IdDoctorNavigation?.Nom ?? "Desconegut", 
                visita.Resum, 
                visita.Pes, 
                visita.HosTextVisita.Count, 
                visita.HosProvas.Count, 
                visita.HosVacunas.Count);

            return visitaResumida;
        }

        public static VeterinariDto ToVeterinariDto(this HosDoctor? doctor)
        {
            if (doctor == null) return new VeterinariDto(Guid.Empty, "Desconegut", "Desconegut", "Desconegut");

            return new VeterinariDto(
                doctor.IdDoctor,
                doctor.IdDoctorNavigation?.Nom ?? "Desconegut",
                doctor.IdDoctorNavigation?.Cognom1 ?? "Desconegut",
                doctor.IdDoctorNavigation?.Cognom2 ?? "Desconegut"
            );
        }

        public static TextVisitaDto ToTextVisitaDto(this HosTextVisitum textVisita)
        {
            return new TextVisitaDto(
                textVisita.IndexText,
                textVisita.TextPla,
                TextVisitaParserService.ParsejarText(textVisita.TextPla)
                );
        }

        public static DetallProvaDto ToDetallProvaDto(this HosDetallProva detall)
        {
            return new DetallProvaDto(
                detall.IdDetallTipusProvaNavigation?.Nom ?? "Desconegut",
                detall.Valor,
                detall.Observacions
            );
        }

        public static ProvaDto ToProvaDto(this HosProva prova)
        {
            return new ProvaDto(
                prova.IdProva,
                prova.IdTipusProvaNavigation?.Nom ?? "Desconegut",
                prova.Ordre,
                prova.CodiMostra,
                prova.Observacions,
                [.. prova.HosDetallProvas.Select(d => d.ToDetallProvaDto())]
                );
        }

        public static VacunaDto ToVacunaDto(this HosVacuna vacuna)
        {
            return new VacunaDto(
                vacuna.IdVacuna,
                vacuna.IdTipusVacunaNavigation?.Nom ?? "Desconegut",
                vacuna.DiaVacuna,
                vacuna.Observacions,
                vacuna.NoRevacunar,
                vacuna.IdTipusVacunaNavigation?.Frequencia ?? 0,
                vacuna.DiaVacuna.AddDays(vacuna.IdTipusVacunaNavigation?.Frequencia ?? 0)
            );
        }

        public static VisitaDetailDto? ToVisitaDetailDto(this HosVisitum? visita)
        {
            if (visita == null) return null;

            var visitaDetail = new VisitaDetailDto(
                visita.IdVisita,
                visita.IdPacient,
                visita.DiaVisita,
                visita.IdDoctorNavigation.ToVeterinariDto(),
                visita.Resum,
                visita.Pes,
                visita.Alsada,
                visita.TipusVisita)
            {
                TextosClínics = [..visita.HosTextVisita.Select(t=>t.ToTextVisitaDto())],
                Proves = [.. visita.HosProvas.Select(p => p.ToProvaDto())],
                Vacunes = [.. visita.HosVacunas.Select(v => v.ToVacunaDto())],
                Resum = visita.Resum
            };

            return visitaDetail;
        }

    }

}
