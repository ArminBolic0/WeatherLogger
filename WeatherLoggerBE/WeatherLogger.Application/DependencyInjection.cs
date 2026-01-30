using Microsoft.Extensions.DependencyInjection;
using WeatherLogger.Application.Services;

namespace WeatherLogger.Application.DependencyInjection
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<WeatherService>();

            return services;
        }
    }
}
