using SearchFight.Core.DTOs;
using SearchFight.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Core.Services
{
    public class EngineService : IEngineService
    {
        public readonly IGoogleService _googleService;
        public readonly IBingService _bingService;
        /// <summary>
        /// EngineService
        /// </summary>
        /// <param name="googleService"></param>
        public EngineService(IGoogleService googleService,
                             IBingService bingService)
        {
            _googleService = googleService;
            _bingService = bingService;
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
                engineDto.GoogleDto = await _googleService.GetResultGoogle(keyWord);
                #endregion

                #region BING
                //for every search in bing I put dto in result object
                engineDto.BingDto = await _bingService.GetResultBing(keyWord);
                #endregion

                //set keyword in result object
                engineDto.KeyWord = keyWord;

                //add object to list
                searchEngineDtoList.Add(engineDto);
            }
            result.EngineDtoList = searchEngineDtoList;

            #region WINNER
            //I find a winner for every search engine
            result.GoogleWinnerDto = await _googleService.GetGoogleWinner(searchEngineDtoList);
            result.BingWinnerDto = await _bingService.GetBingWinner(searchEngineDtoList);

            //winner at all
            result.Winner = await GetWinner(result);
            #endregion

            return result;
        }

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
