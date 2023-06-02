using API.Repositories;
using API.Repositories.Implementation;

namespace API.DI
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection ConfigureDependency(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ILessonsRepository, LessonsRepository>();

            // Services
            // Services should be here

            return services;
        }
    }
}