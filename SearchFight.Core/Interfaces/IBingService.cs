using SearchFight.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Core.Interfaces
{
    public interface IBingService
    {
        Task<BingDto> GetResultBing(string keyWord);
        Task<BingWinnerDto> GetBingWinner(List<SearchEngineDto> engineDtoList);
    }
}
