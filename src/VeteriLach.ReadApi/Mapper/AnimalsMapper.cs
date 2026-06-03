using VeteriLach.ReadApi.Application.Animals.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper;

public static class AnimalsMapper
{
    public static AnimalDetailDto? ToAnimalDetailDto(this VetAnimal? animal)
    {
        if (animal == null) return null;

        var propietari = animal.IdPropietariNavigation?.IdPropietari1;
        if (propietari == null) return null;

        var personaPropietari = propietari?.FacClient?.IdClientNavigation;
        if(personaPropietari == null) return null;

        var propietariDto = new PropietariDto(
                propietari.IdPersona,
                personaPropietari.Nom,
                string.Join(' ', personaPropietari.Cognom1, personaPropietari.Cognom2),
                personaPropietari.Email,
                personaPropietari.SlcTelefons?.FirstOrDefault()?.Numero,
                personaPropietari.Adresa,
                personaPropietari.CodiPostal,
                personaPropietari.Poblacio);


        return new AnimalDetailDto(
            animal.IdAnimal,
            animal.IdAnimalNavigation?.IdPacient1?.Nom,
            animal.IdAnimalNavigation?.IdPacient1?.Sexe,
            animal.IdAnimalNavigation?.IdPacient1?.Naixement,
            animal.IdRasaNavigation?.IdEspecieNavigation?.Nom,
            animal.IdRasaNavigation?.Nom,
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
}
