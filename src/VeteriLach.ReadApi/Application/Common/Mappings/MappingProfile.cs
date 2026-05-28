using AutoMapper;
using VeteriLach.ReadApi.Application.Animals.DTOs;
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
    }
}
