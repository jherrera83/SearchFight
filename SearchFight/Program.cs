using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.Core.Interfaces;
using SearchFight.Core.Services;
using SearchFight.Infraestructure.Interfaces;
using SearchFight.Infraestructure.Options;
using SearchFight.Infraestructure.Repositories;
using System.IO;
using System.Threading.Tasks;

namespace SearchFight
{
    class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            //args = new string[] { ".net", "java" };
            await serviceProvider.GetService<App>().Run(args);
        }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <returns></returns>
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);

            //get parameters from appsetting.json
            services.Configure<ConfigurationGoogleOptions>(config.GetSection("ConfigurationGoogleOptions"));
            services.Configure<ConfigurationBingOptions>(config.GetSection("ConfigurationBingOptions"));

            //dependency injection container
            services.AddTransient<IGoogleRepository, GoogleRepository>();
            services.AddTransient<IBingRepository, BingRepository>();
            services.AddTransient<IEngineService, EngineService>();

            services.AddTransient<App>();

            return services;
        }

        /// <summary>
        /// LoadConfiguration
        /// </summary>
        /// <returns></returns>
        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

    }
}
