using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper
{
    public static class CommonMapper
    {
        public static string? GetTelefon(this SlcPersona? persona)
        {
            return persona?.SlcTelefons?.FirstOrDefault()?.Numero;
        }
    }
}
