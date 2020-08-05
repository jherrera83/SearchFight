using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SearchFight.Infraestructure.Entities;
using SearchFight.Infraestructure.Interfaces;
using SearchFight.Infraestructure.Options;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SearchFight.Infraestructure.Repositories
{
    public class BingRepository : IBingRepository
    {
        private readonly ConfigurationBingOptions _configurationBingOptions;

        public BingRepository(IOptions<ConfigurationBingOptions> options)
        {
            _configurationBingOptions = options.Value;
        }

        public async Task<BingResponse> Search(string keyword)
        {
            string apiKey = _configurationBingOptions.ApiKey;
            string uri = _configurationBingOptions.Uri + "?q=" + keyword;
           
            WebRequest request = WebRequest.Create(uri);
            request.Headers[_configurationBingOptions.Ocp] = apiKey;

            var response = (HttpWebResponse)request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseString = await reader.ReadToEndAsync();

            //get json and convert to object
            BingResponse bingResponse = JsonConvert.DeserializeObject<BingResponse>(responseString);

            return bingResponse;
        }

    }
}
