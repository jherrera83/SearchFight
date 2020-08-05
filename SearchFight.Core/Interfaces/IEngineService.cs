using SearchFight.Core.DTOs;
using System.Threading.Tasks;

namespace SearchFight.Core.Interfaces
{
    public interface IEngineService
    {
        Task<SearchFightDto> SearchFight(string[] keywords);
    }
}
