using SearchFight.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Core.Interfaces
{
    public interface IGoogleService
    {
        Task<GoogleDto> GetResultGoogle(string keyWord);
        Task<GoogleWinnerDto> GetGoogleWinner(List<SearchEngineDto> engineDtoList);
    }
}