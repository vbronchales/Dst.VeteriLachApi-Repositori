namespace VeteriLach.ReadApi.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Aquí puedes agregar tus serveis de infraestructura, com repositoris, contextos de base de dades, etc.
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IVisitesRepository, VisitesRepository>();
            services.AddScoped<IRacesRepository, RacesRepository>();
            services.AddScoped<IEspecieRepository, EspecieRepository>();

            return services;
        }
    }
}
