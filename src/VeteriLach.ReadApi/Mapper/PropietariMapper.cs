using VeteriLach.ReadApi.Application.Propietaris.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper
{
    public static class PropietariMapper
    {
        public static PropietariListDto? ToPropietariListDto(this VetPropietari? propietari)
        {
            if (propietari == null)
                return null;

            var dadesPersona = propietari.IdPropietariNavigation.IdClientNavigation;
            if(dadesPersona == null)
                return null;

            return new PropietariListDto(
                propietari.IdPropietari,
                dadesPersona.Nom!,
                string.Join(' ', dadesPersona.Cognom1, dadesPersona.Cognom2 ),
                dadesPersona.Email,
                dadesPersona.SlcTelefons.FirstOrDefault(t => t.IdPersona == propietari.IdPropietari)?.Numero,
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
                string.Join(' ', dadesPersona.Cognom1, dadesPersona.Cognom2),
                dadesPersona.Nif,
                dadesPersona.Naixement,
                dadesPersona.Email,
                dadesPersona.AmbWhatsApp,
                dadesPersona.Adresa,
                dadesPersona.CodiPostal,
                dadesPersona.Poblacio,
                dadesPersona.Provincia,
                dadesPersona.Pais)
            {
                Actiu = dadesPersona.Actiu,
                Observacions = dadesPersona.Observacions
            };
            // Telefons
            result.Telefons = dadesPersona.SlcTelefons
                .Where(t => t.IdPersona == propietari.IdPropietari)
                .Select(t => new TelefonDto(t.Numero, t.TipusTelefon, "", t.Ordre, t.Observacions))
                .ToList();
            // Animals
            result.Animals = propietari.VetAnimals
                .Select(a => new AnimalResumatDto(
                    a.IdAnimal,
                    a.IdAnimalNavigation.IdPacientNavigation.IdClientNavigation.Nom,
                    a.IdRasaNavigation.IdEspecieNavigation.Nom,
                    a.IdRasaNavigation.Nom,
                    a.IdAnimalNavigation.IdPacientNavigation.IdClientNavigation.Sexe.ToString(),
                    a.IdAnimalNavigation.IdPacientNavigation.IdClientNavigation.Naixement,
                    a.NumXip,
                    a.Castrat))
                .ToList();
            return result;
        }
}
