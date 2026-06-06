using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper
{
    public static class CommonMapper
    {
        public static string? GetTelefon(this SlcPersona? persona)
        {
            return persona?.SlcTelefons?.FirstOrDefault()?.Numero;
        }

        public static string? GetFullName(this SlcPersona? persona)
        {
            if (persona == null) return null;
            return string.Join(',', persona.Nom, string.Join(' ', persona.Cognom1, persona.Cognom2));
        }
    }
}
