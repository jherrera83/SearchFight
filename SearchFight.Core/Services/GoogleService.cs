using SearchFight.Core.DTOs;
using SearchFight.Core.Enumerations;
using SearchFight.Core.Interfaces;
using SearchFight.Infraestructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchFight.Core.Services
{
    public class GoogleService : IGoogleService
    {
        public readonly IGoogleRepository _googleRepository;

        public GoogleService(IGoogleRepository googleRepository)
        {
            _googleRepository = googleRepository;
        }
        /// <summary>
        /// GetResultGoogle
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public async Task<GoogleDto> GetResultGoogle(string keyWord)
        {
            var result = new GoogleDto();
            var resultGoogleSearch = await _googleRepository.Search(keyWord);
            if (resultGoogleSearch != null)
            {
                result.SearchEngine = EnumEngine.Google.ToString();
                result.Total = resultGoogleSearch.searchInformation.totalResults;
            }

            return result;
        }
        /// <summary>
        /// GetGoogleWinner
        /// </summary>
        /// <param name="engineDtoList"></param>
        /// <returns></returns>
        public async Task<GoogleWinnerDto> GetGoogleWinner(List<SearchEngineDto> engineDtoList)
        {
            //create a dicctionary for total and keywork
            Dictionary<long, string> googleList = new Dictionary<long, string>();
            foreach (var item in engineDtoList)
            {
                googleList.Add(item.GoogleDto.Total, item.KeyWord);
            }

            //put in descending Order  and take the first
            var result = googleList.OrderByDescending(p => p.Key).FirstOrDefault();

            //already have google winner
            return await Task.FromResult(new GoogleWinnerDto
            {
                KeyWord = result.Value,
                Total = result.Key,
                Result = $"{EnumEngine.Google} {"winner: "} {result.Value}"
            });
        }
    }
}
