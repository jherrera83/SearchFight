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
    public class GoogleRepository : IGoogleRepository
    {
        private readonly ConfigurationGoogleOptions _configurationGoogleOptions;
        public GoogleRepository(IOptions<ConfigurationGoogleOptions> options)
        {
            _configurationGoogleOptions = options.Value;
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns></returns>
        public async Task<GoogleResponse> Search(string keyword)
        {
            string apiKey = _configurationGoogleOptions.ApiKey;
            string cx = _configurationGoogleOptions.Cx;
            string uri = _configurationGoogleOptions.Uri;

            var request = WebRequest.Create(uri + apiKey + "&cx=" + cx + "&q=" + keyword.ToUpper());
            var response = (HttpWebResponse)request.GetResponse();

            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseString = await reader.ReadToEndAsync();

            //get json and convert to object
            GoogleResponse googleReponse = JsonConvert.DeserializeObject<GoogleResponse>(responseString);

            return googleReponse;
        }
    }
}
