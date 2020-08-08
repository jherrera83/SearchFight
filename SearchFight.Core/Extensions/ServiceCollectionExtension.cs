using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.Core.Interfaces;
using SearchFight.Core.Services;
using SearchFight.Infraestructure.Interfaces;
using SearchFight.Infraestructure.Options;
using SearchFight.Infraestructure.Repositories;

namespace SearchFight.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationGoogleOptions>(configuration.GetSection("ConfigurationGoogleOptions"));
            services.Configure<ConfigurationBingOptions>(configuration.GetSection("ConfigurationBingOptions"));

            return services;
        }


        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IGoogleRepository, GoogleRepository>();
            services.AddTransient<IBingRepository, BingRepository>();
            services.AddTransient<IGoogleService, GoogleService>();
            services.AddTransient<IBingService, BingService>();
            services.AddTransient<IEngineService, EngineService>();

            return services;
        }
    }
}
