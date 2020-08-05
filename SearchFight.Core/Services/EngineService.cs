using SearchFight.Core.DTOs;
using SearchFight.Core.Enumerations;
using SearchFight.Core.Interfaces;
using SearchFight.Infraestructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchFight.Core.Services
{
    public class EngineService : IEngineService
    {
        public readonly IGoogleRepository _googleRepository;
        public readonly IBingRepository _bingRepository;
        /// <summary>
        /// EngineService
        /// </summary>
        /// <param name="googleRepository"></param>
        public EngineService(IGoogleRepository googleRepository,
                             IBingRepository bingRepository)
        {
            _googleRepository = googleRepository;
            _bingRepository = bingRepository;
        }

        /// <summary>
        /// SearchFight
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public async Task<SearchFightDto> SearchFight(string[] keyWords)
        {
            var result = new SearchFightDto();
            var searchEngineDtoList = new List<SearchEngineDto>();
            foreach (var keyWord in keyWords)
            {
                var engineDto = new SearchEngineDto();

                #region GOOGLE
                //for every search in google I put dto in result object
                engineDto.GoogleDto = await GetResultGoogle(keyWord);
                #endregion

                #region BING
                //for every search in bing I put dto in result object
                engineDto.BingDto = await GetResultBing(keyWord);
                #endregion

                //set keyword in result object
                engineDto.KeyWord = keyWord;

                //add object to list
                searchEngineDtoList.Add(engineDto);
            }
            result.EngineDtoList = searchEngineDtoList;

            #region WINNER
            //I find a winner for every search engine
            result.GoogleWinnerDto = await GetGoogleWinner(searchEngineDtoList);
            result.BingWinnerDto = await GetBingWinner(searchEngineDtoList);

            //winner at all
            result.Winner = await GetWinner(result);
            #endregion

            return result;
        }

        #region GOOGLE SUPPORT METHODS
        /// <summary>
        /// GetResultGoogle
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private async Task<GoogleDto> GetResultGoogle(string keyWord)
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
        private async Task<GoogleWinnerDto> GetGoogleWinner(List<SearchEngineDto> engineDtoList)
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
        #endregion

        #region BING SUPPORT METHODS
        /// <summary>
        /// GetResultBing
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private async Task<BingDto> GetResultBing(string keyWord)
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
        private async Task<BingWinnerDto> GetBingWinner(List<SearchEngineDto> engineDtoList)
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
        #endregion

        #region SEARCHFIGHT SUPPORT METHODS
        /// <summary>
        /// GetWinner
        /// </summary>
        /// <param name="searchFightDto"></param>
        /// <returns></returns>
        private async Task<string> GetWinner(SearchFightDto searchFightDto)
        {
            _ = searchFightDto.BingWinnerDto.KeyWord;
            string keyWord;
            if (searchFightDto.BingWinnerDto.Total > searchFightDto.GoogleWinnerDto.Total)
                keyWord = searchFightDto.BingWinnerDto.KeyWord;
            else keyWord = searchFightDto.GoogleWinnerDto.KeyWord;
            var result = $"{"Total Winner: "} {keyWord}";

            return await Task.FromResult(result);
        }
        #endregion
    }
}
