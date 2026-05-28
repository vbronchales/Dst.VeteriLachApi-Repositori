using AutoMapper;
using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Application.MedicalHistory.DTOs;
using VeteriLach.ReadApi.Application.Propietaris.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Application.Common.Mappings;

/// <summary>
/// Perfil d'AutoMapper per a mapatges entitat-DTO
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ===== Animals =====
        CreateMap<VetAnimal, AnimalListDto>()
            .ForMember(dest => dest.IdAnimal, opt => opt.MapFrom(src => src.IdAnimal))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Nom ?? string.Empty))
            .ForMember(dest => dest.Sexe, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Sexe))
            .ForMember(dest => dest.DataNaixement, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Naixement))
            .ForMember(dest => dest.Especie, opt => opt.MapFrom(src => src.IdRasaNavigation.IdEspecieNavigation.Nom ?? string.Empty))
            .ForMember(dest => dest.Rasa, opt => opt.MapFrom(src => src.IdRasaNavigation.Nom ?? string.Empty))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.NumXip, opt => opt.MapFrom(src => src.NumXip))
            .ForMember(dest => dest.Castrat, opt => opt.MapFrom(src => src.Castrat));

        CreateMap<VetAnimal, AnimalDetailDto>()
            .ForMember(dest => dest.IdAnimal, opt => opt.MapFrom(src => src.IdAnimal))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Nom ?? string.Empty))
            .ForMember(dest => dest.Sexe, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Sexe))
            .ForMember(dest => dest.DataNaixement, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Naixement))
            .ForMember(dest => dest.Especie, opt => opt.MapFrom(src => src.IdRasaNavigation.IdEspecieNavigation.Nom ?? string.Empty))
            .ForMember(dest => dest.Rasa, opt => opt.MapFrom(src => src.IdRasaNavigation.Nom ?? string.Empty))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.NumXip, opt => opt.MapFrom(src => src.NumXip))
            .ForMember(dest => dest.Castrat, opt => opt.MapFrom(src => src.Castrat))
            .ForMember(dest => dest.Capa, opt => opt.MapFrom(src => src.Capa))
            .ForMember(dest => dest.Tatuatge, opt => opt.MapFrom(src => src.Tatuatge))
            .ForMember(dest => dest.Caracter, opt => opt.MapFrom(src => src.Caracter))
            .ForMember(dest => dest.Propietari, opt => opt.MapFrom(src => src.IdPropietariNavigation));

        CreateMap<VetPropietari, PropietariDto>()
            .ForMember(dest => dest.IdPropietari, opt => opt.MapFrom(src => src.IdPropietari))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.IdPropietari1.Nom ?? string.Empty))
            .ForMember(dest => dest.Cognoms, opt => opt.MapFrom(src => 
                (src.IdPropietari1.Cognom1 ?? "") + " " + (src.IdPropietari1.Cognom2 ?? "")))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.IdPropietari1.Email))
            .ForMember(dest => dest.Telefon, opt => opt.MapFrom(src => 
                src.IdPropietari1.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault() != null
                    ? src.IdPropietari1.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault()!.Numero
                    : null))
            .ForMember(dest => dest.Adresa, opt => opt.MapFrom(src => src.IdPropietari1.Adresa))
            .ForMember(dest => dest.CodiPostal, opt => opt.MapFrom(src => src.IdPropietari1.CodiPostal))
            .ForMember(dest => dest.Poblacio, opt => opt.MapFrom(src => src.IdPropietari1.Poblacio));

        // ===== Propietaris =====
        CreateMap<VetPropietari, PropietariListDto>()
            .ForMember(dest => dest.IdPropietari, opt => opt.MapFrom(src => src.IdPropietari))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.IdPropietari1.Nom ?? string.Empty))
            .ForMember(dest => dest.Cognoms, opt => opt.MapFrom(src => 
                ((src.IdPropietari1.Cognom1 ?? "") + " " + (src.IdPropietari1.Cognom2 ?? "")).Trim()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.IdPropietari1.Email))
            .ForMember(dest => dest.Telefon, opt => opt.MapFrom(src => 
                src.IdPropietari1.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault() != null
                    ? src.IdPropietari1.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault()!.Numero
                    : null))
            .ForMember(dest => dest.Poblacio, opt => opt.MapFrom(src => src.IdPropietari1.Poblacio))
            .ForMember(dest => dest.CodiPostal, opt => opt.MapFrom(src => src.IdPropietari1.CodiPostal))
            .ForMember(dest => dest.TotalAnimals, opt => opt.MapFrom(src => src.VetAnimals.Count))
            .ForMember(dest => dest.Actiu, opt => opt.MapFrom(src => src.IdPropietari1.Actiu));

        CreateMap<VetPropietari, PropietariDetailDto>()
            .ForMember(dest => dest.IdPropietari, opt => opt.MapFrom(src => src.IdPropietari))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.IdPropietari1.Nom ?? string.Empty))
            .ForMember(dest => dest.Cognom1, opt => opt.MapFrom(src => src.IdPropietari1.Cognom1))
            .ForMember(dest => dest.Cognom2, opt => opt.MapFrom(src => src.IdPropietari1.Cognom2))
            .ForMember(dest => dest.Cognoms, opt => opt.MapFrom(src => 
                ((src.IdPropietari1.Cognom1 ?? "") + " " + (src.IdPropietari1.Cognom2 ?? "")).Trim()))
            .ForMember(dest => dest.Nif, opt => opt.MapFrom(src => src.IdPropietari1.Nif))
            .ForMember(dest => dest.DataNaixement, opt => opt.MapFrom(src => src.IdPropietari1.Naixement))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.IdPropietari1.Email))
            .ForMember(dest => dest.AmbWhatsApp, opt => opt.MapFrom(src => src.IdPropietari1.AmbWhatsApp))
            .ForMember(dest => dest.Adresa, opt => opt.MapFrom(src => src.IdPropietari1.Adresa))
            .ForMember(dest => dest.CodiPostal, opt => opt.MapFrom(src => src.IdPropietari1.CodiPostal))
            .ForMember(dest => dest.Poblacio, opt => opt.MapFrom(src => src.IdPropietari1.Poblacio))
            .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.IdPropietari1.Provincia))
            .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.IdPropietari1.Pais))
            .ForMember(dest => dest.Telefons, opt => opt.MapFrom(src => 
                src.IdPropietari1.SlcTelefons.OrderBy(t => t.Ordre).ToList()))
            .ForMember(dest => dest.Animals, opt => opt.MapFrom(src => src.VetAnimals))
            .ForMember(dest => dest.Actiu, opt => opt.MapFrom(src => src.IdPropietari1.Actiu))
            .ForMember(dest => dest.Observacions, opt => opt.MapFrom(src => src.IdPropietari1.Observacions));

        CreateMap<SlcTelefon, TelefonDto>()
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
            .ForMember(dest => dest.TipusTelefon, opt => opt.MapFrom(src => src.TipusTelefon))
            .ForMember(dest => dest.TipusTelefonDescripcio, opt => opt.MapFrom(src => GetTipusTelefonDescripcio(src.TipusTelefon)))
            .ForMember(dest => dest.Ordre, opt => opt.MapFrom(src => src.Ordre))
            .ForMember(dest => dest.Observacions, opt => opt.MapFrom(src => src.Observacions));

        CreateMap<VetAnimal, AnimalResumatDto>()
            .ForMember(dest => dest.IdAnimal, opt => opt.MapFrom(src => src.IdAnimal))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Nom ?? string.Empty))
            .ForMember(dest => dest.Especie, opt => opt.MapFrom(src => src.IdRasaNavigation.IdEspecieNavigation.Nom ?? string.Empty))
            .ForMember(dest => dest.Rasa, opt => opt.MapFrom(src => src.IdRasaNavigation.Nom))
            .ForMember(dest => dest.Sexe, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Sexe))
            .ForMember(dest => dest.DataNaixement, opt => opt.MapFrom(src => src.IdAnimalNavigation.IdPacient1.Naixement))
            .ForMember(dest => dest.NumXip, opt => opt.MapFrom(src => src.NumXip))
            .ForMember(dest => dest.Castrat, opt => opt.MapFrom(src => src.Castrat));

        // ===== Historial Clínic =====
        CreateMap<HosVisitum, VisitaResumatDto>()
            .ForMember(dest => dest.IdVisita, opt => opt.MapFrom(src => src.IdVisita))
            .ForMember(dest => dest.DiaVisita, opt => opt.MapFrom(src => src.DiaVisita))
            .ForMember(dest => dest.Veterinari, opt => opt.MapFrom(src => 
                (src.IdDoctorNavigation.IdDoctorNavigation.Nom ?? "") + " " +
                ((src.IdDoctorNavigation.IdDoctorNavigation.Cognom1 ?? "") + " " + 
                 (src.IdDoctorNavigation.IdDoctorNavigation.Cognom2 ?? "")).Trim()))
            .ForMember(dest => dest.Resum, opt => opt.MapFrom(src => src.Resum))
            .ForMember(dest => dest.Pes, opt => opt.MapFrom(src => src.Pes))
            .ForMember(dest => dest.TotalTextos, opt => opt.MapFrom(src => src.HosTextVisita.Count))
            .ForMember(dest => dest.TotalProves, opt => opt.MapFrom(src => src.HosProvas.Count))
            .ForMember(dest => dest.TotalVacunes, opt => opt.MapFrom(src => src.HosVacunas.Count));

        CreateMap<HosVisitum, VisitaDetailDto>()
            .ForMember(dest => dest.IdVisita, opt => opt.MapFrom(src => src.IdVisita))
            .ForMember(dest => dest.IdPacient, opt => opt.MapFrom(src => src.IdPacient))
            .ForMember(dest => dest.DiaVisita, opt => opt.MapFrom(src => src.DiaVisita))
            .ForMember(dest => dest.Veterinari, opt => opt.MapFrom(src => src.IdDoctorNavigation))
            .ForMember(dest => dest.Resum, opt => opt.MapFrom(src => src.Resum))
            .ForMember(dest => dest.Pes, opt => opt.MapFrom(src => src.Pes))
            .ForMember(dest => dest.Alsada, opt => opt.MapFrom(src => src.Alsada))
            .ForMember(dest => dest.TipusVisita, opt => opt.MapFrom(src => src.TipusVisita))
            .ForMember(dest => dest.TextosClínics, opt => opt.MapFrom(src => src.HosTextVisita))
            .ForMember(dest => dest.Proves, opt => opt.MapFrom(src => src.HosProvas))
            .ForMember(dest => dest.Vacunes, opt => opt.MapFrom(src => src.HosVacunas));

        CreateMap<HosDoctor, VeterinariDto>()
            .ForMember(dest => dest.IdDoctor, opt => opt.MapFrom(src => src.IdDoctor))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => 
                (src.IdDoctorNavigation.Nom ?? "") + " " +
                ((src.IdDoctorNavigation.Cognom1 ?? "") + " " + 
                 (src.IdDoctorNavigation.Cognom2 ?? "")).Trim()))
            .ForMember(dest => dest.Especialitat, opt => opt.MapFrom(src => src.Especialitat))
            .ForMember(dest => dest.NumColegiat, opt => opt.MapFrom(src => src.NumColegiat));

        CreateMap<HosTextVisitum, TextVisitaDto>()
            .ForMember(dest => dest.IndexText, opt => opt.MapFrom(src => src.IndexText))
            .ForMember(dest => dest.TextPla, opt => opt.MapFrom(src => src.TextPla));

        CreateMap<HosProva, ProvaDto>()
            .ForMember(dest => dest.IdProva, opt => opt.MapFrom(src => src.IdProva))
            .ForMember(dest => dest.TipusProva, opt => opt.MapFrom(src => src.IdTipusProvaNavigation.Nom))
            .ForMember(dest => dest.Ordre, opt => opt.MapFrom(src => src.Ordre))
            .ForMember(dest => dest.CodiMostra, opt => opt.MapFrom(src => src.CodiMostra))
            .ForMember(dest => dest.Observacions, opt => opt.MapFrom(src => src.Observacions))
            .ForMember(dest => dest.Resultats, opt => opt.MapFrom(src => src.HosDetallProvas));

        CreateMap<HosDetallProva, DetallProvaDto>()
            .ForMember(dest => dest.Parametre, opt => opt.MapFrom(src => src.IdDetallTipusProvaNavigation.Nom))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            .ForMember(dest => dest.Observacions, opt => opt.MapFrom(src => src.Observacions));

        CreateMap<HosVacuna, VacunaDto>()
            .ForMember(dest => dest.IdVacuna, opt => opt.MapFrom(src => src.IdVacuna))
            .ForMember(dest => dest.TipusVacuna, opt => opt.MapFrom(src => src.IdTipusVacunaNavigation.Nom))
            .ForMember(dest => dest.DiaVacuna, opt => opt.MapFrom(src => src.DiaVacuna))
            .ForMember(dest => dest.Observacions, opt => opt.MapFrom(src => src.Observacions))
            .ForMember(dest => dest.NoRevacunar, opt => opt.MapFrom(src => src.NoRevacunar))
            .ForMember(dest => dest.FrequenciaDies, opt => opt.MapFrom(src => src.IdTipusVacunaNavigation.Frequencia))
            .ForMember(dest => dest.ProximaVacuna, opt => opt.MapFrom(src => 
                src.NoRevacunar ? (DateTime?)null : src.DiaVacuna.AddDays(src.IdTipusVacunaNavigation.Frequencia)));
    }

    /// <summary>
    /// Converteix el tipus de telèfon (int) a descripció
    /// </summary>
    private static string GetTipusTelefonDescripcio(int tipusTelefon)
    {
        return tipusTelefon switch
        {
            0 => "Mòbil",
            1 => "Fix",
            2 => "Feina",
            3 => "Fax",
            _ => "Altre"
        };
    }
}

