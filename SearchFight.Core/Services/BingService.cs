using SearchFight.Core.DTOs;
using SearchFight.Core.Enumerations;
using SearchFight.Core.Interfaces;
using SearchFight.Infraestructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchFight.Core.Services
{
    public class BingService : IBingService
    {
        public readonly IBingRepository _bingRepository;

        public BingService(IBingRepository bingRepository)
        {
            _bingRepository = bingRepository;
        }

        /// <summary>
        /// GetResultBing
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public async Task<BingDto> GetResultBing(string keyWord)
        {
            var result = new BingDto();
            var resultBingSearch = await _bingRepository.Search(keyWord);
            if (resultBingSearch != null)
            {
                result.SearchEngine = EnumEngine.Bing.ToString();
                result.Total = resultBingSearch.webPages.totalEstimatedMatches;
            }

            return result;
        }

        /// <summary>
        /// GetBingWinner
        /// </summary>
        /// <param name="engineDtoList"></param>
        /// <returns></returns>
        public async Task<BingWinnerDto> GetBingWinner(List<SearchEngineDto> engineDtoList)
        {
            //create a dicctionary for total and keywork
            Dictionary<long, string> bingList = new Dictionary<long, string>();
            foreach (var item in engineDtoList)
            {
                bingList.Add(item.BingDto.Total, item.KeyWord);
            }

            //put in descending Order  and take the first
            var result = bingList.OrderByDescending(p => p.Key).FirstOrDefault();

            //already have google winner
            return await Task.FromResult(new BingWinnerDto
            {
                KeyWord = result.Value,
                Total = result.Key,
                Result = $"{EnumEngine.Bing} {"winner: "} {result.Value}"
            });
        }
    }
}
