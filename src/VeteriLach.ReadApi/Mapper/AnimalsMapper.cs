using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Domain.Animals;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper;

public static class AnimalsMapper
{
    public static AnimalDetailDto? ToAnimalDetailDto(this VetAnimal? animal)
    {
        if (animal == null) return null;

        var propietari = animal.IdPropietariNavigation;
        if (propietari == null) return null;

        var propietariDto = propietari.ToPropietariDetailDto();

        var pacientPersona = animal.IdAnimalNavigation?.IdPacientNavigation?.IdClientNavigation;
        if(pacientPersona == null)
            return null;

        return new AnimalDetailDto(
            animal.IdAnimal,
            pacientPersona.Nom!,
            pacientPersona.Sexe,
            pacientPersona.Naixement,
            animal.IdRasaNavigation?.IdEspecieNavigation?.Nom!,
            animal.IdRasaNavigation?.Nom!,
            animal.Color,
            animal.NumXip,
            animal.Castrat,
            animal.Capa,
            animal.Tatuatge,
            animal.Caracter,
            propietariDto);
    }

    public static AnimalListDto ToAnimalListDto(this VetAnimal animal)
    {
        return new AnimalListDto(
            animal.IdAnimal,
            animal.IdAnimalNavigation?.IdPacient1?.Nom ?? string.Empty,
            animal.IdAnimalNavigation?.IdPacient1?.Sexe,
            animal.IdAnimalNavigation?.IdPacient1?.Naixement,
            animal.IdRasaNavigation?.IdEspecieNavigation?.Nom ?? string.Empty,
            animal.IdRasaNavigation?.Nom ?? string.Empty,
            animal.Color,
            animal.NumXip,
            animal.Castrat);
    }

    public static PropietariListDto? ToPropietariListDto(this VetPropietari? propietari)
    {
        if (propietari == null)
            return null;

        var dadesPersona = propietari.IdPropietariNavigation.IdClientNavigation;
        if (dadesPersona == null)
            return null;

        return new PropietariListDto(
            propietari.IdPropietari,
            dadesPersona.Nom!,
            dadesPersona.Cognom1, 
            dadesPersona.Cognom2,
            dadesPersona.Email,
            dadesPersona.GetTelefon(),
            dadesPersona.Poblacio,
            dadesPersona.CodiPostal,
            propietari.VetAnimals.Count,
            dadesPersona.Actiu);
    }

    public static PropietariDetailDto? ToPropietariDetailDto(this VetPropietari? propietari)
    {
        if (propietari == null)
            return null;
        var dadesPersona = propietari.IdPropietariNavigation.IdClientNavigation;
        if (dadesPersona == null)
            return null;

        var result = new PropietariDetailDto(
            propietari.IdPropietari,
            dadesPersona.Nom!,
            dadesPersona.Cognom1,
            dadesPersona.Cognom2,
            dadesPersona.Nif,
            dadesPersona.Email,
            dadesPersona.GetTelefon(),
            dadesPersona.Adresa,
            dadesPersona.CodiPostal,
            dadesPersona.Poblacio
            );

        return result;
    }
}
