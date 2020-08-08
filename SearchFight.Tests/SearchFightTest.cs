using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.Core.Extensions;
using SearchFight.Core.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SearchFight.Tests
{
    public class SearchFightTest
    {
        private ServiceProvider serviceProvider { get; set; }
        private IEngineService dataRetriever { get; set; }

        public SearchFightTest()
        {
            var services = ConfigureServices();
            serviceProvider = services.BuildServiceProvider();
            dataRetriever = serviceProvider.GetService<IEngineService>();
        }

        [Fact]
        public async Task SearchFight_ReturnNotNull()
        {
            //Arrange
            string[] args = new string[] { "java", ".net" };
            //Act
            var result = await dataRetriever.SearchFight(args);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SearchFight_ReturnWinner()
        {
            //Arrange
            string[] args = new string[] { "java", ".net" };
            //Act
            var result = await dataRetriever.SearchFight(args);
            var winner = result.Winner.Split(':');
            //Assert
            Assert.Equal(2, winner.Length);
        }

        [Fact]
        public async Task SearchFight_Return3Results()
        {
            //Arrange
            string[] args = new string[] { "java", ".net", "cobol" };
            //Act
            var result = await dataRetriever.SearchFight(args);
            //Assert
            Assert.Equal(3, result.EngineDtoList.Count);
        }

        [Fact]
        public async Task SearchFight_GoogleReturnWinner()
        {
            //Arrange
            string[] args = new string[] { "java", ".net" };
            //Act
            var result = await dataRetriever.SearchFight(args);
            var winner = result.GoogleWinnerDto.Result.Split(':');
            //Assert
            Assert.Equal(2, winner.Length);
        }

        [Fact]
        public async Task SearchFight_BingReturnWinner()
        {
            //Arrange
            string[] args = new string[] { "java", ".net" };
            //Act
            var result = await dataRetriever.SearchFight(args);
            var winner = result.BingWinnerDto.Result.Split(':');
            //Assert
            Assert.Equal(2, winner.Length);
        }

        [Fact]
        public async Task SearchFight_OneWordToFindAtLeast()
        {
            //Arrange
            string[] args = new string[] { ".net" };
            //Act
            var result = await dataRetriever.SearchFight(args);
            //Assert
            Assert.Contains(".net", result.Winner);
        }

        #region CONFIG
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
            services.AddOptions(config);

            //dependency injection container
            services.AddServices();

            services.AddTransient<SearchFightTest>();

            return services;
        }

        /// <summary>
        /// LoadConfiguration
        /// </summary>
        /// <returns></returns>
        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
        #endregion
    }
}
