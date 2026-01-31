using Microsoft.Extensions.DependencyInjection;
using WeatherLogger.Application.Interfaces;
using WeatherLogger.Application.Services;

namespace WeatherLogger.Application.DependencyInjection
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }
    }
}
