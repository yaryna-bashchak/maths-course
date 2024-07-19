using API.Repositories;
using API.Repositories.Implementation;
using API.Services;

namespace API.DI
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection ConfigureDependency(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ILessonsRepository, LessonsRepository>();
            services.AddScoped<ITestsRepository, TestsRepository>();
            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<ISectionsRepository, SectionsRepository>();
            services.AddScoped<IOptionsRepository, OptionsRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();

            // Services
            services.AddScoped<TokenService>();
            services.AddScoped<VideoService>();
            services.AddScoped<ImageService>();
            services.AddScoped<PaymentService>();

            return services;
        }
    }
}